/**
 * jQuery auto-correct plugin
 *
 * Version 1.0
 *
 * Copyright (c) 2009 Amit Badkas <amit@sanisoft.com>
 *
 * Dual licensed under the MIT (MIT-LICENSE.txt) and GPL (GPL-LICENSE.txt) licenses
 *
 * @URL http://www.sanisoft.com/blog/2009/06/22/jquery-auto-correct-plugin/
 *
 * @Example example.html
 */

// Wrap in a closure
jQuery.fn.autocorrect = function (options) {

    // If plugin attached to text/textarea field then don't need to proceed further
    if (0 > jQuery.inArray(jQuery(this).attr("type"), new Array("text", "textarea"))) {
        return;
    }

    // Default parameters for plugin with some default corrections
    var defaults = {
        corrections: {
            teh: "the",
            gr8: "great",
            taht: "that",
            ur: "you are",
            arent: "are not"
        }
    };

    // Merge corrections passed at run-time
    if (options && options.corrections) {
        options.corrections = jQuery.extend(defaults.corrections, options.corrections);
    }

    // Merge options passed at run-time
    var opts = jQuery.extend(defaults, options);

    // Function used to get caret's position
    getCaretPosition = function (oField) {
        var textArea = getTextAreaSelection(oField);
        return (textArea.selectionStart);
    }

    // Function used to set caret's position
    function setCaretPosition(oField, iCaretPos) {
        // IE Support
        if (document.selection) {
            var range = oField.createTextRange();
            range.moveStart('character', -oField.value.length);
            range.moveEnd('character', -oField.value.length);
            range.moveStart('character', iCaretPos);
            range.moveEnd('character', iCaretPos);
            range.collapse();
            range.select();
        }
        // Firefox support
        else if (oField.selectionStart || oField.selectionStart == "0") {
            oField.selectionStart = iCaretPos;
            oField.selectionEnd = iCaretPos;
        }
    }

    function getTextAreaSelection(oField) {
        var textArea = oField;

        if (textArea.selectionStart) { //FF
            //textArea.selectedText = textArea.substring(textArea.selectionStart, textArea.selectionEnd);
//            alert(textArea.selectionStart + ' ' + textArea.selectionEnd)
        }
        else if (document.selection) { //IE
            var bm = document.selection.createRange().getBookmark();
            var sel = textArea.createTextRange();
            sel.moveToBookmark(bm);

            var sleft = textArea.createTextRange();
            sleft.collapse(true);
            sleft.setEndPoint("EndToStart", sel);
            textArea.selectionStart = sleft.text.length
            textArea.selectionEnd = sleft.text.length + sel.text.length;
            textArea.selectedText = sel.text;
        }

        return textArea;
    }

    // Capture 'on key up' event for auto-correction
    this.keyup(function (e) {
        // If currently entered key is not 'space' then don't need to proceed further
        //Space: 32  COMMA: 188
        if (e.keyCode != 32 && e.keyCode != 188 && e.keyCode != 190) {
            return;
        }

        // Get caret's current position
        var caretPosition = getCaretPosition(this) - 1;

        // If caret's current position is less than one then don't need to proceed further
        if (1 > caretPosition) {
            return;
        }



        // Value of current field
        var valueOfField = this.value;

        // Get value of field upto caret's current position from start
        var stringUptoCaretPosition = (valueOfField).substr(0, caretPosition);

        // If more than one 'space' continuously then don't need to proceed further
        if (" " == stringUptoCaretPosition.charAt(caretPosition - 1)) {
            return;
        }

        // Get last element of array as string to search for correction
        var stringToSearch = stringUptoCaretPosition.substr(stringUptoCaretPosition.lastIndexOf(" ") + 1, stringUptoCaretPosition.length);
        stringUptoCaretPosition = stringUptoCaretPosition.substr(0, stringUptoCaretPosition.lastIndexOf(" "))

        // If string to search don't have any matching record in corrections then don't need to proceed further
        if (!opts.corrections[stringToSearch]) {
            return;
        }

        // Build string to replace using correction
        var stringToReplace = opts.corrections[stringToSearch];

        // Join the array to build new string
        if (stringUptoCaretPosition == "")
            stringUptoCaretPosition = stringToReplace;
        else
            stringUptoCaretPosition += " " + stringToReplace;

        // Get value of field upto end from caret's current position
        var stringFromCaretPositionUptoEnd = (valueOfField).substr(caretPosition);

        // Set new value of field
        this.value = (stringUptoCaretPosition + stringFromCaretPositionUptoEnd);

        // Set caret's position
        setCaretPosition(this, stringUptoCaretPosition.length + 1);
    });
};