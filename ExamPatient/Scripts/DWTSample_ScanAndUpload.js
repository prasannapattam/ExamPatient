var _dwtParam = {
'productKey': '',
    'containerID': 'dwtcontrolContainer',   //The ID of Dynamic Web TWAIN control div in HTML.This value is required.
    /*
    'isTrial': 'true',  
    isTrial is used to judge whether Dynamic Web TWAIN control is trial or full. This value is optional.
    The default value is 'TRUE', which means the control is a trial one. The value of isTrial is 'TRUE' or 'FALSE'.
    */

    /*
    'version': '9,1',   
    The version of Dynamic Web TWAIN control, which is used to judge the version when downloading CAB.
    This value is optional. The default value is '9,1'.
    */

    /*
    'resourcesPath': 'Resources',   
    The relative path of MSI, CAB and PKG.
    This value is optional. The default value is 'Resources'.
    */

    /*
    'width': 580,       //The width of Dynamic Web TWAIN control
    This value is optional. The default value is '580'.
    */

    /*
    'height': 600       //The height of  Dynamic Web TWAIN control
    This value is optional. The default value is '600'.
    */

    /*  These are events. The name of ‘OnPostAllTransfer’shouldn’t be changed, but the name of ‘Dynamsoft_OnPostAllTransfers’ can be modified. 
    Please pay attention, the name of ‘Dynamsoft_OnPostAllTransfers’ and ‘function Dynamsoft_OnPostAllTransfers()’ event must be coincident.
        
    Events are as follows. You can choose one or many according to you needs.
    Once an event is added, you must make sure the corresponding function is defined in your code.
        
    'onPostTransfer':Dynamsoft_OnPostTransfer,
    'onPostAllTransfers':Dynamsoft_OnPostAllTransfers,  
    'onMouseClick':Dynamsoft_OnMouseClick,   
    'onPostLoad':Dynamsoft_OnPostLoadfunction,    
    'onImageAreaSelected':Dynamsoft_OnImageAreaSelected,   
    'onMouseDoubleClick':Dynamsoft_OnMouseDoubleClick,   
    'onMouseRightClick':Dynamsoft_OnMouseRightClick,   
    'onTopImageInTheViewChanged':Dynamsoft_OnTopImageInTheViewChanged,   
    'onImageAreaDeSelected':Dynamsoft_OnImageAreaDeselected,    
    'onGetFilePath':Dynamsoft_OnGetFilePath  
    */ 
     'onTopImageInTheViewChanged':Dynamsoft_OnTopImageInTheViewChanged                              
};


var gWebTwain;
(function() {
	gWebTwain = new Dynamsoft.WebTwain(_dwtParam);
})();

var seed;
function onPageLoad() {
    //initInfo();            //Add guide info
    initPara();
    seed = setInterval(initControl, 500);
}


function initControl() {
    var DWObject = gWebTwain.getInstance();
    if (DWObject) {
        if (DWObject.ErrorCode == 0) {
            clearInterval(seed);
            DWObject.BrokerProcessType = 1;
        }
    }
}

function acquireImage() {
    var DWObject = gWebTwain.getInstance();
    if (DWObject) {
        if (DWObject.SourceCount > 0) {
            DWObject.SelectSource();
            DWObject.AcquireImage();
        }
        else
            alert("No TWAIN compatible drivers detected.");
    }
}

function Dynamsoft_OnTopImageInTheViewChanged(index) {
    var DWObject = gWebTwain.getInstance();
    if (DWObject) {
        DWObject.CurrentImageIndexInBuffer = index;
    }
}

//--------------------------------------------------------------------------------------
//************************** Upload Image***********************************
//--------------------------------------------------------------------------------------
function btnUpload_onclick() {
     var DWObject = gWebTwain.getInstance();
     if (DWObject) {
         if (DWObject.HowManyImagesInBuffer == 0) {
             return;
         }
         var i, strHTTPServer, strActionPage, strImageType;
         varFileName.className = "";

         strHTTPServer = location.hostname; //Demo: "www.dynamsoft.com";
         DWObject.HTTPPort = location.port == "" ? 80 : location.port; //Demo: 80;
         var varUserName = document.getElementById("txtUserName");
         /*if (varUserName) {
             if (varUserName.value != "") {
                 DWObject.HTTPUserName = varUserName.value;

                 var varPassword = document.getElementById("txtPassword");
                 if (varPassword) {
                     DWObject.HTTPPassword = varPassword.value;
                 }
             }
             else {
                 DWObject.HTTPUserName = "";
                 DWObject.HTTPPassword = "";
             }        
         }*/
         strActionPage = document.getElementById("txtActionPage").value; //the ActionPage's file path
         for (i = 0; i < 4; i++) {
             if (document.getElementsByName("ImageType").item(i).checked == true) {
                 strImageType = i + 1;
                 break;
             }
         }
         
         var uploadfilename = varFileName.value + "." + document.getElementsByName("ImageType").item(i).value;
         if (strImageType == 2 && varMultiPageTIFF.checked) {
             if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
                 DWObject.HTTPUploadAllThroughPostAsMultiPageTIFF(
                strHTTPServer,
                strActionPage,
                uploadfilename);
             }
             else {
                 DWObject.HTTPUploadThroughPostAsMultiPageTIFF(
                strHTTPServer,
                strActionPage,
                uploadfilename);
             }
         }
         else if (strImageType == 4 && varMultiPagePDF.checked) {
             if ((DWObject.SelectedImagesCount == 1) || (DWObject.SelectedImagesCount == DWObject.HowManyImagesInBuffer)) {
                 DWObject.HTTPUploadAllThroughPostAsPDF(
                strHTTPServer,
                strActionPage,
                uploadfilename);
             }
             else {
                 DWObject.HTTPUploadThroughPostAsMultiPagePDF(
                strHTTPServer,
                strActionPage,
                uploadfilename);
             }
         }
         else {
             DWObject.HTTPUploadThroughPostEx(
            strHTTPServer,
            DWObject.CurrentImageIndexInBuffer,
            strActionPage,
            uploadfilename,
            strImageType);
         }

         alert(DWObject.ErrorString + DWObject.HTTPPostResponseString);
     }
}


