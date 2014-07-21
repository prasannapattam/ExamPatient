<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintQueue.aspx.cs" Inherits="PrintQueue" Title="Print Queue" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabPrint" runat="server">
        <dct:Tab ID="PrintTab" runat="server" HeaderText= "Print Queue">
            
            <br />
            &nbsp;<b>Doctor:</b>&nbsp;&nbsp; <asp:DropDownList ID="ddlFilter" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

            <asp:GridView ID="PrintGrid" runat="server" AutoGenerateColumns="false" DataKeyNames="PrintQueueID"
                        ClientIDMode="AutoID" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                        onrowdatabound="PrintGrid_RowDataBound" 
                        >
            <Columns>
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Patient Name" DataField="PatientName"></asp:BoundField>
                <asp:BoundField HeaderText="Exam Date" DataField="ExamDate" DataFormatString="{0:d}"></asp:BoundField>
                <asp:BoundField HeaderText="Doctor" DataField="DoctorName"></asp:BoundField>
                <asp:HyperLinkField HeaderText="Letter" Text="Letter" DataNavigateUrlFields="ExamID" DataNavigateUrlFormatString="Report.aspx?ExamID={0}" DataTextFormatString="{0:d}" />
                <asp:BoundField HeaderText="TimeStamp" DataField="LastUpdatedDate"></asp:BoundField>
            </Columns>
            </asp:GridView> 

            <asp:Panel ID="ErrorPanel" runat="server" Visible="false">
                <asp:Label ID="resultError" runat="server" Text="No patient notes to print" SkinID="skinError" Width="500"></asp:Label>
                <br />
                <br /><br />
                <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="Home" SkinID="skinBtn"></asp:Button> 
            </asp:Panel>

            <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnProcess" runat="server" Text="Remove" SkinID="skinBtn"
                                onclick="btnProcess_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnReset" CausesValidation="false"
                                OnClientClick="return ResetDiv('PrintTab')" runat="server" Text="RESET"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="ResultPanel" runat="server" Visible="false">
                <p>
                <br />
                <br />
                <asp:Label ID="result" runat="server" Text="Selected print queue records removed successfully" SkinID="lblHeader" Width="800"></asp:Label>
                <br /><br />
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnHome2" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="Home" SkinID="skinBtn"></asp:Button> 
                        </td>
                        <td>
                            <asp:Button ID="btnPrintQueue" runat="server" OnClientClick="location.href='PrintQueue.aspx';return false;" CausesValidation="false" Text="Print Queue" SkinID="skinBtn"></asp:Button> 
                        </td>
                    </tr>
                </table>
            </p>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>

