<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" Title="Dashboard" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabDashboard" runat="server">
        <dct:Tab ID="DashboardTab" runat="server" HeaderText= "Dashboard">
            <table>
                <tr>
                    <td>
                        <b>Doctor:</b>
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlDoctor" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <b>Schedule Date:</b>
                    </td>
                    <td><dct:ExamMulti ID="scheduleDate" ControlType="Date" EnableValidators="false" ShowValidators="false" runat="server" ErrorText="Schedule Date"></dct:ExamMulti>
                    </td>
                    <td>
                        <asp:Button ID="btnSchedule" runat="server" Text="APPOINTMENTS" SkinID="skinBtn"
                            onclick="btnSchedule_Click"></asp:Button>
                            <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
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



            <dct:ExamPanel id="pnlSchedule" runat="server" HeaderText="Appointments">
            <asp:Label ID="AppointmentStatus" runat="server" Text="No appointments" SkinID="lblHeader" Width="500" Visible="true"></asp:Label>
                <asp:GridView ID="ScheduleGrid" runat="server" AutoGenerateColumns="false" DataKeyNames="ScheduleID"
                            ClientIDMode="AutoID" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="Patient Name" DataField="PatientName"></asp:BoundField>
                    <asp:BoundField HeaderText="Schedule Date" DataField="ScheduleDate" DataFormatString="{0:d}"></asp:BoundField>
                    <asp:BoundField HeaderText="Schedule Time" DataField="ScheduleDate" DataFormatString="{0:t}"></asp:BoundField>
                    <asp:BoundField HeaderText="Doctor" DataField="DoctorName"></asp:BoundField>
                    <asp:HyperLinkField HeaderText="" Text="Exam Notes" DataNavigateUrlFields="ScheduleID, PatientID" DataNavigateUrlFormatString="ExamPatient.aspx?PatientID={1}&ScheduleID={0}" />
                    <asp:HyperLinkField HeaderText="" Text="View/Edit Patient" DataNavigateUrlFields="PatientID" DataNavigateUrlFormatString="Patient.aspx?PatientID={0}" />
                    <asp:HyperLinkField HeaderText="" Text="View History" DataNavigateUrlFields="PatientID" DataNavigateUrlFormatString="History.aspx?PatientID={0}" />
                    <asp:HyperLinkField HeaderText="" Text="Schedule" DataNavigateUrlFields="ScheduleID, PatientID" DataNavigateUrlFormatString="Schedule.aspx?ScheduleID={0}&PatientID={1}" />
                </Columns>
                </asp:GridView> 
            </dct:ExamPanel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>


