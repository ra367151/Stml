(function ($) {
    var $table = $('#tb-users');

    var funcs = {
        initTable: function () {
            $table.bootstrapTable("destroy").bootstrapTable({
                method: 'get',
                url: '/User/List',
                dataType: "json",
                columns: [{
                    field: 'userName',
                    title: '用户名',
                    align: 'center'
                }, {
                    field: 'email',
                    title: '邮箱',
                    align: 'center'
                }, {
                    field: 'creationTime',
                    title: '创建日期',
                    align: 'center'
                }, {
                    field: 'state',
                    title: '状态',
                    align: 'center'
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