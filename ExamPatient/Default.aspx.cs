using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string patientNumber = tbPatientNumber.Text.Trim();
        string firstName = tbFirstName.Text.Trim();
        string lastName = tbLastName.Text.Trim();

        if (patientNumber != "" || firstName != "" || lastName != "")
        {
            string cmdText = "SELECT PatientID, PatientNumber, FirstName + ' ' + LastName as PatientName, NickName, DateOfBirth FROM Patient WHERE 1 = 1";
            if (patientNumber != "")
                cmdText += " AND PatientNumber LIKE '" + patientNumber + "%'";
            if (firstName != "")
                cmdText += " AND FirstName LIKE '" + firstName + "%'";
            if (lastName != "")
                cmdText += " AND LastName LIKE '" + lastName + "%'";

            SqlDataReader dr = DBUtil.ExecuteReader(cmdText);

            pnlResults.Visible = true;
            
            patientResults.DataSource = dr;
            patientResults.DataBind();
            patientResults.SelectedIndex = 0;

            dr.Close();
            dr.Dispose();

            //giving errormessage if there are no matching records
            if (patientResults.Rows.Count == 0)
            {
                ButtonsPanel.Visible = false;
                resultError.Visible = true;
            }
            else
            {
                ButtonsPanel.Visible = true;
                resultError.Visible = false;
            }
        }


    }

    private string firstRowClientID;
    protected void patientResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowPatientID = ((System.Data.Common.DbDataRecord)e.Row.DataItem)["PatientID"].ToString();
            if (firstRowClientID == null)
            {
                firstRowClientID = e.Row.ClientID;
                patientID.Value = rowPatientID;
            }
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "', '" + firstRowClientID + "', " + rowPatientID + ")");
        }

    }
}
