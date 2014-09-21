//--------------------------------------------------------------------------------------
//************************** Import Image*****************************
//--------------------------------------------------------------------------------------
/*-----------------select source---------------------*/

var _last_selected_index = -1;

function source_onchange() {
    if (D_get("divTwainType"))
        D_get("divTwainType").style.display = '';
		
    if (D_get("btnScan"))
		D_get("btnScan").value = 'Scan';

	DWObject.CloseSource();
			
	if (D_get(_divDWTSourceContainerID)){
		_last_selected_index = D_get(_divDWTSourceContainerID).selectedIndex;
	}

}


/*-----------------Acquire Image---------------------*/
function acquireImage() {

	if (_last_selected_index === -1){
		_last_selected_index = D_get(_divDWTSourceContainerID).selectedIndex;
	}
    
	var _obj = {};
	_obj.SelectSourceByIndex = _last_selected_index;
    _obj.IfShowUI = D_get("ShowUI").checked;

    for (var i = 0; i < 3; i++) {
        if (document.getElementsByName("PixelType").item(i).checked === true){
            _obj.PixelType = i;
			break;
		}
    }
    _obj.Resolution = D_get("Resolution").value;
    _obj.IfFeederEnabled = D_get("ADF").checked;
    _obj.IfDuplexEnabled = D_get("Duplex").checked;
    if (DynamLib.env.bWin)
        g_DWT_PrintMsg("Pixel Type: " + _obj.PixelType + "<br />Resolution: " + _obj.Resolution);

    _obj.IfDisableSourceAfterAcquire = true;
	
	_iTwainType = 0;
		
    DWObject.AcquireImage(_obj);	
	
}

/*-----------------Load Image---------------------*/
function btnLoad_onclick() {
    DWObject.IfShowFileDialog = true;
    DWObject.LoadImageEx('', 5, function(){
		g_DWT_PrintMsg("Loaded an image successfully.");
	}, function(){
		checkErrorString();
	});
    
}
function loadSampleImage(nIndex) {
	var ImgArr;

    switch (nIndex) {
        case 1:
            ImgArr = '/images/twain_associate1.png';
            break;
        case 2:
            ImgArr = '/images/twain_associate2.png';
            break;
        case 3:
            ImgArr = '/images/twain_associate3.png';
            break;
    }

    if (location.hostname != '') {
        DWObject.HTTPPort = location.port;
		DWObject.IfSSL = DynamLib.detect.ssl;
        DWObject.HTTPDownload(location.hostname, DynamLib.getRealPath(ImgArr), function(){
			g_DWT_PrintMsg('Loaded a demo image successfully. (Http Download)');
			updatePageInfo();
		}, function(){
			checkErrorString();
		});
    }
    else {
        DWObject.IfShowFileDialog = false;
        DWObject.LoadImage(DynamLib.getRealPath(ImgArr), function(){
			DWObject.IfShowFileDialog = true;
			g_DWT_PrintMsg('Loaded a demo image successfully.');
			updatePageInfo();
		}, function(){
			DWObject.IfShowFileDialog = true;
			checkErrorString();
		});
    }

}

//--------------------------------------------------------------------------------------
//************************** Edit Image ******************************

//--------------------------------------------------------------------------------------
function btnShowImageEditor_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.ShowImageEditor();
}

function btnRotateRight_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.RotateRight(DWObject.CurrentImageIndexInBuffer);
    DynamLib.appendMessage('<b>Rotate right: </b>');
    if (checkErrorString()) {
        return;
    }
}
function btnRotateLeft_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.RotateLeft(DWObject.CurrentImageIndexInBuffer);
    DynamLib.appendMessage('<b>Rotate left: </b>');
    if (checkErrorString()) {
        return;
    }
}

function btnRotate180_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.Rotate(DWObject.CurrentImageIndexInBuffer, 180, true);
    DynamLib.appendMessage('<b>Rotate 180: </b>');
    if (checkErrorString()) {
        return;
    }
}

function btnMirror_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.Mirror(DWObject.CurrentImageIndexInBuffer);
    DynamLib.appendMessage('<b>Mirror: </b>');
    if (checkErrorString()) {
        return;
    }
}
function btnFlip_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.Flip(DWObject.CurrentImageIndexInBuffer);
    DynamLib.appendMessage('<b>Flip: </b>');
    if (checkErrorString()) {
        return;
    }
}

