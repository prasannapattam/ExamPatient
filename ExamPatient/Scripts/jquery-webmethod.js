(function ($) {
    $.webMethod = function (options) {
        var settings = $.extend({
            'methodName': '',
            'async': false,
            'cache': false,
            timeout: 30000,
            debug: false,
            'parameters': {},
            success: function (response) { },
            error: function (response) { }
        }, options); var parameterjson = "{}";
        var result = null;
        if (settings.parameters != null) { parameterjson = $.toJSON(settings.parameters); }
        $.ajax({
            type: "POST",
            url: location.pathname + "/" + settings.methodName,
            data: parameterjson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: settings.async,
            cache: settings.cache,
            timeout: settings.timeout,
            success: function (value) {
                result = value.d;
                settings.success(result);
            },
            error: function (response) {
                settings.error(response);
                if (settings.debug) { alert("Error Calling Method \"" + settings.methodName + "\"\n\n+" + response.responseText); }
            }
        });
        return result;
    };
})(jQuery);