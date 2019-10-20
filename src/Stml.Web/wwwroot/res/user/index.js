(function ($) {
    var $table = $('#tb-users');

    var funcs = {
        initTable: function () {
            $table.bootstrapTable("destroy").bootstrapTable({
                method: 'get',
                url: '/User/List',
                columns: [{
                    field: 'UserName',
                    title: '用户名'
                }, {
                    field: 'Email',
                    title: '邮箱'
                }, {
                    field: 'CreationTime',
                    title: '创建日期'
                }, {
                    field: 'State',
                    title: '状态'
                }],
                uniqueId: "Id",
                pagination: true,
                sidePagination: "server",
                queryParamsType: 'limit',
                //queryParams: funcs.queryParams()
            });
        },
        queryParams: function () {
            var params = {};

            console.log(params);
            return params;
        }
    };

    $(function () {
        funcs.initTable();
    });

    $('#user-tb').bootstrapTable();
})(jQuery);