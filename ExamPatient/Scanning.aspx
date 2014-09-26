<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scanning.aspx.cs" Inherits="Scanning" Title="Scan" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/scan-style.css" type="text/css" rel="stylesheet" />
    <style>
        label {
            display: inline;
        }

    </style>

    <dct:tabs ID="tabFileManager" runat="server">
        <dct:Tab ID="FileManagerTab" runat="server" HeaderText= "Documents">
            <asp:Panel ID="pnlMain" runat="server">
            <fieldset style="background-color:#CEDEFF">
                <div>


<div id="container" class="DWTPage body_Broad_width" style="margin:0 auto;">

<div id="DWTcontainer" class="body_Broad_width">
<div class="Content_Left">
<div id="dwtcontrolContainer"></div>
<div id="DWTNonInstallContainerID" style="width:580px"></div>
<div id="DWTemessageContainer" style="clear: both;width:580px;"></div>
</div>
<div class="Content_Right">
<div id="ScanWrapper">
<div id="divScanner" class="divinput">
    <ul class="PCollapse">
        <li>
        <div class="divType"><div class="mark_arrow expanded"></div>Scan</div>
            <div id="div_ScanImage" class="divTableStyle">
                <ul id="ulScaneImageHIDE" >
                    <li style="padding-left: 15px;">
                        <label for="source">Select Source:</label>
                        <select size="1" id="source" style="position:relative;width: 220px;" onchange="source_onchange()">
                            <option value = ""></option>    
                        </select>
					</li>
					<li style="display:none;position:relative" id="pNoScanner">
                            <a href="javascript: void(0)" class="ShowtblLoadImage" style="color:red; background-color:#f0f0f0; position:relative" id="aNoScanner"><b>No TWAIN compatible drivers detected.</b></a>

<div id="tblLoadImage" style="display:none;height:80px">
<ul>
    <li><b>You can:</b><a href="javascript: void(0)" style="text-decoration:none; padding-left:200px" class="ClosetblLoadImage">X</a></li>
</ul>
<div id="notformac1" style="background-color:#f0f0f0; padding:5px;">
<ul>
    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Install a Virtual Scanner:</b></li>
    <li style="text-align:center;"><a id="samplesource32bit" href="http://www.dynamsoft.com/demo/DWT/Sources/twainds.win32.installer.2.1.3.msi">32-bit Sample Source</a>
        <a id="samplesource64bit" style="display:none;" href="http://www.dynamsoft.com/demo/DWT/Sources/twainds.win64.installer.2.1.3.msi">64-bit Sample Source</a>
        from <a href="http://www.twain.org">TWG</a></li>
</ul>
</div>
</div>
					</li>
					<li id="divProductDetail"></li>
                    <li style="text-align:center;">
                        <input id="btnScan" class="bigbutton" style="color:#C0C0C0;" disabled="disabled" type="button" value="Scan" onclick="acquireImage();"/>
					</li>
                </ul>
            </div>
        </li>  
<%--        <li>
        <div class="divType"><div class="mark_arrow collapsed"></div>Load the Sample Images</div>
        <div id="div_SampleImage" style="display: none" class="divTableStyle">
            <ul>
                <li><b>Samples:</b></li>
                <li style="text-align: center;">
                    <table style="border-spacing: 2px; width: 100%;">
                        <tr>
                           <td style="width: 33%">
                                <input name="SampleImage3" type="image" src="Images/icon_associate3.png" style="width: 50px;
                                    height: 50px" onclick="loadSampleImage(3);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate3.png');"
                                    onmouseout="Over_Out_DemoImage(this,'Images/icon_associate3.png');" />
                            </td>
                            <td style="width: 33%">
                                <input name="SampleImage2" type="image" src="Images/icon_associate2.png" style="width: 50px;
                                    height: 50px" onclick="loadSampleImage(2);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate2.png');"
                                    onmouseout="Over_Out_DemoImage(this,'Images/icon_associate2.png');" />
                            </td>
                             <td style="width: 33%">
                                <input name="SampleImage1" type="image" src="Images/icon_associate1.png" style="width: 50px;
                                    height: 50px" onclick="loadSampleImage(1);" onmouseover="Over_Out_DemoImage(this,'Images/icon_associate1.png');"
                                    onmouseout="Over_Out_DemoImage(this,'Images/icon_associate1.png');" />
                            </td>
                        </tr>
                        <tr>
                           <td>
                                B&W Image
                            </td>
                            <td>
                                Grey Image
                            </td>
                             <td>
                                Color Image
                            </td>
                        </tr>
                    </table>                 
                </li>
            </ul>
        </div>
    </li>--%>
    <li>
        <div class="divType"><div class="mark_arrow collapsed"></div>Load a Local Image</div>
        <div id="div_LoadLocalImage" style="display: none" class="divTableStyle">
            <ul>
                <li style="text-align: center; height:35px; padding-top:8px;">
                    <input type="button" value="Load Image" style="width: 130px; height:30px; font-size:medium;" onclick="return btnLoad_onclick()" />
                </li>
            </ul>
        </div>
    </li>
   
    </ul>

