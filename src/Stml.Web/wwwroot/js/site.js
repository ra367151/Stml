"use strict";

(function () {
    var TOASTR_POSITION_CLASS = "toast-bottom-right"
        , CHECKBOX_CLASS_NAME = "icheckbox_minimal-red"
        , PERMISSIONS_REQUEST_URL = "/Stml/GetUserPermissions";

    var activator = {
        input: {
            activate: function ($parentSelector) {
                $parentSelector = $parentSelector || $('body');
                $parentSelector.find('.form-control').focus(function () {
                    $(this).parent().addClass('focused');
                });
                $parentSelector.find('.form-control').focusout(function () {
                    var $this = $(this);
                    if ($this.parents('.form-group').hasClass('form-float')) {
                        if ($this.val() == '') { $this.parents('.form-line').removeClass('focused'); }
                    }
                    else {
                        $this.parents('.form-line').removeClass('focused');
                    }
                });
                $parentSelector.on('click', '.form-float .form-line .form-label', function () {
                    $(this).parent().find('input').focus();
                });
                $parentSelector.find('.form-control').each(function () {
                    if ($(this).val() !== '') {
                        $(this).parents('.form-line').addClass('focused');
                    }
                });
            }
        },
        select: {
            activate: function () {
                if ($.fn.selectpicker) { $('select:not(.ms)').selectpicker(); }
            }
        }
    };

    // get user object to check permission.
    window.user = {
        check: function (permission) {
            return localStorage.permissions != null && JSON.parse(localStorage.permissions).indexOf(permission) > 0;
        }
    };

    var funcs = {
        setDefaultToastrOptions: function () {
            toastr.options.positionClass = TOASTR_POSITION_CLASS;
        },
        setDefaultAjaxErrorEvent: function () {
            $(document).ajaxError(function (event, xhr, ajaxOptions, thrownError) {
                toastr.error('抱歉！服务端发生了未知的错误', "错误");
            });
        },
        setDefaultModalShownEvent: function () {
            $('.modal[role="dialog"]').on('shown.bs.modal', function (e) {
                activator.input.activate($(this).find('form'));
                $(this).find('input:not([type=hidden]):first').focus();

                // init iCheck.
                $('input[type="checkbox"]').iCheck({ checkboxClass: CHECKBOX_CLASS_NAME });

                // add validation to dynamic form.
                $.validator.unobtrusive.parse($(this).find('form'));
            });
        },
        loadPermissions: function () {
            $.get(PERMISSIONS_REQUEST_URL, function (resp) {
                localStorage.permissions = JSON.stringify(resp);
            });
        }
    };


    funcs.setDefaultToastrOptions();
    funcs.setDefaultAjaxErrorEvent();
    funcs.setDefaultModalShownEvent();
    funcs.loadPermissions();
    activator.input.activate();
    activator.select.activate();
})();
