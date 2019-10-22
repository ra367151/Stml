; (function ($) {
    var funcs = {
        cfgToastr: function (options) {
            options.positionClass = 'toast-bottom-right';
        }
    };

    funcs.cfgToastr(toastr.options);
})(jQuery);