/*----------------------Crop Method---------------------*/
function btnCrop_onclick(oButton) {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    if (_iLeft != 0 || _iTop != 0 || _iRight != 0 || _iBottom != 0) {
        DWObject.Crop(
            DWObject.CurrentImageIndexInBuffer,
            _iLeft, _iTop, _iRight, _iBottom
        );
        _iLeft = 0;
        _iTop = 0;
        _iRight = 0;
        _iBottom = 0;
        DynamLib.appendMessage('<b>Crop: </b>');
        if (checkErrorString()) {
            return;
        }
        return;
    }
	
	var oCrop = D_get('Crop');

    DynamLib.toggle('Crop');
	
    oCrop.style.top = ds_gettop(oButton) + oButton.offsetHeight + 'px';
    oCrop.style.left = ds_getleft(oButton) - 200 + 'px';
}

function btnCropCancel_onclick() {
	DynamLib.hide('Crop');
}
function btnCropOK_onclick() {
    D_get("img_left").className = "";
    D_get("img_top").className = "";
    D_get("img_right").className = "";
    D_get("img_bottom").className = "";
    if (!re.test(D_get("img_left").value)) {
        D_get("img_left").className += " invalid";
        D_get("img_left").focus();
        g_DWT_PrintMsg("Please input a valid <b>left</b> value.");
        return;
    }
    if (!re.test(D_get("img_top").value)) {
        D_get("img_top").className += " invalid";
        D_get("img_top").focus();
        g_DWT_PrintMsg("Please input a valid <b>top</top> value.");
        return;
    }
    if (!re.test(D_get("img_right").value)) {
        D_get("img_right").className += " invalid";
        D_get("img_right").focus();
        g_DWT_PrintMsg("Please input a valid <b>right</b> value.");
        return;
    }
    if (!re.test(D_get("img_bottom").value)) {
        D_get("img_bottom").className += " invalid";
        D_get("img_bottom").focus();
        g_DWT_PrintMsg("Please input a valid <b>bottom</b> value.");
        return;
    }
    DWObject.Crop(
        DWObject.CurrentImageIndexInBuffer,
        D_get("img_left").value,
        D_get("img_top").value,
        D_get("img_right").value,
        D_get("img_bottom").value
    );
    DynamLib.appendMessage('<b>Crop: </b>');
    if (checkErrorString()) {
        DynamLib.hide('Crop');
        return;
    }
}

/*----------------Change Image Size--------------------*/
function btnChangeImageSize_onclick(oButton) {
    if (!checkIfImagesInBuffer()) {
        return;
    }
	var o = D_get('ImgSizeEditor');
	DynamLib.toggle('ImgSizeEditor');
	
    o.style.top = ds_gettop(oButton) + oButton.offsetHeight + 'px';
    o.style.left = ds_getleft(oButton) - 230 + 'px';
}

function btnCancelChange_onclick() {
    DynamLib.hide('ImgSizeEditor');
}

