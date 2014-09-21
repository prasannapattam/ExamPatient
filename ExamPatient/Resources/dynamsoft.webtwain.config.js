/*!
* Dynamsoft JavaScript Library for Basic Initiation of Dynamic Web TWAIN
* More info on DWT: http://www.dynamsoft.com/Products/WebTWAIN_Overview.aspx
*
* Copyright 2014, Dynamsoft Corporation 
* Author: Dynamsoft Support Team
*/
var _dwtParam = {
	
'productKey': '43C8DCF97DEC6F9437FDEB5D7F16A37E6E28D9C706E977E897B009C41EB1AE556E28D9C706E977E88D35779CD551FBF76E28D9C706E977E879DBF4FF3F95968230000000',
	
	//The ID of Dynamic Web TWAIN control div in HTML.This value is required.
    'containerID': 'dwtcontrolContainer',
	
	'bShowNoControl': false,
    'bNotAllowedForChrome': false,

	// The ID of the container (Usually a DIV) which is used to show a message if DWT is not installed. User must specify it.
	'_divDWTNonInstallContainerID': 'DWTNonInstallContainerID',
	'_divDWTemessageContainer': 'DWTemessageContainer',
	'_divInstallBody': 'InstallBody',
	
    /*
     * This value is optional. The default value is '580'.
	 'width': 580,       //The width of Dynamic Web TWAIN control  
    */
    
    /*
	 * This value is optional. The default value is '600'.
	 'height': 600,       //The height of  Dynamic Web TWAIN control 
	*/
	
    /*
     * The relative path of MSI, CAB and PKG.
     * This value is optional. The default value is 'Resources'.
    'resourcesPath': 'Resources',   
    */    

    /*  These are events. The name of 'OnPostAllTransfer' shouldn't be changed, but the name of 'Dynamsoft_OnPostAllTransfers' can be modified. 
     *  Please pay attention, the name of 'Dynamsoft_OnPostAllTransfers' and 'function Dynamsoft_OnPostAllTransfers()' event must be coincident.
	 *
     *  Events are as follows. You can choose one or many according to you needs.
     *  Once an event is added, you must make sure the corresponding function is defined in your code.
        
        'onPostTransfer':Dynamsoft_OnPostTransfer,
        'onPostAllTransfers':Dynamsoft_OnPostAllTransfers,  
        'onMouseClick':Dynamsoft_OnMouseClick,   
        'onPostLoad':Dynamsoft_OnPostLoadfunction,    
        'onImageAreaSelected':Dynamsoft_OnImageAreaSelected,   
        'onMouseDoubleClick':Dynamsoft_OnMouseDoubleClick,   
        'onMouseRightClick':Dynamsoft_OnMouseRightClick,   
        'onTopImageInTheViewChanged':Dynamsoft__OnTopImageInTheViewChanged,
        'onImageAreaDeSelected':Dynamsoft_OnImageAreaDeselected,    
        'onGetFilePath':Dynamsoft_OnGetFilePath,
		'onPrintMsg': print message
    */
	
	'debug': false,
	'bReady': false,
	'seed': null
	
}, EmptyFunction = function(){},
	gWebTwain, DWObject;
	
DynamLib.product.version = '10,0,0,911';


/* optional Events:
Dynamsoft_ChangeConfig
Dynamsoft_OnLoad
Dynamsoft_OnReady
Dynamsoft_OnNotReady
Dynamsoft__OnTopImageInTheViewChanged
*/

function Dynamsoft__OnTopImageInTheViewChanged(index) {
	if(DWObject){
		if(DWObject.CurrentImageIndexInBuffer != index)
			DWObject.CurrentImageIndexInBuffer = index;
		
		if(typeof(DWObject.__innerRefreshImage) === 'function')
			DWObject.__innerRefreshImage();
	}
}

function Dynamsoft__onReady(){
	if(_dwtParam.bReady)
		return;

	closetblInstall_onclick();
	
	if(DynamLib.product.bChromeEdition && !DWObject){
		DWObject = gWebTwain;
		Dynamsoft_NeedUpgrade();
	}
	
	if(typeof(Dynamsoft_OnReady) === 'function')
		Dynamsoft_OnReady();
	
	
	_dwtParam.bReady = true;
}

