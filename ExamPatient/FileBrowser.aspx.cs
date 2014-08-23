using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileBrowser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string patientID;
            patientID = Request.QueryString["PatientID"].ToString();
            hdnPatientID.Value = patientID;
            FileBrowser1.UserName = WebConfigurationManager.AppSettings["FileBrowserUserName"];
            FileBrowser1.UserDomain = WebConfigurationManager.AppSettings["FileBrowserUserDomain"];
            FileBrowser1.UserPassword = WebConfigurationManager.AppSettings["FileBrowserUserPwd"];
            FileBrowser1.CurrentFolder = WebConfigurationManager.AppSettings["FileBrowserInitialPath"] + patientID + "\\";
            FileBrowser1.RootFolder = FileBrowser1.CurrentFolder ;
            FileBrowser1.Refresh();
            Label2.Text = FileBrowser1.CurrentFolder;

        }
    }

    protected void FileBrowser1_SelectedFileChanged(object sender, EventArgs e)
    {
        string itemText = FileBrowser1.SelectedFile.Name;
        //string itemValue = FileBrowser1.SelectedFile.FullName.Split(new string[] { FileBrowser1.RootFolder }, StringSplitOptions.RemoveEmptyEntries)[0].Replace('\\', '/');
        Label1.Text = itemText;
        //Label2.Text = FileBrowser1.CurrentFolder;
    }

    protected void FileBrowser1_SelectedFolderChanged(object sender, EventArgs e)
    {
        //string itemText = FileBrowser1.SelectedFile.Name;
        //string itemValue = FileBrowser1.SelectedFile.FullName.Split(new string[] { FileBrowser1.RootFolder }, StringSplitOptions.RemoveEmptyEntries)[0].Replace('\\', '/');
        Label2.Text = FileBrowser1.CurrentFolder;
    }
}