"use strict";

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

    $('.form-group .form-line').focusin(function () {
        $(this).closest('form').find('.focused').removeClass('focused');
        $(this).addClass("focused");
    }).focusout(function () {
        $(this).removeClass("focused");
    });


    // clear form
    $.fn.clearForm = function (options) {
        var settings = $.extend({
            formId: this.closest('form')
        }, options);

        var $form = $(settings.formId);

        $form.validate().resetForm();

        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        return $form;
    };
})(jQuery);