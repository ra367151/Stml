; (function ($) {
    var funcs = {
        cfgToastr: function (options) {
            options.positionClass = 'toast-bottom-right';
        },
        ajaxError: function (event, request, settings) {
            toastr.error('抱歉！服务端发生了未知的错误', "错误");
        }
    };

    funcs.cfgToastr(toastr.options);

    $(document).ajaxError(funcs.ajaxError);
})(jQuery);