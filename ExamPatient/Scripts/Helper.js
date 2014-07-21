function enterOnevalue(obj, e) {
    //debugger;
     docket = $('#txtDocketNumber').val();
     citation = $('#txtCitationNumber').val();
    if (e.type == 'mousedown' && e.button == 2) {
        if (obj == 0) {
            //citationCtrl.value = '';
            $('#txtCitationNumber').val('');
        }
        if (obj == 1) {
            //docketCtrl.value = '';
            $('#txtDocketNumber').val('')
        }
    }
    else {
        if (obj == 0) {
            if (docket != null && docket.length >= 1) {
                //citationCtrl.value = '';
                $('#txtCitationNumber').val('');
            }
        }
        if (obj == 1) {
            if (citation != null && citation.length >= 1) {
                //docketCtrl.value = '';
                $('#txtDocketNumber').val('')
            }
        }
    }
}
function ProcessMaintenance(value) {
    if (value == 'Cash Receipts Register Report' || value == 'Cash Receipts Summary Report'
    || value == 'Disbursements Report' || value == 'Dismissal Report' || value == 'Distribution Report' || value=="Bond Company Report" || value=="Bond Register Report"
    || value == 'Void Receipt Report' || value == 'User Reconciliation Report' || value == 'Audit Browse Report' || value =='Put Money Back in Trust Report') {
        document.body.style.cursor = '';
        return true;
    }
    var timeoutID = window.setTimeout(ProcessImg, 500);
    $('#hdnTimeOut').val(timeoutID);
    return true;
}
function RadioButtonCheckedUnChecked() {
    var allRadios = $('input[type=radio]')
    var radioChecked;

    var setCurrent =
                                        function (e) {
                                            var obj = e.target;

                                            radioChecked = $(obj).attr('checked');
                                        }

    var setCheck =
                                    function (e) {

                                        if (e.type == 'keypress' && e.charCode != 32) {
                                            return false;
                                        }

                                        var obj = e.target;

                                        if (radioChecked) {
                                            $(obj).attr('checked', false);
                                        } else {
                                            $(obj).attr('checked', true);
                                        }
                                    }

    $.each(allRadios, function (i, val) {
        var label = $('label[for=' + $(this).attr("id") + ']');

        $(this).bind('mousedown keydown', function (e) {
            setCurrent(e);
        });

        label.bind('mousedown keydown', function (e) {
            e.target = $('#' + $(this).attr("for"));
            setCurrent(e);
        });

        $(this).bind('click', function (e) {
            setCheck(e);
        });

    });
}
function ProcessImg() {
    document.body.style.cursor = 'wait';
    $("#mydialog").dialog({width:110,height: 80,modal: true});
    $(".ui-dialog-titlebar").hide()     
    tdVisibility('mydialog', 'visible');
}

function ProcessUnload() {
    document.body.style.cursor = '';
    window.clearTimeout($('#hdnTimeOut').val());
    tdVisibility('mydialog', 'hidden');
 }

function ProcessStart() {
    var timeoutID = window.setTimeout(ProcessImg, 500);
    $('#hdnTimeOut').val(timeoutID);
    return true;
}

function attachAddress() {
    $('#defendantAddress').dialog('open');
    $("#defendantAddress").dialog({
        width: 800,
        modal: true,
        buttons: {
            OK: function () {
                $(this).dialog('close');
            }
        }
    });
    tdVisibility('defendantAddress', 'visible');
    return false;
}

function attachImage() {
    $('#citationImage').dialog('open');
    $("#citationImage").dialog({
        width: 500,
        height: 555,
        modal: true,
        buttons: {
            OK: function () {
                $(this).dialog('close');
            }
        }
    });
    tdVisibility('citationImage', 'visible');
    return false;
}

function notAuthorised() {
    $('#notAuthorisedImg').dialog('open');
    $("#notAuthorisedImg").dialog({
        modal: true,
        buttons: {
            OK: function () {
                window.location.replace("HomePage.aspx"); 
                $(this).dialog('close');
            }
        }
    });
    tdVisibility('notAuthorisedImg', 'visible');
}


function tdVisibility(tdName, visibility) {
    if ($("#" + tdName).length != 0)
        $("#" + tdName)[0].style.visibility = visibility;
}

function AssignDateVal(id) {
    var dateText = $("#" + id + "_txtYear").val();
    if (dateText.length == 10) {
        setValue(id, dateText, 'D');
    }
    return false;
}

