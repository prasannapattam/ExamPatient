
;(function() 
{
    $.fn.tbBorder = function(config) 
        {
            return this.each(
                function() 
                {
		            if ("INPUT" != this.tagName.toUpperCase())
		                return;	    
		            
                     //getting the input text type
                    var $input = $(this);
                    var inputtype = $input.attr('type');
                    if (inputtype === undefined)
                        return;
                    var inputTypedValue = inputtype.toLowerCase();
                    if(inputTypedValue == 'text' || inputTypedValue == 'password')
                    {
                        $input.blur(
                            function()
                            {
                                $input.css({ borderColor:"" });
                            });
                            
                        $input.focus(
                            function()
                            {
                                $input.css({ borderColor:"red" });
                            });
                    }

                    else if(inputTypedValue == 'submit' || inputTypedValue == 'button')
                    {
                        $input.blur(
                            function()
                            {
                                $input.css({ borderStyle:"" });
                            });
                            
                        $input.focus(
                            function()
                            {
                                $input.css({ borderStyle:"ridge" });
                            });
                    }
                    else if(inputTypedValue=='checkbox' || inputTypedValue=="radio")
                    {

                    $input.click(function()
                    {
                            $(":checkbox,:radio").each(function () {
                                if($(this).attr("checked"))
                                {
                                    $(this).css({ backgroundColor: "yellow" });
                                }
                                else
                                {
                                    $(this).css({ backgroundColor: "" });
                                }
                            });
                    });  
                    }
                
                  });  
        };
 })(jQuery); 
   