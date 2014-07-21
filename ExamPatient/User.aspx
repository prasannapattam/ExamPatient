<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" Title="USERS LIST"%>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabUsers" runat="server" HideTab="false">
        <dct:Tab ID="UserTab" runat="server" HeaderText= "Users">
            <dct:ExamPanel id="pnlSearch" runat="server" HeaderText="Users" Visible="false">
            </dct:ExamPanel>            
                <asp:GridView ID="UserResults" runat="server" AutoGenerateColumns="false" DataKeyNames="UserID"
                            onrowdatabound="UserResults_RowDataBound" ClientIDMode="AutoID"
                                RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:BoundField HeaderText="First Name" DataField="FirstName"></asp:BoundField>
                    <asp:BoundField HeaderText="Last Name" DataField="LastName"></asp:BoundField>
                    <asp:BoundField HeaderText="User Name" DataField="UserName"></asp:BoundField>
                </Columns>
                </asp:GridView> 
                <asp:HiddenField ID="patientID" runat="server"></asp:HiddenField>
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <asp:Label ID="resultError" runat="server" Text="No Users found" SkinID="skinError" Width="500"></asp:Label>
                    <br /><br />
                </asp:Panel>
                <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnEditUser" runat="server" Text="VIEW/EDIT USER" SkinID="skinBtn" 
                                OnClientClick="return RedirectPatient('user', 1);" ></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAddUser" CausesValidation="false"
                                OnClientClick="location.href='UserEdit.aspx';return false;" runat="server" Text="ADD USER"
                                SkinID="skinBtn"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