//--------------------------------------------------------------------------------------
//*********************************radio response***************************************
//--------------------------------------------------------------------------------------
var varFileName, varMultiPageTIFF, varMultiPagePDF;
function initPara() {
    var varHTTPServer = document.getElementById("txtHTTPServer");
    if (varHTTPServer)
        varHTTPServer.value = location.hostname;
        
    var varHTTPPort = document.getElementById("txtHTTPPort");
    if (varHTTPPort)
        varHTTPPort.value = location.port == "" ? 80 : location.port;

    var varActionPage = document.getElementById("txtActionPage");
    if (varActionPage) {
        if (location.hostname != "") {
            var CurrentPathName = unescape(location.pathname); // get current PathName in plain ASCII
            varActionPage.value = CurrentPathName.substring(0, CurrentPathName.lastIndexOf("/") + 1) + "SaveToFile.aspx"; //the ActionPage's file path
        }
        else
            varActionPage.value = "SaveToFile.aspx"; //the ActionPage's file path
    }
    
    var varImgTypejpeg = document.getElementById("imgTypejpeg");
    if (varImgTypejpeg)
        varImgTypejpeg.checked = true;

    varFileName = document.getElementById("txtFileName");
    if (varFileName)
        varFileName.value = "WebTWAINImage";

    varMultiPageTIFF = document.getElementById("MultiPageTIFF");
    if (varMultiPageTIFF)
        varMultiPageTIFF.disabled = true;
    varMultiPagePDF = document.getElementById("MultiPagePDF");
    if (varMultiPagePDF)
        varMultiPagePDF.disabled = true;
}


function rdTIFF_onclick() {
    varMultiPageTIFF.disabled = false;

    varMultiPageTIFF.checked = false;
    varMultiPagePDF.checked = false;
    varMultiPagePDF.disabled = true;
}
function rdPDF_onclick() {
    varMultiPagePDF.disabled = false;

    varMultiPageTIFF.checked = false;
    varMultiPagePDF.checked = true;
    varMultiPageTIFF.disabled = true;
}
function rd_onclick() {
    varMultiPageTIFF.checked = false;
    varMultiPagePDF.checked = false;

    varMultiPageTIFF.disabled = true;
    varMultiPagePDF.disabled = true;
}


//******************Instructions*******************//
function initInfo() {
    var MessageBody = document.getElementById("divInfo");
    if (MessageBody) {
        var ObjString = "<div>";
        ObjString += "This sample demonstrates how to scan documents and upload them to your web server via HTTP using Dynamic Web TWAIN.<br />";
        ObjString += "<br />";
        ObjString += "<b>Steps to try:</b><br />";
        ObjString += "1. Deploy the sample(especially the Action Page) to your web server or open the sample as a website in the  Visual Studio<br />";
        ObjString += "2. Connect your scanner<br />";
        ObjString += "3. Click the \"Scan\" button<br />";
        ObjString += "4. Fill in all the uploading fields<br />";
        ObjString += "5. Click the \"Upload Image\" button<br />";
        ObjString += "<br />";
        ObjString += "<b>Note:</b><br />";
        ObjString += "1. You must configure an HTTP server to receive the uploaded images. <br />";
        ObjString += "2. The sample Action Page is included as part of the sample, please put it on your HTTP server and fill in \"Action Page:\" field with the correct path. ";
        ObjString += "<br />";
        ObjString += "<br />";
        ObjString += "Any questions? <a target='blank' href='mailto:support@dynamsoft.com'>Please let us know!</a>";
        ObjString += "<br />";
        ObjString += "</div>";
        MessageBody.innerHTML = ObjString;
    }
}
