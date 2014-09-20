using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Scanning : System.Web.UI.Page
{
    protected string saveUrl;
    protected string redirectUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        saveUrl = VirtualPathUtility.ToAbsolute("~/SaveToFile.aspx") + "?" + Request.QueryString.ToString();
        redirectUrl = VirtualPathUtility.ToAbsolute("~/FileManager.aspx") + "?" + Request.QueryString.ToString();

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
        FileManagerTab.HeaderText = "Scan for " + patientName;


    }
}