function btnChangeImageSizeOK_onclick() {
    D_get("img_height").className = "";
    D_get("img_width").className = "";
    if (!re.test(D_get("img_height").value)) {
        D_get("img_height").className += " invalid";
        D_get("img_height").focus();
        g_DWT_PrintMsg("Please input a valid <b>height</b>.");
        return;
    }
    if (!re.test(D_get("img_width").value)) {
        D_get("img_width").className += " invalid";
        D_get("img_width").focus();
        g_DWT_PrintMsg("Please input a valid <b>width</b>.");
        return;
    }
    DWObject.ChangeImageSize(
        DWObject.CurrentImageIndexInBuffer,
        D_get("img_width").value,
        D_get("img_height").value,
        D_get("InterpolationMethod").selectedIndex + 1
    );
    DynamLib.appendMessage('<b>Change Image Size: </b>');
    if (checkErrorString()) {
        DynamLib.hide('ImgSizeEditor');
        return;
    }
}
//--------------------------------------------------------------------------------------
//************************** Save Image***********************************
//--------------------------------------------------------------------------------------
function btnSave_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    var i, strimgType_save;
    var NM_imgType_save = document.getElementsByName("imgType_save");
    for (i = 0; i < 5; i++) {
        if (NM_imgType_save.item(i).checked == true) {
            strimgType_save = NM_imgType_save.item(i).value;
            break;
        }
    }
    DWObject.IfShowFileDialog = true;
    _txtFileNameforSave.className = "";
    var bSave = false;
    if (!strre.test(_txtFileNameforSave.value)) {
        _txtFileNameforSave.className += " invalid";
        _txtFileNameforSave.focus();
        g_DWT_PrintMsg("Please input <b>file name</b>.<br />Currently only English names are allowed.");
        return;
    }
    var strFilePath = [
		DynamLib.env.bWin ? 'C:\\' : '',
		_txtFileNameforSave.value,
		'.',
		strimgType_save
	].join('');
	
	if(DynamLib.env.bMac)
		strFilePath = '';
	
    if (strimgType_save == "tif" && _chkMultiPageTIFF_save.checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            bSave = DWObject.SaveAllAsMultiPageTIFF(strFilePath);
        }
        else {
            bSave = DWObject.SaveSelectedImagesAsMultiPageTIFF(strFilePath);
        }
    }
    else if (strimgType_save == "pdf" && D_get("MultiPagePDF_save").checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            bSave = DWObject.SaveAllAsPDF(strFilePath);
        }
        else {
            bSave = DWObject.SaveSelectedImagesAsMultiPagePDF(strFilePath);
        }
    }
    else {
        switch (i) {
            case 0: bSave = DWObject.SaveAsBMP(strFilePath, DWObject.CurrentImageIndexInBuffer); break;
            case 1: bSave = DWObject.SaveAsJPEG(strFilePath, DWObject.CurrentImageIndexInBuffer); break;
            case 2:
                bSave = DWObject.SaveAsTIFF(strFilePath, DWObject.CurrentImageIndexInBuffer); break;
            case 3: bSave = DWObject.SaveAsPNG(strFilePath, DWObject.CurrentImageIndexInBuffer); break;
            case 4: bSave = DWObject.SaveAsPDF(strFilePath, DWObject.CurrentImageIndexInBuffer); break;
        }
    }

    if (bSave)
        DynamLib.appendMessage('<b>Save Image: </b>');
    if (checkErrorString()) {
        return;
    }
}
//--------------------------------------------------------------------------------------
//************************** Upload Image***********************************
//--------------------------------------------------------------------------------------
function btnUpload_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    var i, strHTTPServer, strActionPage, strImageType;
    _txtFileName.className = "";
    if (!strre.test(_txtFileName.value)) {
        _txtFileName.className += " invalid";
        _txtFileName.focus();
        g_DWT_PrintMsg("Please input <b>file name</b>.<br />Currently only English names are allowed.");
        return;
    }
    //DWObject.MaxInternetTransferThreads = 5;
    strHTTPServer = _strServerName;
    DWObject.HTTPPort = _strPort;
    var CurrentPathName = unescape(location.pathname); // get current PathName in plain ASCII	
    var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf("/") + 1);
    //strActionPage = CurrentPath + _strActionPage; //the ActionPage's file path
    //var redirectURLifOK = CurrentPath + "DWTSample_List.aspx";
    strActionPage = _strActionPage; 
    var redirectURLifOK = _strRedirectURL;
    for (i = 0; i < 4; i++) {
        if (document.getElementsByName("ImageType").item(i).checked == true) {
            strImageType = i + 1;
            break;
        }
    }
	
	var sFun = function(){
		DynamLib.appendMessage('<b>Upload: </b>');
		if (checkErrorString()) {
			if (strActionPage.indexOf("SaveToFile") != -1){
				var strErrorString = DWObject.ErrorString;
				window.location.href = redirectURLifOK;
				//alert(strErrorString)//if save to file.
			}else{
				window.location.href = redirectURLifOK;
			}
		}
	}, fFun = function(){
		checkErrorString();
	};
	
	
    var uploadfilename = _txtFileName.value + "." + document.getElementsByName("ImageType").item(i).value;
    if (strImageType == 2 && D_get("MultiPageTIFF").checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            DWObject.HTTPUploadAllThroughPostAsMultiPageTIFF(
                strHTTPServer,
                strActionPage,
                uploadfilename,
				sFun, fFun
            );
        }
        else {
            DWObject.HTTPUploadThroughPostAsMultiPageTIFF(
                strHTTPServer,
                strActionPage,
                uploadfilename,
				sFun, fFun
            );
        }
    }
    else if (strImageType == 4 && D_get("MultiPagePDF").checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            DWObject.HTTPUploadAllThroughPostAsPDF(
                strHTTPServer,
                strActionPage,
                uploadfilename,
				sFun, fFun
            );
        }
        else {
            DWObject.HTTPUploadThroughPostAsMultiPagePDF(
                strHTTPServer,
                strActionPage,
                uploadfilename,
				sFun, fFun
            );
        }
    }
    else {
        DWObject.HTTPUploadThroughPostEx(
            strHTTPServer,
            DWObject.CurrentImageIndexInBuffer,
            strActionPage,
            uploadfilename,
            strImageType,
			sFun, fFun
        );
    }

}



