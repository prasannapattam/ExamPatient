<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCorrect.aspx.cs" Inherits="AutoCorrect" Title="" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript">
    function DeleteAC() {
        var mesg = 'Please confirm to delete \'' + $('#patientID').val() + '\'';
        var ret = confirm(mesg);
        return ret;
    }
    
</script>
   <dct:tabs ID="tabAutoCorrect" runat="server">
        <dct:Tab ID="AutoCorrectTab" runat="server" HeaderText= "Auto Correct List">
            <table cellpadding="0" cellspacing="0"><tr><td>
                <asp:Label ID="result" runat="server" Text="" SkinID="lblHeader" Width="500"></asp:Label>
            </td></tr></table>
             &nbsp;<b>Doctor:</b>&nbsp;&nbsp; <asp:DropDownList ID="ddlFilter" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

            <asp:Panel ID="pnlGrid" runat="server">
                <asp:GridView ID="AutoCorrectResults" runat="server" AutoGenerateColumns="false" 
                            onrowdatabound="AutoCorrectResults_RowDataBound" ClientIDMode="AutoID"
                                AlternatingRowStyle-CssClass="gridRowAlternate" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                                AllowPaging="true" datasourceid="AutoCorrectSource" PageSize="25"
                            >
                <Columns>
                    <asp:BoundField HeaderText="Name" DataField="Name"></asp:BoundField>
                    <asp:BoundField HeaderText="Value" DataField="Value"></asp:BoundField>
                </Columns>
                </asp:GridView> 
                <asp:HiddenField ID="patientID" runat="server"></asp:HiddenField>
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <asp:Label ID="resultError" runat="server" Text="No Auto Correct records found" SkinID="skinError" Width="500"></asp:Label>
                    <br /><br />
                </asp:Panel>
                <asp:Panel ID="ButtonPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnEditAC" runat="server" Text="EDIT" SkinID="skinBtn" OnClick="btnEdit_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAddAC" runat="server" Text="ADD" SkinID="skinBtn" OnClick="btnAdd_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnDeleteAC" CausesValidation="false"
                                OnClientClick="return DeleteAC();" OnClick="btnDelete_Click" runat="server" Text="DELETE"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnCopy" runat="server" Text="Copy from Existing" SkinID="skinBtn" OnClick="btnCopy_Click"  style="width: 140px;background-size: 140px 30px;background-repeat: no-repeat;"></asp:Button>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderStyle">Name:</td>
                    <td>
                        <asp:TextBox ID="tbName" MaxLength="50" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbName" ErrorMessage="Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Value:</td>
                    <td>
                        <asp:TextBox ID="tbValue" MaxLength="200" runat="server" SkinID="skintxtLarge"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="tbValue" ErrorMessage="Value is required" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                            OnClientClick="return ResetDiv('pnlEdit')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                        <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="CANCEL" 
                            SkinID="skinBtn" onclick="btnCancel_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlCopy" runat="server" Visible="false">
                <br />
                Copy from : <asp:DropDownList ID="ddlDoctors" runat="server"></asp:DropDownList>
                <br /><br />
                <div style="color:red"> Note: Copying will delete existing autocorrect values (if any)</div>
                <br />
                 
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnCopySave" runat="server" Text="COPY" SkinID="skinBtn"
                            onclick="btnCopySave_Click"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnCopyCancel" CausesValidation="false" runat="server" Text="CANCEL" 
                            SkinID="skinBtn" onclick="btnCancel_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbCopyError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 

    <asp:sqldatasource id="AutoCorrectSource"
    selectcommand="Select [Name], [Value] From [AutoCorrect] WHERE UserName = @UserName ORDER BY [Name]"
    connectionstring="<%$ ConnectionStrings:EPConnectionString%>" 
    runat="server">
     <SelectParameters>
            <asp:Parameter Name="UserName"  />
     </SelectParameters>
     </asp:sqldatasource>

</asp:Content>
