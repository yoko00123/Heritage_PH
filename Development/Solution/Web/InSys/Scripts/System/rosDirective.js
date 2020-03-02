'use-strict';
angular.module('rosWidget', [])
    .directive('rosWidget', [function () {
        return {
            templateUrl: '/Web/Template/Widget-Tab.tmpl.html',
            scope: { webWidget: '=' },
            restrict: 'E',
            controller: ['$scope', 'DataService', '$controller', '$q', function ($s, $ds, $c, $q) {
                $c('BaseController', { $scope: $s });
                $s.widgets = [];
                $s.$watch('webWidget', function () {
                    $s.pendingWidget = [];
                    if ($s.webWidget != undefined) {
                        angular.forEach($s.webWidget, function (obj, idx) {
                            $s.pendingWidget.push($ds.GetWebWidget(obj.ID));
                        });
                        $q.all($s.pendingWidget).then(function (w) {
  
                            $s.widgets = w;
                            $s.dataSourceSet = [];
                            $s.widgetCount = {};
                            angular.forEach($s.widgets, function (obj) {
                                var tableWidgets = Enumerable.From(obj.tWebWidgets_Detail).Where(function (x) { return x.ID_WebWidgetsType == 1 }).ToArray();
                                angular.forEach(tableWidgets, function (detail) {
                                    var dsSet = { ID: detail.ID, DS: detail.EffectiveDataSource };
                                    $s.dataSourceSet.push(dsSet);
                                });
                            });

                            $s.Request("FetchWidgetCount", { ds: $s.dataSourceSet }).then(function (obj) {
                                if (obj.error == undefined) {
                                    $s.widgetCount = obj.data;
                                    angular.forEach($s.widgets, function (obj) {
                                        var tableWidgets = Enumerable.From(obj.tWebWidgets_Detail).Where(function (x) { return x.ID_WebWidgetsType == 1 }).ToArray();
                                        angular.forEach(tableWidgets, function (detail) {
                                            detail.Cnt = $s.widgetCount[detail.ID];
                                        });
                                    });
                                } else {
                                    $s.Toast(obj.error, 'FetchWidgetCount', 'warning');
                                }
                            });
                            if ($s.widgets.length > 0) {
                                $s.activeTab = $s.widgets[0].tWebWidgets.ID;
                            }
                        });
                    }
                });
                $s.setActiveTab = function (id) {
                    $s.activeTab = id;
                }
            }]
        }
    }])
    .directive('rosWidgetPanel', [function () {
        return {
            templateUrl: '/Web/Template/Widget-Panel.tmpl.html',
            scope: { widget: '=' },
            restrict: 'E',
            controller: ['$scope', '$controller', function ($s, $c) {
                $c('BaseController', { $scope: $s });
            }]
        }
    }])
    .directive('expandPanel', [function () {
        return {
            restrict: 'A',
            link: function ($s, $e, $a) {
                $e.on('click', function () {
                    $(".dash-board").removeClass("toggled-dashboard");
                    var tbl = $("#div_body_" + $a.pid);
                    if ($e.parent().parent().parent().hasClass('widget-expand')) {
                        $e.parent().parent().parent().removeClass('widget-expand');
                        $e.parent().parent().removeClass('max-height');
                        $e.addClass("fa-expand");
                        $e.removeClass("fa-compress");
                        tbl.css("height", "");
                        tbl.css("maxHeight", 150);
                    } else {
                        $e.parent().parent().parent().css("maxWidth", "");
                        $e.parent().parent().parent().addClass('widget-expand');
                        $e.parent().parent().addClass('max-height');
                        $e.removeClass("fa-expand");
                        $e.addClass("fa-compress");
                        var pw = tbl.parent().parent()[0].clientHeight;
                        tbl.css("height", pw - 93);
                        tbl.css("maxHeight", "");
                    }
                    var arrWidth = [];
                    var p = $("#tbl_head_" + $a.pid);
                    var b = $("#tbl_body_" + $a.pid);
                    setTimeout(function () {
                        var pw = p.parent()[0].clientWidth;
                        var tw = 0;
                        b.children().children().each(function (idx) {
                            var childs = $(this)[0].children;
                            for (var x = 0; x < childs.length; x++) {
                                var w = (childs[x].textContent.length * 10) + 20;
                                if ($a.rc == 0) {
                                    w = 20;
                                }
                                if (idx == 0) {
                                    arrWidth.push(w);
                                } else {
                                    if (arrWidth[x] < w) {
                                        arrWidth[x] = w;
                                    }
                                }
                            }
                        });
                        b.children().children().each(function (idx) {
                            var childs = $(this)[0].children;
                            for (var x = 0; x < childs.length; x++) {
                                childs[x].style.width = arrWidth[x] + "px";
                            }
                        });
                        for (var x = 0; x < arrWidth.length; x++) {
                            tw += arrWidth[x]
                        }
                        p.children().children().children().each(function (idx) {
                            $(this).css("width", arrWidth[idx]);
                        });

                        if (pw > tw) {
                            p[0].style.width = "100%";
                            b[0].style.width = "100%";
                        } else {
                            p.css("width", tw);
                            b.css("width", tw);
                        }
                        $("#div_body_" + $a.pid).on("scroll", function () {
                            $("#div_head_" + $a.pid).scrollLeft($(this)[0].scrollLeft)
                        });
                    }, 1200);
                });
            }
        }
    }])
    .directive('rosWidgetTable', ['$controller', function ($c) {
        return {
            restrict: 'A',
            scope: { panelData: '=' },
            link: function ($s, $e, $a) {
                $c('BaseController', { $scope: $s });
                $s.currPage = 1;
                $s.countPerPage = 10;
                var cols = Enumerable.From($s.panelData.Columns).Select(function (x) { return x.Name }).ToArray();
                $s.getData = function () {
                    $s.Request("FetchTableData", { page: $s.currPage, count: $s.countPerPage, ds: $s.panelData.EffectiveDataSource, filter: $s.panelData.Filter, cols: cols.join(",") }).then(function (data) {
                        if (data.error != undefined) {
                            $s.Toast(data.error, 'FetchTableData', 'warning');
                        } else {
                            var pageArray = [];
                            for (var x = 0; x < data.pages; x++) {
                                var o = { ID: (x + 1), Label: (x + 1) };
                                pageArray.push(o);
                            }
                            $s.panelData.pages = pageArray;
                            $s.panelData.data = data.data;
                            $s.panelData.changePage = function () {
                                $s.currPage = $s.panelData.selectedPage;
                                $s.getData();
                            }
                            $s.panelData.changeDisplayPerPage = function () {
                                $s.currPage = 1
                                $s.panelData.selectedPage = 1;
                                $s.countPerPage = $s.panelData.countPerPage;
                                $s.getData();
                            }
                            $s.$apply();
                        }
                    });
                }
                $s.getData();
            }
        }
    }])
    .directive('repeatFinish', [function () {
        return {
            restrict: "A",
            link: function ($s, $e, $a) {
                if ($s.$last) {
                    var arrWidth = [];
                    var totalWidth = 0;
                    var p = $("#tbl_head_" + $a.pid);
                    setTimeout(function () {
                        var pw = p.parent()[0].clientWidth;
                        var tw = 0;
                        $e.parent().children().each(function (idx) {
                            var childs = $(this)[0].children;
                            for (var x = 0; x < childs.length; x++) {
                                var w = (childs[x].textContent.length * 10) + 20;
                                if (idx == 0) {
                                    arrWidth.push(w);
                                } else {
                                    if (arrWidth[x] < w) {
                                        arrWidth[x] = w;
                                    }
                                }
                            }
                        });
                        $e.parent().children().each(function (idx) {
                            var childs = $(this)[0].children;
                            for (var x = 0; x < childs.length; x++) {
                                childs[x].style.width = arrWidth[x] + "px";
                            }
                        });
                        for (var x = 0; x < arrWidth.length; x++) {
                            tw += arrWidth[x]
                        }
                        p.children().children().children().each(function (idx) {
                            $(this).css("width", arrWidth[idx]);
                        })
                        if (pw > tw) {
                            p[0].style.width = "100%";
                            $e.parent().parent()[0].style.width = "100%";
                        } else {
                            p.css("width", tw);
                            $e.parent().parent().css("width", tw);
                        }
                        $("#div_body_" + $a.pid).on("scroll", function () {
                            $("#div_head_" + $a.pid).scrollLeft($(this)[0].scrollLeft)
                        });
                    }, 1000);
                }
            }
        }
    }])
    .directive('rosWidgetChart', [function () {
        return {
            scope: { panelData: '=' },
            restrict: 'A',
            controller: ['$scope', '$controller', function ($s, $c) {
                $c('BaseController', { $scope: $s });
                $s.randomColors = function() {
                    var letters = '0123456789ABCDEF'.split('');
                    var color = '#';
                    for (var i = 0; i < 6; i++) {
                        color += letters[Math.floor(Math.random() * 16)];
                    }
                    return color;
                }
                $s.Request('FetchWidgetChartData', { data: $s.panelData }).then(function (obj) {
                    $s.panelData.chartColors = [];
                    $s.panelData.chartLabels = [];
                    $s.panelData.chartData = [];
                    $s.panelData.chartPercentage = [];
                    for (var x = 0; x < obj.data.length; x++) {
                        var color = $s.randomColors();
                        while ($s.panelData.chartColors.indexOf(color) > -1) {
                            color = $s.randomColors();
                        }
                        $s.panelData.chartColors.push(color);
                        if ($s.panelData.chartLabels.indexOf(obj.data[x].Name) > -1) {
                            $s.panelData.chartLabels.push(obj.data[x].Name + "_duplicateName_" + x);
                        } else {
                            $s.panelData.chartLabels.push(obj.data[x].Name);
                        }
                        
                        $s.panelData.chartData.push(obj.data[x].Value);
                        $s.panelData.chartPercentage.push(obj.data[x].Percentage)
                    }
                });

                //$s.chartWidth = $(".w2").width() * .9;
                //$s.chartHeight = $(".w2").height() * .9;

                //console.log("width: " + $s.chartWidth + " " + "height: " + $s.chartHeight)
            }]
        }
    }])
    .directive('expandPanelChart', [function () {
        return {
            restrict: 'A',
            scope: { panelData: '=' },
            link: function ($s, $e, $a) {
                $e.on('click', function () {
                    $(".dash-board").removeClass("toggled-dashboard");
                    if ($e.parent().parent().parent().hasClass('widget-expand')) {
                        $e.parent().parent().parent().removeClass('widget-expand');
                        $e.parent().parent().removeClass('max-height');
                        $e.addClass("fa-expand");
                        $e.removeClass("fa-compress");
                        $s.panelData.IsExpanded = false;
                    } else {
                        $e.parent().parent().parent().css("maxWidth", "");
                        $e.parent().parent().parent().addClass('widget-expand');
                        $e.parent().parent().addClass('max-height');
                        $e.removeClass("fa-expand");
                        $e.addClass("fa-compress");
                        $s.panelData.IsExpanded = true;
                    }
                    $s.$apply();
                });
            }
        }
    }])
    .directive('ngTableGrouping', [function () {
        return {
            restrict: 'E',
            templateUrl: '/Web/Template/ngTableGrouping.tmpl.html',
            scope: {
                grpData: "=",
                grpVar: "=",
                formatColumn: "=",
                loadDefault: "=",
                openInfo: "=",
                allowDelete: "=",
                chk: "=",
                parentGroup: "=",
                arrangeIcon: "=",
                arrangeRecord: "="
            },
            link: function ($s, $e, $a) {
                $s.grid = $s.grpData;
                $s.a = $s.grpVar;
                $s.FormatColumn = $s.formatColumn;
                $s.IsLoadDefault = $s.loadDefault;
                $s.OpenInfo = $s.openInfo;
                $s.IsAllowDelete = $s.allowDelete;
                $s.checkChildren = $s.chk;
                $s.sortIcon = $s.arrangeIcon;
                $s.sortRecord = $s.arrangeRecord;
                $s.NotGroup = function (item) {
                    return item.GroupSeqNo === 0;
                }
                $s.guid = function () {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                          .toString(16)
                          .substring(1);
                    }
                    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                      s4() + '-' + s4() + s4() + s4();
                }
                $s.GroupChildren = function (grp, source, idx) {
                    try {
                        if (grp.length > idx) {
                            var n = (grp[idx].Name.startsWith('ID_') ? grp[idx].Name.substr(3) : grp[idx].Name);
                            return Enumerable.From(source).GroupBy('$.' + n, null, function (key, g) {
                                return { id: $s.guid(), GroupName: key, Children: (g.source.length > 0 ? (grp.length == (idx + 1) ? g.source : $s.GroupChildren(grp, g.source, (idx + 1))) : g.source) }
                            }).ToArray();
                        }
                    } catch (ex) {
                        console.error(ex);
                    }
                }
                $s.GroupItems = function () {
                    try {
                        $s.grpItems = [];
                        var grp = Enumerable.From($s.grid.Columns).Where(function (x) { return x.GroupSeqNo > 0 && x.ID > 0 }).ToArray();
                        grp.sort(function (a, b) {
                            return a.GroupSeqNo - b.GroupSeqNo;
                        });
                        if (grp.length > 0 && $s.grid.Rows.length > 0) {
                            var n = (grp[0].Name.startsWith('ID_') ? grp[0].Name.substr(3) : grp[0].Name);
                            $s.grpItems.push(Enumerable.From($s.grid.Rows).GroupBy('$.' + n, null, function (key, g) {
                                return { id: $s.guid(), GroupName: key, Children: (grp.length > 1 ? $s.GroupChildren(grp, g.source, 1) : g.source) }
                            }).ToArray());
                        }

                        //});
                    } catch (ex) { 
                        console.error(ex)
                    }
                }
                $s.toggleGroup = function (indx) {
                    if ($('.groupIndx_' + indx).hasClass('hide')) {
                        $(".groupIndx_" + indx).removeClass("hide");
                    } else {
                        $(".groupIndx_" + indx).addClass("hide");
                    }
                }

                $s.changeToggle = function (id) {
                    return ($('.groupIndx_' + id).hasClass('hide') ? 'fa fa-plus-square' : 'fa fa-minus-square');
                }
            }
        }
    }])
    //.directive('largeWidth', [function () {
    //    return {
    //        restrict: "A",
    //        link: function ($s, $e, $a) {
    //            var press = false;
    //            var startX;
    //            var startTbl;
    //            var w;
    //            var body;
    //            var colMinW;
    //            $e.on("mousedown", function (e) {
    //                press = true;
    //                startX = e.pageX;
    //                startTbl = parseInt($e.parent().parent().parent().css("width").replace("px"), "");
    //                w = parseInt($e.css("maxWidth").replace("px", ""));
    //            });
    //            $e.on("mouseover", function (e) {
    //                $e.css("cursor", "col-resize");
    //            })
    //            $(document).on("mousemove", function (e) {
    //                var tw = w + (e.pageX - startX)
    //                if (press) {
    //                    $e.addClass("colresizing");
    //                    if (tw <= parseInt($e.css("minWidth").replace("px", ""))) {
    //                        $e.css("maxWidth", parseInt($e.css("minWidth").replace("px", "")));
    //                        $e.css("width", parseInt($e.css("minWidth").replace("px", "")));
    //                    } else {
    //                        $e.css("maxWidth", tw);
    //                        $e.css("width", tw);
    //                        var mw = (e.pageX - startX);
    //                        var tableWidth = startTbl + mw;
    //                        $e.parent().parent().parent().css("width", tableWidth)
    //                        $e.parent().parent().parent().css("maxWidth", tableWidth)
    //                    }
    //                    body = $e.parent().parent().parent().children()[1].children;
    //                    var tdCount = (body[0] != undefined ? body[0].children.length : 0);
    //                    for (var x = 0; x < body.length; x++) {
    //                        if (body[x].children.length == tdCount) {
    //                            var cc = $(body[x].children[$e.index()]).find('input, select, textarea');
    //                            if (cc.length == 0) {
    //                                body[x].children[$e.index()].style.maxWidth = tw + "px";
    //                                body[x].children[$e.index()].style.width = tw + "px";
    //                                if (tw > parseInt($e.css("minWidth").replace("px", ""))) {
    //                                    body[x].children[$e.index()].children[0].style.maxWidth = tw + "px";
    //                                }
    //                            } else {
    //                                $(body[x].children[$e.index()]).find('input:not(:checkbox), select, textarea').css("width", "100%");
    //                                //body[x].children[$e.index()].style.width = "100%";
    //                            }
    //                            if (!body[x].children[$e.index()].classList.contains("colresizing")) {
    //                                body[x].children[$e.index()].className += " colresizing";
    //                            }
    //                        }
    //                    }
    //                }
    //            })
    //            $(document).on("mouseup", function (e) {
    //                if (press != undefined && press == true) {
    //                    $e.removeClass("colresizing");
    //                    if (body != undefined) {
    //                        var tdCount = (body[0] != undefined ? body[0].children.length : 0);
    //                        for (var x = 0; x < body.length; x++) {
    //                            if (body[x].children.length == tdCount) {
    //                                body[x].children[$e.index()].className = body[x].children[$e.index()].className.replace(" colresizing", "");
    //                            }
    //                        }
    //                    }
    //                }
    //                press = false;
    //            })
    //            if ($s.$last) {
    //                var tw = 0;
    //                setTimeout(function () {
    //                    $e.parent().children().each(function (idx) {

    //                        var _ = $(this);
    //                        var w = (_.text().length * 10) + 20;
    //                        if (idx == 0) {
    //                            w = 20;
    //                        }
    //                        _.css("minWidth", w);
    //                        _.css("maxWidth", w);
    //                        _.css("width", w);
    //                        tw += w;
    //                    });
    //                    setTimeout(function () {
    //                        $e.parent().parent().parent().css("minWidth", $e.parent().parent().parent().css("width"));
    //                        $e.parent().parent().parent().css("maxWidth", $e.parent().parent().parent().css("minWidth"));
    //                    }, 500)
    //                    var main = parseInt($(".main-content").css("width").replace("px", "")) - 5;
    //                    if (tw < main) tw = main;
    //                    $e.parent().parent().parent().css("minWidth", tw);
    //                    $e.parent().parent().parent().css("maxWidth", tw);
    //                }, 100)
                    
    //            }
    //        }
    //    }
    //}])
    .directive('shortenText', [function () {
        return {
            restrict: "A",
            link: function ($s, $e, $a) {
                var idx = $e.parent().parent().index();
                var tbl = $a.tbl;
                var txt = $a.txt;
                var arrWidth = [];
                $(".groupIndx_" + tbl).children().children().children().each(function () {
                    var _ = $(this);
                    if (_.css("maxWidth") != "none") {
                        arrWidth.push(parseInt(_.css("maxWidth").replace("px", "")));
                    }

                });
                $e.parent().parent().css("user-select", "none");
                if (((txt.length * 10) + 20) > arrWidth[idx]) {
                    $e.parent().parent().css("maxWidth", arrWidth[idx]);
                    $e.parent().parent().css("overflow", "hidden");
                    $e.parent().parent().css("textOverflow", "ellipsis");
                    $e.parent().parent().css("whiteSpace", "nowrap");
                    $e.parent().css("maxWidth", arrWidth[idx]);
                    $e.parent().css("overflow", "hidden");
                    $e.parent().css("textOverflow", "ellipsis");
                    $e.parent().css("whiteSpace", "nowrap");
                }
            }
        }
    }])
    .directive('catchCheck', [function () {
        return {
            restrict: "A",
            require: "ngModel",
            link: function ($s, $e, $a, $ngModel) {
                $e.on("change", function () {
                    var _ = $(this);
                    var val = _.is(":checked");
                    $ngModel.$setViewValue(val);
                    $ngModel.$render();
                })
            }
        }
    }])
    .directive('rosSpectrum', [function () {
        return {
            restrict: "A",
            require: "ngModel",
            scope: { model: "=ngModel" },
            link: function ($s, $e, $a, $ngModel) {
                setTimeout(function () {
                    $s.$watch("model", function () {
                        if ($s.model == null) {
                            $s.model = "#FFF";
                        }
                    })
                    $e.spectrum({
                        allowEmpty: false,
                        color: $s.model || "#FFF",
                        showInput: true,
                        containerClassName: "full-spectrum",
                        showInitial: true,
                        showPalette: true,
                        showSelectionPalette: true,
                        showAlpha: true,
                        maxPaletteSize: 10,
                        preferredFormat: "hex",
                        move: function (color) {

                        },
                        show: function () {

                        },
                        beforeShow: function () {

                        },
                        hide: function (color) {

                        },

                        palette: [
                            ["rgb(0, 0, 0)", "rgb(67, 67, 67)", "rgb(102, 102, 102)",
                            "rgb(204, 204, 204)", "rgb(217, 217, 217)", "rgb(255, 255, 255)"],
                            ["rgb(152, 0, 0)", "rgb(255, 0, 0)", "rgb(255, 153, 0)", "rgb(255, 255, 0)", "rgb(0, 255, 0)",
                            "rgb(0, 255, 255)", "rgb(74, 134, 232)", "rgb(0, 0, 255)", "rgb(153, 0, 255)", "rgb(255, 0, 255)"],
                            ["rgb(230, 184, 175)", "rgb(244, 204, 204)", "rgb(252, 229, 205)", "rgb(255, 242, 204)", "rgb(217, 234, 211)",
                            "rgb(208, 224, 227)", "rgb(201, 218, 248)", "rgb(207, 226, 243)", "rgb(217, 210, 233)", "rgb(234, 209, 220)",
                            "rgb(221, 126, 107)", "rgb(234, 153, 153)", "rgb(249, 203, 156)", "rgb(255, 229, 153)", "rgb(182, 215, 168)",
                            "rgb(162, 196, 201)", "rgb(164, 194, 244)", "rgb(159, 197, 232)", "rgb(180, 167, 214)", "rgb(213, 166, 189)",
                            "rgb(204, 65, 37)", "rgb(224, 102, 102)", "rgb(246, 178, 107)", "rgb(255, 217, 102)", "rgb(147, 196, 125)",
                            "rgb(118, 165, 175)", "rgb(109, 158, 235)", "rgb(111, 168, 220)", "rgb(142, 124, 195)", "rgb(194, 123, 160)",
                            "rgb(166, 28, 0)", "rgb(204, 0, 0)", "rgb(230, 145, 56)", "rgb(241, 194, 50)", "rgb(106, 168, 79)",
                            "rgb(69, 129, 142)", "rgb(60, 120, 216)", "rgb(61, 133, 198)", "rgb(103, 78, 167)", "rgb(166, 77, 121)",
                            "rgb(91, 15, 0)", "rgb(102, 0, 0)", "rgb(120, 63, 4)", "rgb(127, 96, 0)", "rgb(39, 78, 19)",
                            "rgb(12, 52, 61)", "rgb(28, 69, 135)", "rgb(7, 55, 99)", "rgb(32, 18, 77)", "rgb(76, 17, 48)"]
                        ]
                    });
                }, 200);
            }
        }
    }])
    .directive('rosChart', ['$controller', function ($c) {
        return {
            restrict: 'A',
            templateUrl: '/Web/Template/Chart.tmpl.html',
            scope: {
                widgetType: '=',
                chartData: '='
            },
            link: function ($s, $e, $a) {
                $c('BaseController', { $scope: $s });
                var value = [];
                var chartDataSet = [];
                var isShared = false;
                if ($s.chartData != undefined) {
                    if ($s.widgetType == 3 || $s.widgetType == 5) {
                        value = Enumerable.From($s.chartData).Select(function (z) { return { "name": z.GroupName, y: z.Value[0] } }).ToArray();
                        chartDataSet.push({
                            type: ($s.widgetType == 3 ? "pie" : "doughnut"),
                            indexLabelFontSize: 14,
                            startAngle: 0,
                            toolTipContent: "{name}: {y} - <strong>#percent%</strong>",
                            indexLabel: "{name} #percent%",
                            dataPoints: value
                        });
                        isShared = false;
                    } else if ($s.widgetType == 4 || $s.widgetType == 6 || $s.widgetType == 7) {
                        if ($s.chartData[0].SeriesName.length > 0) {
                            for (var cs = 0; cs < $s.chartData[0].SeriesName.length; cs++) {
                                chartValues = Enumerable.From($s.chartData).Select(function (z) { return z.Value[cs] }).ToArray();
                                var value = Enumerable.From($s.chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[cs] } }).ToArray();
                                chartDataSet.push({
                                    type: ($s.widgetType == 4 ? "column" : ($s.widgetType == 6 ? "bar" : "line")),
                                    name: $s.chartData[0].SeriesName[cs],
                                    legendText: $s.chartData[0].SeriesName[cs],
                                    dataPoints: value
                                });
                            }
                            isShared = true;
                        } else {
                            chartValues = Enumerable.From($s.chartData).Select(function (z) { return z.Value[0] }).ToArray();
                            var value = Enumerable.From($s.chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[0] } }).ToArray();
                            chartDataSet.push({
                                type: ($s.widgetType == 4 ? "column" : ($s.widgetType == 6 ? "bar" : "line")),
                                dataPoints: value
                            });
                            isShared = false;
                        }
                    }
                    var chart = new CanvasJS.Chart("chartTest", {
                        colorSet: "myColorSet",
                        theme: "theme3",
                        animationEnabled: true,
                        toolTip: { shared: isShared },
                        data: chartDataSet
                    });
                    chart.render()
                } else {
                    $s.Toast("No data found.", 'Preview Chart', 'warning');
                }
            }
        }
    }])
    .directive('shortText', [function () {
        return {
            restrict: 'A',
            link: function ($s, $e, $a) {
                $e[0].style.maxWidth = $a.textWidth + "px";
                $e[0].style.width = $a.textWidth + "px";
                $e[0].style.textOverflow = 'ellipsis';
                $e[0].style.whiteSpace = 'nowrap';
                $e[0].style.overflow = 'hidden';
            }
        }
    }])