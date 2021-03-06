﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamPatient.aspx.cs" Inherits="ExamPatient" Title="Exam Patient" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function MoveTab(tabName) {
            $tabs.tabs('select', tabName);
            return false;
        }

        $(function () {
            $('input[type="text"], textarea, select').each(function () {
                if ($(this).attr("CustomColourType") == "1")
                    $(this).addClass("focusctrl");
                if ($(this).attr("CustomColourType") == "2")
                    $(this).addClass("correctctrl");
            });

            $('input[type="text"], textarea, select').focus(function () {
                if ($(this).attr("CustomColourType") == "1") {
                    $(this).removeClass("focusctrl");
                    var ColourType = $("#hdnColourType").val() + ',' + $(this).attr("id");
                    $("#hdnColourType").val(ColourType);
                    dirty = true;
                }
            });

            $('input[type="text"], textarea, select').change(function () {
                dirty = true;
            });

            $('input[type="checkbox"]').click(function () {
                dirty = true;
            });

            //showing premature details as needed
            SetPremature();

            var doctorUserName = $('#User').val()
            $.webMethod({ 'methodName': 'GetAutoCorrections', 'parameters': { 'doctorUserName': doctorUserName },
                success: function (value) {
                    var correctValues = jQuery.parseJSON(value);
                    SetAutoCorrectValues(correctValues);
                }
            });

        });

        function SetAutoCorrectValues(correctValues) {
            $('input[type="text"], textarea, select').each(function () {
                $(this).autocorrect({ corrections: correctValues });

            });
        }

        function SetSummary()
        {
            var tabindex = $tabs.tabs('option', 'selected');
            if(tabindex == 4) //summary tab
            {
                var summarytext = $('#Summary').val();
                var GAtext = $('#GA').val();
                var PCAtext= $('#PCA').val();
                var BirthWttext= $('#BirthWt').val();

                if(GAtext != "weeks")
                    summarytext = summarytext.replace("[GA]", GAtext);
                if(PCAtext != "weeks")
                summarytext = summarytext.replace("[PCA]", PCAtext);
                if(BirthWttext != "")
                    summarytext = summarytext.replace("[BW]", BirthWttext);
                $('#Summary').val(summarytext);
                //$('#Summary').addClass("focusctrl");
            }
        }

        var dirty = false;
        var forceDirty = false;
        window.onbeforeunload = function() {   
            if (dirty === true && forceDirty === false) {
                return 'You have made changes on this page that you have not yet confirmed. If you navigate away from this page you will loose your unsaved changes';  
            }
        }

        function ClearDirty()
        {
            dirty = false;
            return true;
        }

        function ValidateNotes()
        {
            var ret = CheckNoPref();

            if(ret)
            {
                if(($('#SLE').attr('checked') || $('#PenLight').attr('checked')) && ($('#Dilate3').val() != ''))
                    ret = ClearDirty();
                else{
                    alert('SLE/Pen-light options and dilated options are required');
                    ret = false;
                }
            }

            return ret;
        }

        function SetRxGiven() {
            
            if($('#PopulateRxGiven').attr('checked'))
            {
                var manod = $('#ManRfxOD1').val();
                var cycod = $('#CycRfxOD').val();

                if (cycod != '')
                    $('#RxOD1').val(cycod);
                else
                    $('#RxOD1').val(manod);

                var manos = $('#ManRfxOS1').val();
                var cycos = $('#CycRfxOS').val();

                if (cycos != '')
                    $('#RXOS1').val(cycos);
                else
                    $('#RXOS1').val(manos);

                var manod2 = $('#ManRfxOD2').val();
                $('#RxOD2').val(manod2);
                var manos2 = $('#ManRfxOS2').val();
                $('#RXOS2').val(manos2);
            }
            if($('#PopulateCtlRx').attr('checked'))
            {
                var manod = $('#ManRfxOD1').val();
                var cycod = $('#CycRfxOD').val();

                if (cycod != '')
                    $('#CTLRxOD1').val(cycod);
                else
                    $('#CTLRxOD1').val(manod);

                var manos = $('#ManRfxOS1').val();
                var cycos = $('#CycRfxOS').val();

                if (cycos != '')
                    $('#CTLRxOS1').val(cycos);
                else
                    $('#CTLRxOS1').val(manos);

                var manod2 = $('#ManRfxOD2').val();
                $('#CTLRxOD2').val(manod2);
                var manos2 = $('#ManRfxOS2').val();
                $('#CTLRxOS2').val(manos2);
            }
        }

        function SetPremature() {
            var bhx = $("#BirthHist1").val();

            if (bhx == "Premature"){
                $("#hdntr1").show();
                $("#hdntr2").show();
                $("#hdntr3").show();
            }
            else{
                $("#hdntr1").hide();
                $("#hdntr2").hide();
                $("#hdntr3").hide();
            }
        }

        function SetCopyTo()
        {
            var refd = $('#Refd').val();
            var refDoctor = $('#RefDoctor').val();

            var copyTo = refd;

            if(refd != refDoctor)
            {
                copyTo += ', ' + refDoctor
            }
    
            $('#CopyTo').val(copyTo);
                
        }

        function CheckNoPref()
        {
            var noprefchecked = $("#NoPref").attr('checked');
            if(noprefchecked)
            {
                var od = $('#VAscOD1').val() + ' ' + $('#VAscOD2').val()
                var os = $('#DistOS1').val() + ' ' + $('#DistOS2').val()

                if(od != os)
                {
                    alert('VA sc Dist OD and OS should be equal when No Pref checkbox is checked')
                    return false;
                }
            }

            return true;
        }

        function AcceptDefaults(divid)
        {
            $('input[type="text"], textarea, select', '#' + divid).each(function () {
                if ($(this).attr("CustomColourType") == "1") {
                    $(this).removeClass("focusctrl");
                    var ColourType = $("#hdnColourType").val() + ',' + $(this).attr("id");
                    $("#hdnColourType").val(ColourType);
                    dirty = true;
                }
            });
        }

        function EditPatient()
        {
            var url = 'Patient.aspx?PatientID=' + $("#hdnPatientID").val() + '&Popup=1'; 
            window.showModalDialog(url, window, "dialogWidth:520px; dialogHeight:500px; center:yes; scroll:no"); 
        }

        function GetDoctorDefaults()
        {
            var previousDoctorUserName = '<% = Request.QueryString["DoctorUserName"] %>';
            var selectedDoctor = $("#User option:selected").text(); 
            var defaultInd = $('#hdnDefaultInd').val();
            var doctorUserName = $('#User').val()
            if(defaultInd == "1")
            {
                if(confirm('Do you want to loose your changes and load defaults for \n' + selectedDoctor))
                {
                    dirty = false;
                    var url = location.href.replace('&DoctorUserName=' + previousDoctorUserName, '');
                    location.href = url + '&DoctorUserName=' + doctorUserName;
                    return;
                }
            }

            //getting the auto correct values
            $.webMethod({ 'methodName': 'GetAutoCorrections', 'parameters': { 'doctorUserName': doctorUserName },
                success: function (value) {
                    var correctValues = jQuery.parseJSON(value);
                    SetAutoCorrectValues(correctValues);
                }
            });
            
        }

        function GetUserDefaults() {
            var previousUserDefaultID = '<% = Request.QueryString["UserDefaultID"] %>';
            var userDefaultText = $("#ddlUserDefaults option:selected").text();
            var userDefaultID = $('#ddlUserDefaults').val();
            var confirmationMessage = 'Do you want to loose your changes and load defaults for ' + userDefaultText;
            if (userDefaultID === "0") {
                confirmationMessage = 'Do you want to loose your changes and re-load notes';
                userDefaultID = "";
            }

            if (userDefaultID != "0") {
                if (confirm(confirmationMessage)) {
                    dirty = false;
                    forceDirty = true;
                    var url = location.href.replace('&UserDefaultID=' + previousUserDefaultID, '');
                    location.href = url + '&UserDefaultID=' + userDefaultID;
                    return false;
                }
                else {
                    $('#ddlUserDefaults').val(previousUserDefaultID);
                }
            }
            else {
            }
        }

        function PopulateTicTacTo() {
            var ocMotDefault = $("#OcMotDefault").val();
            if (ocMotDefault === '') {
                alert('Please enter a default value');
                return false;
            }

            $("#OcMot1a").val(ocMotDefault);
            $("#OcMot2a").val(ocMotDefault);
            $("#OcMot3a").val(ocMotDefault);
            $("#OcMot4a").val(ocMotDefault);
            $("#OcMot5a").val(ocMotDefault);
            $("#OcMot6a").val(ocMotDefault);
            $("#OcMot7a").val(ocMotDefault);
            $("#OcMot8a").val(ocMotDefault);
            $("#OcMot9a").val(ocMotDefault);

            return false;
        }

    </script>
    <br/>
    <asp:Panel ID="pnlDefault" runat="server" Visible="false">
        <fieldset style="elevation:inherit; border-width:medium">
            <br />
            <table cellspacing="0" cellpadding="3" border="0" id="tblCitHdr">
            <tr valign="middle">
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td class="labelHeaderStyle">
                <asp:HiddenField ID="hdnExamDefaultID" runat="server" />
                Default Name:
                    <asp:TextBox ID="DefaultName" MaxLength="50" runat="server" SkinID="skintxtLarge"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DefaultNameRequired" runat="server" ControlToValidate="DefaultName" Display="Dynamic" ErrorMessage="Default Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <asp:Panel ID="pnlDefaultAge" runat="server" Visible="false">
                <td class="labelHeaderStyle">
                Age Start:
                    <asp:TextBox ID="AgeStart" MaxLength="4" runat="server" SkinID="skintxtTiny" Text="0"></asp:TextBox> months
                    <asp:RequiredFieldValidator ID="AgeStartRequired" runat="server" ControlToValidate="AgeStart" Display="Dynamic" ErrorMessage="Age Start is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="AgeStartRegEx" runat="server" ControlToValidate="AgeStart" Display="Dynamic" ErrorMessage="Age Start is invalid" ForeColor="Red" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td class="labelHeaderStyle">
                Age End:
                    <asp:TextBox ID="AgeEnd" MaxLength="4" runat="server" SkinID="skintxtTiny" Text="0"></asp:TextBox> months
                    <asp:RequiredFieldValidator ID="AgeEndRequired" runat="server" ControlToValidate="AgeEnd" Display="Dynamic" ErrorMessage="Age End is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="AgeEndRegEx" runat="server" ControlToValidate="AgeEnd" Display="Dynamic" ErrorMessage="Age End is invalid" ForeColor="Red" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </asp:Panel>
            </tr>
            <asp:Panel ID="pnlDefaultDoctor" runat="server" Visible="false">
            <tr valign="middle">
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td class="labelHeaderStyle">
                Premature Birth:
                    <asp:CheckBox ID="PrematureBirth" runat="server" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td class="labelHeaderStyle">
                Doctor:
                    <asp:DropDownList ID="DoctorList" runat="server"></asp:DropDownList>
                </td>
            </tr>
            </asp:Panel>
            </table>
            <br />
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlHeader" runat="server">
    <fieldset style="elevation:inherit; border-width:medium">
    <br />
    <table cellspacing="0" cellpadding="2" border="0" id="tblCitHdr">
        <tr valign="middle">
            <td>&nbsp;&nbsp;&nbsp;</td>
            <td class="labelHeaderStyle">
            <asp:HiddenField ID="hdnPatientID" runat="server" />
            <asp:HiddenField ID="hdnExamID" runat="server" />
            <asp:HiddenField ID="hdnNoteType" runat="server" />
            <asp:HiddenField ID="hdnColourType" runat="server" />
            <asp:HiddenField ID="hdnDefaultInd" runat="server" Value="0" />

            PT Name:
                <asp:TextBox ID="Greeting" MaxLength="5" runat="server" SkinID="skintxtTiny" ReadOnly="false"></asp:TextBox>
                <asp:TextBox ID="FirstName" MaxLength="20" runat="server" SkinID="skintxtMini" ReadOnly="false"></asp:TextBox>
                <asp:TextBox ID="MiddleName" MaxLength="20" runat="server" Width="35" CssClass="textStyle" ReadOnly="false"></asp:TextBox>
                <asp:TextBox ID="LastName" MaxLength="20" runat="server" SkinID="skintxtMini" ReadOnly="false"></asp:TextBox>
            </td>
            <td class="labelHeaderStyle">DOB:
                <asp:TextBox ID="DOB" MaxLength="10" runat="server" SkinID="skintxtMini" ReadOnly="false"></asp:TextBox>
                <asp:HiddenField ID="Age" runat="server" />
            </td>
            <td class="labelHeaderStyle">Age:
                <asp:TextBox ID="tbAge" runat="server" MaxLength="20" Width="60" CssClass="textStyle" ReadOnly="false"></asp:TextBox>
                <asp:HiddenField ID="PatientMonths" runat="server" />
            </td>
            <td class="labelHeaderStyle">Sex:
                <asp:TextBox ID="Sex" runat="server" MaxLength="10" Width="40" CssClass="textStyle" ReadOnly="false"></asp:TextBox>
                <asp:HiddenField ID="BoyGirlAdult" runat="server" />
            </td>
            <td class="labelHeaderStyle">Premature:<asp:CheckBox ID="Premature" runat="server" Enabled="false" />
            </td>
            <td class="labelHeaderStyle" nowrap="nowrap">Date:</td>
            <td>
                <dct:ExamMulti ID="ExamDate" ControlType="Date" EnableValidators="true" ShowValidators="true" runat="server"></dct:ExamMulti>
            </td>
            <td class="labelHeaderStyle" nowrap="nowrap">Doctor:</td>
            <td>
                <asp:DropDownList ID="User" runat="server" OnChange="GetDoctorDefaults()"></asp:DropDownList>
            </td>
        </tr>
        <tr valign="middle">
            <td>&nbsp;&nbsp;&nbsp;</td>
            <td class="labelHeaderStyle">Hx From:
                <asp:TextBox ID="HxFrom" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="false" Visible="false"></asp:TextBox>
                <asp:DropDownList id="ddlHxFrom" runat="server" Visible="true"></asp:DropDownList>
                <asp:TextBox ID="tbHxOther" MaxLength="30" runat="server" SkinID="skintxtSmall" Visible="true"></asp:TextBox>
            </td>
            <td colspan="5" class="labelHeaderStyle">Ref'd By:
                <asp:TextBox ID="Refd" MaxLength="50" runat="server" SkinID="skintxtXMedium" onchange="SetCopyTo()"></asp:TextBox>
                (Dr. <asp:TextBox ID="RefDoctor" MaxLength="50" runat="server" SkinID="skintxtXMedium" onchange="SetCopyTo()"></asp:TextBox>)
            </td>
            <td colspan="3" class="labelHeaderStyle">Allergies:
                <asp:TextBox ID="Allergies" MaxLength="50" runat="server" SkinID="skintxtMedium" ReadOnly="false"></asp:TextBox>
            </td>
        </tr>
        <tr valign="middle">
            <td>&nbsp;&nbsp;&nbsp;</td>
            <td colspan="3" class="labelHeaderStyle">Grade Level/Occupation:
                <asp:TextBox ID="Occupation" MaxLength="50" runat="server" SkinID="skintxtMedium" ReadOnly="false"></asp:TextBox>
            </td>
            <td colspan="6" class="labelHeaderStyle" style="text-align:right">Defaults:
                <asp:DropDownList ID="ddlUserDefaults" runat="server" onChange="return GetUserDefaults()" ></asp:DropDownList>
                <asp:Button ID="btnEditPatient" runat="server" SkinID="skinBtnSmall" Text="Edit" UseSubmitBehavior="false" CausesValidation="false"
                        OnClientClick="return EditPatient()" Visible="False"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
    </fieldset>
    </asp:Panel>
    <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
    
    <asp:Panel ID="pnlTabs" runat="server">
    <dct:tabs ID="ExamPatientTab" runat="server" Visible="true" SelectedTabIndex="0">
        <dct:Tab ID="ComplaintTab" runat="server" HeaderText= "CC/History">
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderRightStyle">Chief Complaint:</td>
                    <td>
                        <asp:TextBox ID="Compliant" runat="server" SkinID="skinMultiLine" Rows="5" 
                            TextMode="MultiLine" Width="366px"></asp:TextBox>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td class=" labelHeaderRightStyle">&nbsp;History:&nbsp; </td>
                    <td>
                        <asp:TextBox ID="SubjectiveHistory" runat="server" SkinID="skinMultiLine" 
                            Rows="5" TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Mentation:</td>
                    <td>
                        <asp:DropDownList id="Mentation1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Mentation2" MaxLength="50" runat="server" 
                            SkinID="skintxtXMedium" Width="289px"></asp:TextBox>
                    </td>
                </tr>
                </table>
                </fieldset>

        <fieldset>
       
        <table>
        
        <tr><td class="note-band" colspan="2"><strong>Past Ocular/Medical History</strong></td></tr>
            <tr style="background-color:#CEDEFF">
                <td style="width: 480px; height: 280px;">
                    <table>
                        <tr>
                            <td class=" labelHeaderStyle">
                                Glasses since:</td>
                            <td>
                                <asp:DropDownList ID="Glasses1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="Glasses2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class=" labelHeaderStyle">
                                Last Exam:</td>
                            <td>
                                <asp:TextBox ID="LastExam" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                <asp:TextBox ID="LastExamElse" runat="server" MaxLength="100" 
                                    SkinID="skintxtXMedium" Width="147px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Contact Lens Hx:</td>
                            <td>
                                <asp:DropDownList ID="ContactLens1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="ContactLens2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Disease/Trauma:</td>
                            <td>
                                <asp:DropDownList ID="DiseaseTrauma1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="DiseaseTrauma2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="DiseaseTrauma3" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="DiseaseTrauma4" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="DiseaseTrauma5" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="DiseaseTrauma6" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                                Surgery/Treatment:</td>
                            <td>
                                <asp:DropDownList ID="SurgeryTreatment1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="SurgeryTreatment2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="SurgeryTreatment3" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="SurgeryTreatment4" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class=" labelHeaderStyle">
                            </td>
                            <td>
                                <asp:TextBox ID="SurgeryTreatment5" runat="server" Rows="3" 
                                    SkinID="skinMultiLine" TextMode="MultiLine" Width="322px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                
                <td style="width: 480px; height: 280px;">
                    <table>
                        <tr>
                            <td class="labelHeaderStyle">
                                Medications:</td>
                            <td>
                                <asp:TextBox ID="Medications" runat="server" Rows="3" SkinID="skinMultiLine" 
                                    Text="" TextMode="MultiLine" Width="322px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                PMH:</td>
                            <td>
                                <asp:DropDownList ID="PMH1" runat="server" SkinID="skinCombo_CCHistory" onchange="SetPremature();">
                                </asp:DropDownList>
                                <asp:TextBox ID="PMH2" runat="server" MaxLength="50" SkinID="skintxtXMedium" 
                                    Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="PMH3" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="PHM4" runat="server" MaxLength="50" SkinID="skintxtXMedium" 
                                    Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Birth Hx:</td>
                            <td>
                                <asp:DropDownList ID="BirthHist1" runat="server" SkinID="skinCombo_CCHistory" onchange="SetPremature();">
                                </asp:DropDownList>
                                <asp:TextBox ID="BirthHist2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr ID="hdntr1" style="display:none">
                            <td class="labelHeaderStyle">
                                GA</td>
                            <td rowspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="GA" runat="server" MaxLength="50" SkinID="skintxtSmall" Text="weeks"></asp:TextBox>
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="BirthHist3" runat="server" Rows="3" SkinID="skinMultiLine" 
                                                TextMode="MultiLine" Width="226px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="PCA" runat="server" MaxLength="50" SkinID="skintxtSmall" Text="weeks"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr ID="hdntr3" style="display:none">
                            <td class="labelHeaderStyle">
                                PC Age</td>
                        </tr>
                        <tr ID="hdntr2" style="display:none">
                            <td class="labelHeaderStyle">
                                Birth Wt:</td>
                            <td>
                                <asp:TextBox ID="BirthWt" runat="server" MaxLength="50" SkinID="skintxtXMedium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Development Hx:</td>
                            <td>
                                <asp:DropDownList ID="DevelopHist1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="DevelopHist2" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList SkinID="skinCombo_CCHistory" ID="DevelopHist3" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="DevelopHist4" runat="server" MaxLength="50" 
                                    SkinID="skintxtXMedium" Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Family Hx:</td>
                            <td>
                                <asp:DropDownList ID="FH1" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="FH2" runat="server" MaxLength="50" SkinID="skintxtXMedium" 
                                    Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="FH3" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="FH4" runat="server" MaxLength="50" SkinID="skintxtXMedium" 
                                    Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderRightStyle">
                            </td>
                            <td>
                                <asp:DropDownList ID="FH5" runat="server" SkinID="skinCombo_CCHistory">
                                </asp:DropDownList>
                                <asp:TextBox ID="FH6" runat="server" MaxLength="50" SkinID="skintxtXMedium" 
                                    Width="198px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
            </fieldset>
            <asp:Panel ID="pnlComplaint" runat="server">
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnComplaintNext" UseSubmitBehavior="false" CausesValidation="false"
                            runat="server" ValidationGroup="valCitaionDetails" Text="NEXT" SkinID="skinBtn"
                            OnClientClick="return MoveTab('#VisualTab')"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnComplaintSave" runat="server" Text="SAVE" UseSubmitBehavior="true" CausesValidation="false"
                            SkinID="skinBtn" OnClick="btnSave_Click" OnClientClick="return ClearDirty();"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnComplaintReset" UseSubmitBehavior="false" CausesValidation="false"
                            OnClientClick="return ResetDiv('ComplaintTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <table width="600">
                            <tr>
                                <td width="400">&nbsp;&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnSignOff1" runat="server" Text="SIGN OFF" UseSubmitBehavior="true" CausesValidation="false"
                                        SkinID="skinBtn" OnClick="btnSignOff_Click" OnClientClick="return ValidateNotes();"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
          </dct:Tab>
        <dct:Tab ID="VisualTab" runat="server" HeaderText="Acuity/VF/Pupils">
                    <fieldset style="background-color:#E0E7FF">
                        <table>
                            <tr>
                                <td colspan="2" class="labelHeaderStyle">Tested with:</td>
                                <td><asp:DropDownList ID="VisualAcuity" runat="server" Visible="true" TabIndex="1"></asp:DropDownList></td>
                                <td class="labelHeaderStyle" width="50">BINOC sc Dist</td>
                                <td class="labelHeaderStyle" width="10"></td>
                                <td width="220"><asp:DropDownList ID="Binocsc1" runat="server" TabIndex="2"></asp:DropDownList>
                                <asp:TextBox ID="Binocsc2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="3"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="50">VA sc Dist</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="220"><asp:DropDownList ID="VAscOD1" runat="server" TabIndex="4"></asp:DropDownList>
                                <asp:TextBox ID="VAscOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="5"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">VA cc Dist</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="220"><asp:DropDownList ID="VAccOD1" runat="server" TabIndex="8"></asp:DropDownList>
                                <asp:TextBox ID="VAccOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="9"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">VA Near</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="200"><asp:DropDownList ID="VAOD1" runat="server" TabIndex="12"></asp:DropDownList>
                                <asp:TextBox ID="VAOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="13"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="50">No Pref <asp:CheckBox ID="NoPref" runat="server" onclick="CheckNoPref()" /></td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:DropDownList ID="DistOS1" runat="server" TabIndex="6"></asp:DropDownList>
                                <asp:TextBox ID="DistOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="7"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50"></td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:DropDownList ID="DistOS3" runat="server" TabIndex="10"></asp:DropDownList>
                                <asp:TextBox ID="DistOS4" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="11"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50"></td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:DropDownList ID="NearOS1" runat="server" TabIndex="14"></asp:DropDownList>
                                <asp:TextBox ID="NearOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="15"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="50">Spc Wr1</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="220"><asp:TextBox ID="SpcWr1OD" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="16"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">Spc Wr2</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="220"><asp:TextBox ID="SpcWr2OD" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="18"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">Spc Wr3</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="220"><asp:TextBox ID="SpcWr3OD" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="20"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="50">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:TextBox ID="SpcWr1OS" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="17"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:TextBox ID="SpcWr2OS" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="19"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td width="220"><asp:TextBox ID="SpcWr3OS" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="21"></asp:TextBox></td>
                            </tr>
                            <tr><td style="height: 19px"></td></tr>
                        </table>
                        </fieldset>
                        <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
                        <fieldset style="background-color:#CEDEFF">
                        <table>
                            <tr>
                                    <td class="labelHeaderStyle" width="50">Man Rfx</td>
                                    <td class="labelHeaderStyle" width="10">OD:</td>
                                    <td width="200"><asp:TextBox ID="ManRfxOD1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="22"></asp:TextBox>
                                    <asp:TextBox ID="ManRfxOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="23"></asp:TextBox></td>
                                    <td class="labelHeaderStyle" width="10">VA</td>
                                    <td class="labelHeaderStyle" width="10">OD:</td>
                                    <td width="200"><asp:DropDownList ID="ManVAOD1" runat="server" TabIndex="26"></asp:DropDownList>
                                    <asp:TextBox ID="ManVAOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="27"></asp:TextBox></td>
                                    <td class="labelHeaderStyle" width="50">Cyc Rfx</td>
                                    <td class="labelHeaderStyle" width="10">OD:</td>
                                    <td width="100"><asp:TextBox ID="CycRfxOD" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="30"></asp:TextBox></td>
                                    <td class="labelHeaderStyle" width="10">VA</td>
                                    <td class="labelHeaderStyle" width="10">OD:</td>
                                    <td width="180"><asp:DropDownList ID="CycVAOD3" runat="server" TabIndex="32"></asp:DropDownList>
                                    <asp:TextBox ID="CycVAOD4" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="33"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="20">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:TextBox ID="ManRfxOS1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="24"></asp:TextBox>
                                <asp:TextBox ID="ManRfxOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="25"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:DropDownList ID="ManVSOS1" runat="server" TabIndex="28"></asp:DropDownList>
                                <asp:TextBox ID="ManVSOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="29"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="30">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:TextBox ID="CycRfxOS" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="31"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:DropDownList ID="CycVSOS1" runat="server" TabIndex="34"></asp:DropDownList>
                                <asp:TextBox ID="CycVSOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="35"></asp:TextBox></td>
                            </tr>
                          </table>
                          </fieldset>  
                            <asp:Panel ID="pnlPriorExam" runat="server" Visible="false">
                            <table width="100%"><tr><td class="note-band" colspan="2"><strong>Prior Exam</strong></td></tr></table>
                            <fieldset style="background-color:#E0E7FF">
                            <table>
                            <tr>
                                <td class="labelHeaderStyle" width="50">Man Rfx</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="200"><asp:TextBox ID="LastManRfxOD1" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="LastManRfxOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">VA</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="200"><asp:DropDownList ID="LastManVAOD1" runat="server" Enabled="false"></asp:DropDownList>
                                <asp:TextBox ID="LastManVAOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="50">Cyc Rfx</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="100"><asp:TextBox ID="LastCycRfxOD" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">VA</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td width="180"><asp:DropDownList ID="LastCycVAOD3" runat="server" Enabled="false"></asp:DropDownList>
                                <asp:TextBox ID="LastCycVAOD4" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="20">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:TextBox ID="LastManRfxOS1" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="LastManRfxOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:DropDownList ID="LastManVSOS1" runat="server" Enabled="false"></asp:DropDownList>
                                <asp:TextBox ID="LastManVSOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="30">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:TextBox ID="LastCycRfxOS" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                                <td class="labelHeaderStyle" width="10">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td><asp:DropDownList ID="LastCycVSOS1" runat="server" Enabled="false"></asp:DropDownList>
                                <asp:TextBox ID="LastCycVSOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            </table>
                            </fieldset>
                            </asp:Panel>
                            <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
                           <fieldset style="background-color:#CEDEFF;">
                            <table>
                            <tr>
                                <td class="labelHeaderStyle" width="50">Rx given</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td><asp:TextBox ID="RxOD1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="36"></asp:TextBox>
                                <asp:TextBox ID="RxOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="37"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="PopulateRxGiven" runat="server" onClick="SetRxGiven();"></asp:CheckBox> Populate
                                </td>
                                <td class="labelHeaderStyle" width="30">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="50">CTL Rx</td>
                                <td class="labelHeaderStyle" width="10">OD:</td>
                                <td>
                                    <asp:TextBox ID="CTLRxOD1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="42"></asp:TextBox>
                                    <asp:TextBox ID="CTLRxOD2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="43"></asp:TextBox>
                                    <asp:TextBox ID="CTLRxOD3" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="44"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:CheckBox ID="PopulateCtlRx" runat="server" onClick="SetRxGiven();"></asp:CheckBox> Populate
                                </td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle" width="20">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td>
                                    <asp:TextBox ID="RXOS1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="38"></asp:TextBox>
                                    <asp:TextBox ID="RXOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="39"></asp:TextBox>
                                    <asp:TextBox ID="RXOS3" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="40"></asp:TextBox>
                                    <asp:TextBox ID="RXOS4" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="41"></asp:TextBox>
                                </td>
                                <td class="labelHeaderStyle" width="30">&nbsp;&nbsp;&nbsp;</td>
                                <td class="labelHeaderStyle" width="50">&nbsp;</td>
                                <td class="labelHeaderStyle" width="10">OS:</td>
                                <td>
                                    <asp:TextBox ID="CTLRxOS1" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="45"></asp:TextBox>
                                    <asp:TextBox ID="CTLRxOS2" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="46"></asp:TextBox>
                                    <asp:TextBox ID="CTLRxOS3" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="47"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                        </fieldset>
                        <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
                        <fieldset style="background-color:#E0E7FF">
                        <table>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td class="labelHeaderStyle" colspan="6">Visual Fields:
                                <asp:DropDownList ID="Confront1" runat="server" TabIndex="48"></asp:DropDownList>&nbsp;&nbsp;
                                <asp:DropDownList ID="Confront2" runat="server" TabIndex="49"></asp:DropDownList>&nbsp;&nbsp;
                                <asp:TextBox ID="Confront3" MaxLength="50" runat="server" SkinID="skintxtSmall" TabIndex="50"></asp:TextBox>
                                </td>
                                <td  class="labelHeaderStyle" colspan="3" align="center">OD</td>
                                <td  class="labelHeaderStyle" colspan="3">OS</td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td class="labelHeaderStyle" colspan="12">
                                <table>
                                    <tr>
                                        <td class="labelHeaderStyle">Pupils:</td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="PupilOD1" runat="server" TabIndex="51"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:DropDownList ID="PupilOD2" runat="server" TabIndex="52"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:DropDownList ID="PupilOS1" runat="server" TabIndex="53"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:DropDownList ID="PupilOS2" runat="server" TabIndex="54"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:TextBox ID="Pupil" MaxLength="50" runat="server" SkinID="skintxtXMedium" TabIndex="55"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>  
                        </fieldset>
               
            <asp:Panel ID="pnlVisual" runat="server">
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnVisualPrevious" CausesValidation="false" UseSubmitBehavior="false"
                            runat="server" OnClientClick="return MoveTab('#ComplaintTab')" Text="PREVIOUS"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnVisualNext" CausesValidation="false" UseSubmitBehavior="false"
                            runat="server" Text="NEXT" OnClientClick="return MoveTab('#OcularTab')"
                            SkinID="skinBtn" ValidationGroup="validSearch"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnVisualSave" runat="server" Text="SAVE" UseSubmitBehavior="true" CausesValidation="false"
                            SkinID="skinBtn" OnClick="btnSave_Click" OnClientClick="return ClearDirty();"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnVisualReset" UseSubmitBehavior="false" CausesValidation="false"
                            OnClientClick="return ResetDiv('VisualTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <table width="400">
                            <tr>
                                <td width="300">&nbsp;&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnSignOff2" runat="server" Text="SIGN OFF" UseSubmitBehavior="true" CausesValidation="false"
                                        SkinID="skinBtn" OnClick="btnSignOff_Click" OnClientClick="return ValidateNotes();"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </dct:Tab>
        <dct:Tab ID="OcularTab" runat="server" HeaderText="Ocular Motility">
        <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td class="labelHeaderStyle">Ocular Motility &nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="OcularMotility6" runat="server"></asp:DropDownList>
                                    <asp:DropDownList ID="OcularMotility1" runat="server"></asp:DropDownList>
                                    <asp:TextBox ID="OcularMotility5" MaxLength="200" runat="server" SkinID="skintxtLarge"></asp:TextBox>
                                </td>
                             </tr>
                             <tr>
                                <td class="labelHeaderStyle"> &nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="OcularMotility2" runat="server"></asp:DropDownList>
                                    <asp:TextBox ID="OcularMotility4" MaxLength="200" runat="server" SkinID="skintxtLarge"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="labelHeaderStyle"><asp:CheckBox ID="OMDefaults" runat="server" onclick="AcceptDefaults('OcularTab');" /> Accept defaults </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td class="labelHeaderStyle">&nbsp;Tic Tac To:</td>
                                <td style="text-align:right">
                                    <asp:TextBox ID="OcMotDefault" MaxLength="50" runat="server" SkinID="skintxtSmall" ></asp:TextBox>  
                                    &nbsp;&nbsp;                                  
                                    <asp:Button ID="btnTicTacToPopulate" runat="server" SkinID="skinBtnSmall" Text="Populate" UseSubmitBehavior="false" CausesValidation="false"
                                            OnClientClick="return PopulateTicTacTo()" Visible="True" style="float:right"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50">&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <table style="border: solid 1px #525252; border-collapse:collapse;"  width="650" border="1px">
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="OcMot1a" MaxLength="50" runat="server" SkinID="skintxtSmall" ></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot1b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot2a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot2b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot3a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot3b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="OcMot4a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot4b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot5a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot5b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot6a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot6b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="OcMot7a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot7b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot8a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot8b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="OcMot9a" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="OcMot9b" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50">&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <table>
                            <tr>
                                <td class="labelHeaderStyle">Head Tilt Right:</td>
                                <td>
                                    <asp:TextBox ID="HeadTiltRight1" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="HeadTiltRight2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                                <td width="20">&nbsp;</td>
                                <td class="labelHeaderRightStyle">Head Tilt Left:</td>
                                <td>
                                    <asp:TextBox ID="HeadTiltLeft1" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                    <asp:TextBox ID="HeadTiltLeft2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="labelHeaderRightStyle">Adaptive Head Positions:</td>
                                <td>
                                    <asp:DropDownList ID="HeadPosition1" runat="server"></asp:DropDownList>
                                    <asp:TextBox ID="HeadPosition2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle">Double Maddox Rod:</td>
                                <td>
                                    <asp:TextBox ID="DMR2" MaxLength="50" runat="server" SkinID="skintxtSmall" Text="N/A"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="labelHeaderStyle">Preference:</td>
                                <td>
                                    <asp:DropDownList ID="Preference" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
             </fieldset>
        <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
             <fieldset style="background-color:#E0E7FF">
            <table width="700">
                <tr>
                    <td class="labelHeaderStyle">Ocular Versions:
                        <asp:DropDownList ID="OcularVersions1" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="OcularVersions11" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="OcularVersions2" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="OcularVersions3" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="OcularVersions31" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="OcularVersions4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="OcularVersions5" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <br />
                        Nystagmus:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                        <asp:DropDownList ID="Nystagmus1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Nystagmus2" MaxLength="50" runat="server" 
                            SkinID="skintxtSmall" Width="197px"></asp:TextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOD1" runat="server"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td><asp:DropDownList ID="OcularVersionOD2" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOD3" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="middle"><img src="images/body_topbg.gif" height="50" alt="" /></td>
                                            <td valign="middle"><img src="images/body_topbg_rotate.gif" width="45" alt="" /></td>
                                            <td valign="middle"><img src="images/body_topbg.gif" height="50" alt="" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="OcularVersionOD4" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOD5" runat="server"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td><asp:DropDownList ID="OcularVersionOD6" runat="server"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOS1" runat="server"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td><asp:DropDownList ID="OcularVersionOS2" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOS3" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="middle"><img src="images/body_topbg.gif" height="50" alt="" /></td>
                                            <td valign="middle"><img src="images/body_topbg_rotate.gif" width="45" alt="" /></td>
                                            <td valign="middle"><img src="images/body_topbg.gif" height="50" alt="" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="OcularVersionOS4" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right"><asp:DropDownList ID="OcularVersionOS5" runat="server"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td><asp:DropDownList ID="OcularVersionOS6" runat="server"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </fieldset>
            <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                <td>
                    <table>
                        <tr>
                            <td class="labelHeaderStyle">FUSIONAL AMPLITUDES</td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                BO&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="FusAmpBO" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                &nbsp;&nbsp; BI &nbsp;<asp:TextBox ID="FusAmpBI" runat="server" MaxLength="50" 
                                    SkinID="skintxtSmall"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                BU&nbsp;&nbsp; &nbsp;<asp:TextBox ID="FusAmpBU" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>&nbsp;&nbsp;
                                BD&nbsp;<asp:TextBox ID="FusAmpBD" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                        </tr>
                     </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td class="labelHeaderStyle">Near Point</td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                                Convergence&nbsp;&nbsp; </td>
                            <td>
                                <asp:TextBox ID="Convergence" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelHeaderStyle">
                            Accommodation</td>
                            <td>
                                <asp:TextBox ID="Accommodation" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td class="labelHeaderStyle">
                                                LVR</td>
                                            <td class="labelHeaderStyle">
                                                <asp:TextBox ID="LVRR1" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                                &nbsp;&nbsp; R&nbsp;&nbsp;
                                                <asp:TextBox ID="LVRR2" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                            </td>
                                            <td width="10">
                                            </td>
                                            <td class="labelHeaderStyle" style="width: 265px">
                                                <asp:TextBox ID="LVRL1" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                                L
                                                <asp:TextBox ID="LVRL2" runat="server" MaxLength="50" SkinID="skintxtSmall"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                
                
                

                </table>
                        
            </fieldset>
            <table width="100%"><tr><td class="note-band-blank" colspan="2"></td></tr></table>
            <fieldset style="background-color:#E0E7FF">
            <table>
                <tr>
                    <td class="labelHeaderRightStyle">BINOCULARITY:</td>
                    <td colspan="3" class="labelHeaderStyle">
                        tested with <asp:DropDownList ID="Binocularity1" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="Binocularity2" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Binocularity3" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                        <asp:TextBox ID="Binocularity4" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">W4D Near:</td>
                    <td>
                        <asp:DropDownList ID="W4DNear1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="W4DNear2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                    <td class="labelHeaderRightStyle" width="100">W4D Distance:</td>
                    <td>
                        <asp:DropDownList ID="W4DDistance1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="W4DDistance2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Stereo(Titmus):</td>
                    <td>
                        <asp:DropDownList ID="Stereo1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Stereo2" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                    <td class="labelHeaderRightStyle" width="100">+ 4 diopter base out:</td>
                    <td>
                        <asp:DropDownList ID="DiopterBaseOut" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
           </fieldset>
            <asp:Panel ID="pnlOcular" runat="server">
           <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnOcularPrevious" CausesValidation="false" 
                            runat="server" OnClientClick="return MoveTab('#VisualTab')" Text="PREVIOUS"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnOcularNext" CausesValidation="false" 
                            runat="server" Text="NEXT" OnClientClick="return MoveTab('#AnteriorTab')"
                            SkinID="skinBtn" ValidationGroup="validSearch"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnOcularSave" runat="server" Text="SAVE" UseSubmitBehavior="true" CausesValidation="false"
                            SkinID="skinBtn" OnClick="btnSave_Click" OnClientClick="return ClearDirty();"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnOcularReset" CausesValidation="false"
                            OnClientClick="return ResetDiv('OcularTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <table width="400">
                            <tr>
                                <td width="300">&nbsp;&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnSignOff3" runat="server" Text="SIGN OFF" UseSubmitBehavior="true" CausesValidation="false"
                                        SkinID="skinBtn" OnClick="btnSignOff_Click" OnClientClick="return ValidateNotes();"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </dct:Tab>
        <dct:Tab ID="AnteriorTab" runat="server" HeaderText="Ant/Post Segment">
        <fieldset style="background-color:#CEDEFF">
          <div id="AnteriorDefaults"> 
              <table>
                <tr>
                    <td class="labelHeaderStyle">Anterior Segment examination:</td>
                    <td>
                        <asp:TextBox ID="AnteriorSegment" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="600px" Text="unremarkable OU"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">SLE:
                        <asp:CheckBox ID="SLE" runat="server" />                    
                        &nbsp;&nbsp;Pen-light:
                        <asp:CheckBox ID="PenLight" runat="server" /> 
                        &nbsp;&nbsp;All WNL:
                        <asp:CheckBox ID="AllWNL" runat="server" onclick="AcceptDefaults('AnteriorDefaults');" /> 
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Lids/Lashes/Lacrimal:</td>
                    <td>
                        <asp:DropDownList ID="LidLashLacrimal1" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="LidLashLacrimal2" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="LidLashLacrimal3" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="LidLashLacrimal4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="LidLashLacrimal5" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Exophthalmometry:</td>
                    <td class="labelHeaderStyle">
                        <asp:DropDownList ID="Exophthalmometry" runat="server"></asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;OD: &nbsp;
                        <asp:TextBox ID="ExophthalmometryOD" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;OS:&nbsp;
                        <asp:TextBox ID="ExophthalmometryOS" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox>
                    </td>
                </tr>
            </table>            
            <table width="500">
                <tr>
                    <td class="labelHeaderRightStyle">OD PF:</td>
                    <td><asp:TextBox ID="ODPF" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">MRD1:</td>
                    <td><asp:TextBox ID="ODMRD1" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">MRD2:</td>
                    <td><asp:TextBox ID="ODMRD2" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">Levator Fxn:</td>
                    <td><asp:DropDownList ID="ODLevatorFxn1" runat="server"></asp:DropDownList> 
                    <asp:TextBox ID="ODLevatorFxn2" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">OS PF:</td>
                    <td><asp:TextBox ID="OSPF" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">MRD1:</td>
                    <td><asp:TextBox ID="OSMRD1" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">MRD2:</td>
                    <td><asp:TextBox ID="OSMRD2" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                    <td class="labelHeaderRightStyle">Levator Fxn:</td>
                    <td><asp:DropDownList ID="OSLevatorFxn1" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="OSLevatorFxn2" MaxLength="50" runat="server" SkinID="skintxtTiny"></asp:TextBox></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="labelHeaderStyle">Conj/Sclera:</td>
                    <td>
                        <asp:DropDownList ID="ConjSclera1" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="ConjSclera2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:DropDownList ID="ConjSclera3" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="ConjSclera4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="ConjSclera5" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Cornea:</td>
                    <td>
                        <asp:DropDownList ID="Cornea1" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Cornea2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:DropDownList ID="Cornea3" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Cornea4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Cornea5" MaxLength="50" runat="server" SkinID="skintxtMedium">HCDs are normal at 10mm OU</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Ant Chamber:</td>
                    <td>
                        <asp:DropDownList ID="AntChamber1" runat="server" Width="170px" ></asp:DropDownList>
                        <asp:DropDownList ID="AntChamber2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:DropDownList ID="AntChamber3" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="AntChamber4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="AntChamber5" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Iris:</td>
                    <td>
                        <asp:DropDownList ID="Iris1" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Iris2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:DropDownList ID="Iris3" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Iris4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Iris5" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Lens:</td>
                    <td>
                        <asp:DropDownList ID="Lens1" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Lens2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:DropDownList ID="Lens3" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Lens4" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="Lens5" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Tono:</td>
                    <td>
                        <asp:DropDownList ID="Tono1" runat="server" Width="170px"></asp:DropDownList>
                        <asp:DropDownList ID="Tono2" runat="server" Width="70px"></asp:DropDownList>
                        <asp:TextBox ID="Tono3" MaxLength="50" runat="server" SkinID="skintxtSmall">STP</asp:TextBox>OD
                        <asp:TextBox ID="Tono4" MaxLength="50" runat="server" SkinID="skintxtSmall">STP</asp:TextBox>OS
                    </td>
                </tr>
            </table>
          </div>
            <br /><br />
            <table>
                <tr>
                    <td class="labelHeaderRightStyle"><asp:CheckBox ID="Dilate1" runat="server" Visible="false" />
                        <asp:CheckBox ID="Dilate2" runat="server" Visible="false" />Dilated:  
                        <asp:DropDownList ID="Dilate3" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td class="labelHeaderRightStyle">Fundus Exam: Optic nerve heads are</td>
                    <td><asp:TextBox ID="Fundus1" runat="server" SkinID="skintxtLarge" MaxLength="200" Text="sharp, flat, and pink OU" Width="393px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">with</td>
                    <td><asp:TextBox ID="Fundus2" runat="server" SkinID="skintxtLarge" MaxLength="200" Text="normal physiologic cupping OU" Width="393px"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td class="labelHeaderStyle">Retina and Mac OU:</td>
                    <td colspan="3"><asp:TextBox ID="RetinaOU" runat="server" SkinID="skinMultiLine" Rows="5" TextMode="MultiLine" Width="600" Text="The retinal vessels are normal in course and caliber OU.  The maculae appear normal OU"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Ret Vessels OD:</td>
                    <td><asp:TextBox ID="RetOD" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="250"></asp:TextBox></td>
                    <td class="labelHeaderStyle">Ret Vessels OS:</td>
                    <td><asp:TextBox ID="RetOS" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="250"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Macula OD:</td>
                    <td><asp:TextBox ID="MaculaOD" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="250"></asp:TextBox></td>
                    <td class="labelHeaderStyle">Macula OS:</td>
                    <td><asp:TextBox ID="MaculaOS" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="250"></asp:TextBox></td>
                </tr>
            </table>
            </fieldset>
            <asp:Panel ID="pnlAnterior" runat="server">
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnAnteriorPrevious" CausesValidation="false" UseSubmitBehavior="false"
                            runat="server" OnClientClick="return MoveTab('#OcularTab')" Text="PREVIOUS"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnAnteriorNext" CausesValidation="false" UseSubmitBehavior="false"
                            runat="server" Text="NEXT" OnClientClick="return MoveTab('#SummaryTab')"
                            SkinID="skinBtn" ValidationGroup="validSearch"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnAnteriorReset" UseSubmitBehavior="false" CausesValidation="false"
                            OnClientClick="return ResetDiv('AnteriorTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnAnteriorSave" runat="server" Text="SAVE" UseSubmitBehavior="true" CausesValidation="false"
                            SkinID="skinBtn" OnClick="btnSave_Click" OnClientClick="return ClearDirty();"></asp:Button>
                    </td>
                    <td>
                        <table width="400">
                            <tr>
                                <td width="300">&nbsp;&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnSignOff4" runat="server" Text="SIGN OFF" UseSubmitBehavior="true" CausesValidation="false"
                                        SkinID="skinBtn" OnClick="btnSignOff_Click" OnClientClick="return ValidateNotes();"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </dct:Tab>
        <dct:Tab ID="SummaryTab" runat="server" HeaderText="Summary">
        <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderRightStyle">Summary:</td>
                    <td><asp:TextBox ID="Summary" runat="server" SkinID="skinMultiLine" Rows="5" 
                            TextMode="MultiLine" Width="600px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Discussed:</td>
                    <td><asp:TextBox ID="Discussed" runat="server" SkinID="skintxtLarge"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Advised:</td>
                    <td><asp:TextBox ID="Advised" runat="server" SkinID="skinMultiLine" Rows="5" 
                            TextMode="MultiLine" Width="600px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Follow-up:</td>
                    <td>
                        <asp:DropDownList ID="FollowUp1" runat="server" Width="67px"></asp:DropDownList>
                        <asp:DropDownList ID="FollowUp2" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="FollowUp3" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="FollowUp4" runat="server" SkinID="skintxtMedium" Text="sooner prn"></asp:TextBox>
                     </td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Letter to:</td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:TextBox ID="CopyTo" runat="server" SkinID="skintxtLarge" Width="227px"></asp:TextBox></td>
                                <td class="labelHeaderRightStyle">&nbsp;&nbsp;&nbsp;Print Queue:</td>
                                <td><asp:CheckBox ID="cbPrintQueue" runat="server" Checked="true"></asp:CheckBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Exam Note to:</td>
                    <td><asp:TextBox ID="ExamNoteTo" runat="server" SkinID="skintxtLarge" Width="227px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelHeaderRightStyle">Notes:</td>
                    <td><asp:TextBox ID="Notes" runat="server" SkinID="skinMultiLine" Rows="5" 
                            TextMode="MultiLine" Width="600px"></asp:TextBox></td>
                </tr>
                <tr>
                </tr>
            </table>
            <br/>
            </fieldset>
            <asp:Panel ID="pnlLoc" runat="server">
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnLocPrevious" runat="server" CausesValidation="false" UseSubmitBehavior="false"
                            Text="PREVIOUS" SkinID="skinBtn" OnClientClick="return MoveTab('#AnteriorTab')">
                        </asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnLocReset" CausesValidation="false" OnClientClick="return ResetDiv('SummaryTab')"
                            UseSubmitBehavior="false" runat="server" Text="RESET" SkinID="skinBtn"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnLocSave" runat="server" Text="SAVE" UseSubmitBehavior="true" CausesValidation="false"
                            SkinID="skinBtn" OnClick="btnSave_Click" OnClientClick="return ClearDirty();"></asp:Button>
                    </td>
                    <td>
                        <table width="600">
                            <tr>
                                <td width="400">&nbsp;&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnSignOff" runat="server" Text="SIGN OFF" UseSubmitBehavior="true" CausesValidation="false"
                                        SkinID="skinBtn" OnClick="btnSignOff_Click" OnClientClick="return ValidateNotes();"></asp:Button>
                                    <asp:Button ID="btnDefault" runat="server" Text="SET DEFAULTS" UseSubmitBehavior="true" CausesValidation="true"
                                        SkinID="skinBtn" OnClick="btnDefault_Click" Visible="false" OnClientClick="ClearDirty();"></asp:Button>
                                    <asp:ValidationSummary ID="valsum" runat="server" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
    <script language="javascript">
        <% =GetSummaryJQuery() %>
    </script>
    </asp:Panel>
    <asp:Panel ID="pnlResult" runat="server" Visible="false">
    <div id="tabs">
	
    <div style='padding-left:8px;'><img id="_imgtopTab" src="images/white_topcurve.gif" style="height:9px;width:966px;" /><div class='tabBackground'><div style='padding-left:20px;'>
        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td>
            <asp:Label ID="result" runat="server" Text="Exam Notes signed off successfully" SkinID="lblHeader" Width="500" Visible="false"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>
            <asp:HiddenField ID="patientID" runat="server" />
            <asp:Button ID="btnReport" runat="server" Text="LETTER" CausesValidation="false" SkinID="skinBtn" />
            <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH" SkinID="skinBtn"></asp:Button> 
            <asp:Button ID="btnPrint" runat="server" Text="VIEW/PRINT" SkinID="skinBtn" CausesValidation="false" Visible="false"></asp:Button>
            <asp:Button ID="btnPrintExam" runat="server" Text="VIEW/PRINT" SkinID="skinBtn" CausesValidation="false"></asp:Button>
        </td>
        </tr>
        </table>
    </div></div><img id="tabComplaint_imgbotTab" src="images/white_botcurve.gif" style="height:9px;width:966px;" /></div>
</div> 

    </asp:Panel>

    <asp:Panel ID="pnlSave" runat="server" Visible="false">
    <div id="Div1">
	
    <div style='padding-left:8px;'><img id="Img1" src="images/white_topcurve.gif" style="height:9px;width:966px;" /><div class='tabBackground'><div style='padding-left:20px;'>
        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td>
            <asp:Label ID="savemsg" runat="server" Text="Exam Notes saved successfully" SkinID="lblHeader" Width="500"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="btnContinue" runat="server" Text="CONTINUE" CausesValidation="false" SkinID="skinBtn" />
            <asp:Button ID="btnSearchPatient" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH" SkinID="skinBtn"></asp:Button> 
        </td>
        </tr>
        </table>
    </div></div><img id="Img2" src="images/white_botcurve.gif" style="height:9px;width:966px;" /></div>
</div> 
    </asp:Panel>
</asp:Content>