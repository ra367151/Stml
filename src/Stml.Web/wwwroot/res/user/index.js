﻿(function () {
    "use strict";

    $(function () {
        var $table = $("#tb-users")
            , CREATE_MODAL_TARGET = "#create-modal"
            , EDIT_MODAL_TARGET = "#edit-modal"
            , $createModal = $(CREATE_MODAL_TARGET)
            , $editModal = $(EDIT_MODAL_TARGET)
            , TABLE_OPTIONS_CLASS = "table"
            , TABLE_OPTIONS_METHOD = "get"
            , TABLE_OPTIONS_URL = "/User/List"
            , TABLE_OPTIONS_UNIQUEID = "Id"
            , TABLE_OPTIONS_SEARCH = true
            , TABLE_OPTIONS_SEARCH_ALIGN = "left"
            , TABLE_OPTIONS_SEARCHONENTERKEY = true
            , TABLE_OPTIONS_PAGINATION = true
            , TABLE_OPTIONS_PAGENUMBER = 1
            , TABLE_OPTIONS_PAGESIZE = 5
            , TABLE_OPTIONS_PAGELIST = [10, 25, 50]
            , TABLE_OPTIONS_SIDE_PAGINATION = "server"
            , TABLE_OPTIONS_LOCALE = "zh-CN"
            , TABLE_OPTIONS_PAGINATIONLOOP = false
            , USER_CREATE_URL = "/User/CreatePartial"
            , USER_EDIT_URL = "/User/EditPartial"
            , USER_DELETE_URL = "/User/Delete"
            , $tableOptions = {
                classes: TABLE_OPTIONS_CLASS,
                method: TABLE_OPTIONS_METHOD,
                url: TABLE_OPTIONS_URL,
                columns: [{
                    field: 'userName',
                    title: '用户名'
                }, {
                    field: 'role',
                    title: '角色',
                    formatter: function (value, row, index) {
                        if (row.roles.length === 0)
                            return '-';
                        return row.roles.map(function (v, i, arr) {
                            return '<span class="badge badge-success">' + v.displayName + '</span>';
                        }).join(' ');
                    }
                }, {
                    field: 'email',
                    title: '邮箱'
                }, {
                    field: 'creation',
                    title: '创建日期',
                    formatter: function (value, row, index) {
                        return '<div>' + row.dateOfCreation + '</div><div class="small text-muted">' + row.timeOfCreation + '</div>';
                    }
                }, {
                    field: 'state',
                    title: '是否启用',
                    formatter: function (value, row, index) {
                        return '<div class="state icheckbox_minimal-red' + (row.isActive ? ' checked' : '') + '"></div>';
                    }
                }, {
                    field: 'operate',
                    title: '操作',
                    width: 100 + 'px',
                    formatter: function (value, row, index) {
                        var operateBtn = '<div class="dropdown">';
                        if (window.user.check('UserEdit') || window.user.check('UserDelete')) {
                            operateBtn += '<a href="#" class="btn text-primary" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars font-lg"></i></a>';
                            operateBtn += '<div class="dropdown-menu pull-right">';
                            if (window.user.check('UserEdit')) {
                                operateBtn += '<a class="dropdown-item edit-user" data-user-id="' + row.id + '" data-toggle="modal"><i class="fa fa-pencil"></i> 编辑</a>';
                            }
                            if (window.user.check('UserDelete')) {
                                operateBtn += '<a class="dropdown-item delete-user" data-user-id="' + row.id + '"><i class="fa fa-trash"></i> 删除</a>';
                            }
                            operateBtn += '</div></div>';
                        }
                        return operateBtn;
                    }
                }],
                uniqueId: TABLE_OPTIONS_UNIQUEID,
                locale: TABLE_OPTIONS_LOCALE,
                search: TABLE_OPTIONS_SEARCH,
                searchAlign: TABLE_OPTIONS_SEARCH_ALIGN,
                searchOnEnterKey: TABLE_OPTIONS_SEARCHONENTERKEY,
                pagination: TABLE_OPTIONS_PAGINATION,
                paginationLoop: TABLE_OPTIONS_PAGINATIONLOOP,
                pageNumber: TABLE_OPTIONS_PAGENUMBER,
                pageSize: TABLE_OPTIONS_PAGESIZE,
                pageList: TABLE_OPTIONS_PAGELIST,
                sidePagination: TABLE_OPTIONS_SIDE_PAGINATION,
                queryParams: function (params) {
                    return params;
                },
                responseHandler: function (res) {
                    return {
                        total: res.total,
                        rows: res.rows
                    }
                }
            };

        var funcs = {
            initTable: function () {
                $table.bootstrapTable("destroy").bootstrapTable($tableOptions);
            },
            create: function () {
                $createModal.find('.modal-content').load(USER_CREATE_URL, function () {
                    $createModal.modal('show');
                });
            },
            edit: function (id) {
                $editModal.find('.modal-content').load(USER_EDIT_URL + '?id=' + id, function () {
                    $editModal.modal('show');
                });
            },
            delete: function (id) {
                swal({
                    title: "确定执行该操作?",
                    icon: "warning",
                    buttons: ["取消", "确定"],
                    dangerMode: true,
                }).then((willDelete) => {
                    if (willDelete) {
                        $.post(USER_DELETE_URL, { id: id }, function (resp) {
                            swal("成功!", "删除用户成功", "success");
                            $table.bootstrapTable('refresh', { pageNumber: 1 });
                        });
                    }
                });

            }
        };

        $('.create-user').click(function (e) {
            funcs.create();
        });

        $(document).on('click', '.edit-user', function (e) {
            var id = $(this).data('user-id');
            funcs.edit(id);
        });

        $(document).on('click', '.delete-user', function (e) {
            var id = $(this).data('user-id');
            funcs.delete(id);
        });

        funcs.initTable();
    });
})();

function createUserSuccess(result) {
    if (result.isSuccess) {
        swal("成功!", "新建用户成功", "success");
        $('#create-modal').modal('hide');
        $("#tb-users").bootstrapTable('refresh', { pageNumber: 1 });
    }
    else {
        swal("失败！", result.errors[0], "error");
    }
}

function editUserSuccess(result) {
    if (result.isSuccess) {
        swal("成功!", "编辑用户成功", "success");
        $('#edit-modal').modal('hide');
        $("#tb-users").bootstrapTable('refresh', { pageNumber: 1 });
    }
    else {
        swal("失败！", result.errors[0], "error");
    }
}