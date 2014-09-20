using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SaveToFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String strExc = "";
        try
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile uploadfile = files["RemoteFile"];

            string patientId = Request.QueryString["PatientID"];
            string path = Request.QueryString["path"];

            path = Server.MapPath(path);
            string filepath = path + @"\" + uploadfile.FileName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            if (File.Exists(filepath))
            {
                Response.Write("Unable to save, file already exists");
            }

            uploadfile.SaveAs(filepath);
            //Response.Redirect("FileManager.aspx?PatientID=" + patientId);
        }
        catch (Exception exc)
        {
            strExc = exc.ToString();
            String strField1Path = HttpContext.Current.Request.MapPath(".") + "/" + "log.txt";
            StreamWriter sw1 = File.CreateText(strField1Path); 
            if (strField1Path != null)
            {                
                sw1.Write(strExc);
                sw1.Close();
            }
            Response.Write(strExc);
        }
    }
}
