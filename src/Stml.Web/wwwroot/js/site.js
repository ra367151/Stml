"use strict";

(function () {
    var TOASTR_POSITION_CLASS = "toast-bottom-right";

    // set global options of toastr.
    toastr.options.positionClass = TOASTR_POSITION_CLASS;

    // set global ajax error event.
    $(document).ajaxError(function (event, xhr, ajaxOptions, thrownError) {
        toastr.error('抱歉！服务端发生了未知的错误', "错误");
    });

    // form focus
    $('.form-group .form-line input.form-control').focusin(function (e) {
        var $this = $(this);
        $this.parent('.form-line').addClass('focused');
    }).focusout(function (e) {
        var $this = $(this);
        if ($this.val().length <= 0) {
            $this.parent('.focused').removeClass('focused');
        }
    });

    // modal show and hide
    $('.modal[role="dialog"]').on('shown.bs.modal', function (e) {
        $(this).find('input.form-control').first().focus();
    }).on('hidden.bs.modal', function (e) {
        $(this).find('form').clearForm();
        $(this).find('form').find('.focused').removeClass('focused');
        $(this).find('form').find('input').iCheck('update');
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

        $form[0].reset();

        return $form;
    };
})();