</div>

<div id="divBlank" style="height:20px">&nbsp;</div>


<div id ="divEdit" class="divinput" style="position:relative">
<ul>
    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Edit Image</b></li>
    <li style="padding-left:9px;">
	<img src="Images/ShowEditor.png" title= "Show Image Editor" alt="Show Image Editor" onclick="btnShowImageEditor_onclick()"/>
    <img src="Images/RotateLeft.png" title="Rotate Left" alt="Rotate Left" onclick="btnRotateLeft_onclick()"/>
    <img src="Images/RotateRight.png" title="Rotate Right" alt="Rotate Right" onclick="btnRotateRight_onclick()"/>
<%--	<img src="images/rotate180.png" alt="Rotate 180" title="Rotate 180" onclick="btnRotate180_onclick()" />--%>
    <img src="Images/Mirror.png" title="Mirror" alt="Mirror" onclick="btnMirror_onclick()"/>
    <img src="Images/Flip.png" title="Flip" alt="Flip" onclick="btnFlip_onclick()"/>
    <img src="Images/ChangeSize.png" title="Change Image Size" alt="Change Image Size" onclick="btnChangeImageSize_onclick(this);"/>
    <img src="Images/Crop.png" title="Crop" alt="Crop" onclick="btnCrop_onclick(this);"/>

	</li>
</ul>

</div>

<div id="divSave" class="divinput" style="position:relative">
<ul>
    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Download Image</b></li>
    <li style="padding-left:15px;">
        <label for="txt_fileNameforSave">File Name: <input type="text" size="20" id="txt_fileNameforSave"/></label></li>
    <li style="padding-left:12px;">
        <label for="imgTypebmp">
            <input type="radio" value="bmp" name="imgType_save" id="imgTypebmp" onclick ="rdsave_onclick();"/>BMP</label>
	    <label for="imgTypejpeg">
		    <input type="radio" value="jpg" name="imgType_save" id="imgTypejpeg" onclick ="rdsave_onclick();"/>JPEG</label>
	    <label for="imgTypetiff">
		    <input type="radio" value="tif" name="imgType_save" id="imgTypetiff" onclick ="rdTIFFsave_onclick();"/>TIFF</label>
	    <label for="imgTypepng">
		    <input type="radio" value="png" name="imgType_save" id="imgTypepng" onclick ="rdsave_onclick();"/>PNG</label>
	    <label for="imgTypepdf">
		    <input type="radio" value="pdf" name="imgType_save" id="imgTypepdf" onclick ="rdPDFsave_onclick();"/>PDF</label></li>
    <li style="padding-left:12px;">
        <label for="MultiPageTIFF_save"><input type="checkbox" id="MultiPageTIFF_save"/>Multi-Page TIFF</label>
        <label for="MultiPagePDF_save"><input type="checkbox" id="MultiPagePDF_save"/>Multi-Page PDF </label></li>
    <li style="text-align: center">
        <input id="btnSave" type="button" value="Download Image" onclick ="btnSave_onclick()"/></li>
</ul>
</div>

<div id="divUpload" class="divinput" style="position:relative">
<ul>
    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Upload Image</b></li>
    <li style="padding-left:9px;">
        <label for="txt_fileName">File Name: <input type="text" size="20" id="txt_fileName" /></label></li>
    <li style="padding-left:9px;">
	    <label for="imgTypejpeg2">
		    <input type="radio" value="jpg" name="ImageType" id="imgTypejpeg2" onclick ="rd_onclick();"/>JPEG</label>
	    <label for="imgTypetiff2">
		    <input type="radio" value="tif" name="ImageType" id="imgTypetiff2" onclick ="rdTIFF_onclick();"/>TIFF</label>
	    <label for="imgTypepng2">
		    <input type="radio" value="png" name="ImageType" id="imgTypepng2" onclick ="rd_onclick();"/>PNG</label>
	    <label for="imgTypepdf2">
		    <input type="radio" value="pdf" name="ImageType" id="imgTypepdf2" onclick ="rdPDF_onclick();"/>PDF</label></li>
    <li style="padding-left:9px;">
        <label for="MultiPageTIFF"><input type="checkbox" id="MultiPageTIFF"/>Multi-Page TIFF</label>
        <label for="MultiPagePDF"><input type="checkbox" id="MultiPagePDF"/>Multi-Page PDF </label></li>
    <li style="text-align: center">
        <input id="btnUpload" type="button" value="Upload Image" onclick ="btnUpload_onclick()"/></li>
