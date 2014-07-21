using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Context.Items["HideMenu"] = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cmdText = "";

        cmdText = "SELECT UserName FROM [User] WHERE UserName = '{0}' AND Password = '{1}'";
        cmdText = string.Format(cmdText, tbUserName.Text, tbPassword.Text);

        try
        {
            object userName = DBUtil.ExecuteScalar(cmdText);

            if (userName == null)
            {
                resultError.Text = "Login Failed";
                resultError.Visible = true;
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(userName.ToString(), false);
            }
        }
        catch (Exception exp)
        {
            resultError.Text = "Login Failed - " + exp.Message;
            resultError.Visible = true;
        }
    }


}