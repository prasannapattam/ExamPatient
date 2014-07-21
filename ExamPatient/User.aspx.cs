using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cmdText = "SELECT UserID, FirstName, LastName, UserName, Password FROM [User]";

            SqlDataReader drUser = DBUtil.ExecuteReader(cmdText);

            UserResults.DataSource = drUser;
            UserResults.DataBind();
            UserResults.SelectedIndex = 0;

            drUser.Close();
            drUser.Dispose();

            if (UserResults.Rows.Count == 0)
            {
                pnlError.Visible = true;
                btnEditUser.Visible = false;
            }
        }
    }

    private string firstRowClientID;
    protected void UserResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowUserID = ((System.Data.Common.DbDataRecord)e.Row.DataItem)["UserID"].ToString();
            if (firstRowClientID == null)
            {
                firstRowClientID = e.Row.ClientID;
                patientID.Value = rowUserID;
            }
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "', '" + firstRowClientID + "', " + rowUserID + ")");
        }

    }
}