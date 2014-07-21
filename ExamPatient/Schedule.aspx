<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Schedule" Title="Schedule Patient" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script>

    function ConfirmCancel(scheduleID, patientID, scheduleDate) {
        var response = confirm('Please confirm to cancel the appointment "' + scheduleDate + '"');
        if (response) {
            location.href = 'Schedule.aspx?ScheduleID=' + scheduleID + '&PatientID=' + patientID + '&Delete=1'
        }
        return false;
    }
</script>

    <dct:tabs ID="tabSchedule" runat="server" HideTab="false">
        <dct:Tab ID="ScheduleTab" runat="server" HeaderText= "SCHEDULE PATIENT">
            <asp:Panel ID="pnlResult" runat="server" Visible="true">
             <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    <asp:Label ID="result" runat="server" Text="" SkinID="lblHeader" Width="500"></asp:Label>
                </td>
              </tr>
             </table>
            </asp:Panel>
            
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderStyle">Patient Name:</td>
                    <td>
                        <asp:HiddenField ID="hdnPatientID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdnScheduleID" runat="server" />
                        <asp:Label ID="lbPatientName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Doctor:</td>
                    <td>
                        <asp:DropDownList ID="ddlDoctor" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlDoctor" ErrorMessage="Doctor is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Schedule Date:</td>
                    <td>
                        <dct:ExamMulti ID="scheduleDate" ControlType="Date" EnableValidators="false" ShowValidators="false" runat="server" ErrorText="Schedule Date"></dct:ExamMulti>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Schedule Time:</td>
                    <td>
                        <dct:ExamMulti ID="scheduleTime" ControlType="Time" EnableValidators="false" ShowValidators="false" runat="server" ErrorText="Schedule Date"></dct:ExamMulti>
                    </td>
                </tr>
            </table>
            </fieldset>
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" SkinID="skinBtn"
                            onclick="btnSave_Click"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnReset" CausesValidation="false"
                            OnClientClick="return ResetDiv('ScheduleTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                            <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
                    </td>
                    <td>
                        <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH PATIENT" SkinID="skinBtn"></asp:Button> 
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlError" runat="server" Visible="false">
             <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500"></asp:Label>
                </td>
              </tr>
             </table>
            </asp:Panel>
            
            
            
            </asp:Panel>
            
            <dct:ExamPanel id="pnlSchedule" runat="server" HeaderText="Active Appointments">
                <asp:Label ID="AppointmentStatus" runat="server" Text="No appointments" SkinID="lblHeader" Width="500" Visible="true"></asp:Label>
                <asp:GridView ID="ScheduleGrid" runat="server" AutoGenerateColumns="false" DataKeyNames="ScheduleID"
                            ClientIDMode="AutoID" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="Patient Name" DataField="PatientName"></asp:BoundField>
                    <asp:BoundField HeaderText="Schedule Date" DataField="ScheduleDate" DataFormatString="{0:d}"></asp:BoundField>
                    <asp:BoundField HeaderText="Schedule Time" DataField="ScheduleDate" DataFormatString="{0:t}"></asp:BoundField>
                    <asp:BoundField HeaderText="Doctor" DataField="DoctorName"></asp:BoundField>
                    <asp:HyperLinkField HeaderText="" Text="Edit" DataNavigateUrlFields="ScheduleID, PatientID" DataNavigateUrlFormatString="Schedule.aspx?ScheduleID={0}&PatientID={1}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="#" onclick="return ConfirmCancel(<%# Eval("ScheduleID")%>, <%# Eval("PatientID")%>, '<%# Eval("ScheduleDate", "{0:g}")%>')">Cancel</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView> 
            </dct:ExamPanel>

        </dct:Tab>
    </dct:tabs> 
</asp:Content>
