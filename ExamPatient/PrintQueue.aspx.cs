using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data.Common;

public partial class PrintQueue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ErrorPanel.Visible = false;
        ResultPanel.Visible = false;
        ButtonsPanel.Visible = true;
        if (!IsPostBack)
        {
            //getting the distinct users
            string cmdText = "SELECT DISTINCT u.FirstName + ' ' + u.LastName as DoctorName, u.UserName FROM [User] u JOIN PrintQueue q ON q.UserName = u.UserName";
            SqlDataReader drFilter = DBUtil.ExecuteReader(cmdText);

            ddlFilter.DataSource = drFilter;
            ddlFilter.DataValueField = "UserName";
            ddlFilter.DataTextField = "DoctorName";
            ddlFilter.DataBind();
            ddlFilter.Items.Insert(0, "");
            
            drFilter.Close();
            drFilter.Dispose();

            BindGrid("");
        }
    }


    protected void BindGrid(string userName)
    {
        //getting list of exams to print
        string cmdText = "SELECT p.FirstName + ' ' + p.LastName as PatientName, ExamDate, q.ExamID, PrintQueueID, PrintExamNote, CorrectExamID, ExamCorrectDate, LastUpdatedDate, u.FirstName + ' ' + u.LastName as DoctorName  FROM PrintQueue q JOIN Exam e on e.ExamID = q.ExamID JOIN Patient p on p.PatientID = e.PatientID JOIN [User] u on u.UserName = q.UserName";
        if (userName != "")
            cmdText += " WHERE q.UserName = '" + userName + "'";

        cmdText += " ORDER BY ExamDate";

        SqlDataReader drQueue = DBUtil.ExecuteReader(cmdText);

        PrintGrid.DataSource = drQueue;
        PrintGrid.DataBind();
        drQueue.Close();
        drQueue.Dispose();

        if (PrintGrid.Rows.Count == 0)
        {
            //error
            ErrorPanel.Visible = true;
            ButtonsPanel.Visible = false;
        }
    }

    protected void PrintGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((DbDataRecord)e.Row.DataItem)["CorrectExamID"] != DBNull.Value)
                e.Row.Cells[2].Text += " (Corrected on " + String.Format("{0:d}", Convert.ToDateTime(((DbDataRecord)e.Row.DataItem)["ExamCorrectDate"].ToString())) + ")";
            if (((DbDataRecord)e.Row.DataItem)["PrintExamNote"] != DBNull.Value)
            {
                HyperLink link = (HyperLink)e.Row.Cells[4].Controls[0];
                link.Text = "Exam Note";
                link.NavigateUrl = "PrintPatient.aspx?ExamID=" + (((DbDataRecord)e.Row.DataItem)["ExamID"].ToString());
            }
        }
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(ddlFilter.SelectedValue);
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        //looping through the grid and priting the records
        for (int iCtr = 0; iCtr < PrintGrid.Rows.Count; iCtr++)
        {
            CheckBox cbSelect = (CheckBox)PrintGrid.Rows[iCtr].FindControl("cbSelect");

            if (cbSelect.Checked)
            {
                sb.Append(PrintGrid.DataKeys[iCtr].Value + ",");
            }
        }

        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
            string cmdText = "DELETE FROM PrintQueue WHERE PrintQueueID in (" + sb.ToString() + ")";
            DBUtil.Execute(cmdText);

            ButtonsPanel.Visible = false;
            ResultPanel.Visible = true;
        }
    }
}
