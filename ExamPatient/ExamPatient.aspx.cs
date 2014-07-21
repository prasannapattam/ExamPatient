using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.Services;

public partial class ExamPatient : System.Web.UI.Page
{
    private DataTable lookUp;
    private Control[] pageControls;
    private ExamNotesType notesType = ExamNotesType.New;
    private Dictionary<string, string> ColourType = new Dictionary<string, string>();

    private string doctorUserName = HttpContext.Current.User.Identity.Name;
    string patientName, patientFirstName, patientLastName, patientAge, patientSex, patientPronoun;
    private string previousAge = "";

    int maxBaby = 6;  //months for a baby
    int maxBoyGirlDecimal = (10 * 12); //years for boy & girl
    int maxBoyGirl = (17 * 12); //years for boy & girl
    int maxYoung = (24 * 12); //years for young

    int patientMonths = 0;
    bool prematureBirth = false;

    string female = "female";
    string male = "male";

    DateTime dtDOB;

    protected void Page_Load(object sender, EventArgs e)
    {
        Context.Items["ShowTitle"] = true;

        pageControls = FlattenHierachy(Page);

        //getting the colour types selected by the user
        string[] arr = hdnColourType.Value.Split(',');
        foreach (string str in arr)
        {
            if (str != "" && !ColourType.ContainsKey(str))
                ColourType.Add(str, "");
            hdnColourType.Value = "";
        }

        //setting the doctor as the userName
        if (Request.QueryString["DoctorUserName"] != null)
            doctorUserName = Request.QueryString["DoctorUserName"];

        if (!IsPostBack)
        {
            LastExamElse.Visible = false;

            string cmdText = "SELECT FirstName + ' ' + LastName as DoctorName, UserName FROM [User]";

            SqlDataReader drUser = DBUtil.ExecuteReader(cmdText);

            WebUtil.BindLookupDropDown(User, drUser, "DoctorName", "UserName");

            drUser.Close();
            drUser.Dispose();

            WebUtil.SelectDropDownValue(User, doctorUserName);

            //populating the drop downs
            cmdText = "SELECT ControlName, FieldValue, RTrim(lu.FieldDescription) as FieldDescription, SortOrder";

            cmdText += ", null as DefaultValue FROM ExamLookUp elu inner join LookUp lu on elu.FieldName = lu.FieldName";
            lookUp = DBUtil.ExecuteDataTable(cmdText);

            PopulateDropDowns();

            if (Request.QueryString["ExamDefaultID"] != null)
                ExamDefault();
            else
                ExamNotes();

            hdnNoteType.Value = ((int)notesType).ToString();
            if (Dilate3.SelectedIndex == 0) //for backward compatibility as I removed the Not Dialted checkbox
                Dilate3.SelectedIndex = 1;
        }

        //getting the notes type for this page
        notesType = (ExamNotesType) Convert.ToInt32(hdnNoteType.Value);

        if (notesType == ExamNotesType.Correct)
        {
            HideSaveButtons();
            btnSignOff.Text = "CORRECT";
            btnSignOff1.Text = "CORRECT";
            btnSignOff2.Text = "CORRECT";
            btnSignOff3.Text = "CORRECT";
            btnSignOff4.Text = "CORRECT";
        }

        if (Request.QueryString["Print"] != null)
        {
            ExamPatientTab.HideTab = true;
            pnlComplaint.Visible = false;
            btnEditPatient.Visible = false;
            pnlVisual.Visible = false;
            pnlOcular.Visible = false;
            pnlAnterior.Visible = false;
            pnlLoc.Visible = false;
        }
    }

