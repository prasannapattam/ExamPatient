<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" Title="HISTORY" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabHistory" runat="server" HideTab="true">
        <dct:Tab ID="HistoryTab" runat="server" HeaderText= "EXAM HISTORY">
            <dct:ExamPanel id="pnlHistory" runat="server" HeaderText="History for ">
                <asp:GridView ID="HistoryResults" runat="server" AutoGenerateColumns="false" DataKeyNames="ExamID"
                            onrowdatabound="HistoryResults_RowDataBound" ClientIDMode="AutoID"
                            AlternatingRowStyle-CssClass="gridRowAlternate" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="Exam Date" DataField="ExamDate" DataFormatString="{0:d}"></asp:BoundField>
                </Columns>
                </asp:GridView> 
                <asp:HiddenField ID="hdnPatientID" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="patientID" runat="server"></asp:HiddenField>
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <asp:Label ID="resultError" runat="server" Text="No History found" SkinID="skinError" Width="500"></asp:Label>
                    <br />
                    <br /><br />
                    <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH PATIENT" SkinID="skinBtn"></asp:Button> 
                </asp:Panel>
                <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnCorrect" runat="server" Text="CORRECT" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('correct', 1);" ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnReport" runat="server" Text="LETTER" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('report', 1);" ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="VIEW/PRINT" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('print', 1);" Visible="false"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnPrintExam" runat="server" Text="VIEW/PRINT" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('printexam', 1);" ></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </dct:ExamPanel>            
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
