
define(['app'], function (app) {

    var Dashboard = (function ($s, $c, $ds, $state) {
        var GridLayout = VueGridLayout.GridLayout;
        var GridItem = VueGridLayout.GridItem;
        $s.widgetToRemove = [];
        $s.tmpWidgetToRemove = [];
        //$s.HasClass = false;
        //$s.Announcements = [];

        $s.$parent.vueLayout = new Vue({
            el: '#appLayout',
            components: {
                GridLayout,
                GridItem,
            },
            data: {
                layout: $s.widgetLayout,
                draggable: false,
                resizable: false,
                viewMenu: false,
                top: '0px',
                left: '0px'
            },
            methods: {
                gotoMenu: function (d) {
                    var rpth = "";
                    if (d.ReportFile == "" || d.ReportFile == null) {
                        switch (parseInt(d.ID_MenuType)) {
                            case 3: //text itu
                                $s.Alert('text view here, under construction');
                                break;
                            default:
                                rpth = "List";
                                break;
                        }
                    } else
                        rpth = "Report";

                    $state.go(rpth, {
                        Name: d.Name.replace(/ /g, '-'),
                        r: LZString.compressToEncodedURIComponent(d.ID.toString())
                    });
                },
                movedEvent: function (i, newX, newY) {

                },
                resizedEvent: function (i, newH, newW, newHPx, newWPx) {
                    if ($("#containerChart_" + i).length > 0) {
                        var h = 265 + ((newH - 8) * 40);
                        $("#containerChart_" + i)[0].style.height = h + "px";
                        var cid = i;
                        var c = Enumerable.From($s.chartCollections).Where(function (x) { return x.options.cid == cid }).ToArray()[0];
                        setTimeout(function () {
                            c.render();
                        }, 500)
                    } else {
                        if ($("#sl_" + i).length > 0) {
                            $("#sl_" + i).find(".menu-abbr")[0].style.textOverflow = "ellipsis";
                            $("#sl_" + i).find(".menu-abbr")[0].style.overflow = "hidden";
                            $("#sl_" + i).find(".menu-abbr")[0].style.fontSize = 60 + ((newH - 4) * 35) + "px";
                        }
                    }
                    
                },
                setMenu: function (top, left) {
                    top = top - $("#appLayout")[0].offsetTop;
                    left = left - $("#appLayout")[0].offsetLeft;

                    this.top = top + 'px';
                    this.left = left + 'px';
                },
                closeMenu: function () {
                    this.viewMenu = false;
                },
                openMenu: function (e) {
                    this.viewMenu = true;

                    Vue.nextTick(function () {
                        this.$refs.right.focus();

                        this.setMenu(e.y, e.x)
                    }.bind(this));
                    e.preventDefault();
                },
                enableEditing: function () {
                    $("#appLayoutBar").css("display", "block");
                    $s.computeWidgetContainerHeight();
                    this.viewMenu = false;
                    this.resizable = true;
                    this.draggable = true;
                },
                RemoveWidget: function (i, o) {
                    var target = $(o.target).parent().parent();
                    target.css("opacity", ".5");
                    var obj = Enumerable.From(angular.copy($s.widgetLayout)).Where(function (w) { return w.i == i }).ToArray()[0];
                    var obj2 = Enumerable.From($s.widgetLayout).Where(function (w) { return w.i == i }).ToArray()[0];
                    obj2.IsRemove = true;
                    $s.tmpWidgetToRemove.push(obj);
                    $s.widgetToRemove.push(parseInt(i));
                    //vcl.Array.Remove($s.widgetLayout, function (wl) {
                    //    return parseInt(wl.i) == parseInt(i)
                    //});
                    vcl.Array.Remove($s.tmpWidget, function (wl) {
                        return parseInt(wl.i) == parseInt(i)
                    });
                },
                SetIcon: function (str) {
                    var abbr = "";
                    str = str.split(" ");
                    for (var i = 0; i < str.length; i++) {
                        abbr += str[i].substr(0, 1);
                    }

                    if (abbr.length > 2) {
                        abbr = abbr.substr(0, 2);
                    }

                    return abbr.toLowerCase();
                },
                SetSlide: function (Id) {
                    var $this = $("#sl_" + Id);
                    var span = $(".ov-container p", $this);
                    var pwidth = $(".ov-container", $this).width();
                    var swidth = $(span, $this).width();

                    if (pwidth < swidth) {
                        $this.addClass("text-slide");
                        span.css("marginLeft", (swidth + 5) * (-1));
                        $(span).bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
                            $s.$parent.vueLayout.DeSetSlide();
                        });
                    }
                },
                DeSetSlide: function () {
                    $(".hasDblClick").removeClass("text-slide");
                    $(".ov-container p").css("marginLeft", 0);
                },
                saveLayout: function () {
                    $s.saveLayout();
                    this.viewMenu = false;
                },
                cancelLayout: function () {
                    $s.closeBar();
                    this.viewMenu = false;
                },
                changeShowType: function (item) {
                    item.showType = !item.showType;
                }
            }
        });
        //$s.GetAnnouncements = (function () {
        //    $s.Request("getAnnouncements", { ID_ApplicationType: $s.ActiveApplicationType }).then(function (obj) {
        //        $s.Announcements = obj;
        //        //$s.openDashHead();
        //        setTimeout(function () {
        //            $s.computeWidgetContainerHeight();
        //        }, 1000);
        //    });
        //})

        $s.FormatCalendarDate = function (date, format) {
            var d = new Date(date);
            var month = [
                "January", "February", "March",
                "April", "May", "June",
                "July", "August", "September",
                "October", "November", "December"
            ];
            if (format == 'month') {
                d = month[d.getUTCMonth()].substr(0, 3);
            } else if (format == 'day') {
                d = d.getUTCDate();
            }
            return d;
        }
        //Chart.defaults.global.legend.display = false;
        $s.myColorSet = [];
        Vue.component('chart', {
            props: ['chartLabels', 'chartValues', 'chartId', 'chartType', 'chartTitle', 'chartShared', 'chartItem'],
            mounted () {
                var labels, values, chartid, type, title, isShared;
                labels = this.chartLabels;
                values = this.chartValues;
                chartid = this.chartId;
                type = this.chartType;
                title = this.chartTitle;
                isShared = this.chartShared;
                this.chartItem.showType = true;
                if (this.chartItem.chartData.colors.length == 0) this.chartItem.chartData.colors = $s.myColorSet;
                if (Enumerable.From($s.chartCollections).Where(function (z) { return z.options.cid == chartid }).ToArray().length == 0) {
                    var h = 265 + ((this.chartItem.h - 8) * 40);
                    $("#containerChart_" + this.chartItem.i)[0].style.height = h + "px";
                    setTimeout(function () {
                        if ($state.current.name == "Dashboard") {
                            var chart = new CanvasJS.Chart("containerChart_" + chartid, {
                                colorSet: "myColorSet",
                                //theme: "theme3",
                                cid: chartid,
                                animationEnabled: true,
                                title: title,
                                toolTip: { shared: isShared },
                                data: values
                            });
                            $s.chartCollections.push(chart);
                            chart.render();
                        }
                    }, 500);
                } else {
                    var chart = Enumerable.From($s.chartCollections).Where(function (z) { return z.options.cid == chartid }).ToArray()[0];
                    var idx = $s.chartCollections.indexOf(chart);
                    $s.chartCollections.splice(idx, 1);
                    chart = null;
                    var h = 265 + ((this.chartItem.h - 8) * 40);
                    $("#containerChart_" + this.chartItem.i)[0].style.height = h + "px";
                    setTimeout(function () {
                        if ($state.current.name == "Dashboard") {
                            chart = new CanvasJS.Chart("containerChart_" + chartid, {
                                colorSet: "myColorSet",
                                //theme: "theme3",
                                cid: chartid,
                                animationEnabled: true,
                                title: title,
                                toolTip: { shared: isShared },
                                data: values
                            });
                            $s.chartCollections.push(chart);
                            chart.render();
                        }
                    }, 500);
                }
            }
        });
        Vue.directive('shortcutLink', {
            bind: function (el, binding, vnode) {
                $(el).find(".menu-abbr")[0].style.textOverflow = "ellipsis";
                $(el).find(".menu-abbr")[0].style.overflow = "hidden";
                $(el).find(".menu-abbr")[0].style.fontSize = 60 + ((binding.value - 4) * 35) + "px";
            }
        })
        $s.FetchNewWidgets = function () {
            $s.Request("FetchNewWidgetChartData", { ID_ApplicationType: parseInt($s.ActiveApplicationType) }).then(function (r) {
                var colorSet = [];
                for (var cc = 0; cc < 64; cc++) {
                    var color = $s.randomColors();
                    while (colorSet.indexOf(color) > -1) {
                        color = $s.randomColors();
                    }
                    colorSet.push(color);
                }
                CanvasJS.addColorSet("myColorSet", colorSet);

                if ($s.widgetLayout.length == 0) {
                    if (r.error != undefined) {
                        $s.Toast(r.error, 'FetchNewWidgetChartData', 'warning');
                    } else {
                        var charts = Enumerable.From(r.data).Where(function (c) { return c.type != 1 && c.type != 2 }).ToArray();
                        for (var x = 0; x < charts.length; x++) {
                            charts[x].menu = Enumerable.From($s.copyMenu).Where(function (a) { return a.ID == charts[x].ID_Menu }).ToArray()[0];
                            var chartData = r.chartList[x];
                            var chartLabels = Enumerable.From(chartData).Select(function (x) { return x.GroupName }).ToArray();
                            var chartValues = [], chartPercentage = [], chartDataSet = [], chartSeries = [], chartColors = [];
                            chartColors = colorSet;
                            $s.myColorSet = colorSet;
                            if (chartData.length > 0) {
                                if (charts[x].type == 3 || charts[x].type == 5) {
                                    chartValues = Enumerable.From(chartData).Select(function (x) { return x.Value[0] }).ToArray();
                                    var value = Enumerable.From(chartData).Select(function (z) { return { "name": z.GroupName, y: z.Value[0] } }).ToArray();
                                    chartDataSet.push({
                                        type: (charts[x].type == 3 ? "pie" : "doughnut"),
                                        indexLabelFontSize: 14,
                                        startAngle: 0,
                                        toolTipContent: "{name}: {y} - <strong>#percent%</strong>",
                                        indexLabel: "{name} #percent%",
                                        dataPoints: value
                                    });
                                    chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                    charts[x].isShared = false;
                                } else if (charts[x].type == 4 || charts[x].type == 6 || charts[x].type == 7) {
                                    if (chartData[0].SeriesName.length > 0) {
                                        for (var cs = 0; cs < chartData[0].SeriesName.length; cs++) {
                                            chartValues = Enumerable.From(chartData).Select(function (z) { return z.Value[cs] }).ToArray();
                                            var value = Enumerable.From(chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[cs] } }).ToArray();
                                            chartDataSet.push({
                                                type: (charts[x].type == 4 ? "column" : (charts[x].type == 6 ? "bar" : "line")),
                                                name: chartData[0].SeriesName[cs],
                                                legendText: chartData[0].SeriesName[cs],
                                                dataPoints: value
                                            });
                                        }
                                        chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                        charts[x].isShared = true;
                                    } else {
                                        chartValues = Enumerable.From(chartData).Select(function (z) { return z.Value[0] }).ToArray();
                                        var value = Enumerable.From(chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[0] } }).ToArray();
                                        chartDataSet.push({
                                            type: (charts[x].type == 4 ? "column" : (charts[x].type == 6 ? "bar" : "line")),
                                            dataPoints: value
                                        });
                                        chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                        charts[x].isShared = false;
                                    }
                                }
                            }
                            $s.widgetChartData = {};
                            $s.widgetChartData.labels = chartLabels;
                            $s.widgetChartData.datasets = chartDataSet;
                            $s.widgetChartData.colors = chartColors;
                            $s.widgetChartData.percentage = chartPercentage;
                            charts[x].chartData = $s.widgetChartData;
                            var t = "";
                            if (charts[x].type != 1) {
                                t = (charts[x].column.indexOf("ID_") > -1 ? charts[x].column.replace("ID_", "").charAt(0).toUpperCase() + charts[x].column.replace("ID_", "").slice(1) : charts[x].column) + " Summary of " + charts[x].name;
                            }
                            charts[x].title = {
                                text: t
                            }
                            $s.widgetLayout.push(charts[x]);
                        }
                        var shortCut = Enumerable.From(r.data).Where(function (c) { return c.type == 1 }).ToArray();
                        for (var x = 0; x < shortCut.length; x++) {
                            shortCut[x].menu = Enumerable.From($s.copyMenu).Where(function (a) { return a.ID == shortCut[x].ID_Menu }).ToArray()[0];
                            var chartData = r.chartList[x];
                            var chartLabels = Enumerable.From(chartData).Select(function (x) { return x.GroupName }).ToArray();
                            var chartValues = [], chartPercentage = [], chartDataSet = [], chartSeries = [], chartColors = [];
                            $s.widgetChartData = {};
                            $s.widgetChartData.labels = chartLabels;
                            $s.widgetChartData.datasets = chartDataSet;
                            $s.widgetChartData.colors = chartColors;
                            $s.widgetChartData.percentage = chartPercentage;
                            shortCut[x].chartData = $s.widgetChartData;
                            var t = "";
                            if (shortCut[x].type != 1) {
                                t = (shortCut[x].column.indexOf("ID_") > -1 ? shortCut[x].column.replace("ID_", "").charAt(0).toUpperCase() + shortCut[x].column.replace("ID_", "").slice(1) : shortCut[x].column) + " Summary of " + shortCut[x].name;
                            }
                            shortCut[x].title = {
                                text: t
                            }
                            $s.widgetLayout.push(shortCut[x]);
                        }
                    }
                }
            });
        }
        $s.closeBar = function () {
            $("#appLayoutBar").css("display", "none");
            $s.$parent.vueLayout._data.resizable = false;
            $s.$parent.vueLayout._data.draggable = false;
            if ($s.tmpWidgetToRemove.length > 0) {
                //angular.forEach($s.tmpWidgetToRemove, function (w) {
                //    $s.widgetLayout.push(w);
                //});
                angular.forEach($s.tmpWidgetToRemove, function (w) {
                    $s.tmpWidget.push(w);
                    var o = $("#sl_" + w.i);
                    var target = o.parent();
                    target.css("opacity", "1");
                });
                $s.tmpWidgetToRemove = [];
                $s.widgetToRemove = [];
            }
            if ($s.tmpWidget.length > 0) {
                angular.forEach($s.tmpWidget, function (w) {
                    var wid = Enumerable.From($s.widgetLayout).Where(function (xw) { return parseInt(xw.i) == parseInt(w.i) }).ToArray()[0];
                    wid.x = w.x;
                    wid.y = w.y;
                    wid.w = w.w;
                    wid.h = w.h;
                    wid.IsRemove = false;
                    wid.showType = true;
                    setTimeout(function () {
                        if ($("#containerChart_" + wid.i).length > 0) {
                            var h = 265 + ((w.h - 8) * 40);
                            $("#containerChart_" + wid.i)[0].style.height = h + "px";
                            var cid = wid.i;
                            var c = Enumerable.From($s.chartCollections).Where(function (x) { return x.options.cid == cid }).ToArray()[0];
                            setTimeout(function () {
                                c.render();
                            }, 500)
                        } else {
                            if ($("#sl_" + wid.i).length > 0) {
                                $("#sl_" + wid.i).find(".menu-abbr")[0].style.textOverflow = "ellipsis";
                                $("#sl_" + wid.i).find(".menu-abbr")[0].style.overflow = "hidden";
                                $("#sl_" + wid.i).find(".menu-abbr")[0].style.fontSize = 60 + ((w.h - 4) * 35) + "px";
                            }
                        }
                    }, 100)
                })
            }
            $s.computeWidgetContainerHeight();
            setTimeout(function () {
                for (var x = 0; x < $s.chartCollections.length; x++) {
                    var c = $s.chartCollections[x];
                    c.render();
                }
            }, 500)
        }
        $s.saveLayout = function () {
            $s.valueData = angular.copy($s.widgetLayout)
            for (var x = 0; x < $s.valueData.length; x++) {
                delete $s.valueData[x].chartData
            }
            $s.Request("SaveLayout", { layout: $s.valueData, idsToRemove: JSON.stringify($s.widgetToRemove) }).then(function (ret) {
                if (ret.error != undefined) {
                    $s.Toast(ret.error, 'SaveLayout', 'warning');
                } else {
                    $s.Toast("Saved successfully.", 'SaveLayout', 'success');
                }
                for (var x in $s.widgetToRemove) {
                    vcl.Array.Remove($s.widgetLayout, function (wl) {
                        return parseInt(wl.i) == parseInt($s.widgetToRemove[x])
                    });
                }

                $("#appLayoutBar").css("display", "none");
                $s.computeWidgetContainerHeight();
                $s.$parent.vueLayout._data.resizable = false;
                $s.$parent.vueLayout._data.draggable = false;
                $s.widgetToRemove = [];
                $s.tmpWidgetToRemove = [];
                var tt = angular.copy($s.widgetLayout);
                angular.forEach(tt, function (a, b) {
                    vcl.Array.Remove($s.widgetLayout, function (wl) {
                        return parseInt(wl.i) == parseInt(a.i)
                    });
                })
                setTimeout(function () {
                    angular.forEach(tt, function (tt2) {
                        $s.widgetLayout.push(tt2)
                    });
                }, 200)
            })
        }
        $s.randomColors = function () {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }
        $s.Init = (function () {
            //$s.GetAnnouncements();
            $s.computeWidgetContainerHeight();
        })
        //$s.closeDashHead = (function () {
        //    $(".dashboard-2").removeClass("open");
        //    $s.HasClass = false;
        //    $s.computeWidgetContainerHeight($(".dashboard-2").height() - 34);
        //})
        //$s.openDashHead = (function () {
        //    if ($s.Announcements.length > 0) {
        //        $(".dashboard-2").addClass("open");
        //        $s.HasClass = true;
        //    } else {
        //        $(".dashboard-2").removeClass("open");
        //        $s.HasClass = false;
        //    }
        //    setTimeout(function () {
        //        $s.computeWidgetContainerHeight();
        //    }, 300)
        //});
        $s.FetchNewWidgets();

        $(window).on('resize', function () {
            $(".widget-graph").each(function () {
                var me = $(this);
                var i = parseInt(me.attr("id").split("_")[1]);
                var c = Enumerable.From($s.chartCollections).Where(function (x) { return x.options.cid == i }).ToArray()[0];
                setTimeout(function () {
                    c.render();
                }, 500)
            })
        })

        //$('.dashboard-2').hover(function () {
        //    $("#crslAnnouncement").carousel('pause');
        //}, function () {
        //    $("#crslAnnouncement").carousel('cycle');
        //});
    });

    app.register.controller('Dashboard3', ['$scope', '$controller', 'DataService', '$state', Dashboard]);
})