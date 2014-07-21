<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Login"%>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabLogin" runat="server" HideTab="true">
        <dct:Tab ID="LoginTab" runat="server" HeaderText= "Login">
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
            <table>
                <tr>
                    <td class="labelHeaderStyle">User Name:</td>
                    <td style="width: 255px">
                        <asp:TextBox ID="tbUserName" MaxLength="20" runat="server" 
                            SkinID="skintxtSmall" Width="218px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="tbUserName" ErrorMessage="User Name is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="labelHeaderStyle">Password:</td>
                    <td style="width: 255px">
                        <asp:TextBox ID="tbPassword" MaxLength="20" runat="server" 
                            SkinID="skintxtSmall" TextMode="Password" Width="218px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="tbPassword" ErrorMessage="Password is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            </fieldset>
            <table cellspacing="5px">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="SUBMIT" SkinID="skinBtn"
                            onclick="btnSubmit_Click"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="btnReset" CausesValidation="false"
                            OnClientClick="return ResetDiv('LoginTab')" runat="server" Text="RESET"
                            SkinID="skinBtn"></asp:Button>
                            <asp:ValidationSummary ID="vsummary" runat="server" ShowMessageBox="True" ShowSummary="false" HeaderText=""/>
                    </td>
                </tr>
            </table>
            <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
