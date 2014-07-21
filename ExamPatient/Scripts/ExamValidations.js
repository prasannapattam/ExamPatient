function setfilterValue(Id, Value, Type) {
   

    switch (Type) {
        case 'N':
            if (Value == '') 
            {
                document.getElementById(Id + "_txtNumeric").value = '';
            }
            else 
            {
                document.getElementById(Id + "_txtNumeric").value = Value;
            }
            break;
        case 'A':
            if (Value == '') {
                document.getElementById(Id + "_txtalphaNumeric").value = '';
            }
            else {
                document.getElementById(Id + "_txtalphaNumeric").value = Value;
            }
            break;
        case 'F':
            if (Value == '') {
                document.getElementById(Id + "_txtFloat").value = '';
            }
            else {
                document.getElementById(Id + "_txtFloat").value = Value;
            }
            break;
    }
}

function getfilterValue(Id, Type) {
    var getfilterctrlValue;
    switch (Type) {
        case 'N':
            getctrlValue = document.getElementById(Id + "_txtNumeric").value;
            break;
        case 'A':
            getctrlValue = document.getElementById(Id + "_txtalphaNumeric").value;
            break;
        case 'F':
            getctrlValue = document.getElementById(Id + "_txtFloat").value;
            break;             
    }
    return getfilterctrlValue;
}
    
   