    private void ExamDefault()
    {
        //for defining defaults
        string examDefaultID  = Request.QueryString["ExamDefaultID"];
        notesType = ExamNotesType.Default;
        Title = "Notes Default";

        pnlHeader.Visible = false;
        pnlDefault.Visible = true;
        HideSaveButtons();
        btnSignOff.Visible = false;
        btnSignOff1.Visible = false;
        btnSignOff2.Visible = false;
        btnSignOff3.Visible = false;
        btnSignOff4.Visible = false;
        btnDefault.Visible = true;

        string cmdText;

        cmdText = "SELECT UserID, FirstName + ' ' + LastName as DoctorName FROM [USER] ORDER BY DoctorName";
        SqlDataReader drDoctor = DBUtil.ExecuteReader(cmdText);

        WebUtil.BindLookupDropDown(DoctorList, drDoctor, "DoctorName", "UserID");
        DoctorList.Items[0].Value = "0"; //setting the blank field value to 0;

        drDoctor.Close();
        drDoctor.Dispose();

        //getting the previous text
        cmdText = "SELECT ExamDefaultID, DefaultName, AgeStart, AgeEnd, PrematureBirth, DoctorUserID, ExamText FROM ExamDefault WHERE ExamDefaultID = " + examDefaultID;
        SqlDataReader drExamDefault = DBUtil.ExecuteReader(cmdText);

        if (drExamDefault.Read())
        {
            hdnExamDefaultID.Value = examDefaultID;
            SetValues(drExamDefault["ExamText"].ToString());
        }
        else
        {
            hdnExamDefaultID.Value = "0";
        }

        drExamDefault.Close();
        drExamDefault.Dispose();
    }

    private void ExamNotes()
    {
        string cmdText;
        string patientID = Request.QueryString["PatientID"];

        if (patientID == null || patientID.Trim() == "")
        {
            throw new ApplicationException("Patient information is not available");
        }

        string examID = Request.QueryString["ExamID"];

        //checking whether the examid exists
        if (examID == null || examID == "")
            examID = "0";
        //if (examID != "")
        //{
            cmdText = String.Format("SELECT TOP 1 ExamID FROM Exam WHERE ExamID = {0} OR (PatientID = {1} AND ExamDate = '{2}') ORDER BY ExamID DESC"
                                , examID, patientID, DateTime.Today.ToShortDateString());
            object retValue = DBUtil.ExecuteScalar(cmdText);
            if (retValue == null)
                examID = "";
            else
            {
                examID = retValue.ToString();
                hdnExamID.Value = examID;
                notesType = ExamNotesType.Correct;
            }
        //}

        PopulateHeader(patientID);

        if (hdnPatientID.Value == "")
            throw new ApplicationException("Patient information is not available");
        else
        {
            //getting the last Exam record for the patient
            cmdText = "SELECT TOP 1 ExamID FROM EXAM WHERE PatientID = " + hdnPatientID.Value;
            if (examID != "")
                cmdText += " AND ExamID = " + examID;
            cmdText += " ORDER BY SavedInd DESC, ExamDate DESC, ExamID DESC";
            SqlDataReader drExam = DBUtil.ExecuteReader(cmdText);
            if (drExam.Read())
            {
                if (drExam["ExamID"].ToString() != "")
                {
                    examID = drExam["ExamID"].ToString();
                    cmdText = "SELECT ExamText, ExamDate, SavedInd FROM Exam WHERE ExamID = " + examID;
                    SqlDataReader drLastExam = DBUtil.ExecuteReader(cmdText);

                    drLastExam.Read();

                    if (drLastExam["SavedInd"].ToString() == "1")
                    {
                        hdnExamID.Value = examID;
                        notesType = ExamNotesType.Saved;
                    }

                    SetValues(drLastExam.GetString(0));
                    if(notesType != ExamNotesType.Saved)
                        SetPriorValues();

                    if (notesType == ExamNotesType.New)
                    {
                        string lastExamDate = drLastExam["ExamDate"].ToString();
                        LastExam.Text = DateTime.Parse(lastExamDate).ToShortDateString();
                    }

                    if (notesType == ExamNotesType.Correct)
                    {
                        string lastExamDate = drLastExam["ExamDate"].ToString();
                        ExamDate.Text = DateTime.Parse(lastExamDate).ToShortDateString();
                    }

                    drLastExam.Close();
                    drLastExam.Dispose();
                }
            }
            else
            {
                //there is no last exam, so getting the defaults
                hdnDefaultInd.Value = "1";  //setting an indicator that the system is setting defaults.
                string examDefaultID = GetDefaultExamID();

                if (examDefaultID != "")
                {
                    cmdText = String.Format("SELECT ExamText FROM ExamDefault WHERE ExamDefaultID = {0}", examDefaultID);

                    SqlDataReader drExamDefault = DBUtil.ExecuteReader(cmdText);
                    drExamDefault.Read();
                    SetValues(drExamDefault.GetString(0));
                    drExamDefault.Close();
                    drExamDefault.Dispose();
                }
            }
            drExam.Close();
            drExam.Dispose();
        }

        if(notesType == ExamNotesType.New)
            PopulateHeader(patientID);

        if (notesType == ExamNotesType.New)
        {
            if(previousAge != "")
                Summary.Text = Summary.Text.Replace(previousAge, Age.Value);
            Discussed.Text = "Discussed findings with " + patientName;
            if (HxFrom.Text.ToString() != "" && HxFrom.Text != "patient")
            {
                string displaySex = "";
                string hxFrom = "";
                if (HxFrom.Text.IndexOf("patient and") >= 0)
                {
                    hxFrom = HxFrom.Text.Replace("patient and", "").Trim();
                    if (Sex.Text.ToLower() == female)
                        displaySex += "her";
                    else if (Sex.Text.ToLower() == male)
                        displaySex += "his";
                    Discussed.Text += " and " + displaySex + " " + hxFrom;
                }
                else
                {
                    Discussed.Text += "'s " + HxFrom.Text;
                }

            }

            //setting the Copy To
            CopyTo.Text = Refd.Text;
            if (Refd.Text != RefDoctor.Text)
                CopyTo.Text += ", " + RefDoctor.Text;

        }
        else
        {
            if(notesType == ExamNotesType.Correct)
                ExamDate.IsReadOnly = true;
        }
        if (notesType == ExamNotesType.New)
            SetBackgroundColours(1);
    }

