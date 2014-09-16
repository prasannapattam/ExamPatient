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
            
            String Path = System.Web.HttpContext.Current.Request.MapPath(".") + "/UploadedImages/";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            uploadfile.SaveAs(Path + uploadfile.FileName);
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