function AssignNewDate(id) {
    var dateText = gettxtValue(id, 'D');
    if ($("#" + id + "_txtYear").val().length == 10) {
        if (dateText.length == 16) {
            $("#" + id + "_txtYear").val(dateText.substring(12, 16));
        }
    }
    return false;
}

function ProcessExecute(id) {
    $("#" + id).datepicker({
        showOn: 'button',
        buttonImage: '/exampatient/Images/expand_button1.gif',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        buttonText: 'Date'
    });
}

function FetchCitationId(docketValue, citationValue,juvenileValue) {
    docket = docketValue;
    citation = citationValue;
    $.ajax({
        type: "POST",
        url: "../WebServices/OfficeServices.asmx/ViewCitationId",
        data: "{docketNumber:'" + docketValue + "',docketHeaderField:'" + citationValue + "',juvenileStatus:'" + juvenileValue + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: cbSuccess,
        async: false,

        error: AjaxFailed
    });
}

function cbSuccesDefault(msg) {
    RedirectToCurrentPage(msg);

}
function RedirectToCurrentPage(msg) {
    var splitDkt = docket.split(',');//in some scenario the docket number is coming duplicate.so to retrict that this is a  work around as we are unable to reproduce that scenario
    if (splitDkt.length > 0) 
    {
        docket = splitDkt[0];
    }
    var citationId = msg.d;
    //var citationNum = msg.d.CitationNumber;
    queryString = '?hdnCitationId=' + citationId;
    queryString = queryString+'&dkt=' + docket;
    queryString = queryString+'&cit=' + citation;
    var split = location.pathname.split('/');
    if (split.length > 0) {
        var pageName = split[split.length - 1];
        location.href = pageName + queryString;
    }
}

function SetSelectedRecord(Grid)
 {
    Grid.selectRecord(0);
 }

/*The Following code is for AutoComplete TextBox*/
var isNN = (navigator.appName.indexOf("Netscape") != -1);
function Alert(Message, Event) {
   
    if (Event != null) 
    {
        Event.preventDefault();
    }   
    $('#lblText')[0].innerText = Message;
    $("#alertMsg").dialog
        (
            {
                bgiframe: true,
                autoOpen: false,
                width: 450,
                title: 'Alert',
                modal: true,
                closeOnEscape: true,
                buttons:
                {
                    'OK': function () {
                        $(this).dialog('close');
                        
                    }
                }
            }
        );
    $("#alertMsg").dialog('open')
}

function Popup(url) {
    var w = window.showModalDialog(url, '', 'dialogWidth:20.0cm;dialogHeight:12.2cm'); 
    return false;
}
function Confirm(Message,Event) {
    if (Event != null) {
        Event.preventDefault();
    }  
    $('#lblText')[0].innerText = Message;
    $("#alertMsg").dialog
        (
            {
                bgiframe: true,
                autoOpen: false,
                width: 450,
                title: 'Confirm',
                modal: true,
                closeOnEscape: true,
                buttons:
                {
                    'Yes': function () {
                        //return true;
                        //debugger;
                        window.location.href = window.location.href;

                    }
                    ,
                    'No': function () {
                        
                        $(this).dialog('close');
                        return false;
                    }
                }
            }
        );
            $("#alertMsg").dialog('open')
}
function autoTab(input, len, e) {
    var keyCode = (isNN) ? e.which : e.keyCode;
    var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];
    if (input.value.length >= len && !containsElement(filter, keyCode)) {
        input.value = input.value.slice(0, len);
        //  debugger;
        if (input.form!=null){
        if (input.form[(getIndex(input) + 1) % input.form.length].disabled == false) {
            var controlIdFocus = input.form[(getIndex(input) + 1) % input.form.length].maxLength;
            if (input.form[(getIndex(input) + 1) % input.form.length].maxLength == 10 || input.form[(getIndex(input) + 1) % input.form.length].maxLength == 12)
        {
            ctrlid = input.form[(getIndex(input) + 2) % input.form.length];
            if (ctrlid != null && ctrlid.value != "" && ctrlid.value != null && ctrlid.type == "text") {
                ctrlid.select();
            }
            $("#" + ctrlid.id).focus();
            }
            else{
                ctrlid = input.form[(getIndex(input) + 1) % input.form.length];
                if (ctrlid != null && ctrlid.value != "" && ctrlid.value != null && ctrlid.type=="text") {
                    ctrlid.select();
                }
            $("#" + ctrlid.id).focus();
            }
    }
        }

    }
    function containsElement(arr, ele) {
        var found = false, index = 0;
        while (!found && index < arr.length)
            if (arr[index] == ele)
                found = true;
            else
                index++;
        return found;
    }

    function getIndex(input) {
        var index = -1, i = 0, found = false;
        while (i < input.form.length && index == -1)
            if (input.form[i] == input) index = i;
            else i++;
        return index;
    }
    return true;
}

