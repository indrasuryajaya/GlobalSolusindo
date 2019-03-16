(function () {
    'use strict';

    /**
     * @ngdoc function
     * @name app.service:dashboardService
     * @description
     * # dashboardService
     * Service of the app
     */

    angular
        .module('global-solusindo')
        .factory('DatatableService', DtService);

    DtService.$inject = ['DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'HttpService', '$cookies', '$state'];

    function DtService(DTOptionsBuilder, DTColumnBuilder, $compile, http, $cookies, $state) {
        var self = this;

        self.init = function dt(tableIdOrClass, apiUrl, param) {
            console.log('this');
            var defaultDom = "lftip";

            var dt = $(tableIdOrClass).DataTable({
                destroy: true,
                processing: true,
                serverSide: true,
                full_numbers: false,
                autoWidth: true,
                pageLength: 10,
                scrolly: true,
                scrollX: true,
                scrollCollapse: true,
                stateSave: param.stateSave === undefined ? true : param.stateSave,
                stateLoadParams: function (settings, data) {
                    data.search.search = "";
                },
                select: true,
                fixedColumns: {
                    leftColumns: param.fixedColumns === undefined ? 0 : param.fixedColumns[0],
                    rightColumns: param.fixedColumns === undefined ? 0 : param.fixedColumns[1]
                },
                rowGroup: {
                    enable: param.rowGroup === undefined ? false : true,
                    dataSrc: param.rowGroup === undefined ? null : param.rowGroup
                },

                ajax: function (data, callback, setting) {
                    var api = new $.fn.dataTable.Api(setting);
                    var pageIndex = Math.floor((data.start / data.length)) + 1;

                    var defaultRequestData = {
                        "pageIndex": pageIndex,
                        "pageSize": data.length,
                        "keyword": data.search["value"],
                        "sortName": data.columns[data.order[0].column].data,
                        "sortDir": data.order[0].dir
                    };

                    var extendRequestData = param.extendRequestData;

                    if (extendRequestData) {
                        extendRequestData.pageIndex = defaultRequestData.pageIndex;
                        extendRequestData.pageSize = defaultRequestData.pageSize;
                        extendRequestData.keyword = defaultRequestData.keyword;
                        extendRequestData.sortName = defaultRequestData.sortName;
                        extendRequestData.sortDir = defaultRequestData.sortDir;
                    }

                    var requestData = (typeof (extendRequestData) != 'undefined') ? extendRequestData : defaultRequestData;
                    //console.log(requestData);
                    if (!requestData.Keyword) {
                        $('.backdrop-login').fadeIn();
                    }
                    // ajaxService.post(apiUrl, requestData, function (json) {
                    //     //console.log(json);
                    //     $('.backdrop-login').fadeOut();
                    //     if (param.onAjaxSuccess) {
                    //         param.onAjaxSuccess(json);
                    //     }
                    //     callback({
                    //         recordsTotal: json.data.totalRecords,
                    //         recordsFiltered: json.data.totalRecordsFiltered,
                    //         data: json.data.Data
                    //     }); 
                    // }, function (json) {
                    //     $('.backdrop-login').fadeOut();
                    //     if (param.onAjaxFailed)
                    //         param.onAjaxFailed(json);
                    //     kairos.alert.error(json.message);
                    // });

                    http.get(apiUrl, {
                        pageIndex: requestData.pageIndex,
                        pageSize: requestData.pageSize
                    }).then(function (res) {
                        callback({
                            recordsTotal: res.data.count.totalRecords,
                            recordsFiltered: res.data.count.totalFiltered,
                            data: res.data.records
                        });

                        console.log(res.data.count.totalRecords);
                    });

                },
                columns: param.columns,
                dom: (typeof (param.dom) == 'undefined') ? defaultDom : param.dom,
                // language: {
                //     search: "",
                //     lengthMenu: "_MENU_",
                //     searchPlaceholder: "Search ..."
                // },
                searchDelay: 600,
                drawCallback: function (setting) {
                    var elem = $('[rel="tooltip"]');
                    elem.tooltip({
                        trigger: 'hover',
                        container: 'main'
                    });

                },
                order: typeof param.order === 'undefined' ? [
                    [0, "desc"]
                ] : param.order
            });
            if (param.rowSequence) {
                dt.on('draw.dt', function () {
                    var PageInfo = $(tableIdOrClass).DataTable().page.info();
                    dt.column(0, {
                        page: 'current'
                    }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1 + PageInfo.start;
                    });
                });
            }

            $(tableIdOrClass + ' tbody').on("dblclick", "tr", function () {
                var data = dt.row(this).data();
                //console.log(data);
                var id = data["Id"];
                if (param.route) {
                    (param.customRoute) ? param.route(data): param.route(id);
                }
            });

            $('.dataTables_filter input[type=search]').val('').change();

            return dt;
        }

        return self;
    }

})();