function Dynamsoft_init(){

	if(DynamLib.product.bChromeEdition){
		_dwtParam.onWSReady = function(){
			DWObject = gWebTwain;

			Dynamsoft__onReady();
		};
	} else {
		if(!_dwtParam.onTopImageInTheViewChanged)
			_dwtParam.onTopImageInTheViewChanged = Dynamsoft__OnTopImageInTheViewChanged;
	}
	
	gWebTwain = new DynamLib.WebTwain(_dwtParam);

	if(typeof(Dynamsoft_OnLoad) === 'function'){
		Dynamsoft_OnLoad();
	}
	
	if(DynamLib.product.bChromeEdition){
		DynamLib.startWS();
	}else{
		_dwtParam.seed = setInterval(Dynamsoft_initControl, 500);
	}
};

function Dynamsoft_initControl() {
    var o = gWebTwain.getInstance();
    if (o) {
        if (o.ErrorCode == 0) {
            clearInterval(_dwtParam.seed);
			_dwtParam.seed = null;
			
			DWObject = o;
            DWObject.BrokerProcessType = 1;
			DWObject.IfAllowLocalCache=true;

			Dynamsoft__onReady();

        }else{
			if(typeof(Dynamsoft_OnNotReady) === 'function'){
				Dynamsoft_OnNotReady();
			}		
		}
    }
}

function Dynamsoft_getRelativePath(){
	var _resourcePath, _resourceRelativePath = 'Advanced/Resources',
		_path = location.pathname,
		_host = location.hostname;

	if(_host === ''){
		if (DynamLib.env.bWin && _path.lastIndexOf('\\') > 1){
			_path = _path.substring(1, _path.lastIndexOf('\\')).replace(/%20/g, ' ');
		}else{
			_path = _path.substring(1, _path.lastIndexOf('/')).replace(/%20/g, ' ');
		}
	} else {
		_path = _path.substring(0, _path.lastIndexOf('/'));
	}
	
	if(DynamLib.env.bFileSystem){
		_resourcePath = _resourceRelativePath;

	} else {
		_resourcePath = [_path, '/', _resourceRelativePath].join('');
	}
	
	return _resourcePath;
}

function Dynamsoft_initControl() {
    var o = gWebTwain.getInstance();
    if (o) {
        if (o.ErrorCode == 0) {
            clearInterval(_dwtParam.seed);
			_dwtParam.seed = null;
			
			DWObject = o;
            DWObject.BrokerProcessType = 1;
			DWObject.IfAllowLocalCache=true;

			if(typeof(Dynamsoft_OnReady) === 'function'){
				Dynamsoft_OnReady();
			}
        }else{
			if(typeof(Dynamsoft_OnNotReady) === 'function'){
				Dynamsoft_OnNotReady();
			}		
		}
    }
}

(function(){
	if(typeof(Dynamsoft_ChangeConfig) === 'function'){
		Dynamsoft_ChangeConfig(_dwtParam);
	}

	/*
	if(typeof(g_onWSMessage) === 'function')
		cfg['OnWSMessage'] = g_onWSMessage;
	if(typeof(g_onWSClose) === 'function')
		cfg['OnWSClose'] = g_onWSClose;
	*/

	
	DynamLib.main(Dynamsoft_init, {
		productKey: _dwtParam.productKey,
		
		onNotAllowedForChrome: Dynamsoft__notAllowedForChrome,
		onNoControl: Dynamsoft__noControl,
		onReady: Dynamsoft__onReady,
		
		// resourcesPath: Dynamsoft_getRelativePath(),
		debug: _dwtParam.debug
		
		/*
			productKey
			debug
			trial
			
			basePath
			resourcesPath
			pathType
			bDiscardBlankImage
			msgPrefix
			msgSuffix
			
			detectType
			tryTimes
			onReady
			onDetectNext
			onNoControl
			onNotAllowedForChrome
			onCreatWS
		*/
	});
})();



var _strDynamsoftWithClose = "<div style='height:40px;width:350px;position:relative;'><span style='padding-right:60px'><img src=\"Images/logo.gif\" alt=\"Dynamsoft: provider of version control solution and TWAIN SDK\" name=\"logo\" width=\"159\" height=\"34\" border=\"0\" align=\"left\" id=\"logo\" title=\"Dynamsoft: provider of version control solution and TWAIN SDK\" /></span><div style='height:30px;padding-left:205px; position:absolute; top:0; left:110px'><a href='javascript: void(0)' style='text-decoration:none; padding:15px' class='ClosetblCanNotScan'>X</a></div></div>";