/*The following 3 methods are used to restrict date when entered*/

function GetTextValue(id, data) {
    $("#" + id).val(data);
    return $("#" + id).val();
}
function ProcessLengthData(textDataLength, TextValue, dayValue, id) {
    var finalText = "";
    if (textDataLength == 1) {
        if (TextValue > 3) {
            finalText = GetTextValue(id, '');
        }
    }
    else if (textDataLength == 2) {
        if (TextValue.substring(0, 1) > 3 && TextValue > dayValue) {
            finalText = GetTextValue(id, '');
        }
        else if (TextValue.substring(1, 2) >= 1 && TextValue.substring(0, 1) >= 3 && dayValue == 30) {
            finalText = GetTextValue(id, TextValue.substring(0, 1));
        }
        else if (TextValue.substring(1, 2) > 1 && TextValue.substring(0, 1) >= 3 && dayValue == 31) {
            finalText = GetTextValue(id, TextValue.substring(0, 1));
        }
    }
    return finalText;
}
function checkDateValidation(id, input, len, e, MonthID) {
    var TextValue = $("#" + id).val();
    var TextValue3 = $("#" + MonthID).val();
    var textDataLength = TextValue.length;
    var TextValue1 = TextValue;
    var MonthTextID = id.indexOf("_txtMonth");
    var DayTextID = id.indexOf("_txtDay");
    var YearTextID = id.indexOf("_txtYear");
    if (MonthTextID != -1) {
        if (textDataLength == 1) {
            if (TextValue > 1) {
                TextValue1 = GetTextValue(id, '');
            }
        }
        else if (textDataLength == 2 && TextValue.substring(0, 1) != 0) {
            if (TextValue.substring(0, 1) > 1 && TextValue > 12) {
                TextValue1 = GetTextValue(id, '');
            }
            else if (TextValue.substring(1, 2) > 2) {
                TextValue1 = GetTextValue(id, TextValue.substring(0, 1));
            }
        }
    }
    else if (DayTextID != -1) {
        if (TextValue3 != undefined && TextValue3 !='') {
            if (TextValue3 == 02) {
                if (textDataLength == 1) {
                    if (TextValue > 2) {
                        TextValue1 = GetTextValue(id, '');
                    }
                }
                else if (textDataLength == 2) {
                    if (TextValue.substring(1, 2) > 9 && TextValue > 29) {
                        TextValue1 = GetTextValue(id, TextValue.substring(0, 1));
                    }
                    else if (TextValue > 29) {
                        TextValue1 = GetTextValue(id, '');
                    }

                }
            }
            else if (TextValue3 == 01 || TextValue3 == 03 || TextValue3 == 05 || TextValue3 == 07 || TextValue3 == 08 || TextValue3 == 10 || TextValue3 == 12) {
                TextValue1 = ProcessLengthData(textDataLength, TextValue, 31, id);
            }
            else if (TextValue3 == 04 || TextValue3 == 06 || TextValue3 == 09 || TextValue3 == 11) {
                TextValue1 = ProcessLengthData(textDataLength, TextValue, 30, id);
            }
        }
        else {
            if (textDataLength == 1) {
                if (TextValue > 3) {
                    TextValue1 = GetTextValue(id, '');
                }
            }
            else if (textDataLength == 2) {
                if (TextValue.substring(0, 1) > 3 && TextValue > 31) {
                    TextValue1 = GetTextValue(id, '');
                }
                else if (TextValue.substring(1, 2) > 1 && TextValue.substring(0, 1) >= 3) {
                    TextValue1 = GetTextValue(id, TextValue.substring(0, 1));
                }
            }
        }
    }
    if ((textDataLength == 2 && (MonthTextID != -1 || DayTextID != -1)) || (YearTextID != -1)) {
        autoTab(input, len, e)
    }
    return false;
}

/**/

