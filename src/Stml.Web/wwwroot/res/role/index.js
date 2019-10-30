(function () {
    "use strict";

    $(function () {
        var $table = $("#tb-roles")
            , CREATE_MODAL_TARGET = "#create-modal"
            , EDIT_MODAL_TARGET = "#edit-modal"
            , $createModal = $(CREATE_MODAL_TARGET)
            , $editModal = $(EDIT_MODAL_TARGET)
            , TABLE_OPTIONS_CLASS = "table"
            , TABLE_OPTIONS_METHOD = "get"
            , TABLE_OPTIONS_URL = "/Role/List"
            , TABLE_OPTIONS_UNIQUEID = "Id"
            , TABLE_OPTIONS_SEARCH = true
            , TABLE_OPTIONS_SEARCH_ALIGN = "left"
            , TABLE_OPTIONS_PAGINATION = true
            , TABLE_OPTIONS_PAGENUMBER = 1
            , TABLE_OPTIONS_PAGESIZE = 5
            , TABLE_OPTIONS_PAGELIST = [10, 25, 50]
            , TABLE_OPTIONS_SIDE_PAGINATION = "server"
            , ROLE_CREATE_URL = "/Role/CreatePartial"
            , ROLE_EDIT_URL = "/Role/EditPartial"
            , ROLE_DELETE_URL = "/Role/Delete"
            , $tableOptions = {
                classes: TABLE_OPTIONS_CLASS,
                method: TABLE_OPTIONS_METHOD,
                url: TABLE_OPTIONS_URL,
                columns: [{
                    field: 'name',
                    title: '角色'
                }, {
                    field: 'displayName',
                    title: '显示名称'
                }, {
                    field: 'operate',
                    title: '操作',
                    width: 100 + 'px',
                    formatter: function (value, row, index) {
                        var operateBtn = '<div class="dropdown">';
                        if (window.user.check('RoleEdit') || window.user.check('RoleDelete')) {
                            operateBtn += '<a href="#" class="btn text-primary" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars font-lg"></i></a>';
                            operateBtn += '<div class="dropdown-menu pull-right">';
                            if (window.user.check('RoleEdit')) {
                                operateBtn += '<a class="dropdown-item edit-role" data-role-id="' + row.id + '" data-toggle="modal" data-target="' + EDIT_MODAL_TARGET + '"><i class="fa fa-pencil"></i> 编辑</a>';
                            }
                            if (window.user.check('RoleDelete')) {
                                operateBtn += '<a class="dropdown-item delete-role" data-role-id="' + row.id + '"><i class="fa fa-trash"></i> 删除</a>';
                            }
                            operateBtn += '</div></div>';
                        }
                        return operateBtn;
                    }
                }],
                uniqueId: TABLE_OPTIONS_UNIQUEID,
                search: TABLE_OPTIONS_SEARCH,
                searchAlign: TABLE_OPTIONS_SEARCH_ALIGN,
                pagination: TABLE_OPTIONS_PAGINATION,
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
                $createModal.find('.modal-content').load(ROLE_CREATE_URL, function () {
                    $createModal.modal('show');
                });
            },
            edit: function (id) {
                $.get(ROLE_EDIT_URL, { id: id }, function (resp) {
                    $editModal.find('.modal-content').html(resp);
                });
            },
            delete: function (id) {
                $.post(ROLE_DELETE_URL, { id: id }, function (resp) {
                    toastr.success("删除角色成功!");
                    $table.bootstrapTable('refresh', { pageNumber: 1 });
                });
            }
        };

        $('.create-role').click(function (e) {
            funcs.create();
        });

        $(document).on('click', '.edit-role', function (e) {
            var id = $(this).data('role-id');
            funcs.edit(id);
        });

        $(document).on('click', '.delete-role', function (e) {
            var id = $(this).data('role-id');
            funcs.delete(id);
        });

        funcs.initTable();
    });
})();

function createRoleSuccess(result) {
    if (result.isSuccess) {
        toastr.success("新建角色成功！");
        $('#create-modal').modal('hide');
        $("#tb-roles").bootstrapTable('refresh', { pageNumber: 1 });
    }
    else {
        toastr.error("新建角色失败： " + result.errors[0]);
    }
}

function editRoleSuccess(result) {
    if (result.isSuccess) {
        toastr.success("编辑角色成功！");
        $('#edit-modal').modal('hide');
        $("#tb-roles").bootstrapTable('refresh', { pageNumber: 1 });
    }
    else {
        toastr.error("编辑角色失败： " + result.errors[0]);
    }
}