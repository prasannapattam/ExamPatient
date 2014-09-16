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
        if (e.CommandName == "CreateScan")
        {
            string patientID = Request.QueryString["PatientID"];
            string scanUrl = "~/Scanning.aspx?PatientID=" + patientID + "&path=" + Server.UrlEncode(fmPatient.CurrentDirectory.VirtualPath);

            Response.Redirect(scanUrl, true);
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