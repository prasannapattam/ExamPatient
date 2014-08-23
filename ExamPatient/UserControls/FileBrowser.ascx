<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileBrowser.ascx.cs" Inherits="FileBrowser" %>

<link href="../Stylesheets/FileBrowser.css" rel="stylesheet" type="text/css" />
<div id="browserContent" >
  <table id="tableMainContent" runat="server" class="mainContent" cellspacing="2" cellpadding="2">
    <tr style="text-align: left; vertical-align: top;">
      <td class="folderColumn">
        <div id="folderList" runat="server" class="folderPanel">
          <table id="tableFolders" style="width:230px; " cellspacing="0" cellpadding="0" runat="server">
            <tr id="trHeader" class="fileListHeader" runat="server">
              <td id="tdFolderHeader1" class="folderListHeaderCell1" runat="server">
                <asp:LinkButton ID="Button_Parent" runat="server" OnClick="Button_Parent_Click" Text="[..]" />
              </td>
              <td id="tdFolderHeader2" class="folderListHeaderCell2" runat="server">
              </td>
            </tr>
          </table>
          <asp:Panel ID="Panel_FolderList" runat="server" Height="284px" ScrollBars="Auto">
            <asp:ListView ID="ListView_Folders" runat="server" OnSelectedIndexChanging="ListView_Folders_SelectedIndexChanging"
              ConvertEmptyStringToNull="False" EnableModelValidation="True">
              <LayoutTemplate>
                <table id="tblFolders" runat="server" class="folderList" cellspacing="0" cellpadding="0">
                  <tr runat="server" id="itemPlaceholder" />
                </table>
              </LayoutTemplate>
              <ItemTemplate>
                <tr id="Tr1" class="rowUnselected" runat="server">
                  <td>
                    <asp:LinkButton ID="Button_Name" runat="server" CommandName="Select" ToolTip='<%#Eval("FullName") %>'
                      Text='<%#Eval("Name") %>' />
                  </td>
                </tr>
              </ItemTemplate>
              <SelectedItemTemplate>
                <tr id="Tr2" class="rowSelected" runat="server">
                  <td>
                    <asp:LinkButton ID="Button_Name" runat="server" CommandName="Select" ToolTip='<%#Eval("FullName") %>'
                      Text='<%#Eval("Name") %>' />
                  </td>
                </tr>
              </SelectedItemTemplate>
              <EmptyItemTemplate>
                <td>
                  <asp:LinkButton ID="Button_Parent" runat="server" OnClick="Button_Parent_Click" Text="[..]" />
                </td>
              </EmptyItemTemplate>
            </asp:ListView>
          </asp:Panel>
        </div>
      </td>
      <td class="filesColumn">
        <div id="fileList" runat="server" class="filesPanel">
        <table id="Table_Attr" cellspacing="0" cellpadding="0" runat="server">
          <tr id="Tr2" class="fileListHeader" runat="server">
            <td id="tdFilesHeader1" runat="server" class="fileListHeaderCell1">
              <asp:Label ID="Label_Name" runat="server" Text="Name" />
            </td>
            <td id="tdFilesHeader2" class="fileListHeaderCell2" runat="server">
              <asp:Label ID="Label_Size" runat="server" Text="Size" />
            </td>
            <td id="tdFilesHeader3" class="fileListHeaderCell3" runat="server">
              <asp:Label ID="Label_LastWriteTimeUtc" runat="server" Text="LastChanged" />
            </td>
          </tr>
        </table>
        <asp:Panel ID="Panel_FileList" runat="server" Height="284px" ScrollBars="Auto">
          <asp:ListView ID="ListView_Files" runat="server" OnSelectedIndexChanging="ListView_Files_SelectedIndexChanging"
            ConvertEmptyStringToNull="False" EnableModelValidation="True">
            <LayoutTemplate>
              <table class="folderList" cellspacing="0" cellpadding="0" runat="server" id="tblFiles">
                <tr runat="server" id="itemPlaceholder" />
              </table>
            </LayoutTemplate>
            <ItemTemplate>
              <tr id="Tr1" class="fileUnselected" runat="server">
                <td style="width: 342px;">
                  <asp:LinkButton ID="Button_Name" runat="server" ToolTip='<%#Eval("FullName") %>'
                    CommandName="Select" Text='<%#Eval("Name") %>' />
                </td>
                <td style="width: 80px;">
                  <asp:Label ID="Label_Size" runat="server" Text='<%#Eval("Length") %>' />
                </td>
                <td style="width: 130px;">
                  <asp:Label ID="Label_LastWriteTimeUtc" runat="server" Text='<%#Eval("LastWriteTimeUtc") %>' />
                </td>
              </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
              <tr id="Tr2" class="fileSelected" runat="server">
                <td style="width: 342px;">
                  <asp:LinkButton ID="Button_Name" runat="server" ToolTip='<%#Eval("FullName") %>'
                    CommandName="Select" Text='<%#Eval("Name") %>' />
                </td>
                <td style="width: 80px;">
                  <asp:Label ID="Label_Size" runat="server" Text='<%#Eval("Length") %>' />
                </td>
                <td style="width: 130px;">
                  <asp:Label ID="Label_LastWriteTimeUtc" runat="server" Text='<%#Eval("LastWriteTimeUtc") %>' />
                </td>
              </tr>
            </SelectedItemTemplate>
            <EmptyItemTemplate>
              <td id="Td1" runat="server">
              </td>
            </EmptyItemTemplate>
          </asp:ListView>
        </asp:Panel>
        </div>
      </td>
    </tr>
  </table>
</div>