</ul>
</div>
<div id="divUpdate" class="divinput" style="position:relative; display:none">
<ul>
    <li><img alt="arrow" src="Images/arrow.gif" width="9" height="12"/><b>Save Image</b></li>
    <li style="text-align: center">
        <input id="btnUpdate" type="button" value="Save Image" onclick ="btnUpload_onclick()"/></li>
</ul>
</div>

<div id="divUpgrade">
</div>

</div>

</div>


<div id="J_waiting" class="D-dialog ks-dialog-hidden">
	<div class="ks-dialog-header"></div>
	<div class="ks-dialog-body">
		<div class="ks-dialog-content"><div id="InstallBody"></div></div>
	</div>
	<div class="ks-dialog-footer"></div>
</div>

</div>

</div>



<!-- Custom Dailog Start -->
<div id="ImgSizeEditor" class="divImgSizeEditor" style="display:none">
	<ul>
		<li><label for="img_height"><b>New Height :</b>
			<input type="text" id="img_height" style="width:50%;" size="10"/>pixel</label></li>
		<li><label for="img_width"><b>New Width :</b>&nbsp;
			<input type="text" id="img_width" style="width:50%;" size="10"/>pixel</label></li>
		<li>Interpolation method:
			<select size="1" id="InterpolationMethod"><option value = ""></option></select></li>
	</ul>
	<div>
		<input type="button" value="  OK  " onclick ="btnChangeImageSizeOK_onclick();"/>
		<span><input type="button" value="Cancel" onclick ="btnCancelChange_onclick();"/></span>
	</div>
</div>

<div id="Crop" class="divCrop" style="display:none">	
	<ul>
		<li><label for="img_left"><b>left:</b></label>
			<input type="text" id="img_left" size="4"/></li>
		<li style="text-align:right"><label for="img_right"><b>right:</b></label>
			<input type="text" id="img_right" size="4"/></li>
		<li><label for="img_top"><b>top:</b></label>
			<input type="text" id="img_top" size="4"/></li>
		<li style="text-align:right"><label for="img_bottom"><b>bottom:</b></label>
			<input type="text" id="img_bottom" size="4"/></li>
	</ul>
	<div>
		<input type="button" value="  OK  " onclick ="btnCropOK_onclick()"/>
		<span><input type="button" value="Cancel" onclick ="btnCropCancel_onclick()"/></span>
	</div>
</div>

<script src="Resources/dynamsoft.webtwain.initiate.js"></script>
<script src="Scripts/DWTSample_AdvancedScan_operation.js"></script>
<script src="Scripts/DWTSample_AdvancedScan_initpage.js"></script>
<script src="Resources/dynamsoft.webtwain.config.js"></script>
<script>
    var S = KISSY;

    S.use(['node', 'dom', 'event'], function (S, Node, DOM, Event) {

        var $ = S.all, _li = $('li', 'ul.PCollapse'), o = S.one('#dwtcontrolContainer');

        $('div.divType', _li).each(function (_this) {

            _this.on('click', function () {
                var _thisDOM = S.one(_this);

                if (_thisDOM.next().css('display') === 'none') {
                    $('.expanded', _li).addClass('collapsed').removeClass('expanded');
                    $('div.divTableStyle', _li).hide();

                    _thisDOM.next().show();
                    $('.mark_arrow', _this).addClass('expanded').removeClass('collapsed');
                }

            });

        });

    });


    _strActionPage = "<% = saveUrl %>";
    var _strRedirectURL = "<% = redirectUrl %>";

    //DWObject.HTTPDownload("localhost", "/WebTWAIN/Images/ImageData.jpg");
    function ScanBeforeLoad() {
        //$('.divinput').not('#divEdit').hide();
        //$('#divUpdate').show();

        //_strDefaultSaveImageName = "New2.jpg";
    }
    function ScanAfterLoad(dw) {
        //dw.HTTPDownload("localhost:2520", "/Data/00000/0000/001/New2.jpg");
    }
</script>
                </div>
            </fieldset>
            </asp:Panel>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>