    private void HideSaveButtons()
    {
        //hiding all the Save buttons
        btnAnteriorSave.Visible = false;
        btnComplaintSave.Visible = false;
        btnLocSave.Visible = false;
        btnOcularSave.Visible = false;
        btnVisualSave.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string xml = GetXml(false, null);
        string cmdText = "";

        if (hdnExamID.Value == "")
        {
            //insert into table
            cmdText = String.Format("INSERT INTO Exam(PatientID, ExamText, ExamDate, ExamCorrectDate, CorrectExamID, SavedInd, LastUpdatedDate, UserName) VALUES({0}, '{1}', '{2}', '{3}', {4}, {5}, '{6}', '{7}'); select Scope_Identity();"
                                    , hdnPatientID.Value, xml, ExamDate.Text, DateTime.Now.ToString(), "null", 1, DateTime.Now.ToString(), User.SelectedValue);

        }
        else
        {
            //update already saved notes
            cmdText = String.Format("UPDATE Exam Set ExamText = '{1}', ExamDate = '{2}', ExamCorrectDate = '{3}',  SavedInd = 1, LastUpdatedDate = '{4}', UserName = '{5}' WHERE ExamID={0}; select {0}", hdnExamID.Value, xml, ExamDate.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), User.SelectedValue);

        }

