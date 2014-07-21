using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
        {
            throw new ApplicationException("Patient information is not available");
        }

        if (!IsPostBack)
        {
            string patientID;
            patientID = Request.QueryString["PatientID"].ToString();

            //getting patient details from database
            string cmdText = "SELECT PatientID, PatientNumber, FirstName, MiddleName, LastName, NickName FROM Patient WHERE PatientID = '" + patientID + "'";

            SqlDataReader drPatient = DBUtil.ExecuteReader(cmdText);

            while (drPatient.Read())
            {
                hdnPatientID.Value = drPatient["PatientID"].ToString();
                string name = drPatient["FirstName"].ToString() + " " + drPatient["LastName"].ToString();
                string patientNumber = drPatient["PatientNumber"].ToString();

                pnlHistory.HeaderText += name + " (Patient Number: " + patientNumber + ")";
            }
            drPatient.Close();
            drPatient.Dispose();

            if (hdnPatientID.Value == "")
                throw new ApplicationException("Patient information is not available");

            cmdText = "SELECT ExamID, ExamDate, ExamCorrectDate, CorrectExamID, SavedInd, LastUpdatedDate FROM EXAM WHERE PatientID = '" + patientID + "' ORDER BY ExamDate ASC, EXAMID ASC";

            SqlDataReader drExam = DBUtil.ExecuteReader(cmdText);

            HistoryResults.DataSource = drExam;
            HistoryResults.DataBind();
            HistoryResults.SelectedIndex = 0;

            drExam.Close();
            drExam.Dispose();

            //giving errormessage if there are no matching records
            if (HistoryResults.Rows.Count == 0)
            {
                ButtonsPanel.Visible = false;
                pnlError.Visible = true;
            }
            else
            {
                ButtonsPanel.Visible = true;
                pnlError.Visible = false;
            }

        }
    }

    private string firstRowClientID;
    protected void HistoryResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowExamid = ((System.Data.Common.DbDataRecord) e.Row.DataItem)["ExamID"].ToString(); 
            string savedInd = ((System.Data.Common.DbDataRecord)e.Row.DataItem)["SavedInd"].ToString();
            if (firstRowClientID == null)
            {
                firstRowClientID = e.Row.ClientID;
                patientID.Value = rowExamid;
            }
            if (((System.Data.Common.DbDataRecord)e.Row.DataItem)["CorrectExamID"] != DBNull.Value)
                e.Row.Cells[0].Text += " (Corrected on " + String.Format("{0:d}", Convert.ToDateTime(((System.Data.Common.DbDataRecord)e.Row.DataItem)["ExamCorrectDate"].ToString())) + ")";
            if (savedInd == "1")
                e.Row.Cells[0].Text += " (Saved on " + String.Format("{0:g}", Convert.ToDateTime(((System.Data.Common.DbDataRecord)e.Row.DataItem)["LastUpdatedDate"].ToString())) + ")";
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "', '" + firstRowClientID + "', " + rowExamid + ", " + savedInd + ")");
        }
    }
}