var dlgInstall = false;
function Dynamsoft__noControl() {
	
	_dwtParam.bShowNoControl = true;
    ua = (navigator.userAgent.toLowerCase());
    // For mac, create the message to tell the user to install the plugin.
    if (DynamLib.env.bMac) {
        createNonInstallDivMac();
        // Display the message and hide the main control
        DynamLib.show(_dwtParam._divDWTNonInstallContainerID);
        DynamLib.hide(_dwtParam.containerID);
        DynamLib.hide(_dwtParam._divDWTemessageContainer);

        KISSY.use("overlay", function(S, o) {

            dlgInstall = new o.Dialog({
                srcNode: "#J_waiting",
                width: 392,
                height: 277,
                closable: false,
                mask: true,
                align: {
                    points: ['cc', 'cc']
                }
            });
            dlgInstall.show();
        });
    }
    // For other browsers on Windows, create the message to tell the user to install the plugin.
    else if (ua.match(/chrome\/([\d.]+)/) || ua.match(/opera.([\d.]+)/) || ua.match(/version\/([\d.]+).*safari/) || ua.match(/firefox\/([\d.]+)/)) {
        createNonInstallDivPlugin();

        // Display the message and hide the main control
        DynamLib.show(_dwtParam._divDWTNonInstallContainerID);
        DynamLib.hide(_dwtParam.containerID);
        DynamLib.hide(_dwtParam._divDWTemessageContainer);

        //var divBlankCtrl = DynamLib.get("divBlank");
        //if (divBlankCtrl)
        //    divBlankCtrl.style.display = "inline";


        KISSY.use("overlay", function(S, o) {

            dlgInstall = new o.Dialog({
                srcNode: "#J_waiting",
                width: 392,
                height: 242,
                closable: false,
                mask: true,
                align: {
                    points: ['cc', 'cc']
                }
            });
            dlgInstall.show();
        });
    }
	for (var i = 0; i < document.links.length; i++) {
		if (document.links[i].className == "ClosetblCanNotScan") {
			document.links[i].onclick = closetblInstall_onclick;
		}
	}		
}

function Dynamsoft__notAllowedForChrome() {
	
	_dwtParam.bNotAllowedForChrome = true;
    ua = (navigator.userAgent.toLowerCase());
    // For mac, create the message to tell the user to install the plugin.
    if (ua.match(/chrome\/([\d.]+)/)) {
        createNonAllowedDivPlugin();
        // Display the message and hide the main control
        DynamLib.show(_dwtParam._divDWTNonInstallContainerID);
        DynamLib.hide(_dwtParam.containerID);
        DynamLib.hide(_dwtParam._divDWTemessageContainer);

        //var divBlankCtrl = DynamLib.get("divBlank");
        //if (divBlankCtrl)
        //    divBlankCtrl.style.display = "inline";

        KISSY.use("overlay", function(S, o) {

            dlgInstall = new o.Dialog({
                srcNode: "#J_waiting",
                width: 392,
                height: 227,
                closable: false,
                mask: true,
                align: {
                    points: ['cc', 'cc']
                }
            });
            dlgInstall.show();
        });
    }
	
	for (var i = 0; i < document.links.length; i++) {
		if (document.links[i].className == "ClosetblCanNotScan") {
			document.links[i].onclick = closetblInstall_onclick;
		}
	}	
}

function createNonAllowedDivPlugin() {

    var ObjString = [
		'<div class="D-dialog-body-NotAllowed">',
		_strDynamsoftWithClose,
		'<div class="box_title">',
		DynamLib.product.name,
		' plugin is not allowed to run on this site.</div>',
		'<ul>',
		'<li>Please click "<b>Always run on this site</b>" for the prompt "',
		DynamLib.product.name,
		' Plugin needs your permission to run", then <a href="javascript:void(0);" style="color:blue" class="ClosetblCanNotScan">close</a> this dialog OR refresh/restart the browser and try again.</li>',
		'</ul>',
		'</div>'];
	
    DynamLib.get(_dwtParam._divInstallBody).innerHTML = ObjString.join('');
}

