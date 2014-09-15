using IZ.WebFileManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //getting the root folder, if not create
            string patientID = Request.QueryString["PatientID"];

            if (patientID == null || patientID.Trim() == "")
            {
                throw new ApplicationException("Patient information is not available");
            }

            string cmdText = "SELECT PatientID, Greeting, FirstName, MiddleName, LastName, NickName FROM Patient WHERE PatientID = " + patientID;
            SqlDataReader drPatient = DBUtil.ExecuteReader(cmdText);
            if (!drPatient.Read())
            {
                throw new ApplicationException("Patient information is not available");
            }

            string patientName = drPatient["FirstName"].ToString() + " " + drPatient["LastName"].ToString();
            FileManagerTab.HeaderText = "Documents for " + patientName;

            string path = GetFolder("~/Data", patientID);

            fmPatient.RootDirectories.Clear();
            RootDirectory rootDirectory = new RootDirectory();
            rootDirectory.DirectoryPath = path;
            rootDirectory.Text = patientName;
            rootDirectory.ShowRootIndex = false;
            fmPatient.RootDirectories.Add(rootDirectory);
        }
    }

    protected void fmPatient_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
    {
        e.ClientScript = "alert('Use ExecuteCommand event to handle your custom command.\\nCommandName=" + e.CommandName +
                         "\\nItem=" + e.Items[0].VirtualPath.Replace("'", "\\'") + "')";
    }

    protected void fmPatient_ToolbarCommand(object sender, CommandEventArgs e)
    {
        string patientID = Request.QueryString["PatientID"];
        Response.Write(fmPatient.CurrentDirectory.VirtualPath);
        Response.Write(patientID);
        if (e.CommandName == "CreateScan")
        {
            //string zipFile = System.IO.Path.Combine(FileManager1.CurrentDirectory.PhysicalPath,
            //                                        "ZIP_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");

            ////Create an empty zip file
            //byte[] emptyzip = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //using (System.IO.FileStream fs = System.IO.File.Create(zipFile))
            //{
            //    fs.Write(emptyzip, 0, emptyzip.Length);
            //    fs.Flush();
            //    fs.Close();
            //}

            //string[] selectedItems = new string[FileManager1.SelectedItems.Length];
            //for (int i = 0; i < FileManager1.SelectedItems.Length; i++)
            //{
            //    selectedItems[i] = System.IO.Path.GetFileName(FileManager1.SelectedItems[i].PhysicalPath);
            //}
            ////Copy a folder and its contents into the newly created zip file
            //Shell32.ShellClass sc = new Shell32.ShellClass();
            //Shell32.Folder DestFlder = sc.NameSpace(zipFile);
            //Shell32.Folder SrcFlder = sc.NameSpace(FileManager1.CurrentDirectory.PhysicalPath);
            //Shell32.FolderItems items = SrcFlder.Items();
            //foreach (Shell32.FolderItem item in items)
            //{
            //    if (Array.LastIndexOf<string>(selectedItems, item.Name) >= 0)
            //        DestFlder.CopyHere(item, 20);
            //}

            ////Ziping a file using the Windows Shell API creates another thread where the zipping is executed.
            ////This means that it is possible that this console app would end before the zipping thread 
            ////starts to execute which would cause the zip to never occur and you will end up with just
            ////an empty zip file. So wait a second and give the zipping thread time to get started
            //System.Threading.Thread.Sleep(1000);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private string GetFolder(string rootPath, string id)
    {
        string path = rootPath;
        string physicalPath;
        int patientID = int.Parse(id);

        //checking and creating directory for thousands
        path += @"/" + ((patientID / 1000) * 1000).ToString().PadLeft(5, '0');
        physicalPath = MapPath(path);
        if (!Directory.Exists(physicalPath))
        {
            Directory.CreateDirectory(physicalPath);
        }

        //finding the 1000 position
        path +=  @"/" + ((patientID / 100) * 100).ToString().PadLeft(4, '0');
        physicalPath = MapPath(path);
        if (!Directory.Exists(physicalPath))
        {
            Directory.CreateDirectory(physicalPath);
        }

        path += @"/" + (patientID).ToString().PadLeft(3, '0');
        physicalPath = MapPath(path);
        if (!Directory.Exists(physicalPath))
        {
            Directory.CreateDirectory(physicalPath);
        }

        return path;

    }

}