<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="UserEdit" Title="USERS" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabComplaint" runat="server">
        <dct:Tab ID="UserTab" runat="server" HeaderText= "Add">
            <asp:Panel ID="pnlMain" runat="server">
            <table>
                <tr>
                    <td class="labelHeaderStyle">FirstName:</td>
                    <td>
                        <asp:HiddenField ID="hdnUserID" runat="server"></asp:HiddenField>
                        <asp:TextBox ID="tbFirstName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="tbFirstName" ErrorMessage="First Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Last Name:</td>
                    <td>
                        <asp:TextBox ID="tbLastName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="tbLastName" ErrorMessage="Last Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">User Name:</td>
                    <td>
                        <asp:TextBox ID="tbUserName" MaxLength="20" runat="server" SkinID="skintxtSmall"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="tbUserName" ErrorMessage="User Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Password:</td>
                    <td>
                        <asp:TextBox ID="tbPassword" MaxLength="20" runat="server" SkinID="skintxtSmall" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="tbPassword" ErrorMessage="Password is required" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" SkinID="skinBtn"
                            OnClientClick="location.href='User.aspx';return false;"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnReset" CausesValidation="false"
                            OnClientClick="return ResetDiv('UserTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                            <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
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
                    <asp:Button ID="btnUser" runat="server" OnClientClick="location.href='User.aspx';return false;" CausesValidation="false" Text="USER LIST" SkinID="skinBtn"></asp:Button>
                    <asp:Button ID="btnHome" runat="server" OnClientClick="location.href='default.aspx';return false;" CausesValidation="false" Text="SEARCH PATIENT" SkinID="skinBtn"></asp:Button> 
                </td>
              </tr>
             </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
