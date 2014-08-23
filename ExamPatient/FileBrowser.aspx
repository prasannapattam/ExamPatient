<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileBrowser.aspx.cs" Inherits="FileBrowser" %>



<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<%@ Register TagPrefix="fb" TagName="FileBrowser" Src= "~/UserControls/FileBrowser.ascx" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
  Namespace="AjaxControlToolkit" TagPrefix="aspa" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabFB" runat="server" HideTab="true">
        <dct:Tab ID="FbTab" runat="server" HeaderText= "Login">
                <asp:HiddenField ID="hdnPatientID" runat="server"></asp:HiddenField>
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
                <table>
                    <tr>
                        <aspa:ToolkitScriptManager ID="ScriptManager1" runat="server">

                        </aspa:ToolkitScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                    <div>
                                        <fb:FileBrowser ID="FileBrowser1" runat="server" OnSelectedFileChanged="FileBrowser1_SelectedFileChanged"  OnSelectedFolderChanged="FileBrowser1_SelectedFolderChanged" />
                                    </div>
                              </ContentTemplate>
                        </asp:UpdatePanel>
                        <div>      
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FileBrowser1" EventName="SelectedFileChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="height: 100px;vertical-align:middle;background-color:#CEDEFF;" >
                                        <table>
                                            <tr>
                                        <b></b>Current Folder:</b> <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                </tr>
                                            <tr>
                                        <Br />  <b>Selected file: </b> <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            </tr>
                                            </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </tr>
                    <asp:Button ID="btnEditPatient" CausesValidation="false"
                                OnClientClick="return RedirectPatient('patient');" runat="server" Text="Scan File"
                                SkinID="skinBtn"></asp:Button>
                  <tr>
                        <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500" Visible="false"></asp:Label>
                    </tr>
                </table>
            </fieldset>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>