        try
        {
            string examID = DBUtil.ExecuteScalar(cmdText).ToString();

            //check and mark schedule
            //MarkSchedule(examID);

            pnlSave.Visible = true;
            pnlHeader.Visible = false;
            ExamPatientTab.Visible = false;
            pnlTabs.Visible = false;
            btnContinue.OnClientClick = String.Format(@"location.href='ExamPatient.aspx?PatientID={1}&ExamID={0}';return false;", examID, hdnPatientID.Value);
            
            hdnExamID.Value = examID;
            hdnNoteType.Value = ((int)ExamNotesType.Saved).ToString();
        }
        catch (Exception exp)
        {
            resultError.Text = "Patient exam data not saved - " + exp.Message;
            resultError.Visible = true;
        }
    }

    protected void btnSignOff_Click(object sender, EventArgs e)
    {
        //PopulateHeader(hdnPatientID.Value);
        string cmdText = "";
        Dictionary<string, string> dict = null;

        //for correction getting the data that is changed
        if (notesType == ExamNotesType.Correct)
        {
            cmdText = "SELECT ExamText FROM Exam WHERE ExamID = " + hdnExamID.Value;
            string xmlOriginalExam = DBUtil.ExecuteScalar(cmdText).ToString();

            dict = WebUtil.GetDictionary(xmlOriginalExam, false);
        }

        string xml = GetXml(true, dict);

        switch (notesType)
        {
            case ExamNotesType.New:
                cmdText = string.Format("INSERT INTO Exam(PatientID, ExamText, ExamDate, SavedInd, LastUpdatedDate, UserName) VALUES({0}, '{1}', '{2}', 0, '{3}', '{4}'); select Scope_Identity();", hdnPatientID.Value, xml, ExamDate.Text, DateTime.Now.ToString(), User.SelectedValue);
                break;
            case ExamNotesType.Saved:
                cmdText = String.Format("UPDATE Exam Set ExamText = '{1}', ExamDate = '{2}', ExamCorrectDate = '{3}',  SavedInd = 0, LastUpdatedDate = '{4}', UserName = '{5}' WHERE ExamID={0}; select {0}", hdnExamID.Value, xml, ExamDate.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), User.SelectedValue);
                break;
            case ExamNotesType.Correct:
                cmdText = string.Format("INSERT INTO Exam(PatientID, ExamText, ExamDate, ExamCorrectDate, CorrectExamID, SavedInd, LastUpdatedDate, UserName) VALUES({0}, '{1}', '{2}', '{3}', {4}, 0, '{5}', '{6}'); select Scope_Identity();", hdnPatientID.Value, xml, ExamDate.Text, DateTime.Now.ToString(), hdnExamID.Value, DateTime.Now.ToString(), User.SelectedValue);
                break;
        }


        try
        {
            string examID = DBUtil.ExecuteScalar(cmdText).ToString();

            if (notesType == ExamNotesType.Correct)
            {
                //deleting the old record from the print queue
                cmdText = String.Format("DELETE FROM PrintQueue WHERE ExamID = {0}", hdnExamID.Value);
                DBUtil.Execute(cmdText);
            }

            if (ExamNoteTo.Text.Trim() != "")
            {
                cmdText = String.Format("INSERT INTO PrintQueue(ExamID, UserName, PrintExamNote ) VALUES({0}, '{1}', 1)", examID, User.SelectedValue);
                DBUtil.Execute(cmdText);
            }

            //inserting into PrintQueue
            if (cbPrintQueue.Checked)
            {
                cmdText = String.Format("INSERT INTO PrintQueue(ExamID, UserName) VALUES({0}, '{1}')", examID, User.SelectedValue);
                DBUtil.Execute(cmdText);
            }

            //check and mark schedule
            MarkSchedule(examID);

            //result.Text = "Patient exam data successfully saved";
            btnReport.Visible = true;
            btnReport.OnClientClick = @"location.href='Report.aspx?ExamID=" + examID + @"';return false;";
            btnPrintExam.OnClientClick = @"location.href='PrintPatient.aspx?ExamID=" + examID + @"';return false;";   //Print=1&PatientID=7&ExamID=87
            result.Visible = true;
            ExamPatientTab.Visible = false;
            pnlTabs.Visible = false;
            pnlResult.Visible = true;
            resultError.Visible = false;
        }
        catch (Exception exp)
        {
            resultError.Text = "Patient exam data not saved - " + exp.Message;
            resultError.Visible = true;
        }

    }

    protected void btnDefault_Click(object sender, EventArgs e)
    {

        string xml = GetXml(true, null);
        string cmdText;

        string examDefaultID = hdnExamDefaultID.Value;
        if (examDefaultID == "0")
            cmdText = "INSERT INTO ExamDefault (DefaultName, AgeStart, AgeEnd, PrematureBirth, DoctorUserID, ExamText) VALUES('{0}', {1}, {2}, {3}, {4}, '{5}')";
        else
            cmdText = "UPDATE ExamDefault SET DefaultName = '{0}', AgeStart = {1}, AgeEnd = {2}, PrematureBirth = {3}, DoctorUserID = {4}, ExamText = '{5}' WHERE ExamDefaultID = {6}";

        cmdText = String.Format(cmdText, DefaultName.Text.Trim(), AgeStart.Text.Trim(), AgeEnd.Text.Trim(), (PrematureBirth.Checked ? 1 : 0), DoctorList.SelectedValue, xml, examDefaultID);

        try
        {
            DBUtil.Execute(cmdText);

            result.Text = "Patient default data successfully saved";
            btnReport.Text = "EXAM DEFAULTS";
            btnReport.Visible = true;
            btnReport.OnClientClick = @"location.href='ExamDefaults.aspx'; return false;";
            result.Visible = true;
            ExamPatientTab.Visible = false;
            pnlTabs.Visible = false;
            pnlResult.Visible = true;
            resultError.Visible = false;
        }
        catch (Exception exp)
        {
            resultError.Text = "Patient defaults not saved - " + exp.Message;
            resultError.Visible = true;
        }

    }

    private void MarkSchedule(string examID)
    {
        if (Request.QueryString["ScheduleID"] != null)
        {
            string cmdText = String.Format("UPDATE Schedule SET ExamID = {1} WHERE ScheduleID = {0}", Request.QueryString["ScheduleID"], examID);
            DBUtil.Execute(cmdText);
        }

    }

    private string GetDefaultExamID()
    {
        //getting all the defaults
        string cmdText = "SELECT ExamDefaultID, AgeStart, AgeEnd, PrematureBirth, u.UserName as DoctorUserName FROM ExamDefault e LEFT JOIN [User] u ON u.UserID = e.DoctorUserID";

        DataTable dtDefaults = DBUtil.ExecuteDataTable(cmdText);

        string filter = "AgeStart <= " + patientMonths + " AND AgeEnd >= " + patientMonths;

        if (prematureBirth && patientMonths <= maxBaby)
            filter += " AND PrematureBirth = 1";            
        else
            filter += " AND PrematureBirth = 0";            

        //checking whether this is a default for the doctor
        DataView dv = new DataView(dtDefaults, filter + " AND DoctorUserName = '" + doctorUserName + "'", "AgeStart DESC", DataViewRowState.CurrentRows);
        if (dv.Count > 0)
            return dv[0]["ExamDefaultID"].ToString();
       
        //check for defaults without any doctor
        dv = new DataView(dtDefaults, filter + " AND DoctorUserName IS NULL", "AgeStart DESC", DataViewRowState.CurrentRows);
        if (dv.Count > 0)
            return dv[0]["ExamDefaultID"].ToString();

        return "";
    }

    public Control[] FlattenHierachy(Control root)
    {
        List<Control> list = new List<Control>();
        list.Add(root);
        if (root.HasControls())
        {
            foreach (Control control in root.Controls)
            {
                list.AddRange(FlattenHierachy(control));
            }
        }
        return list.ToArray();
    }

    public string GetXml(bool acceptDefaults, Dictionary<string, string> dict)
    {
        dict = (dict == null) ? new Dictionary<string, string>() : dict;

        //setting the HxFrom
        HxFrom.Text = ddlHxFrom.SelectedValue == "" ? tbHxOther.Text : ddlHxFrom.SelectedValue;

        StringWriter stringWriter = new StringWriter();
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.CheckCharacters = false;
        XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
        //XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
        
        //xmlWriter.WriteStartDocument();
        xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"");
        xmlWriter.WriteStartElement("patient");

        foreach (Control ctrl in pageControls)
        {
            string customColourType = "0";
            if (ctrl.GetType().Name == "TextBox" || ctrl.GetType().Name == "DropDownList")
            {
                if (acceptDefaults || ColourType.ContainsKey(ctrl.ID))
                    customColourType = "0";
                else
                    customColourType = ((WebControl)ctrl).Attributes["CustomColourType"];
            }

            switch (ctrl.GetType().Name)
            {
                case "TextBox":
                    TextBox tb = (TextBox)ctrl;
                    xmlWriter.WriteStartElement(ctrl.ID);
                    if (dict.ContainsKey(ctrl.ID) && dict[ctrl.ID] != tb.Text.Trim())
                        customColourType = "2";
                    xmlWriter.WriteAttributeString("CustomColourType", customColourType);
                    tb.Attributes["CustomColourType"] = customColourType;
                    xmlWriter.WriteCData(tb.Text.Trim().Replace("'", "''"));
                    xmlWriter.WriteEndElement();
                    break;
                case "HiddenField":
                    xmlWriter.WriteStartElement(ctrl.ID);
                    xmlWriter.WriteCData(((HiddenField)ctrl).Value);
                    xmlWriter.WriteEndElement();
                    break;
                case "DropDownList":
                    DropDownList ddl = (DropDownList)ctrl;
                    xmlWriter.WriteStartElement(ctrl.ID);
                    if (dict.ContainsKey(ctrl.ID) && dict[ctrl.ID] != ddl.SelectedValue.Trim())
                        customColourType = "2";
                    xmlWriter.WriteAttributeString("CustomColourType", customColourType);
                    ddl.Attributes["CustomColourType"] = customColourType;
                    xmlWriter.WriteCData(ddl.SelectedValue.Trim().Replace("'", "''"));
                    xmlWriter.WriteEndElement();
                    break;
                case "CheckBox":
                    xmlWriter.WriteStartElement(ctrl.ID);
                    xmlWriter.WriteString(((CheckBox)ctrl).Checked.ToString());
                    xmlWriter.WriteEndElement();
                    break;
            }
        }

        xmlWriter.WriteEndElement();
        //xmlWriter.WriteEndDocument();
        xmlWriter.Flush();
        xmlWriter.Close();
        stringWriter.Flush();
        string xml = stringWriter.ToString();
        stringWriter.Dispose();
        return xml;
    }

    private void SetValues(string xml)
    {
        StringReader stringReader = new StringReader(xml);
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.CheckCharacters = false;
        XmlReader reader = XmlReader.Create(stringReader, settings);
        //XmlTextReader reader = new XmlTextReader(stringReader);
        //reader.WhitespaceHandling = WhitespaceHandling.None;

        string fieldName = "";
        string fieldValue = "";
        string fieldAttr = "";
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    fieldName = reader.Name;
                    fieldAttr = reader.GetAttribute("CustomColourType");
                    break;
                case XmlNodeType.Text:
                    fieldValue = reader.Value;
                    break;
                case XmlNodeType.CDATA:
                    fieldValue = reader.Value;
                    break;
                case XmlNodeType.EndElement:
                    SetControlValue(fieldName, fieldValue, fieldAttr);
                    fieldName = "";
                    fieldValue = "";
                    fieldAttr = "";
                    break;
            }
        }

        reader.Close();
        stringReader.Close();
        stringReader.Dispose();
    }

    private void SetControlValue(string fieldName, string fieldValue, string fieldAttr)
    {
        if (fieldName == "")
            return;

        if (hdnDefaultInd.Value == "1" && fieldName.ToLower() == "user")
            return;

        if (fieldName == "Age")
            previousAge = fieldValue;

        Control ctrl =  null;
        for (int i = 0; i < pageControls.Length; i++)
        {
            if (pageControls[i].ID == fieldName)
            {
                ctrl = pageControls[i];
                break;
            }
        }

        if (ctrl != null)
        {
            switch (ctrl.GetType().Name)
            {
                case "TextBox":
                    TextBox tb = (TextBox)ctrl;
                    //replacing variables
                    if (notesType == ExamNotesType.New)
                    {
                        fieldValue = fieldValue.Replace("[PatientName]", patientName);
                        fieldValue = fieldValue.Replace("[FirstName]", patientFirstName);
                        fieldValue = fieldValue.Replace("[LastName]", patientLastName);
                        fieldValue = fieldValue.Replace("[Age]", patientAge);
                        fieldValue = fieldValue.Replace("[Sex]", patientSex);
                        //fieldValue = fieldValue.Replace("[GA]", patientSex);
                        //fieldValue = fieldValue.Replace("[BW]", patientSex);
                        //fieldValue = fieldValue.Replace("[PCA]", patientSex);
                    }

                    tb.Text = fieldValue;
                    tb.Attributes.Add("CustomColourType", fieldAttr);
                    break;
                case "DropDownList":
                    DropDownList ddl = (DropDownList)ctrl;
                    ListItem li = ddl.Items.FindByValue(fieldValue);
                    if (li != null)
                    {
                        ddl.ClearSelection();
                        li.Selected = true;
                    }
                    ddl.Attributes.Add("CustomColourType", fieldAttr);
                    break;
                case "CheckBox":
                    ((CheckBox)ctrl).Checked = Convert.ToBoolean(fieldValue);
                    break;
            }
        }
    }
        
    private void SetPriorValues()
    {
        if(ManRfxOD1.Text != "")
            LastManRfxOD1.Text = ManRfxOD1.Text;
        if(ManRfxOD2.Text != "")
            LastManRfxOD2.Text = ManRfxOD2.Text;
        if(ManVAOD1.SelectedIndex != 0)
            LastManVAOD1.SelectedIndex = ManVAOD1.SelectedIndex;
        if (ManVAOD2.Text != "")
            LastManVAOD2.Text = ManVAOD2.Text;
        if (CycRfxOD.Text != "")
            LastCycRfxOD.Text = CycRfxOD.Text;
        if (CycVAOD3.SelectedIndex != 0)
            LastCycVAOD3.SelectedIndex = CycVAOD3.SelectedIndex;
        if (CycVAOD4.Text != "")
            LastCycVAOD4.Text = CycVAOD4.Text;

        if (ManRfxOS1.Text != "")
            LastManRfxOS1.Text = ManRfxOS1.Text;
        if (ManRfxOS2.Text != "")
            LastManRfxOS2.Text = ManRfxOS2.Text;
        if (ManVSOS1.SelectedIndex != 0)
            LastManVSOS1.SelectedIndex = ManVSOS1.SelectedIndex;
        if (ManVSOS2.Text != "")
            LastManVSOS2.Text = ManVSOS2.Text;
        if (CycRfxOS.Text != "")
            LastCycRfxOS.Text = CycRfxOS.Text;
        if (CycVSOS1.SelectedIndex != 0)
            LastCycVSOS1.SelectedIndex = CycVSOS1.SelectedIndex;
        if (CycVSOS2.Text != "")
            LastCycVSOS2.Text = CycVSOS2.Text;

        //setting the original values to blank
        ManRfxOD1.Text = "";
        ManRfxOD2.Text = "";
        ManVAOD1.SelectedIndex = 0;
        ManVAOD2.Text = "";
        CycRfxOD.Text = "";
        CycVAOD3.SelectedIndex = 0;
        CycVAOD4.Text = "";

        ManRfxOS1.Text = "";
        ManRfxOS2.Text = "";
        ManVSOS1.SelectedIndex = 0;
        ManVSOS2.Text = "";
        CycRfxOS.Text = "";
        CycVSOS1.SelectedIndex = 0;
        CycVSOS2.Text = "";


    }

    private void PopulateDropDowns()
    {
        //getting all the drop downs and applying filter
        foreach (Control ctrl in pageControls)
        {
            if (ctrl.GetType().Name == "DropDownList")
            {
                DropDownList ddl = (DropDownList)ctrl;
                DataView dv = new DataView(lookUp, "ControlName='" + ddl.ID + "'", "SortOrder ", DataViewRowState.CurrentRows);

                if (dv.Count > 0)
                {
                    ddl.Items.Add(new ListItem("", ""));
                    ddl.AppendDataBoundItems = true;
                    ddl.DataSource = dv;
                    ddl.DataTextField = "FieldValue";
                    ddl.DataValueField = "FieldDescription";
                    ddl.DataBind();

                    //checking for duplicate values
                    Dictionary<string, string> dups = new Dictionary<string, string>();
                    int dupCount = 1;
                    foreach (ListItem liDups in ddl.Items)
                    {
                        if (dups.ContainsKey(liDups.Value))
                        {
                            liDups.Value += "~" + dupCount.ToString();
                            dupCount++;
                        }
                        else
                        {
                            dups.Add(liDups.Value, "");
                        }
                    }

                    //selecting the default value
                    string defaultValue = dv[0]["DefaultValue"] == DBNull.Value ? "" : dv[0]["DefaultValue"].ToString();
                    ListItem li = ddl.Items.FindByValue(defaultValue);
                    if (li != null)
                    {
                        li.Selected = true;
                        //break;
                    }
                }
                dv.Dispose();
            }
        }
    }

    private void PopulateHeader(string patientID)
    {
        if (patientID == "")
            return;
        //getting patient details from database
        string cmdText = "SELECT PatientID, Greeting, FirstName, MiddleName, LastName, NickName, DateOfBirth, Sex, Occupation, HxFrom, ReferredFrom, ReferredDoctor, Allergies, Medications, PrematureBirth FROM Patient WHERE PatientID = " + patientID;

        SqlDataReader drPatient = DBUtil.ExecuteReader(cmdText);

        while (drPatient.Read())
        {
            hdnPatientID.Value = drPatient["PatientID"].ToString();
            Greeting.Text = drPatient["Greeting"].ToString();
            FirstName.Text = drPatient["FirstName"].ToString();
            MiddleName.Text = drPatient["MiddleName"].ToString();
            LastName.Text = drPatient["LastName"].ToString();
            Sex.Text = drPatient["Sex"].ToString();
            string dateofbirth = drPatient["DateOfBirth"].ToString();
            if (dateofbirth != "")
            {
                dtDOB = DateTime.Parse(dateofbirth);
                DOB.Text = dtDOB.ToShortDateString();

                string age = GetAge(dtDOB, Sex.Text);
                Age.Value = age.Substring(0, age.IndexOf('~') - 1);
                tbAge.Text = Age.Value;
                BoyGirlAdult.Value = age.Substring(age.IndexOf('~') + 1);
                PatientMonths.Value = patientMonths.ToString();
            }

            //when there is a postback then editable fields should not be updated
            if (!IsPostBack)
            {
                Refd.Text = drPatient["ReferredFrom"].ToString();
                RefDoctor.Text = drPatient["ReferredDoctor"].ToString();
                Allergies.Text = drPatient["Allergies"].ToString();
                if (Medications.Text == "")
                    Medications.Text = drPatient["Medications"].ToString();
            }

            HxFrom.Text = drPatient["HxFrom"].ToString();
            WebUtil.SelectDropDownTextBox(ddlHxFrom, tbHxOther, drPatient["HxFrom"].ToString());
            Occupation.Text = drPatient["Occupation"].ToString();
            prematureBirth = Convert.ToBoolean(drPatient["PrematureBirth"]);
            if (prematureBirth)
                Premature.Checked = true;
            if (Greeting.Text == "")
                patientName = FirstName.Text;
            else
                patientName = Greeting.Text + " " + LastName.Text;
            patientFirstName = FirstName.Text;
            patientLastName = LastName.Text;
            patientAge = tbAge.Text;
            patientSex = BoyGirlAdult.Value;
        }

        drPatient.Close();
        drPatient.Dispose();

        ExamDate.Text = DateTime.Now.ToShortDateString();
    }

    private int monthDifference(DateTime startDate, DateTime endDate)
    {
        int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
        return Math.Abs(monthsApart);
    }

    private string GetAge(DateTime dob, string sex)
    {
        string age = "";
        string displaySex = "~";
        patientMonths = monthDifference(dob, DateTime.Now);

        int iYear, iMonth, iWeek;

        iYear = patientMonths / 12;
        iMonth = patientMonths % 12;
        TimeSpan ts = DateTime.Now - dob;
        iWeek = ts.Days / 7;

        if (patientMonths <= maxBaby)
        {
            //for babies we show weeks
            displaySex += "gestation";
            age = iWeek.ToString() + " weeks " + displaySex;
        }
        else if (patientMonths < maxBoyGirlDecimal)
        {
            //boy or girl

            if (sex.ToLower() == female)
                displaySex += "girl";
            else if (sex.ToLower() == male)
                displaySex += "boy";


            age = iYear.ToString() + "." + iMonth + " year-old " + displaySex;
        }
        else if (patientMonths < maxBoyGirl)
        {
            //boy or girl

            if (sex.ToLower() == female)
                displaySex += "girl";
            else if (sex.ToLower() == male)
                displaySex += "boy";


            age = iYear.ToString() + " year-old " + displaySex;
        }
        else if (patientMonths < maxYoung)
        {
            //young man/lady

            if (sex.ToLower() == female)
                displaySex += "young lady";
            else if (sex.ToLower() == male)
                displaySex += "young man";

            age = iYear.ToString() + " year-old " + displaySex;
        }
        else
        {

            if (sex.ToLower() == female)
                displaySex += "lady";
            else if (sex.ToLower() == male)
                displaySex += "gentleman";

            age = iYear.ToString() + " year-old " + displaySex;
        }

        return age;
    }

    private void SetBackgroundColours(int type)
    {
        if (type == 1)
        {
            foreach (Control ctrl in pageControls)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        TextBox tb = (TextBox)ctrl;
                        if (tb.Text != "" && tb.ReadOnly == false)
                            tb.Attributes.Add("CustomColourType", "1");
                        else
                            tb.Attributes.Add("CustomColourType", "0");
                        break;
                    case "DropDownList":
                        DropDownList ddl = (DropDownList)ctrl;
                        if(ddl.SelectedIndex > 0 && ddl.SelectedValue != "OU")
                            ddl.Attributes.Add("CustomColourType", "1");
                        else
                            ddl.Attributes.Add("CustomColourType", "0");
                        break;
                }
            }
        }
    }

    public string GetSummaryJQuery()
    {
        if (Request.QueryString["Print"] != null)
            return "";
        else
            return @"$(function () { $tabs.bind(""tabsshow"", SetSummary); });";
    }

    [WebMethod]
    public static string GetAutoCorrections(string doctorUserName)
    {
        StringBuilder sb = new StringBuilder();

        SqlDataReader dr = DBUtil.ExecuteReader("SELECT Name, Value FROM AutoCorrect where username = '" + doctorUserName + "'");

        while (dr.Read())
        {
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append(@"""" + dr["Name"].ToString() + @""": """ + dr["Value"] + @"""");
        }
        dr.Close();
        dr.Dispose();

        return "{ " + sb.ToString() + " }";
    }

}

public enum ExamNotesType
{
    New = 1,
    Correct = 2,
    Saved = 3,
    Default = 4
}
