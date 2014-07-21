using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class ExamDefaults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string cmdText = "SELECT ExamDefaultID, DefaultName, AgeStart, AgeEnd, (CASE WHEN e.PrematureBirth = 1 THEN 'Yes' ELSE 'No' END) As  PrematureBirth, u.FirstName + ' ' + u.LastName as DoctorName FROM ExamDefault e LEFT JOIN [User] u on e.DoctorUserID = u.UserID";

        SqlDataReader drDefault = DBUtil.ExecuteReader(cmdText);

        DefaultResults.DataSource = drDefault;
        DefaultResults.DataBind();
        DefaultResults.SelectedIndex = 0;

        drDefault.Close();
        drDefault.Dispose();

        if (DefaultResults.Rows.Count == 0)
        {
            pnlError.Visible = true;
            btnEditDefault.Visible = false;
            btnDeleteDefault.Visible = false;
        }
    }

    private string firstRowClientID;
    protected void DefaultResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowID = ((System.Data.Common.DbDataRecord)e.Row.DataItem)["ExamDefaultID"].ToString();
            if (firstRowClientID == null)
            {
                firstRowClientID = e.Row.ClientID;
                patientID.Value = rowID;
            }
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "', '" + firstRowClientID + "', " + rowID + ")");
        }

    }

    protected void btnDeleteDefault_Click(object sender, EventArgs e)
    {
        //delete the defaults
        string cmdText = "DELETE FROM ExamDefault WHERE ExamDefaultID = " + patientID.Value;
        DBUtil.Execute(cmdText);
        BindGrid();
    }

}