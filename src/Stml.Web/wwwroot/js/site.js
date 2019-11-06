"use strict";

(function () {
    var CHECKBOX_CLASS_NAME = "icheckbox_minimal-red"
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
            return localStorage.permissions !== 'undefined' && JSON.parse(localStorage.permissions).indexOf(permission) > 0;
        }
    };

    var funcs = {
        setDefaultAjaxErrorEvent: function () {
            $(document).ajaxError(function (event, xhr, ajaxOptions, thrownError) {
                swal("抱歉！", "服务端发生了致命的错误", "error");
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

                // switch to first invalid tab when form invalid.
                $(this).find('.nav-tabs').closest('form').bind('invalid-form.validate', function (event, validator) {
                    var firstInvalidElement = validator.errorList[0].element;
                    var firstInvalidElementOnTabId = $(firstInvalidElement).closest('.tab-pane').attr('id');
                    $(this).find('a.nav-item[href="#' + firstInvalidElementOnTabId + '"]').tab('show');
                })
            });
        },
        loadPermissions: function () {
            $.get(PERMISSIONS_REQUEST_URL, function (resp) {
                localStorage.permissions = JSON.stringify(resp);
            });
        }
    };


    funcs.setDefaultAjaxErrorEvent();
    funcs.setDefaultModalShownEvent();
    funcs.loadPermissions();
    activator.input.activate();
    activator.select.activate();

    $.validator.setDefaults({
        ignore: []
    });
})();