/*JavaScript function to accept only numeric values*/
function allowNumeric() {
    var iKeyCode;

    iKeyCode = event.keyCode;

    if (iKeyCode < 48 || iKeyCode > 57) {
        event.keyCode = null;
    }
    return;
}


function allowLimitedNumeric(e,control) {
    var iKeyCode;

    iKeyCode = event.keyCode;

    if (iKeyCode < 48 || iKeyCode > 57) {
        event.keyCode = null;
    }
    if (control.value.length >= 6) {
        if (document.selection.createRange().text.length == 0) {
            e.keyCode = 0;
        }
    }
    return;
}
/*JavaScript function to copy only numeric values */
function IsNumeric(sText) {
    var ValidChars = "0123456789";
    var IsNumber = true;
    var Char;
    for (i = 0; i < sText.value.length && IsNumber == true; i++) {
        Char = sText.value.toString().charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            IsNumber = false;
            break;
        }
    }
    if (!IsNumber) {
        sText.value = "";
    }
}

/*JavaScript function to accept only numeric values with comma*/
function allowNumericWithComma() {
    var iKeyCode;

    iKeyCode = event.keyCode;

    if ((iKeyCode < 48 || iKeyCode > 57) && (iKeyCode != 44)) {
        event.keyCode = null;
    }
   
    return;
}

/*JavaScript function to accept only numeric values with a decimal point*/
function allowFloat(e, control) {
    if (e.keyCode == 46) {
        var valuepart = new RegExp("\\.");
        var ch = valuepart.exec(control.value);
        if (ch == ".") {
            if (document.selection.createRange().text.length == 0) {
                e.keyCode = 0;
            }
        }
    }
    else if ((e.keyCode >= 48 && e.keyCode <= 57) || e.keyCode == 8)//Numbers or BackSpace
    {
        if (control.value.indexOf('.') != -1)//. Exisist in TextBox 
        {
            var pointIndex = control.value.indexOf('.');
            var beforePoint = control.value.substring(0, pointIndex);
            var afterPoint = control.value.substring(pointIndex + 1);
            var iCaretPos = 0;
            if (document.selection) {
                if (control.type == 'text') // textbox
                {
                    var selectionRange = document.selection.createRange();
                    selectionRange.moveStart('character', -control.value.length);
                    iCaretPos = selectionRange.text.length;
                }
            }
            if (iCaretPos > pointIndex && afterPoint.length >= 2) {
                if (document.selection.createRange().text.length == 0) {
                    e.keyCode = 0;
                }
            }
            else if (iCaretPos <= pointIndex && beforePoint.length >= 6) {
                if (document.selection.createRange().text.length == 0) {
                    e.keyCode = 0;
                }
            }
        }
        else//. Not Exisist in TextBox
        {
            if (control.value.length >= 6) {
                if (document.selection.createRange().text.length == 0) {
                    e.keyCode = 0;
                }
            }
        }
    }
    else {
        if (document.selection.createRange().text.length <= 11) {
            e.keyCode = 0;
        }
    }
}

function checkFloat(text) {
    if (text.value == '.') {
        $('#' + text.id).val('0.');
    }
}

/*JavaScript function to accept only alphanumeric values*/
function allowAlphaNumeric() {
    var iKeyCode;

    iKeyCode = event.keyCode;

    if (!((iKeyCode > 47 && iKeyCode < 58) || (iKeyCode > 64 && iKeyCode < 91) || (iKeyCode == 32) || (iKeyCode > 96 && iKeyCode < 123))) {
        event.keyCode = null;
    }
    return;
}

/*JavaScript function to accept only alphanumeric values with out space*/
function allowAlphaNumericWithOutSpace() {
    var iKeyCode;

    iKeyCode = event.keyCode;

    if (!((iKeyCode > 47 && iKeyCode < 58) || (iKeyCode > 64 && iKeyCode < 91) || (iKeyCode > 96 && iKeyCode < 123))) {
        event.keyCode = null;
    }
    return;
}


function EnableAllValidators(enable) {
    for (var i = 0; i < Page_Validators.length; i++) {
        ValidatorEnable($('#' + Page_Validators[i].id)[0], enable)
    }
}
/*JavaScript function to perform expand / collapse functionality*/
function expandcollapse(exObj, clObj) {
    var dataObj = eval(exObj);
    if (dataObj != null) {
        for (var i = 0; i < dataObj.length; i++) {
            if (clObj == 0) {

                $("#" + dataObj[i]).slideDown('fast');
            }
            else {

                $("#" + dataObj[i]).slideUp('fast');
            }
        }
    }
    return false;
}

