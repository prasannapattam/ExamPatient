<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="PATIENT"%>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabPatients" runat="server" HideTab="true">
        <dct:Tab ID="PatientTab" runat="server" HeaderText= "SEARCH">
            <dct:ExamPanel id="pnlSearch" runat="server" HeaderText="Search Patient">
            <fieldset style="background-color:#CEDEFF;">
            <table>
                <tr>
                    <td class="labelHeaderStyle">Patient Number:</td>
                    <td>
                        <asp:TextBox ID="tbPatientNumber" MaxLength="20" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">First Name:</td>
                    <td>
                        <asp:TextBox ID="tbFirstName" MaxLength="20" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Last Name:</td>
                    <td>
                        <asp:TextBox ID="tbLastName" MaxLength="20" runat="server" SkinID="skintxtXMedium"></asp:TextBox>
                    </td>
                </tr>
                </table>
            </fieldset>
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" SkinID="skinBtn" 
                                onclick="btnSearch_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAddPatient" CausesValidation="false"
                                OnClientClick="location.href='Patient.aspx'; return false;" runat="server" Text="ADD PATIENT"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                    </tr>
                </table>
            </dct:ExamPanel>
            
            <dct:ExamPanel ID="pnlResults" runat="server" Visible="false" HeaderText="Search Results">
                <asp:GridView ID="patientResults" runat="server" AutoGenerateColumns="false" DataKeyNames="PatientID"
                            onrowdatabound="patientResults_RowDataBound" ClientIDMode="AutoID"
                             AlternatingRowStyle-CssClass="gridRowAlternate" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="Patient Number" DataField="PatientNumber"></asp:BoundField>
                    <asp:BoundField HeaderText="Name" DataField="PatientName"></asp:BoundField>
                    <asp:BoundField HeaderText="DOB" DataField="DateOfBirth" DataFormatString="{0:d}"></asp:BoundField>
                </Columns>
                </asp:GridView> 
                <asp:HiddenField ID="patientID" runat="server"></asp:HiddenField>
                <asp:Label ID="resultError" runat="server" Text="No patients found" SkinID="skinError" Width="500" Visible="false"></asp:Label>
                <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnExam" runat="server" Text="EXAM NOTES" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('exam', 1);" ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnEditPatient" CausesValidation="false"
                                OnClientClick="return RedirectPatient('patient');" runat="server" Text="VIEW/EDIT"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnHistory" CausesValidation="false"
                                OnClientClick="return RedirectPatient('history');" runat="server" Text="VIEW HISTORY"
                                SkinID="skinBtn"></asp:Button>
                        </td>
<%--                        <td>
                            <asp:Button ID="btnSchedule" CausesValidation="false"
                                OnClientClick="return RedirectPatient('schedule');" runat="server" Text="SCHEDULE"
                                SkinID="skinBtn"></asp:Button>
                        </td>
--%>                    </tr>
                </table>
            </asp:Panel>
            </dct:ExamPanel>            
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
