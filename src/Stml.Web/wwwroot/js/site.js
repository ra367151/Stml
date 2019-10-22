"use strict";

(function () {
    var TOASTR_POSITION_CLASS = "toast-bottom-right";

    // set global options of toastr.
    toastr.options.positionClass = TOASTR_POSITION_CLASS;

    // set global ajax error event.
    $(document).ajaxError(function (event, xhr, ajaxOptions, thrownError) {
        toastr.error('抱歉！服务端发生了未知的错误', "错误");
    });

    // form focus event
    $('.form-group .form-line').focusin(function (e) {
        var $this = $(this);
        $.each($this.closest('form').find('.focused'), function (i, item) {
            if ($(item).val().length > 0) {
                $(item).removeClass('focused');
            }
        });
        //$this.closest('form').find('.focused').removeClass('focused');
        $this.addClass('focused');
    }).focusout(function (e) {
        var $this = $(this);
        if ($(this).find('input').val().length <= 0) {
            $this.removeClass('focused');
        }
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
