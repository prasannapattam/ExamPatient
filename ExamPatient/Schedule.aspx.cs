using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.Common;

public partial class Schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlResult.Visible = false;
        pnlError.Visible = false;
        if (!IsPostBack)
        {
            string patientID;
            patientID = Request.QueryString["PatientID"];

            if (patientID == null)
                throw new Exception("Invalid patient");

            //getting patient details from database
            string cmdText = "SELECT PatientID, FirstName + ' ' + LastName as PatientName FROM Patient WHERE PatientID = '" + patientID + "'";

            SqlDataReader drPatient = DBUtil.ExecuteReader(cmdText);

            while (drPatient.Read())
            {
                hdnPatientID.Value = drPatient["PatientID"].ToString();
                lbPatientName.Text = drPatient["PatientName"].ToString();
            }
            drPatient.Close();
            drPatient.Dispose();

            if (hdnPatientID.Value == "")
                throw new ApplicationException("Patient information is not available");

            WebUtil.BindDoctorDropDown(ddlDoctor, "");

            string scheduleID = Request.QueryString["ScheduleID"];
            if (scheduleID != null && scheduleID != "")
            {
                cmdText = String.Format("SELECT ScheduleDate, DoctorUserName FROM Schedule WHERE ScheduleID = {0} AND PatientID = {1} AND Status = 'ACC'", scheduleID, patientID);
                SqlDataReader drSchedule = DBUtil.ExecuteReader(cmdText);
                while (drSchedule.Read())
                {
                    hdnScheduleID.Value = scheduleID;
                    DateTime scheduleDateTime = Convert.ToDateTime(drSchedule["ScheduleDate"]);
                    scheduleDate.Text = scheduleDateTime.ToShortDateString();
                    scheduleTime.Text = scheduleDateTime.ToShortTimeString();
                    string doctorUserName = drSchedule["DoctorUserName"].ToString();
                    WebUtil.SelectDropDownValue(ddlDoctor, doctorUserName);


                }
                drSchedule.Close();
                drSchedule.Dispose();
            }

            //deleting the schedule
            if (Request.QueryString["Delete"] != null && hdnScheduleID.Value != "")
            {
                cmdText = String.Format("DELETE FROM Schedule WHERE ScheduleID = {0}", hdnScheduleID.Value);
                DBUtil.Execute(cmdText);
                pnlResult.Visible = true;
                result.Text = "Appointment deleted Successfully";
                ClearFields();
                //Response.Redirect("Schedule.aspx?PatientID=" + patientID);
            }

            //bind the grid
            BindGrid(patientID);

        }

    }

    private void BindGrid(string patientID)
    {
        string cmdText = "SELECT s.ScheduleID, s.ScheduleDate, s.PatientID, p.FirstName + ' ' + p.LastName as PatientName, u.FirstName + ' ' + u.LastName as DoctorName FROM Schedule s INNER JOIN Patient p ON s.PatientID = p.PatientID INNER JOIN [User] u ON u.UserName = s.DoctorUserName WHERE s.ExamID IS NULL AND s.PatientID = " + patientID + " ORDER BY ScheduleDate";
        DataTable  dtSchedule = DBUtil.ExecuteDataTable(cmdText);
        ScheduleGrid.DataSource = dtSchedule;
        ScheduleGrid.DataBind();

        if (ScheduleGrid.Rows.Count == 0)
            AppointmentStatus.Visible = true;
        else
            AppointmentStatus.Visible = false;

        //setting the selected index if there is a scheduleID
        ScheduleGrid.SelectedIndex = -1;
        if (hdnScheduleID.Value != "")
        {
            for (int counter = 0; counter < dtSchedule.Rows.Count; counter++)
            {
                if (dtSchedule.Rows[counter]["ScheduleID"].ToString() == hdnScheduleID.Value)
                {
                    ScheduleGrid.SelectedIndex = counter;
                    break;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string appointmentDate = scheduleDate.Text;
        string appointmentTime = scheduleTime.Text;

        DateTime appointmentDateTime;


        try
        {
            appointmentDateTime = Convert.ToDateTime(appointmentDate + " " + appointmentTime);
        }
        catch
        {
            pnlError.Visible = true;
            resultError.Text = "Invalid schedule date and time";
            return;
        }

        string cmdText = "";
        if (hdnScheduleID.Value == "")
            cmdText = String.Format("INSERT INTO Schedule(PatientID, ScheduleDate, DoctorUserName, Status) VALUES('{0}', '{1}', '{2}', 'ACC')", hdnPatientID.Value, appointmentDateTime.ToString(), ddlDoctor.SelectedValue);
        else
            cmdText = String.Format("UPDATE Schedule SET ScheduleDate = '{1}', DoctorUserName = '{2}', Status = 'ACC' WHERE ScheduleID = {0}", hdnScheduleID.Value, appointmentDateTime.ToString(), ddlDoctor.SelectedValue);

        try
        {
            DBUtil.Execute(cmdText);
            pnlResult.Visible = true;
            result.Text = "Appointment Saved Successfully";
            ClearFields();
            BindGrid(hdnPatientID.Value);
        }
        catch (Exception exp)
        {
            resultError.Text = "Patient schedule not saved - " + exp.Message;
            pnlError.Visible = true;
        }

    }

    public void ClearFields()
    {
        hdnScheduleID.Value = "";
        ddlDoctor.SelectedIndex = -1;
        scheduleTime.Text = "";
        scheduleDate.Text = "";
    }

}