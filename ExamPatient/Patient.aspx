<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Patient.aspx.cs" Inherits="Patient" Title="PATIENT" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript">
    function SetPCP() {
        if ($("#cbPCP").attr('checked') == 'checked') {
            var refd = $("#tbRefd").val();
            $("#tbRefDoctor").val(refd);
        }
    }
</script>
    <asp:Literal ID="javascript" runat="server"></asp:Literal>
<br /><br />
    <dct:tabs ID="tabComplaint" runat="server">
        <dct:Tab ID="PatientTab" runat="server" HeaderText= "Add">
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderStyle">Patient Number:</td>
                    <td>
                        <asp:HiddenField ID="hdnPatientID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnPatientNumber" runat="server" value="-1-1"></asp:HiddenField>
                        <asp:TextBox ID="tbPatientNumber" MaxLength="20" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="tbPatientNumber" ErrorMessage="Patient Number is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Name:</td>
                    <td>
                        <asp:DropDownList ID="ddlGreeting" runat="server">
                            <asp:ListItem Value=""></asp:ListItem> 
                            <asp:ListItem Value="Mr.">Mr.</asp:ListItem> 
                            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem> 
                            <asp:ListItem Value="Ms.">Ms.</asp:ListItem> 
                            <asp:ListItem Value="Dr.">Dr.</asp:ListItem> 
                        </asp:DropDownList>
                        <asp:TextBox ID="tbFirstName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:TextBox ID="tbMiddleName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:TextBox ID="tbLastName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Nick Name:</td>
                    <td>
                        <asp:TextBox ID="tbNickName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Date Of Birth:</td>
                    <td>
                        <dct:ExamMulti ID="dob" ControlType="Date" EnableValidators="false" ShowValidators="false" runat="server" ErrorText="Date Of Birth"></dct:ExamMulti>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Sex:</td>
                    <td>
                        <asp:DropDownList id="ddlSex" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Grade Level/Occupation:</td>
                    <td>
                        <asp:TextBox ID="tbOccupation" MaxLength="30" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Hx From:</td>
                    <td>
                        <asp:DropDownList id="ddlHxFrom" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="tbHxOther" MaxLength="30" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Ref'd By:</td>
                    <td class="labelHeaderStyle">
                        <asp:TextBox ID="tbRefd" MaxLength="30" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                        (<b>PCP:</b><asp:TextBox ID="tbRefDoctor" MaxLength="30" runat="server" SkinID="skintxtXMedium"></asp:TextBox>)
                        <asp:CheckBox ID="cbPCP" runat="server" onclick="SetPCP()" /> Same
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Allergies:</td>
                    <td>
                        <asp:TextBox ID="tbAllergies" MaxLength="50" runat="server" SkinID="skintxtMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Medications:</td>
                    <td>
                        <asp:TextBox ID="tbMedications" runat="server" SkinID="skinMultiLine" Rows="3" TextMode="MultiLine" Width="250" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Premature Birth:</td>
                    <td>
                        <asp:CheckBox ID="cbPrematureBirth" runat="server" />
                    </td>
                </tr>
            </table>
            </fieldset>
             <table>
              <tr>
                <td>
                </td>
                </tr>
             </table>
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" SkinID="skinBtn"
                            onclick="btnSave_Click"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnReset" CausesValidation="false"
                            OnClientClick="return ResetDiv('PatientTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                            <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
                    </td>
                    <td>
                            <asp:Button ID="btnFileManager"  Text="Documents" SkinID="skinBtn"  CausesValidation="false" runat="server"
                                OnClientClick="return RedirectToFileManager();" />

                        </td>
                </tr>
            </table>
            <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlResult" runat="server" Visible="false">
             <table>
              <tr>
                <td>
                    <asp:Label ID="result" runat="server" Text="" SkinID="lblHeader" Width="500" Visible="false"></asp:Label>
                </td>
              </tr>
              <tr>
                <td>
                    <asp:Button ID="btnExamInfant" runat="server" OnClientClick="return false;" CausesValidation="false" Text="EXAM NOTES" SkinID="skinBtn"></asp:Button>
                    <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH PATIENT" SkinID="skinBtn"></asp:Button> 
                    <asp:Button ID="btnClose" runat="server" OnClientClick="return SetParent()" CausesValidation="false" Text="CLOSE" SkinID="skinBtn" Visible="false"></asp:Button>
                </td>
              </tr>
             </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
