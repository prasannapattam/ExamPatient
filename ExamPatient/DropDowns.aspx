<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DropDowns.aspx.cs" Inherits="DropDowns"  Title="Drop Down Values" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabDropDowns" runat="server" HideTab="false">
        <dct:Tab ID="DropDownsTab" runat="server" HeaderText= "Drop Down">
            <dct:ExamPanel id="pnlSearch" runat="server" HeaderText="Drop Downs" Visible="false">
            </dct:ExamPanel>            
            <br />
                Control Name: <asp:DropDownList ID="ControlList" runat="server" OnSelectedIndexChanged="ControlList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <asp:GridView ID="DDResults" runat="server" AutoGenerateColumns="false"
                             ClientIDMode="AutoID" RowStyle-CssClass="gridRow" CssClass="gridAll" SelectedRowStyle-CssClass="gridSelectedRow"
                            >
                <Columns>
                    <asp:TemplateField HeaderText = "Field Value">
                        <ItemTemplate>
                            <asp:HiddenField ID="LookUpID" runat="server" Value='<%# Eval("LookUpID") %>' />
                            <asp:TextBox ID="FieldValue" runat="server" SkinID="skintxtMedium" Text='<%# Eval("FieldValue") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "Field Description (used in letter)">
                        <ItemTemplate>
                            <asp:TextBox ID="FieldDescription" runat="server" SkinID="skintxtLarge" Text='<%# Eval("FieldDescription") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "Sort Order">
                        <ItemTemplate>
                            <asp:TextBox ID="SortOrder" runat="server" SkinID="skintxtTiny" Text='<%# Eval("SortOrder") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView> 
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500"></asp:Label>
                    <br /><br />
                </asp:Panel>
                <asp:Panel ID="pnlResult" runat="server" Visible="false">
                    <asp:Label ID="result" runat="server" Text="Drop Down values sucessfully updated" SkinID="lblHeader" Width="500"></asp:Label>
                    <br /><br />
                </asp:Panel>
                <asp:Panel ID="ButtonsPanel" runat="server">
                <table cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="skinBtn"  OnClick="btnSave_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" CausesValidation="false" runat="server" Text="ADD" OnClick="btnAdd_Click" SkinID="skinBtn"></asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
