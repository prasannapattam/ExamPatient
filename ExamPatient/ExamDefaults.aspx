<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamDefaults.aspx.cs" Inherits="ExamDefaults" Title="Notes Defaults" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
        function ConfirmDelete() {
        var ret = confirm('Please confirm to delete the selected default');
        return ret;
    }
</script>

    <dct:tabs ID="tabDefaults" runat="server" HideTab="false">
        <dct:Tab ID="DefaultsTab" runat="server" HeaderText= "Defaults">
            <dct:ExamPanel id="pnlSearch" runat="server" HeaderText="Defaults" Visible="false">
            </dct:ExamPanel>            
                <asp:GridView ID="DefaultResults" runat="server" AutoGenerateColumns="false" DataKeyNames="ExamDefaultID"
                            onrowdatabound="DefaultResults_RowDataBound" ClientIDMode="AutoID"
                            AlternatingRowStyle-CssClass="gridRowAlternate" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="DefaultName" DataField="DefaultName"></asp:BoundField>
                    <asp:BoundField HeaderText="Age Start (months)" DataField="AgeStart" Visible="false"></asp:BoundField>
                    <asp:BoundField HeaderText="Age End (months)" DataField="AgeEnd" Visible="false"></asp:BoundField>
                    <asp:BoundField HeaderText="Premature Birth" DataField="PrematureBirth" Visible="false"></asp:BoundField>
                    <asp:BoundField HeaderText="Doctor" DataField="DoctorName" Visible="false"></asp:BoundField>
                </Columns>
                </asp:GridView> 
                <asp:HiddenField ID="patientID" runat="server"></asp:HiddenField>
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <asp:Label ID="resultError" runat="server" Text="No Defaults found" SkinID="skinError" Width="500"></asp:Label>
                    <br /><br />
                </asp:Panel>
                <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnEditDefault" runat="server" Text="VIEW/EDIT" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('default', 1);" ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAddDefault" CausesValidation="false"
                                OnClientClick="location.href='ExamPatient.aspx?ExamDefaultID=0';return false;" runat="server" Text="ADD DEFAULT"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnDeleteDefault" CausesValidation="false" OnClick="btnDeleteDefault_Click"
                                OnClientClick="return ConfirmDelete();" runat="server" Text="DELETE"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
