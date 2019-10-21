(function ($) {
    var $table = $('#tb-users');

    var funcs = {
        initTable: function () {
            $table.bootstrapTable("destroy").bootstrapTable({
                classes: 'table table-hover',
                method: 'get',
                url: '/User/List',
                dataType: "json",
                columns: [{
                    field: 'userName',
                    title: '用户名'
                }, {
                    field: 'email',
                    title: '邮箱'
                }, {
                    field: 'creationTime',
                    title: '创建日期'
                }, {
                    field: 'state',
                    title: '状态',
                    formatter: function (value, row, index) {
                        return '<div class="state icheckbox_minimal-red' + (row.isEnable ? ' checked' : '') + '"></div>';
                    }
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    formatter: function (value, row, index) {
                        var operateBtn = '<div class="dropdown">';
                        operateBtn += '<a href="#" class="btn text-primary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars font-lg"></i></a>';
                        operateBtn += '<div class="dropdown-menu" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);">';
                        operateBtn += '<a class="dropdown-item" href="#"><i class="fa fa-pencil"></i> 编辑</a>';
                        operateBtn += '<a class="dropdown-item" href="#"><i class="fa fa-trash"></i> 删除</a>';
                        operateBtn += '</div></div>';
                        return operateBtn;
                    }
                }],
                uniqueId: "Id",
                search: true,
                searchAlign: 'left',
                pagination: true,
                pageNumber: 1,
                pageSize: 10,
                pageList: [10, 25, 50, 100],
                sidePagination: "server",
                queryParams: function (params) {
                    return params;
                },
                responseHandler: function (res) {
                    return {
                        total: res.total,
                        rows: res.rows
                    }
                }
            });
        }
    };

    $(function () {
        funcs.initTable();
    });
})(jQuery);