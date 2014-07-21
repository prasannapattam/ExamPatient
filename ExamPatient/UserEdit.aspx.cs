using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class UserEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (Request.QueryString["UserID"] == null || Request.QueryString["UserID"] == "")
            {
                UserTab.HeaderText = "ADD";
            }
            else
            {
                string userID;
                UserTab.HeaderText = "VIEW / EDIT";
                userID = Request.QueryString["UserID"].ToString();

                //getting patient details from database
                string cmdText = "SELECT UserID, FirstName, LastName, UserName, Password FROM [User] WHERE UserID = '" + userID + "'";

                SqlDataReader drUser = DBUtil.ExecuteReader(cmdText);

                while (drUser.Read())
                {
                    hdnUserID.Value = drUser["UserID"].ToString();
                    tbFirstName.Text = drUser["FirstName"].ToString();
                    tbLastName.Text = drUser["LastName"].ToString();
                    tbUserName.Text = drUser["UserName"].ToString();
                    tbPassword.Text = drUser["Password"].ToString();
                }
                drUser.Close();
                drUser.Dispose();

                if (hdnUserID.Value == "")
                    throw new ApplicationException("User information is not available");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string cmdText = "";

        if (hdnUserID.Value != "")
        {
            cmdText = @"UPDATE [User] SET FirstName = '{0}', LastName = '{1}', UserName = '{2}', Password = '{3}'
                        WHERE UserID = {4}; SELECT {4}";
        }
        else
        {

            cmdText = @"INSERT INTO [User](FirstName, LastName, UserName, Password)";
            cmdText += @" VALUES('{0}', '{1}', '{2}', '{3}'); select Scope_Identity();";
        }

        cmdText = string.Format(cmdText, tbFirstName.Text, tbLastName.Text, tbUserName.Text, tbPassword.Text, hdnUserID.Value);

        try
        {
            string UserID = DBUtil.ExecuteScalar(cmdText).ToString();
            result.Text = "User info successfully saved";
            result.Visible = true;
            pnlMain.Visible = false;
            pnlResult.Visible = true;
            resultError.Visible = false;

        }
        catch (Exception exp)
        {
            resultError.Text = "User info not saved - " + exp.Message;
            resultError.Visible = true;
        }
    }

}