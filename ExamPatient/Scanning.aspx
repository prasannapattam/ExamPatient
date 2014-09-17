<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scanning.aspx.cs" Inherits="Scanning" Title="Scan" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/scan-style.css" type="text/css" rel="stylesheet" />

    <dct:tabs ID="tabFileManager" runat="server">
        <dct:Tab ID="FileManagerTab" runat="server" HeaderText= "Documents">
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
                <div>

    <div class="DWTPage body_Broad_width">
        <div class="DWTBody" >
            <!--This is where Dynamic Web TWAIN control will be rendered.-->
            <div id="dwtcontrolContainer" class="DWTContainer"></div>

            <!--This is where you add the actual buttons to control the component.-->
            <div class="ScanWrapper" >
                <div class="divTableStyle" style="text-align:center;">
                        <input class="DWTScanButton btn" type="button" value="Scan" onclick="acquireImage();" /> </div>
                <div style="height:15px;"></div>
                <div id="divSave" class="divTableStyle" >
                <ul>
                    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Upload Image</b></li>
                    <li>
                        <table>
                            <tr style="display:none">
                                <td><label>HTTP Server:</label></td>
                                <td><input type="text" size="20" id="txtHTTPServer" /></td>
                            </tr>
                            <tr style="display:none">
                                <td><label>HTTP Port:</label></td>
                                <td><input type="text" size="20" id="txtHTTPPort" /></td>
                            </tr>
                            <tr style="display:none">
                                <td><label>User Name:</label></td>
                                <td><input type="text" size="20" id="txtUserName" /></td>
                            </tr>
                            <tr style="display:none">
                                <td><label>Password:</label></td>
                                <td><input type="text" size="20" id="txtPassword" /></td>
                            </tr>
                            <tr>
                                <td><label>Action Page: <% = "~/SaveToFile.aspx1" %></label></td>
                                <td><input type="text" size="20" id="txtActionPage" value="<% = "~/SaveToFile.aspx1" %>" /></td>
                            </tr>
                             <tr>
                                <td><label>File Name:</label></td>
                                <td><input type="text" size="20" id="txtFileName" /></td>
                            </tr>
                        </table>
                    </li>
                    <li>
	                    <label for="imgTypejpeg" style="display: inline;">
		                    <input type="radio" value="jpg" name="ImageType" id="imgTypejpeg" onclick ="rd_onclick();"/>JPEG</label>
	                    <label for="imgTypetiff" style="display: inline;">
		                    <input type="radio" value="tif" name="ImageType" id="imgTypetiff" onclick ="rdTIFF_onclick();"/>TIFF</label>
	                    <label for="imgTypepng" style="display: inline;">
		                    <input type="radio" value="png" name="ImageType" id="imgTypepng" onclick ="rd_onclick();"/>PNG</label>
	                    <label for="imgTypepdf" style="display: inline;">
		                    <input type="radio" value="pdf" name="ImageType" id="imgTypepdf" onclick ="rdPDF_onclick();"/>PDF</label></li>
                    <li style="padding-left:9px;">
                        <label for="MultiPageTIFF" style="display: inline;"><input type="checkbox" id="MultiPageTIFF"/>Multi-Page TIFF</label>
                        <label for="MultiPagePDF" style="display: inline;"><input type="checkbox" id="MultiPagePDF"/>Multi-Page PDF </label></li>
                </ul>
                <input id="btnUpload" class="DWTScanButton btn" type="button" value="Upload Image" onclick ="btnUpload_onclick()"/>
                </div>
            </div>
            <div class="body_clr"></div>
        </div>
        
    </div>
    <script src="Scripts/dynamsoft.webtwain.initiate.js"></script>
    <script src="Scripts/DWTSample_ScanAndUpload.js"></script>
    <script>
        (function () {
            onPageLoad();
        })(); 
    </script>
                </div>
            </fieldset>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>

