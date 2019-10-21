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
                    class: 'dropdown',
                    formatter: function (value, row, index) {
                        return '操作';
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