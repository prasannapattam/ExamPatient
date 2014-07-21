using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Dashboard : System.Web.UI.Page
{
    private string doctorUserName = HttpContext.Current.User.Identity.Name;
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlError.Visible = false;
        AppointmentStatus.Visible = false;
        if (!IsPostBack)
        {
            WebUtil.BindDoctorDropDown(ddlDoctor, doctorUserName);
            DateTime appointmentDate = DateTime.Today;
            scheduleDate.Text = appointmentDate.ToShortDateString();


            BindGrid(doctorUserName, appointmentDate);
        }
    }

    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime appointmentDate = Convert.ToDateTime(scheduleDate.Text);

            BindGrid(ddlDoctor.SelectedValue, appointmentDate);
        }
        catch
        {
            pnlError.Visible = true;
            resultError.Text = "Invalid schedule date";
            return;
        }
    }

    private void BindGrid(string doctor, DateTime scheduleDate)
    {
        string cmdText = String.Format(@"SELECT s.ScheduleID, s.ScheduleDate, s.PatientID, p.FirstName + ' ' + p.LastName as PatientName, u.FirstName + ' ' + u.LastName as DoctorName, s.ExamID 
                            FROM Schedule s INNER JOIN Patient p ON s.PatientID = p.PatientID INNER JOIN [User] u ON u.UserName = s.DoctorUserName 
                            WHERE s.ScheduleDate BETWEEN '{0}' AND '{1}'", scheduleDate.ToShortDateString(), scheduleDate.AddDays(1).ToShortDateString());
        if (doctor != "")
        {
            cmdText += String.Format(" AND s.DoctorUserName = '{0}'", doctor);
        }
        cmdText += " ORDER BY s.ExamID, ScheduleDate";
        DataTable dtSchedule = DBUtil.ExecuteDataTable(cmdText);
        ScheduleGrid.DataSource = dtSchedule;
        ScheduleGrid.DataBind();

        if (ScheduleGrid.Rows.Count == 0)
            AppointmentStatus.Visible = true;
        else
            AppointmentStatus.Visible = false;

        //checking and setting the exam notes
        for (int counter = 0; counter < dtSchedule.Rows.Count; counter++)
        {
            if (dtSchedule.Rows[counter]["ExamID"] != DBNull.Value)
            {
                //ScheduleGrid.SelectedIndex = counter;
                ScheduleGrid.Rows[counter].CssClass = "gridSelectedRow";
                HyperLink hyp = (HyperLink)ScheduleGrid.Rows[counter].Cells[4].Controls[0];
                hyp.Text = "Correct Notes";
                hyp.NavigateUrl += "&ExamID=" + dtSchedule.Rows[counter]["ExamID"].ToString();
            }
        }



    }
}