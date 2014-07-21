using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Patient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable lookUp = DBUtil.ExecuteDataTable("SELECT FieldName, FieldValue, SortOrder FROM LookUp WHERE FieldName in ('Sex', 'HxFrom')");

            WebUtil.BindLookupDropDown(ddlSex, lookUp, "Sex");
            WebUtil.BindLookupDropDown(ddlHxFrom, lookUp, "HxFrom");
            WebUtil.SelectDropDownValue(ddlHxFrom, "mother");

            if (Request.QueryString["PatientID"] == null || Request.QueryString["PatientID"] == "")
            {
                PatientTab.HeaderText = "ADD";
            }
            else
            {
                string patientID;
                PatientTab.HeaderText = "VIEW / EDIT";
                patientID = Request.QueryString["PatientID"].ToString();

                //getting patient details from database
                string cmdText = "SELECT PatientID, PatientNumber, Greeting, FirstName, MiddleName, LastName, NickName, DateOfBirth, Sex, Occupation, HxFrom, ReferredFrom, ReferredDoctor, Allergies, Medications, PrematureBirth FROM Patient WHERE PatientID = '" + patientID + "'"; 

                SqlDataReader drPatient = DBUtil.ExecuteReader(cmdText);

                while (drPatient.Read())
                {
                    hdnPatientID.Value = drPatient["PatientID"].ToString();
                    hdnPatientNumber.Value = drPatient["PatientNumber"].ToString();
                    tbPatientNumber.Text = drPatient["PatientNumber"].ToString();
                    ddlGreeting.Items.FindByValue(drPatient["Greeting"].ToString()).Selected = true;
                    tbFirstName.Text = drPatient["FirstName"].ToString();
                    tbMiddleName.Text = drPatient["MiddleName"].ToString();
                    tbLastName.Text = drPatient["LastName"].ToString();
                    tbNickName.Text = drPatient["NickName"].ToString();
                    tbOccupation.Text = drPatient["Occupation"].ToString();
                    tbRefd.Text = drPatient["ReferredFrom"].ToString();
                    tbRefDoctor.Text = drPatient["ReferredDoctor"].ToString();
                    tbAllergies.Text = drPatient["Allergies"].ToString();
                    tbMedications.Text = drPatient["Medications"].ToString();
                    cbPrematureBirth.Checked = Convert.ToBoolean(drPatient["PrematureBirth"].ToString());

                    string dateofbirth = drPatient["DateOfBirth"].ToString();
                    if (dateofbirth != "")
                        dob.Text = DateTime.Parse(dateofbirth).ToShortDateString();

                    WebUtil.SelectDropDownValue(ddlSex, drPatient["Sex"].ToString());
                    WebUtil.SelectDropDownTextBox(ddlHxFrom, tbHxOther, drPatient["HxFrom"].ToString());
                }
                drPatient.Close();
                drPatient.Dispose();

                if (hdnPatientID.Value == "")
                    throw new ApplicationException("Patient information is not available");
            }

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string hxFrom = ddlHxFrom.SelectedValue == "" ? tbHxOther.Text : ddlHxFrom.SelectedValue;

        string cmdText = "";

        //checking whether the patient number already exists
        cmdText = "SELECT PatientID FROM Patient WHERE PatientNumber = '" + tbPatientNumber.Text + "' AND PatientNumber <> '" + hdnPatientNumber.Value + "'";
        object retValue = DBUtil.ExecuteScalar(cmdText);

        if (retValue != null)
            throw new ApplicationException("Unable to save. Patient Number already exists");

        if (hdnPatientID.Value != "")
        {
            cmdText = @"UPDATE Patient SET PatientNumber = '{0}', Greeting = '{1}', FirstName = '{2}', MiddleName = '{3}', LastName = '{4}', NickName = '{5}', DateOfBirth = '{6}'
                        , Sex = '{7}', Occupation = '{8}', HxFrom = '{9}', ReferredFrom = '{10}', ReferredDoctor = '{11}', Allergies = '{12}', Medications = '{13}'
                        , PrematureBirth = {14} WHERE PatientID = {15}; SELECT {15}";
        }
        else
        {
            cmdText = @"INSERT INTO Patient(PatientNumber, Greeting, FirstName, MiddleName, LastName, NickName, DateOfBirth, Sex, Occupation, HxFrom, ReferredFrom, ReferredDoctor, Allergies, Medications, PrematureBirth)";
            cmdText += @" VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', {14}); select Scope_Identity();";
        }

        cmdText = string.Format(cmdText, tbPatientNumber.Text, ddlGreeting.SelectedValue, tbFirstName.Text, tbMiddleName.Text
                                    , tbLastName.Text, tbNickName.Text, dob.Text, ddlSex.SelectedValue
                                    , tbOccupation.Text, hxFrom, tbRefd.Text.Replace("'", "''"), tbRefDoctor.Text.Replace("'", "''")
                                    , tbAllergies.Text, tbMedications.Text, cbPrematureBirth.Checked == true ? 1 : 0, hdnPatientID.Value
                                    );
        try
        {
            string patientID = DBUtil.ExecuteScalar(cmdText).ToString();
            result.Text = "Patient info successfully saved";
            result.Visible = true;
            pnlMain.Visible = false;
            pnlResult.Visible = true;
            resultError.Visible = false;

            btnExamInfant.OnClientClick = "location.href='ExamPatient.aspx?PatientID=" + patientID + "'; return false;";

            //checking for popup
            if (Request.QueryString["Popup"] == "1")
            {
                btnClose.Visible = true;
                btnHome.Visible = false;
                btnExamInfant.Visible = false;

                string copyTo = tbRefd.Text == tbRefDoctor.Text ? tbRefd.Text : tbRefd.Text + ", " + tbRefDoctor.Text;
                copyTo = copyTo.Replace("'", @"\'");

                StringBuilder sb = new StringBuilder();

                sb.Append(@"<script type=""text/javascript"">
                    function SetParent() {
                        var window_opener = window.dialogArguments;
                        var window_doc = window_opener.document;");

                sb.AppendLine(@"$(""#Greeting"", window_doc).val('" + ddlGreeting.SelectedValue + "');");
                sb.AppendLine(@"$(""#FirstName"", window_doc).val('" + tbFirstName.Text + "');");
                sb.AppendLine(@"$(""#MiddleName"", window_doc).val('" + tbMiddleName.Text + "');");
                sb.AppendLine(@"$(""#LastName"", window_doc).val('" + tbLastName.Text + "');");
                sb.AppendLine(@"$(""#DOB"", window_doc).val('" + dob.Text + "');");
                sb.AppendLine(@"$(""#Sex"", window_doc).val('" + ddlSex.SelectedValue + "');");
                sb.AppendLine(@"$(""#HxFrom"", window_doc).val('" + hxFrom + "');");
                sb.AppendLine(@"$(""#Occupation"", window_doc).val('" + tbOccupation.Text + "');");
                sb.AppendLine(@"$(""#Premature"", window_doc).attr('checked', " + cbPrematureBirth.Checked.ToString().ToLower() + ");");
                sb.AppendLine(@"if($(""#Refd"", window_doc).val() != '" + tbRefd.Text.Replace("'", @"\'") + @"' || $(""#RefDoctor"", window_doc).val() != '" + tbRefDoctor.Text.Replace("'", @"\'") + @"') $(""#CopyTo"", window_doc).val('" + copyTo + "');");
                sb.AppendLine(@"$(""#Refd"", window_doc).val('" + tbRefd.Text.Replace("'", @"\'") + "');");
                sb.AppendLine(@"$(""#RefDoctor"", window_doc).val('" + tbRefDoctor.Text.Replace("'", @"\'") + "');");
                sb.AppendLine(@"$(""#Allergies"", window_doc).val('" + tbAllergies.Text + "');");
                sb.AppendLine(@"$(""#Medications"", window_doc).val('" + tbMedications.Text + "');");


                sb.AppendLine(@"window.close();
                            return false;
                        }
                    </script>");

                javascript.Text = sb.ToString();
            }
        }
        catch (Exception exp)
        {
            resultError.Text = "Patient info not saved - " + exp.Message;
            resultError.Visible = true;
        }
    }

    private void DisableControls()
    {
        tbPatientNumber.ReadOnly = true;
        tbFirstName.ReadOnly = true;
        tbMiddleName.ReadOnly = true;
        tbLastName.ReadOnly = true;
        tbNickName.ReadOnly = true;
        tbOccupation.ReadOnly = true;
        ddlSex.Enabled = false;
        ddlHxFrom.Enabled = false;
        tbHxOther.ReadOnly = true;
        tbRefd.ReadOnly = true;
        tbAllergies.ReadOnly = true;
        tbMedications.ReadOnly = true;
        tbRefDoctor.ReadOnly = true;
        dob.IsControlEnable = false;
    }
}