function createNonInstallDivPlugin() {
	
    var ObjString = ['<div class="D-dialog-body">',
		_strDynamsoftWithClose,
		'<div class="box_title">',
		DynamLib.product.name,
		' is not installed</div>',
		'<ul>',
		'<li>Please click the below button to start downloading. You need manually install it after the downloading.</li>',
		'</ul>',
		'<p class="red">If you still see this dialog after the installation, please RESTART your browser.</p>',
		'<a id="btnInstall" href="',
		DynamLib.product.bChromeEdition ? DynamLib.product._strChromeEditionPath : DynamLib.product._strMSIPath,
		'" onclick="onClickInstallButton();"><div class="button"></div></a>',
		'</div>'];
	
    DynamLib.get(_dwtParam._divInstallBody).innerHTML = ObjString.join('');
}

function onClickInstallButton() {
    DynamLib.get("btnInstall").style.display = "none";
    //DynamLib.get("txtDetect").style.display = "";
    //DynamLib.get("imgWait").style.display = "";
}

function createNonInstallDivMac() {

    var ObjString = ['<div class="D-dialog-body-Mac">',
		_strDynamsoftWithClose,
		'<div class="box_title">',
		DynamLib.product.name,
		' is not installed</div>',
		'<ul>',
		'<li>Please click the below button to start downloading. You need manually install it after the downloading.</li>',
		'</ul>',
		'<p class="red">If you still see this dialog after the installation, please RESTART your browser.</p>',
		'If you are using Safari 5.0, you need to <a href="http://kb.dynamsoft.com/questions/666/How+to+run+Safari+5.0+in+32-bit+mode+on+Mac+OS+X"><span class="link">run the browser in 32-bit Mode</span></a>.',
		'<a id="btnInstall" href="',
		DynamLib.product._strPKGPath,
		'" onclick="onClickInstallButton()"><div class="button"></div></a>',
		'</div>'];
	
    DynamLib.get(_dwtParam._divInstallBody).innerHTML = ObjString.join('');
}

function closetblInstall_onclick() {
	if(dlgInstall){
		dlgInstall.destroy();
		dlgInstall = false;
	}
	
    DynamLib.hide(_dwtParam._divDWTNonInstallContainerID);
	DynamLib.show(_dwtParam.containerID);
	DynamLib.show(_dwtParam._divDWTemessageContainer);	
}

String.prototype.replaceAll = function(str1, str2) {
    var str = this;
    var result = str.replace(eval('/' + str1 + '/gi'), str2);
    return result;
}
function isDWTVersionLatest(latest) {
    var current = DWObject.VersionInfo.toLowerCase().replace(DynamLib.product.name.toLowerCase() + ' ', '');
    current = current.replace('trial'.toLowerCase(), '');
    current = current.replaceAll('[.]', ',');
    latest = latest.replaceAll('[.]', ',');

    currentArray = current.split(',');
    latestArray = latest.split(',');

    index = currentArray.length > latestArray.length ? currentArray.length : latestArray.length;
    for (var i = 0; i < index; i++) {
        if (currentArray[i] == null)
            currentArray[i] = 0;
        if (latestArray[i] == null)
            latestArray[i] = 0;

        else if (parseInt(currentArray[i]) < parseInt(latestArray[i])) {
            return false;
        }
    }
    return true;
}
function Dynamsoft_NeedUpgrade() {

				
    if (!isDWTVersionLatest(DynamLib.product.version)) {
	
		var ObjString = ['<div class="D-dialog-body">',
			_strDynamsoftWithClose,
			'<div class="box_title">',
			'A new version is available</div>',
			'<ul>',
			'<li>A new version of ' + DynamLib.product.name + ' is available.</li>',
			'<li>Do you want to upgrade now?</li>',
			'</ul>',
			'<p class="red">If you still see this dialog after the installation, please RESTART your browser.</p>',
			'<a id="btnInstall" href="',
			DynamLib.product.bChromeEdition ? DynamLib.product._strChromeEditionPath : DynamLib.product._strMSIPath,
			'" onclick="onClickInstallButton();"><div class="button"></div></a>',
			'</div>'];
		
		DynamLib.get(_dwtParam._divInstallBody).innerHTML = ObjString.join('');	

	
        // Display the message and hide the main control
        DynamLib.show(_dwtParam._divDWTNonInstallContainerID);
        DynamLib.hide(_dwtParam.containerID);
        DynamLib.hide(_dwtParam._divDWTemessageContainer);


        KISSY.use("overlay", function(S, o) {

            dlgInstall = new o.Dialog({
                srcNode: "#J_waiting",
                width: 392,
                height: 227,
                closable: false,
                mask: true,
                align: {
                    points: ['cc', 'cc']
                }
            });
            dlgInstall.show();
        });

    }
	
}
