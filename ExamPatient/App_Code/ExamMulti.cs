using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace Exam
{
    public class ExamMulti : Control, INamingContainer
    {

        #region VARIABLES

        private TextBox txtMonth = null;
        private TextBox txtDay = null;
        private TextBox txtYear = null;
        private TextBox txtExchange = null;
        private TextBox txtArea = null;
        private TextBox txtPhone = null;
        private TextBox txtZip = null;
        private TextBox txtCode = null;
        private TextBox txtEmail = null;
        private TextBox txtHour = null;
        private TextBox txtMinute = null;
        private DropDownList cbTime = null;
        private TextBox txtssnArea = null;
        private TextBox txtGroup = null;
        private TextBox txtSerial = null;
        private string _clientValidationFunction1 = string.Empty;
        private string _clientValidationFunction2 = string.Empty;
        private string _clientValidationFunction3 = string.Empty;
        private string _clientValidationFunction = string.Empty;
        private string ValidationExpression = string.Empty;
        private string _getValue = string.Empty;
        private string _valueAssigned = string.Empty;
        private const string SLASH = "/";
        private const string COLON = ":";
        private const string EIPHEN = "-";
        private const string UNDERSCORE = "_";
        private const string OPENBBRACE = "(";
        private const string CLOSEBRACE = ")";
        private const int TXT_MONTH_WIDTH = 18;
        private const int TXT_DAY_WIDTH = 18;
        private const int TXT_YEAR_WIDTH = 30;
        private const int TXT_AREA_WIDTH = 22;
        private const int TXT_EXCHANGE_WIDTH = 22;
        private const int TXT_PHONE_WIDTH = 28;
        private const int TXT_ZIP_WIDTH = 34;
        private const int TXT_CODE_WIDTH = 30;
        private const int TXT_HOUR_WIDTH = 18;
        private const int TXT_MINUTE_WIDTH = 18;
        private const int TXT_SSNAREA_WIDTH = 22;
        private const int TXT_GROUP_WIDTH = 16;
        private const int TXT_SERIAL_WIDTH = 28;
        private const int TXT_MONTH_MAXLENGTH = 2;
        private const int TXT_DAY_MAXLENGTH = 2;
        private const int TXT_YEAR_MAXLENGTH = 4;
        private const int TXT_AREA_MAXLENGTH = 3;
        private const int TXT_EXCHANGE_MAXLENGTH = 3;
        private const int TXT_PHONE_MAXLENGTH = 4;
        private const int TXT_ZIP_MAXLENGTH = 5;
        private const int TXT_CODE_MAXLENGTH = 4;
        private const int TXT_HOUR_MAXLENGTH = 2;
        private const int TXT_MINUTE_MAXLENGTH = 2;
        private const int TXT_SSNAREA_MAXLENGTH = 3;
        private const int TXT_GROUP_MAXLENGTH = 2;
        private const int TXT_SERIAL_MAXLENGTH = 4;
        private const string CBO_TIME_AM = "AM";
        private const string CBO_TIME_PM = "PM";
        private readonly string FIELD_REQUIRED = "{0}";
        private readonly string INVALID_FORMAT = "{0}";
        private const string TITLE = ".";
        private bool _enableValidators = true;
        private bool _showValidators = true;

        #endregion

        #region ENUM
        public enum ControlTypes
        {
            Date,
            PhoneNumber,
            Zip,
            Email,
            Time,
            SSN
        }
        #endregion

        #region PROPERTIES
        public string ErrorText { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadOnly { get; set; }
        public int EmailMaxLength { get; set; }
        public bool SetDefaultFocus { get; set; }
        private bool _isControlEnable = true;

        public bool IsControlEnable
        {
            get { return _isControlEnable; }
            set { _isControlEnable = value; }
        }

        public bool EnableValidators
        {
            set
            {
                _enableValidators = value;

            }
        }
        public bool ShowValidators
        {
            set
            {
                _showValidators = value;

            }
        }
        public string ValidationGroup { get; set; }
        public string ErrorMessage { get; set; }
        public ControlTypes ControlType { get; set; }
        public string Text
        {

            get
            {
                EnsureChildControls();
                switch (ControlType)
                {
                    case ControlTypes.Date:
                        if (txtMonth.Text == "" && txtDay.Text == "" && txtYear.Text == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = txtMonth.Text + SLASH + txtDay.Text + SLASH + txtYear.Text;
                        }
                        break;
                    case ControlTypes.PhoneNumber:
                        if (txtArea.Text == "" && txtExchange.Text == "" && txtPhone.Text == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = txtArea.Text + EIPHEN + txtExchange.Text + EIPHEN + txtPhone.Text;
                        }
                        break;
                    case ControlTypes.Zip:
                        if (txtZip.Text == "" && txtCode.Text == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = txtZip.Text + EIPHEN + txtCode.Text;
                        }
                        break;
                    case ControlTypes.Email:

                        _getValue = txtEmail.Text;
                        break;
                    case ControlTypes.Time:
                        if (txtHour.Text == "" && txtMinute.Text == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = txtHour.Text + COLON + txtMinute.Text + " " + cbTime.SelectedItem;
                        }
                        break;
                    case ControlTypes.SSN:
                        if (txtssnArea.Text == "" && txtGroup.Text == "" && txtSerial.Text == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = txtssnArea.Text + EIPHEN + txtGroup.Text + EIPHEN + txtSerial.Text;
                        }

                        break;

                }
                return _getValue;

            }
            set
            {

                AssignValue(value, ControlType);

            }
        }

        //Obout_0.6.0.10
        public string RequestText
        {
            get
            {
                EnsureChildControls();
                switch (ControlType)
                {
                    case ControlTypes.Date:
                        if (HttpContext.Current.Request[txtMonth.UniqueID] == "" && HttpContext.Current.Request[txtDay.UniqueID] == "" && HttpContext.Current.Request[txtYear.UniqueID] == "")
                        {
                            _getValue = "";
                        }
                        else
                        {
                            _getValue = HttpContext.Current.Request[txtMonth.UniqueID] + SLASH + HttpContext.Current.Request[txtDay.UniqueID] + SLASH + HttpContext.Current.Request[txtYear.UniqueID];
                        }
                        break;
                }
                return _getValue;
            }
        }

        #endregion

        public ExamMulti()
        {
            FIELD_REQUIRED = "{0} is Required"; //adc.GetErrorCodes(ErrorCodeEnum.Mandatory, FIELD_REQUIRED).ErrorMessage;
            INVALID_FORMAT = "{0} is Invalid Format"; //adc.GetErrorCodes(ErrorCodeEnum.Invalid, INVALID_FORMAT).ErrorMessage;
        }
        #region METHODS

        private void AssignValue(string ValueToSet, ControlTypes ControlToAssign)
        {
            EnsureChildControls();
            string[] strSplit = null;


            switch (ControlToAssign)
            {
                case ControlTypes.Date:
                    if (ValueToSet == "")
                    {
                        txtMonth.Text = "";
                        txtDay.Text = "";
                        txtYear.Text = "";
                    }
                    else
                    {
                        strSplit = ValueToSet.Split(Convert.ToChar(SLASH));
                        if (strSplit[0].Length == 1)
                        {
                            txtMonth.Text = "0" + strSplit[0];
                        }
                        else
                        {
                            txtMonth.Text = strSplit[0];
                        }
                        if (strSplit[1].Length == 1)
                        {
                            txtDay.Text = "0" + strSplit[1];
                        }
                        else
                        {
                            txtDay.Text = strSplit[1];
                        }
                        txtYear.Text = strSplit[2];
                    }
                    break;
                case ControlTypes.PhoneNumber:
                    if (ValueToSet == "")
                    {
                        txtArea.Text = "";
                        txtExchange.Text = "";
                        txtPhone.Text = "";
                    }
                    else
                    {
                        strSplit = ValueToSet.Split(Convert.ToChar(EIPHEN));
                        txtArea.Text = strSplit[0];
                        txtExchange.Text = strSplit[1];
                        txtPhone.Text = strSplit[2];
                    }
                    break;
                case ControlTypes.Zip:
                    if (ValueToSet == "")
                    {
                        txtZip.Text = "";
                        txtCode.Text = "";
                    }
                    else
                    {
                        strSplit = ValueToSet.Split(Convert.ToChar(EIPHEN));
                        txtZip.Text = strSplit[0];
                        txtCode.Text = strSplit[1];
                    }
                    break;
                case ControlTypes.Email:
                    if (ValueToSet == "")
                    {
                        txtEmail.Text = "";
                    }
                    else
                    {
                        txtEmail.Text = ValueToSet;
                    }
                    break;
                case ControlTypes.Time:
                    if (ValueToSet == "")
                    {
                        txtHour.Text = "";
                        txtMinute.Text = "";
                        cbTime.SelectedIndex = 0;
                    }
                    else
                    {
                        strSplit = ValueToSet.Split(Convert.ToChar(COLON));
                        if (txtHour != null)
                        {
                            txtHour.Text = strSplit[0];
                            txtMinute.Text = strSplit[1].Split(' ')[0];
                            if (strSplit[1].Split(' ')[1] == "AM")
                            {
                                cbTime.SelectedIndex = 0;
                            }
                            else if (strSplit[1].Split(' ')[1] == "PM")
                            {
                                cbTime.SelectedIndex = 1;
                            }
                        }
                    }

                    break;
                case ControlTypes.SSN:
                    if (ValueToSet == "")
                    {
                        txtssnArea.Text = "";
                        txtGroup.Text = "";
                        txtSerial.Text = "";
                    }
                    else
                    {
                        strSplit = ValueToSet.Split(Convert.ToChar(EIPHEN));
                        txtssnArea.Text = strSplit[0];
                        txtGroup.Text = strSplit[1];
                        txtSerial.Text = strSplit[2];
                    }
                    break;
            }
        }
        private Image GetImage(string errorMessage, string Append)
        {
            Image img = new Image();
            img.ID = this.ClientID + Append;
            img.SkinID = "imgRequired";
            img.ToolTip = errorMessage;
            return img;
        }
        /// </summary>
        /// <param name="ControlToValidate"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        private RequiredFieldValidator GetRequiredFieldValidator(string controlToValidate, string errorMessage)
        {
            string displayMessage = string.Format(FIELD_REQUIRED, ErrorText);
            RequiredFieldValidator reqFiled = new RequiredFieldValidator();
            reqFiled.Controls.AddAt(0, GetImage(displayMessage, "reqIMG"));
            reqFiled.ID = controlToValidate + "req";
            reqFiled.ControlToValidate = controlToValidate;
            reqFiled.ToolTip = string.Format(displayMessage, ErrorText);
            reqFiled.ErrorMessage = string.Format(displayMessage, ErrorText);
            //reqFiled.SetFocusOnError = true;
            reqFiled.ValidationGroup = ValidationGroup;

            if (!_enableValidators)
            {
                reqFiled.Enabled = false;

            }
            reqFiled.Visible = _showValidators;
            return reqFiled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ControlToValidate"></param>
        /// <param name="ValidationHandler"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        private CustomValidator GetCustomValidator(string controlToValidate, string clientValidationFunction, string errorMessage, bool showValidator = true)
        {
            string displayMessage = string.Format(INVALID_FORMAT, ErrorText);
            CustomValidator customValidator = new CustomValidator();
            string ctrlType = string.Empty;
            customValidator.ClientValidationFunction = clientValidationFunction;
            if (this.ClientIDMode == ClientIDMode.AutoID)
            {
                customValidator.Attributes.Add("classname", this.ClientID + UNDERSCORE + this.ClientID);
            }
            else
            {
                customValidator.Attributes.Add("classname", this.ClientID);
            }
            if (controlToValidate.Equals("") == false)
            {

                customValidator.ControlToValidate = controlToValidate;
                customValidator.ID = controlToValidate + "cust";

            }
            else
            {

                ctrlType = ControlType.ToString().Substring(0, 1);
                customValidator.ID = this.ClientID + ctrlType + "cust";
                //customValidator.Controls.AddAt(0, GetImage(FIELD_REQUIRED, "reqIMG"));

            }
            customValidator.ToolTip = displayMessage;
            customValidator.ErrorMessage = displayMessage;
            customValidator.ValidationGroup = ValidationGroup;
            //customValidator.SetFocusOnError = true;
            customValidator.Controls.AddAt(0, new LiteralControl("&nbsp;"));
            if (!showValidator)
            {
                customValidator.Visible = false;
            }

            if (!_enableValidators)
            {
                customValidator.Enabled = false;

            }
            customValidator.Visible = _showValidators;
            return customValidator;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void CreateChildControls()
        {
            switch (ControlType)
            {
                case ControlTypes.Date:
                    txtMonth = new TextBox();
                    txtMonth.ID = this.ClientID + UNDERSCORE + "txtMonth";
                    txtDay = new TextBox();
                    txtDay.ID = this.ClientID + UNDERSCORE + "txtDay";
                    txtYear = new TextBox();
                    txtYear.ID = this.ClientID + UNDERSCORE + "txtYear";
                    _clientValidationFunction1 = "ValidateDate";
                    _clientValidationFunction = "ValidateTotalDate";
                    txtMonth.Width = Unit.Pixel(TXT_MONTH_WIDTH);
                    txtDay.Width = Unit.Pixel(TXT_DAY_WIDTH);
                    txtYear.Width = Unit.Pixel(TXT_YEAR_WIDTH);
                    txtMonth.MaxLength = TXT_MONTH_MAXLENGTH;
                    txtDay.MaxLength = TXT_DAY_MAXLENGTH;
                    txtYear.MaxLength = TXT_YEAR_MAXLENGTH;
                    txtMonth.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtDay.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtYear.Attributes.Add("onKeyPress", "return allowNumeric();");
                    string monthID = string.Empty;
                    string dayID = string.Empty;
                    string yearID = string.Empty;
                    monthID = GetControlID(this.ClientIDMode, txtMonth.ClientID,this.ClientID);
                    dayID = GetControlID(this.ClientIDMode, txtDay.ClientID, this.ClientID);
                    yearID = GetControlID(this.ClientIDMode, txtYear.ClientID, this.ClientID); 
                    
                    txtMonth.Attributes.Add("onKeyUp", "return checkDateValidation('" + monthID + "',this,2,event);");
                    txtDay.Attributes.Add("onKeyUp", "return checkDateValidation('" + dayID + "',this, 2, event,'" + monthID + "');");
                    txtYear.Attributes.Add("onKeyUp", "return checkDateValidation('" + yearID + "',this, 4, event);");

                    #region DatePicker

                    string datavalueID = string.Empty;
                    if (this.ClientIDMode == ClientIDMode.AutoID)
                    {
                        datavalueID = this.ClientID + UNDERSCORE + this.ClientID;
                    }
                    else
                    {
                        datavalueID = this.ClientID;
                    }

                    txtYear.Attributes.Add("onfocus", "return AssignNewDate('" + datavalueID + "');");
                    txtYear.Attributes.Add("onchange", "return AssignDateVal('" + datavalueID + "');");

                    #endregion
                    txtMonth.CssClass = "txtBorder";
                    txtDay.CssClass = "txtBorder";
                    txtYear.CssClass = "txtBorder";
                    Controls.Add(new LiteralControl("<div class='divDateBorder'>"));
                    Controls.Add(txtMonth);
                    Controls.Add(new LiteralControl(SLASH));
                    Controls.Add(txtDay);
                    Controls.Add(new LiteralControl(SLASH));
                    Controls.Add(txtYear);

                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td' id='" + txtYear.ID + "'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtYear.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtYear.ID, _clientValidationFunction1, ErrorMessage));
                    if (!IsRequired)
                    {
                        Controls.Add(GetCustomValidator("", _clientValidationFunction, ErrorMessage));
                    }
                    Controls.Add(new LiteralControl("</div>"));
                    break;

                case ControlTypes.PhoneNumber:

                    txtArea = new TextBox();
                    txtArea.ID = this.ClientID + UNDERSCORE + "txtArea";
                    txtExchange = new TextBox();
                    txtExchange.ID = this.ClientID + UNDERSCORE + "txtExchange";
                    txtPhone = new TextBox();
                    txtPhone.ID = this.ClientID + UNDERSCORE + "txtPhone";
                    //txtArea.Attributes.Add("title", TITLE);
                    //txtExchange.Attributes.Add("title", TITLE);
                    //txtPhone.Attributes.Add("title", TITLE);
                    _clientValidationFunction1 = "ValidatePhone";
                    _clientValidationFunction = "ValidateTotalPhone";
                    txtArea.Width = TXT_AREA_WIDTH;
                    txtExchange.Width = TXT_EXCHANGE_WIDTH;
                    txtPhone.Width = TXT_PHONE_WIDTH;
                    txtArea.MaxLength = TXT_AREA_MAXLENGTH;
                    txtExchange.MaxLength = TXT_EXCHANGE_MAXLENGTH;
                    txtPhone.MaxLength = TXT_PHONE_MAXLENGTH;
                    txtArea.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtExchange.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtPhone.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtArea.Attributes.Add("onKeyUp", "return autoTab(this, 3, event);");
                    txtExchange.Attributes.Add("onKeyUp", "return autoTab(this, 3, event);");
                    txtPhone.Attributes.Add("onKeyUp", "return autoTab(this, 4, event);");
                    txtArea.CssClass = "txtBorder";
                    txtExchange.CssClass = "txtBorder";
                    txtPhone.CssClass = "txtBorder";
                    Controls.Add(new LiteralControl("<div class='divPhoneBorder'>"));
                    Controls.Add(new LiteralControl(OPENBBRACE));
                    Controls.Add(txtArea);
                    Controls.Add(new LiteralControl(CLOSEBRACE));
                    Controls.Add(txtExchange);
                    Controls.Add(new LiteralControl(EIPHEN));
                    Controls.Add(txtPhone);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtPhone.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtPhone.ID, _clientValidationFunction1, ErrorMessage));
                    if (!IsRequired)
                    {
                        Controls.Add(GetCustomValidator("", _clientValidationFunction, ErrorMessage));
                    }
                    Controls.Add(new LiteralControl("</div>"));
                    break;

                case ControlTypes.Zip:
                    txtZip = new TextBox();
                    txtZip.ID = this.ClientID + UNDERSCORE + "txtZip";
                    txtCode = new TextBox();
                    txtCode.ID = this.ClientID + UNDERSCORE + "txtCode";
                    _clientValidationFunction1 = "ValidateZip";
                    _clientValidationFunction = "ValidateTotalZip";
                    txtZip.Width = TXT_ZIP_WIDTH;
                    txtCode.Width = TXT_CODE_WIDTH;
                    txtZip.MaxLength = TXT_ZIP_MAXLENGTH;
                    txtCode.MaxLength = TXT_CODE_MAXLENGTH;
                    txtZip.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtCode.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtZip.Attributes.Add("onKeyUp", "return autoTab(this, 5, event);");
                    txtCode.Attributes.Add("onKeyUp", "return autoTab(this, 4, event);");
                    txtZip.CssClass = "txtBorder";
                    txtCode.CssClass = "txtBorder";
                    //txtCode.Attributes.Add("title", TITLE);
                    //txtZip.Attributes.Add("title", TITLE);

                    Controls.Add(new LiteralControl("<div class='divZipBorder'>"));
                    Controls.Add(txtZip);
                    Controls.Add(new LiteralControl(EIPHEN));
                    Controls.Add(txtCode);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtZip.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtZip.ID, _clientValidationFunction1, ErrorMessage));
                    if (!IsRequired)
                    {
                        Controls.Add(GetCustomValidator("", _clientValidationFunction, ErrorMessage));
                    }
                    Controls.Add(new LiteralControl("</div>"));
                    break;

                case ControlTypes.Email:
                    txtEmail = new TextBox();
                    txtEmail.ID = this.ClientID + UNDERSCORE + "txtEmail";
                    _clientValidationFunction1 = "ValidateEmail";
                    txtEmail.CssClass = "divBorder";
                    txtEmail.MaxLength = EmailMaxLength;
                    //txtEmail.Attributes.Add("title", TITLE);

                    Controls.Add(txtEmail);
                    Controls.Add(new LiteralControl("<div class='td'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtEmail.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtEmail.ID, _clientValidationFunction1, ErrorMessage));
                    Controls.Add(new LiteralControl("</div>"));
                    break;

                case ControlTypes.Time:
                    txtHour = new TextBox();
                    txtHour.ID = this.ClientID + UNDERSCORE + "txtHour";
                    txtMinute = new TextBox();
                    txtMinute.ID = this.ClientID + UNDERSCORE + "txtMinute";
                    cbTime = new DropDownList();
                    cbTime.ID = this.ClientID + UNDERSCORE + "cbTime";
                    ListItem licbData = new ListItem(CBO_TIME_AM);
                    licbData.Selected = true;
                    ListItem licbData1 = new ListItem(CBO_TIME_PM);
                    cbTime.Items.Add(licbData);
                    cbTime.Items.Add(licbData1);
                    _clientValidationFunction1 = "ValidateTime";
                    _clientValidationFunction = "ValidateTotalTime";
                    txtHour.Width = TXT_HOUR_WIDTH;
                    txtMinute.Width = TXT_MINUTE_WIDTH;
                    txtHour.MaxLength = TXT_HOUR_MAXLENGTH;
                    txtMinute.MaxLength = TXT_MINUTE_MAXLENGTH;
                    txtHour.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtMinute.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtHour.Attributes.Add("onKeyUp", "return autoTab(this, 2, event);");
                    txtMinute.Attributes.Add("onKeyUp", "return autoTab(this, 2, event);");
                    txtHour.CssClass = "txtBorder";
                    txtMinute.CssClass = "txtBorder";
                    //txtHour.Attributes.Add("title", TITLE);
                    //txtMinute.Attributes.Add("title", TITLE);
                    Controls.Add(new LiteralControl("<div class='divTimeBorder'>"));
                    Controls.Add(txtHour);
                    Controls.Add(new LiteralControl(COLON));
                    Controls.Add(txtMinute);

                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td' style='padding-left:5px;'>"));
                    Controls.Add(cbTime);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtMinute.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtMinute.ID, _clientValidationFunction1, ErrorMessage));
                    if (!IsRequired)
                    {
                        Controls.Add(GetCustomValidator("", _clientValidationFunction, ErrorMessage));
                    }
                    Controls.Add(new LiteralControl("</div>"));
                    break;

                case ControlTypes.SSN:

                    txtssnArea = new TextBox();
                    txtssnArea.ID = this.ClientID + UNDERSCORE + "txtssnArea";
                    txtGroup = new TextBox();
                    txtGroup.ID = this.ClientID + UNDERSCORE + "txtGroup";
                    txtSerial = new TextBox();
                    txtSerial.ID = this.ClientID + UNDERSCORE + "txtSerial";
                    _clientValidationFunction1 = "ValidateSSN";
                    _clientValidationFunction = "ValidateTotalSSN";
                    txtssnArea.Width = TXT_SSNAREA_WIDTH;
                    txtGroup.Width = TXT_GROUP_WIDTH;
                    txtSerial.Width = TXT_SERIAL_WIDTH;
                    txtssnArea.MaxLength = TXT_SSNAREA_MAXLENGTH;
                    txtGroup.MaxLength = TXT_GROUP_MAXLENGTH;
                    txtSerial.MaxLength = TXT_SERIAL_MAXLENGTH;
                    txtssnArea.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtGroup.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtSerial.Attributes.Add("onKeyPress", "return allowNumeric();");
                    txtssnArea.Attributes.Add("onKeyUp", "return autoTab(this, 3, event);");
                    txtGroup.Attributes.Add("onKeyUp", "return autoTab(this, 2, event);");
                    txtSerial.Attributes.Add("onKeyUp", "return autoTab(this, 4, event);");
                    txtssnArea.CssClass = "txtBorder";
                    txtGroup.CssClass = "txtBorder";
                    txtSerial.CssClass = "txtBorder";
                    //txtssnArea.Attributes.Add("title", TITLE);
                    //txtGroup.Attributes.Add("title", TITLE);
                    //txtSerial.Attributes.Add("title", TITLE);

                    Controls.Add(new LiteralControl("<div class='divSSNBorder'>"));
                    Controls.Add(txtssnArea);
                    Controls.Add(new LiteralControl(EIPHEN));
                    Controls.Add(txtGroup);
                    Controls.Add(new LiteralControl(EIPHEN));
                    Controls.Add(txtSerial);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class='td'>"));
                    if (IsRequired)
                    {
                        Controls.Add(GetRequiredFieldValidator(txtSerial.ID, ErrorMessage));
                    }
                    Controls.Add(GetCustomValidator(txtSerial.ID, _clientValidationFunction1, ErrorMessage));
                    if (!IsRequired)
                    {
                        Controls.Add(GetCustomValidator("", _clientValidationFunction, ErrorMessage));
                    }
                    Controls.Add(new LiteralControl("</div>"));
                    break;
            }
        }

        private string GetControlID(System.Web.UI.ClientIDMode clientIDMode, string controlID,string thisClientID)
        {
            if (clientIDMode == ClientIDMode.AutoID)
            {
                return thisClientID + UNDERSCORE + controlID;
            }
            else
            {
                return controlID;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            switch (ControlType)
            {
                case ControlTypes.Date:

                    txtMonth.ReadOnly = IsReadOnly;
                    txtDay.ReadOnly = IsReadOnly;
                    txtYear.ReadOnly = IsReadOnly;
                    txtMonth.Enabled = IsControlEnable;
                    txtDay.Enabled = IsControlEnable;
                    txtYear.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        txtMonth.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtDay.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtYear.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    string vpPath = VirtualPathUtility.GetDirectory("~");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), this.ClientID, "$(document).ready(function () {ProcessExecute('" + txtYear.ClientID + "', '" + vpPath + "');});", true);
                    break;
                case ControlTypes.PhoneNumber:
                    txtArea.ReadOnly = IsReadOnly;
                    txtExchange.ReadOnly = IsReadOnly;
                    txtPhone.ReadOnly = IsReadOnly;
                    txtArea.Enabled = IsControlEnable;
                    txtExchange.Enabled = IsControlEnable;
                    txtPhone.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        txtArea.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtExchange.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtPhone.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    break;
                case ControlTypes.Zip:
                    txtZip.ReadOnly = IsReadOnly;
                    txtCode.ReadOnly = IsReadOnly;
                    txtZip.Enabled = IsControlEnable;
                    txtCode.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        txtZip.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtCode.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    break;
                case ControlTypes.Email:
                    txtEmail.ReadOnly = IsReadOnly;
                    txtEmail.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        txtEmail.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    break;
                case ControlTypes.Time:
                    txtHour.ReadOnly = IsReadOnly;
                    txtMinute.ReadOnly = IsReadOnly;
                    txtHour.Enabled = IsControlEnable;
                    txtMinute.Enabled = IsControlEnable;
                    cbTime.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        cbTime.Enabled = false;
                        txtHour.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtMinute.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    else if (IsReadOnly == false)
                    {
                        cbTime.Enabled = true;
                    }
                    break;
                case ControlTypes.SSN:
                    txtssnArea.ReadOnly = IsReadOnly;
                    txtGroup.ReadOnly = IsReadOnly;
                    txtSerial.ReadOnly = IsReadOnly;
                    txtssnArea.Enabled = IsControlEnable;
                    txtGroup.Enabled = IsControlEnable;
                    txtSerial.Enabled = IsControlEnable;
                    if (IsReadOnly == true)
                    {
                        txtssnArea.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtGroup.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                        txtSerial.Attributes.Add("onKeyDown", "return readOnlyKeyDownFn();");
                    }
                    break;
            }
            base.Render(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            switch (ControlType)
            {
                case ControlTypes.Date:
                    if (SetDefaultFocus == true)
                    {
                        txtMonth.Focus();
                    }
                    break;
                case ControlTypes.PhoneNumber:
                    if (SetDefaultFocus == true)
                    {
                        txtArea.Focus();
                    }
                    break;
                case ControlTypes.Zip:
                    if (SetDefaultFocus == true)
                    {
                        txtZip.Focus();
                    }
                    break;
                case ControlTypes.Email:
                    if (SetDefaultFocus == true)
                    {
                        txtEmail.Focus();
                    }
                    break;
                case ControlTypes.Time:
                    if (SetDefaultFocus == true)
                    {
                        txtHour.Focus();
                    }
                    break;
                case ControlTypes.SSN:
                    if (SetDefaultFocus == true)
                    {
                        txtssnArea.Focus();
                    }
                    break;

            }
            base.OnPreRender(e);
        }
        #endregion

    }
}
