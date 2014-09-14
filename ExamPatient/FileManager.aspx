<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="FileManager" Title="Documents" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>
<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabFileManager" runat="server">
        <dct:Tab ID="FileManagerTab" runat="server" HeaderText= "Documents">
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
                <div>
        <iz:FileManager ID="fmPatient" runat="server" Height="400px" Width="900px"
            ClientIDMode="AutoID"
            OnToolbarCommand="fmPatient_ToolbarCommand"
            HiddenFilesAndFoldersPrefix="_" ShowHiddenFilesAndFolders="true" HiddenFiles="config, ini">
            <CustomToolbarButtons>
                <iz:CustomToolbarButton Text="Scan" CommandName="CreateScan" ImageUrl="images/16x16/scanner.png" />
                <iz:CustomToolbarButton Text="Scan" PerformPostBack="false" OnClientClick="alert('Hello!')"
                    ImageUrl="images/16x16/scanner.png" />
            </CustomToolbarButtons>
            <Templates>
                <iz:NewDocumentTemplate Name="HTML Page" NewFileName="New HTML File" MasterFileUrl="Templates/HTMLPage.htm" />
                <iz:NewDocumentTemplate Name="JScript File" NewFileName="JScript" MasterFileUrl="Templates/JScript.js" />
                <iz:NewDocumentTemplate Name="Style Sheet" NewFileName="StyleSheet" MasterFileUrl="Templates/StyleSheet.css" />
            </Templates>
            <FileTypes>
                <iz:FileType Extensions="zip, rar, iso" Name="Archive" SmallImageUrl="images/16x16/compressed.gif"
                    LargeImageUrl="images/32x32/compressed.gif">
                </iz:FileType>
                <iz:FileType Extensions="doc, rtf" Name="Microsoft Word Document" SmallImageUrl="images/16x16/word.gif"
                    LargeImageUrl="images/32x32/word.gif">
                </iz:FileType>
                <iz:FileType Extensions="xls, csv" Name="Microsoft Excel Worksheet" SmallImageUrl="images/16x16/excel.gif"
                    LargeImageUrl="images/32x32/excel.gif">
                </iz:FileType>
                <iz:FileType Extensions="ppt, pps" Name="Microsoft PowerPoint Presentation" SmallImageUrl="images/16x16/PowerPoint.gif"
                    LargeImageUrl="images/32x32/PowerPoint.gif">
                </iz:FileType>
                <iz:FileType Extensions="gif, jpg, png" Name="Image" SmallImageUrl="images/16x16/image.gif"
                    LargeImageUrl="images/32x32/image.gif">
                </iz:FileType>
                <iz:FileType SmallImageUrl="images/16x16/media.gif" Name="Windows Media File" Extensions="mp3,wma,vmv,avi,divx"
                    LargeImageUrl="images/32x32/media.gif">
                </iz:FileType>
                <iz:FileType Extensions="txt" Name="Text Document">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="xml, xsl, xsd" Name="XML Document" LargeImageUrl="images/32x32/xml.gif"
                    SmallImageUrl="images/16x16/xml.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="css" Name="Cascading Style Sheet" LargeImageUrl="images/32x32/styleSheet.gif"
                    SmallImageUrl="images/16x16/styleSheet.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="js, vbs" Name="Script File" LargeImageUrl="images/32x32/script.gif"
                    SmallImageUrl="images/16x16/script.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
                <iz:FileType Extensions="htm, html" Name="HTML Document" LargeImageUrl="images/32x32/html.gif"
                    SmallImageUrl="images/16x16/html.gif">
                    <Commands>
                        <iz:FileManagerCommand Name="Edit with WYSWYG editor" CommandName="WYSWYG" />
                        <iz:FileManagerCommand Name="Edit" CommandName="EditText" SmallImageUrl="images/16x16/edit.gif" />
                    </Commands>
                </iz:FileType>
            </FileTypes>
        </iz:FileManager>
                </div>
            </fieldset>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>
