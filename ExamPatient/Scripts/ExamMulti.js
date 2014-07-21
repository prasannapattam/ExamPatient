           function ValidateDate(sender, args) {
                var strDate;
                var strYear = args.Value;
                var classname=sender.classname;
                var strTextBox1= $("#" + classname + '_' + 'txtMonth').val();
                var strTextBox2= $("#" + classname + '_' + 'txtDay').val();
                var validationExpression;
                 ctrlType="Date";
                 validationExpression = /^(\d{1,2})[./-](\d{1,2})[./-](\d{4})$/; //for date(mm/dd/yyyy)
                 checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args);
               var actualDays = getDaysMonth(strYear, strTextBox1);
               if (parseInt(strTextBox2) > parseInt(actualDays) || (actualDays==0)) {
                   args.IsValid = false;
               }
                strTextBox1='';
               strTextBox2='';
               return;
           }
           
           function ValidatePhone(sender, args) {
                var strDate;
                var strYear = args.Value;
                var classname=sender.classname;
                var strTextBox1= $("#" + classname + '_' + 'txtArea').val();
                var strTextBox2= $("#" + classname + '_' + 'txtExchange').val();
                var validationExpression;
                ctrlType="Phone";
                validationExpression =/^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/; //for USA format Phone no.
               checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args);              
               return;
           }

           function ValidateZip(sender, args) {
                var strDate;
                var strYear = args.Value;
                var classname=sender.classname;
                var strTextBox1= $("#" + classname + '_' + 'txtCode').val();
                var strTextBox2='';
                var validationExpression;
                ctrlType="Zipcode"; 
                 validationExpression = /^([0-9]){5}(([ ]|[-])?([0-9]){4})?$/; //for US Zip Code 
                checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args);
               return;
           }
           
           function ValidateEmail(sender, args) {
                var strDate;
                var strYear = args.Value;
                var validationExpression;
                var strTextBox1='';
                var strTextBox2='';
                ctrlType="Email"; 
                validationExpression = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; //for Email
                checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args);
               return;
           }
           
           function ValidateTime(sender, args) {
                var strDate;
                var strYear = args.Value;
                var classname=sender.classname;
                var strTextBox1= $("#" + classname + '_' + 'txtHour').val();
                var strTextBox2='';
                var validationExpression;
                ctrlType="Time";     
                validationExpression = /^([0-9]){2}(([ ]|[:])?([0-9]){2})?$/; //for Time
                checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args); 
      
                   if (strYear>59 || strTextBox1>12 || strTextBox1<1) {
                   args.IsValid = false;
               }
               strTextBox1='';
               return;
           }           
           
            function ValidateSSN(sender, args) {
                var strDate;
                var strYear = args.Value;
                var classname=sender.classname;
                var strTextBox1= $("#" + classname + '_' + 'txtssnArea').val();
                var strTextBox2= $("#" + classname + '_' + 'txtGroup').val();
                var validationExpression;
                 ctrlType="SSN";    
                 validationExpression =/^([0-9]{3}[-]*[0-9]{2}[-]*[0-9]{4})*$/; //for USA SSN  
                 checkValidations(strTextBox1,strTextBox2,strYear,validationExpression,ctrlType,args);     
               return;
           }
           
           function ValidateTotalDate(sender, args) {
               var txtValue=gettxtValue(sender.classname,'D',true)
               if (txtValue=='')
               {
                   args.IsValid = false;
               }
               return;
           }
           
           function ValidateTotalPhone(sender, args) {
               var txtValue=gettxtValue(sender.classname,'P',true)
               if (txtValue=='')
               {
                   args.IsValid = false;
               }
               return;
           }
           
           function ValidateTotalZip(sender, args) {
               var txtValue=gettxtValue(sender.classname,'Z',true)
               if (txtValue=='')
               {
                   args.IsValid = false;
               }
               return;
           }
          
           function ValidateTotalTime(sender, args) {
               var txtValue=gettxtValue(sender.classname,'T',true)
               if (txtValue=='')
               {
                   args.IsValid = false;
               }
               return;
           }
           
           function ValidateTotalSSN(sender, args) {
               var txtValue=gettxtValue(sender.classname,'S',true)
               if (txtValue=='')
               {
                   args.IsValid = false;
               }
               return;
           }
           
           function getDaysMonth(year, month) {
           
               var days = 0;
               if(month!=null && month.length!=2)
               {
                    month="0" + month;
               }
               switch (month) {
                   case '01':
                       days = 31;
                       break;
                   case '02':
                       if (year % 4 == 0) {
                           days = 29;
                       }
                       else {
                           days = 28;
                       }
                       break;
                   case '03':
                       days = 31;
                       break;
                   case '04':
                       days = 30;
                       break;
                   case '05':
                       days = 31;
                       break;
                   case '06':
                       days = 30;
                       break;
                   case '07':
                       days = 31;
                       break;
                   case '08':
                       days = 31;
                       break;
                   case '09':
                       days = 30;
                       break;
                   case '10':
                       days = 31;
                       break;
                   case '11':
                       days = 30;
                       break;
                   case '12':
                       days = 31;
                       break;
                   default:
                       days = 0;
                       break;
               }
               return days;
           }
           
        // The following method is used to set value to the control
        function setValue(Id, Value,Type) {
        var strSplit;
        var strSplit1;
        
        switch (Type) 
        {
            case 'D':
            if(Value=='')
            {
                document.getElementById(Id + "_txtMonth").value = '';
                document.getElementById(Id + "_txtDay").value = '';
                document.getElementById(Id + "_txtYear").value = '';
            }
            else{
                strSplit = Value.split('/');
                if(strSplit[0].length==1)
                {
                document.getElementById(Id + "_txtMonth").value = '0' + strSplit[0];
                }
                else
                {
                document.getElementById(Id + "_txtMonth").value = strSplit[0];
                }
                if(strSplit[1].length==1)
                {
                document.getElementById(Id + "_txtDay").value = '0' + strSplit[1];
                }
                else
                {
                document.getElementById(Id + "_txtDay").value = strSplit[1];
                }
                document.getElementById(Id + "_txtYear").value = strSplit[2];
                }
                break;
            case 'T':
            if (document.getElementById(Id + "_txtHour") != null)
            {
            if(Value=='')
            {
            document.getElementById(Id + "_txtHour").value = '';
                document.getElementById(Id + "_txtMinute").value = '';
                document.getElementById(Id + "_cbTime").selectedIndex = 0;
                }
            else
            {
                strSplit = Value.split(':');
                            if(strSplit[0].length==1)
                {
                document.getElementById(Id + "_txtHour").value = '0' + strSplit[0];
                }
                else
                {
                document.getElementById(Id + "_txtHour").value = strSplit[0];
                }

               if(strSplit[1].length==1)
                {
                document.getElementById(Id + "_txtMinute").value = '0' + strSplit[1];
                }
                else
                {
                document.getElementById(Id + "_txtMinute").value = strSplit[1];
                }
                strSplit1 = document.getElementById(Id + "_txtMinute").value.split(' ');
                document.getElementById(Id + "_txtMinute").value = strSplit1[0]
                if (strSplit1[1] == 'AM') {
                    document.getElementById(Id + "_cbTime").selectedIndex = 0;
                }
                else if (strSplit1[1] == 'PM') {
                    document.getElementById(Id + "_cbTime").selectedIndex = 1;
                }
                }
                
                }
                break;
            case 'E': 
            if(Value=='')
            {
            document.getElementById(Id + "_txtEmail").value = '';
            }
            else{
            document.getElementById(Id + "_txtEmail").value = Value;
            }
            break;
            case 'P': 
                 if(Value=='')
            {
            document.getElementById(Id + "_txtArea").value = '';
                document.getElementById(Id + "_txtExchange").value = '';
                document.getElementById(Id + "_txtPhone").value = '';
            }
            else{
                strSplit = Value.split('-');
                document.getElementById(Id + "_txtArea").value = strSplit[0];
                document.getElementById(Id + "_txtExchange").value = strSplit[1];
                document.getElementById(Id + "_txtPhone").value = strSplit[2];
                }
            
            break;
            case 'Z': 
             if(Value=='')
            {
           document.getElementById(Id + "_txtZip").value = '';
                document.getElementById(Id + "_txtCode").value = '';
            }
            else{
                strSplit = Value.split('-');
                document.getElementById(Id + "_txtZip").value = strSplit[0];
                document.getElementById(Id + "_txtCode").value = strSplit[1];
                }
            break;
            case 'S': 
             if(Value=='')
            {
           document.getElementById(Id + "_txtssnArea").value = '';
                document.getElementById(Id + "_txtGroup").value = '';
                document.getElementById(Id + "_txtSerial").value = '';
            }
            else{
                strSplit = Value.split('-');
                document.getElementById(Id + "_txtssnArea").value = strSplit[0];
                document.getElementById(Id + "_txtGroup").value = strSplit[1];
                document.getElementById(Id + "_txtSerial").value = strSplit[2];
                }
            break;
            
        }
    }
    
    function checkValidations(firstBox,secondBox,thirdBox,expression,controlType,args) {
    var strDate;
    var regExp;
    regExp = new RegExp(expression);
     switch (controlType) 
        {
            case 'Date':
             if (firstBox == '' || secondBox == '') 
               {
                   args.IsValid = false;
                   return;
               }    
             strDate = firstBox + '/' + secondBox + '/' + thirdBox;
              if (strDate.length != 10 || !regExp.test(strDate)) 
               {
                   args.IsValid = false;
                   return;
               }    
               var subYear = thirdBox.substring(0,1);
               if (firstBox == 00 || secondBox == 00 || subYear==0) 
               {
                   args.IsValid = false;
                   return;
               }                     
                break;

            case 'Phone':  
            if (firstBox == '' || secondBox == '') 
               {
                   args.IsValid = false;
                   return;
               }      
            strDate = firstBox + '-' + secondBox + '-' + thirdBox;
               firstBox='';
               secondBox='';       
             if (strDate.length != 12 || !regExp.test(strDate)) 
               {
                   args.IsValid = false;
                   return;
               }
            break;
            
            case 'Zipcode': 
            if (thirdBox == '') 
               {
                   args.IsValid = false;
                   return;
               }                             
            strDate = thirdBox + '-' + firstBox; 
            
             if (thirdBox.length != 5 || (strDate.length != 10 && firstBox!='')) 
               {
                   args.IsValid = false;
                   return;
               }
               thirdBox=''; 
            break;
            
            case 'Email':  
             strDate =  thirdBox; 
             if ( !regExp.test(strDate)) 
               {
                   args.IsValid = false;
                   return;
               }
            break;
            
            case 'Time':  
            if (firstBox == '') 
               {
                   args.IsValid = false;
                   return;
               }                             
            strDate = firstBox + ':' + thirdBox;             
             if (strDate.length != 5 || !regExp.test(strDate)) 
               {
                   args.IsValid = false;
                   return;
               }
            break;
            
            case 'SSN':  
            if (firstBox == '' || secondBox == '') 
               {
                   args.IsValid = false;
                   return;
               }      
            strDate = firstBox + '-' + secondBox + '-' + thirdBox;
               firstBox='';
               secondBox='';       
             if (strDate.length != 11 || !regExp.test(strDate)) 
               {
                   args.IsValid = false;
                   return;
               }
            break;
            
        }
    }


    function IsValidDate(Day,Mn,Yr){
    var DateVal = Mn + "/" + Day + "/" + Yr;
    var dt = new Date(DateVal);

    if(dt.getDate()!=Day){
       // alert('Invalid Date');
        return(false);
        }
    else if(dt.getMonth()!=Mn-1){
    //this is for the purpose JavaScript starts the month from 0
      //  alert('Invalid Date');
        return(false);
        }
    else if(dt.getFullYear()!=Yr){
       // alert('Invalid Date');
        return(false);
        }
        
    return(true);
 }





    
    // The following method is used to get value form the control and also used to validate partial data
     function gettxtValue(Id,Type,PartialValuesCheck,IsValidDateInd) {
        var getctrlValue;
        switch (Type) 
        {
            case 'D':

            if(IsValidDateInd)
            {

            getctrlValue='';

             if($("#" + Id + "_txtMonth").val() !='' && $("#" + Id + "_txtDay").val() !='' && $("#" + Id + "_txtYear").val() !='')
                    {
                         if(IsValidDate( $("#" + Id + "_txtDay").val(),$("#" + Id + "_txtMonth").val() ,$("#" + Id + "_txtYear").val()))
                         {
                            getctrlValue=$("#" + Id + "_txtMonth").val() +'/'+  $("#" + Id + "_txtDay").val() +'/'+ $("#" + Id + "_txtYear").val();
                         }
                    }
                     break;
            }


                   if(PartialValuesCheck)
                   {
                        if($("#" + Id + "_txtYear").val()=='')
                        {    
                            if(!($("#" + Id + "_txtMonth").val()=='' && $("#" + Id + "_txtDay").val()=='' && $("#" + Id + "_txtYear").val()==''))
                            {
                                getctrlValue='';
                            }
                        }
                     break;
                    }
                   
                    if($("#" + Id + "_txtMonth").val()=='' && $("#" + Id + "_txtDay").val()=='' && $("#" + Id + "_txtYear").val()=='')
                    {
                        getctrlValue='';
                    }
                    else
                    {
                        getctrlValue=$("#" + Id + "_txtMonth").val() +'/'+  $("#" + Id + "_txtDay").val() +'/'+ $("#" + Id + "_txtYear").val();
                    }
                    break;
                    
            case 'T':
             if(PartialValuesCheck)
                   {
                        if($("#" + Id + "_txtMinute").val()=='')
                        {    
                            if(!($("#" + Id + "_txtHour").val()=='' && $("#" + Id + "_txtMinute").val()=='' ))
                            {
                                getctrlValue='';
                            }
                        }
                     break;
                    }
                    
                if($("#" + Id + "_txtHour").val()=='' && $("#" + Id + "_txtMinute").val()=='')
                {
                    getctrlValue='';
                }
                else
                {
                    getctrlValue=$("#" + Id + "_txtHour").val() +':'+  $("#" + Id + "_txtMinute").val() + ' '+ $("#" + Id + "_cbTime").val();
                }
                break;
            case 'E': 
            getctrlValue=$("#" + Id + "_txtEmail").val();
            break;
            case 'Z': 
                if(PartialValuesCheck)
                   {
                        if($("#" + Id + "_txtZip").val()=='')
                        {    
                            if(!($("#" + Id + "_txtZip").val()=='' && $("#" + Id + "_txtCode").val()=='' ))
                            {
                                getctrlValue='';
                            }
                        }
                     break;
                    }
            
            if($("#" + Id + "_txtZip").val() =='' &&  $("#" + Id + "_txtCode").val()=='')
            {
            getctrlValue='';
            }
            else
            {
            getctrlValue=$("#" + Id + "_txtZip").val() +'-'+  $("#" + Id + "_txtCode").val();
            }
            break;
            case 'S': 
            
            if(PartialValuesCheck)
                   {
                        if($("#" + Id + "_txtSerial").val()=='')
                        {    
                            if(!($("#" + Id + "_txtssnArea").val()=='' && $("#" + Id + "_txtGroup").val()=='' && $("#" + Id + "_txtSerial").val()==''))
                            {
                                getctrlValue='';
                            }
                        }
                     break;
                    }
            
            if($("#" + Id + "_txtssnArea").val() =='' &&  $("#" + Id + "_txtGroup").val()=='' && $("#" + Id + "_txtSerial").val()=='')
            {
            getctrlValue='';
            }
            else
            {
            getctrlValue=$("#" + Id + "_txtssnArea").val() +'-'+  $("#" + Id + "_txtGroup").val() +'-'+ $("#" + Id + "_txtSerial").val();
            }
            break;
            case 'P': 
                        if(PartialValuesCheck)
                   {
                        if($("#" + Id + "_txtPhone").val()=='')
                        {    
                            if(!($("#" + Id + "_txtArea").val()=='' && $("#" + Id + "_txtExchange").val()=='' && $("#" + Id + "_txtPhone").val()==''))
                            {
                                getctrlValue='';
                            }
                        }
                     break;
                    }
            
            if($("#" + Id + "_txtArea").val() =='' &&  $("#" + Id + "_txtExchange").val() =='' && $("#" + Id + "_txtPhone").val()=='')
            {
            getctrlValue='';
            }
            else
            {
            getctrlValue=$("#" + Id + "_txtArea").val() +'-'+  $("#" + Id + "_txtExchange").val() +'-'+ $("#" + Id + "_txtPhone").val();
            }
            break;
        }
        return getctrlValue;
    }
    
    // The following method is used to set validators
    function ValidatorsEnable(Id,Type,enable,Req) 
   {
        var ctrl;
        var arr=new Array();
        
        switch (Type) 
        {
            case 'D':
                if(Req)
                {
                  arr[0]=$("#" + Id  + "_txtYear" + "req")
                  
                }
                else
                {
                    arr[0]=$("#"  + Id + 'D' + "cust")//THIS IS FOR WHOLE CONTROL
                    
                }
                
               
                arr[1]=$("#" + Id  + "_txtYear" + "cust")
                
            break;
            case 'T':
                if(Req)
                {
                    arr[0]=$("#" + Id  + "_txtMinute" + "req")
                  
                }
                else
                {
                    arr[0]=$("#"  + Id  + 'T' + "cust")//THIS IS FOR WHOLE CONTROL
                  
                }
                
                arr[1]=$("#" + Id  + "_txtMinute" + "cust")
                
                
            break;
            case 'E': 
                if(Req)
                {
                    arr[0]=$("#" + Id  + "_txtEmail" + "req")
                    
                }
               
                
                arr[1]=$("#" + Id  + "_txtEmail" + "cust")
                
            break;
            case 'Z': 
             if(Req)
                {
                   arr[0]=$("#" + Id  + "_txtCode" + "req")
                }
                else
                {
                    arr[0]=$("#"  + Id + 'Z' + "cust")//THIS IS FOR WHOLE CONTROL
                }
                
                arr[1]=$("#" + Id  + "_txtZip" + "cust")
                
           
            break;
            case 'S': 
             if(Req)
                {
                    arr[0]=$("#" + Id  + "_txtSerial" + "req")
                }
                else
                {
                    arr[0]=$("#"  + Id + 'S' + "cust")//THIS IS FOR WHOLE CONTROL
                }
                
                arr[1]=$("#" + Id  + "_txtSerial" + "cust")
                
            break;
            case 'P': 
             if(Req)
                {
                arr[0]=$("#" + Id  + "_txtPhone" + "req")
                
                }
                else
                {
                arr[0]=$("#"  + Id + 'P' + "cust")//THIS IS FOR WHOLE CONTROL
                }
                
                arr[1]=$("#" + Id  + "_txtPhone" + "cust")
                
            break;
        }
        for(var i=0;i<arr.length;i++)
        {
            ValidatorEnable(arr[i][0],enable)
        }
        arr=null;
   }
    
    // The following method is used to set enable, disable and readonly for a control
   function TcmsMultiEnableReadOnly(Id,Type,attributename,attributevalue)
   {
       switch (Type) 
        {
            case 'D':  
           if(attributename=='readonly' && attributevalue==false)
                {
                    $("#" + Id + "_txtMonth").removeAttr("readonly");
                    $("#" + Id + "_txtDay").removeAttr("readonly");
                    $("#" + Id + "_txtYear").removeAttr("readonly");
                    $("#" + Id + "_txtMonth").removeAttr("onKeyDown");
                    $("#" + Id + "_txtDay").removeAttr("onKeyDown");
                    $("#" + Id + "_txtYear").removeAttr("onKeyDown");
                }  
            $("#" + Id + "_txtMonth").attr(attributename,attributevalue);
            $("#" + Id + "_txtDay").attr(attributename,attributevalue);
            $("#" + Id + "_txtYear").attr(attributename,attributevalue); 
             break;
            case 'E':    
            if(attributename=='readonly' && attributevalue==false)
                {
                    $("#" + Id + "_txtEmail").removeAttr("readonly");
                    $("#" + Id + "_txtEmail").removeAttr("onKeyDown");
                } 
            $("#" + Id + "_txtEmail").attr(attributename,attributevalue);
            break;
            case 'Z':  
                if(attributename=='readonly' && attributevalue==false)
                {
                  $("#" + Id + "_txtZip").removeAttr("readonly");
                  $("#" + Id + "_txtCode").removeAttr("readonly");
                  $("#" + Id + "_txtZip").removeAttr("onKeyDown");
                  $("#" + Id + "_txtCode").removeAttr("onKeyDown");
                }  
            $("#" + Id + "_txtZip").attr(attributename,attributevalue);
            $("#" + Id + "_txtCode").attr(attributename,attributevalue);  
            break;
            case 'S': 
            if(attributename=='readonly' && attributevalue==false)
                {
                  $("#" + Id + "_txtSerial").removeAttr("readonly");
                  $("#" + Id + "_txtssnArea").removeAttr("readonly");
                  $("#" + Id + "_txtGroup").removeAttr("readonly");
                  $("#" + Id + "_txtSerial").removeAttr("onKeyDown");
                  $("#" + Id + "_txtssnArea").removeAttr("onKeyDown");
                  $("#" + Id + "_txtGroup").removeAttr("onKeyDown");
                }  
            $("#" + Id + "_txtSerial").attr(attributename,attributevalue);
            $("#" + Id + "_txtssnArea").attr(attributename,attributevalue); 
            $("#" + Id + "_txtGroup").attr(attributename,attributevalue);  
            break;
            case 'P': 
            if(attributename=='readonly' && attributevalue==false)
                {
                  $("#" + Id + "_txtPhone").removeAttr("readonly");
                  $("#" + Id + "_txtArea").removeAttr("readonly");
                  $("#" + Id + "_txtExchange").removeAttr("readonly");
                  $("#" + Id + "_txtPhone").removeAttr("onKeyDown");
                  $("#" + Id + "_txtArea").removeAttr("onKeyDown");
                  $("#" + Id + "_txtExchange").removeAttr("onKeyDown");
                }  
            $("#" + Id + "_txtPhone").attr(attributename,attributevalue);
            $("#" + Id + "_txtArea").attr(attributename,attributevalue); 
            $("#" + Id + "_txtExchange").attr(attributename,attributevalue);  
            break;
            case 'T': 
            if(attributename=='readonly' && attributevalue==false)
                {
                  $("#" + Id + "_txtHour").removeAttr("readonly");
                  $("#" + Id + "_txtMinute").removeAttr("readonly");
                  $("#" + Id + "_txtHour").removeAttr("onKeyDown");
                  $("#" + Id + "_txtMinute").removeAttr("onKeyDown");
                }  
            $("#" + Id + "_txtHour").attr(attributename,attributevalue);
            $("#" + Id + "_txtMinute").attr(attributename,attributevalue); 
            $("#" + Id + "_cbTime").attr(attributename,attributevalue);  
            break;
        }    
   }
   
   // The following method is used to set default focus
   function TcmsMultiSetDefaultFocus(Id,Type,attributevalue)
   {
   if(attributevalue==true){
       switch (Type) 
        {
            case 'D':  
           $("#" + Id + "_txtMonth").focus();
             break;
            case 'E':    
           $("#" + Id + "_txtEmail").focus();
            break;
            case 'Z':  
                $("#" + Id + "_txtZip").focus();
            break;
            case 'S': 
            $("#" + Id + "_txtssnArea").focus();
            break;
            case 'P': 
           $("#" + Id + "_txtArea").focus();
            break;
            case 'T': 
            $("#" + Id + "_txtHour").focus();
            break;
        } 
        }   
   }