/*JavaScript function to perform reset functionality for the given input ID*/
function ResetDiv(divid) {

    var message = 'Are you sure you want to reset and loose changes';
    if (!confirm(message))
        return false;

    //Textbox, Password, TextAread and hidden field
    $('input:text, input:hidden, input:password, TEXTAREA', '#' + divid).each(function () {
        this.value = this.defaultValue
    });
    //Checkbox, Radio buttons
    $('input:checkbox, input:radio', '#' + divid).each(function () {
        this.checked = this.defaultChecked
    });
    //Select
    $('select option', '#' + divid).each(function () {
        this.selected = this.defaultSelected
    });

    return false;
}

/*JavaScript function to perform reset functionality for the given input ID*/
function ResetClearDiv(divid) {
    //Textbox, Password, TextAread and hidden field
    $('input:text, TEXTAREA', '#' + divid).each(function () {
        this.value = ''
    });
    //Checkbox, Radio buttons
    $('input:checkbox, input:radio', '#' + divid).each(function () {
        this.checked = false
    });
    //Select
    $('select', '#' + divid).each(function () {
        this.selectedIndex = 0;
    });

    //EnableAllValidators(false);
    return false;
}
function ControlDisabled(divid) {
    $('input:text,TEXTAREA,select,checkbox,radio', '#' + divid).each(function () {
        this.disabled = true
    });

}
function ControlEnabled(divid) {
    $('input:text,TEXTAREA,select,checkbox', '#' + divid).each(function () {
        this.removeAttribute('disabled')
    });
    $('input:radio', '#' + divid).each(function () {
        this.removeAttribute('disabled')
    });
}

//To trim the sent value
function StringTrim(value) {
    var a = value.replace(/^\s+/, '');
    return a.replace(/\s+$/, '');
};

function readOnlyKeyDownFn() {
    var code;
    code = event.keyCode;
    if (code == '8') {
        event.cancelBubble = true;
        event.returnValue = false;
        event.keyCode = 0;
    }
}
function PreventMoveBack() {
    var code;
    code = event.keyCode;
    if ((code == '8' || code == '13') && event.srcElement.readOnly == true) {
        event.returnValue = false;
        event.keyCode = 0;
    }

}
/* The following function is used to restrict a multiline textbox to its maxlength */
function CheckLength(text, maxlength) {
    if (text.value.length > maxlength) {
        text.value = text.value.substring(0, maxlength);
    }
}


function ResetSearchDiv(searchdivid, updatedivid, adddivid) {
    $("#" + updatedivid).hide();
    $("#" + adddivid).hide();
    ResetClearDiv(searchdivid);
    return false;
}

function DateFormat(str) {
    if (str.match("/")) {
        var dateArray = str.split("/");
        if (dateArray[0].length == 1) {
            dateArray[0] = "0" + dateArray[0];
        }
        if (dateArray[1].length == 1) {
            dateArray[1] = "0" + dateArray[1];
        }
        str = dateArray[0] + "/" + dateArray[1] + "/" + dateArray[2];
        return str;
    }
}
function GetTimeFormat(time) {
    var timeFormat;

    if (time != null && time != "") {
        var Hours = time.split(':')[0];
        var mints = time.split(':')[1].split(' ')[0];
        if (time.split(':')[1].split(' ')[1] == "PM") {
            if (parseInt(Hours) < 12) {
                Hours = (parseInt(Hours) + 12);
            }
        }

        if (time.split(':')[1].split(' ')[1] == "AM") {
            if (parseInt(Hours) >= 12) {
                Hours = (parseInt(Hours) - 12);
            }
        }
        timeFormat = " " + Hours + ":" + mints;
    }
    else {
        timeFormat = " 00:00:00";
    }

    return timeFormat;
}

function HandleFocus(targetControlId) {
    //debugger;
           if (event.which || event.keyCode) {
        if ((event.which == 9) || (event.keyCode == 9)) {
                $("#" + targetControlId).focus();
            return false;
        }
    }
}
function CurrencyFormatted(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}
function AssignClass(cntrl) {
    
    $(":checkbox").each(function () {
        if (this.checked == true) {
            this.className = "chkCss";
        }
        else {
            this.className = "";
        }
    });

    $(":radio").each(function () {
        if (this.checked == true) {
            this.className = "chkCss";
        }
        else {
            this.className = "";
        }
    });
}