function btnUploadToFTP_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }

    _txtFileName.className = "";

    var strFTPServer = "192.168.1.20";
    DWObject.FTPPort = 21;
    DWObject.FTPUserName = "DWT";
    DWObject.FTPPassword = "DWT";
    var remoteDirectory = "/images/";

    for (i = 0; i < 4; i++) {
        if (document.getElementsByName("ImageType").item(i).checked == true) {
            strImageType = i + 1;
            break;
        }
    }
    var uploadfilename = _txtFileName.value + "." + document.getElementsByName("ImageType").item(i).value;
    var uploadFullPath = remoteDirectory + uploadfilename;

    if (strImageType == 2 && _chkMultiPageTIFF.checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            DWObject.FTPUploadAllAsMultiPageTIFF(strFTPServer, uploadFullPath);
        }
        else {
            DWObject.FTPUploadAsMultiPageTIFF(strFTPServer, uploadFullPath);
        }
    }
    else if (strImageType == 4 && MultiPagePDF.checked) {
        if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
            DWObject.FTPUploadAllAsPDF(strFTPServer, uploadFullPath);
        }
        else {
            DWObject.FTPUploadAsMultiPagePDF(strFTPServer, uploadFullPath);
        }
    }
    else {
        DWObject.FTPUploadEx(
            strFTPServer,
            DWObject.CurrentImageIndexInBuffer,
            uploadFullPath,
            strImageType
        );
    }
    DynamLib.appendMessage('<b>Upload: </b>');
    checkErrorString();
}


//--------------------------------------------------------------------------------------
//************************** Navigator functions***********************************
//--------------------------------------------------------------------------------------

function btnPreImage_wheel() {
    if (DWObject.HowManyImagesInBuffer != 0)
        btnPreImage_onclick();
}

function btnNextImage_wheel() {
    if (DWObject.HowManyImagesInBuffer != 0)
        btnNextImage_onclick();
}

function btnFirstImage_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
	DWObject.first();
}

function btnPreImage_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    else if (DWObject.CurrentImageIndexInBuffer == 0) {
        return;
    }
	DWObject.previous();
}
function btnNextImage_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    else if (DWObject.CurrentImageIndexInBuffer == DWObject.HowManyImagesInBuffer - 1) {
        return;
    }
	DWObject.next();
}

function btnLastImage_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
	DWObject.last();
}

function btnRemoveCurrentImage_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.RemoveAllSelectedImages();
    if (DWObject.HowManyImagesInBuffer == 0) {
        D_get("DW_TotalImage").value = DWObject.HowManyImagesInBuffer;
        D_get("DW_CurrentImage").value = '';
        return;
    }
    else {
        updatePageInfo();
    }
}


function btnRemoveAllImages_onclick() {
    if (!checkIfImagesInBuffer()) {
        return;
    }
    DWObject.RemoveAllImages();
    D_get("DW_TotalImage").value = "0";
    D_get("DW_CurrentImage").value = "0";
}
function setlPreviewMode() {
    DWObject.SetViewMode(parseInt(D_get("DW_PreviewMode").selectedIndex + 1), parseInt(D_get("DW_PreviewMode").selectedIndex + 1));
    if (DynamLib.env.bMac) {
        return;
    }
    else if (D_get("DW_PreviewMode").selectedIndex != 0) {
		DWObject.MouseShape = true;
    }
    else {
        DWObject.MouseShape = false;
    }
}

//--------------------------------------------------------------------------------------
//*********************************radio response***************************************
//--------------------------------------------------------------------------------------
function rdTIFFsave_onclick() {
    _chkMultiPageTIFF_save.disabled = false;

    _chkMultiPageTIFF_save.checked = false;
    _chkMultiPagePDF_save.checked = false;
    _chkMultiPagePDF_save.disabled = true;
}
function rdPDFsave_onclick() {
    _chkMultiPagePDF_save.disabled = false;

    _chkMultiPageTIFF_save.checked = false;
    _chkMultiPagePDF_save.checked = false;
    _chkMultiPageTIFF_save.disabled = true;
}
function rdsave_onclick() {
    _chkMultiPageTIFF_save.checked = false;
    _chkMultiPagePDF_save.checked = false;

    _chkMultiPageTIFF_save.disabled = true;
    _chkMultiPagePDF_save.disabled = true;
}
function rdTIFF_onclick() {
    _chkMultiPageTIFF.disabled = false;

    _chkMultiPageTIFF.checked = false;
    _chkMultiPagePDF.checked = false;
    _chkMultiPagePDF.disabled = true;
}
function rdPDF_onclick() {
    _chkMultiPagePDF.disabled = false;

    _chkMultiPageTIFF.checked = false;
    _chkMultiPagePDF.checked = false;
    _chkMultiPageTIFF.disabled = true;
}
function rd_onclick() {
    _chkMultiPageTIFF.checked = false;
    _chkMultiPagePDF.checked = false;

    _chkMultiPageTIFF.disabled = true;
    _chkMultiPagePDF.disabled = true;
}
