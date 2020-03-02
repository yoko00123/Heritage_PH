
define(['app'], function (app) {

    var Dashboard = (function ($s, $c, $ds, $state) {
        var GridLayout = VueGridLayout.GridLayout;
        var GridItem = VueGridLayout.GridItem;
        $s.widgetToRemove = [];
        $s.tmpWidgetToRemove = [];
        $s.HasClass = true;
        $s.Announcements = [];

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
                }
            }
        });
        $s.GetAnnouncements = (function () {
            $s.Request("getAnnouncements", { ID_ApplicationType: $s.ActiveApplicationType }).then(function (obj) {
                $s.Announcements = obj;
                $s.openDashHead();
            });
        })

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
        Chart.defaults.global.legend.display = false;
        Vue.component('pie-chart', {
            extends: VueChartJs.Pie,
            props: ['labels', 'colors', 'datas', 'title'],
            mounted () {
                this.renderChart({
                    labels: this.labels,
                    datasets: [
                      {
                          backgroundColor: this.colors,
                          data: this.datas
                      }
                    ]
                }, {
                    responsive: true,
                    maintainAspectRatio: false,
                    title: {
                        display: true,
                        text: this.title,
                        fontColor: "#555",
                        fontWeight: 'normal',
                        fontStyle: 'normal',
                        fontSize: 14
                    }
                })
            }
        });
        Vue.component('bar-chart', {
            extends: VueChartJs.Bar,
            props: ['labels', 'colors', 'datas', 'title'],
            mounted () {
                this.renderChart({
                    labels: this.labels,
                    datasets: [
                      {
                          backgroundColor: this.colors,
                          data: this.datas
                      }
                    ]
                }, {
                    responsive: true,
                    maintainAspectRatio: false,
                    title: {
                        display: true,
                        text: this.title,
                        fontColor: "#555",
                        fontWeight: 'normal',
                        fontStyle: 'normal',
                        fontSize: 14
                    }
                })
            }
        });

        $s.FetchNewWidgets = function () {
            $s.Request("FetchNewWidgets", { ID_ApplicationType: parseInt($s.ActiveApplicationType) }).then(function (r) {
                if ($s.widgetLayout.length == 0) {
                    if (r.error != undefined) {
                        $s.Toast(r.error, 'FetchNewWidgets', 'warning');
                    } else {
                        for (var x = 0; x < r.data.length; x++) {
                            r.data[x].menu = Enumerable.From($s.copyMenu).Where(function (a) { return a.ID == r.data[x].ID_Menu }).ToArray()[0];
                            if (r.data[x].type == 3) {
                                var pie = Enumerable.From(r.pieData).Where(function (p) { return p[r.data[x].i] }).ToArray()[0][r.data[x].i];
                                var pieLabel = Enumerable.From(pie).Select(function (p) { return p.Name }).ToArray();
                                var pieData = Enumerable.From(pie).Select(function (p) { return p.Value }).ToArray();
                                var piePercentage = Enumerable.From(pie).Select(function (p) { return p.Percentage }).ToArray();
                                var pieColors = [];
                                for (var p = 0; p < pie.length; p++) {
                                    var color = $s.randomColors();
                                    while (pieColors.indexOf(color) > -1) {
                                        color = $s.randomColors();
                                    }
                                    pieColors.push(color);
                                }
                                r.data[x].pieData = {
                                    Label: pieLabel,
                                    Data: pieData,
                                    Percentage: piePercentage,
                                    Colors: pieColors
                                }
                            } else if (r.data[x].type == 4) {
                                var bar = Enumerable.From(r.barData).Where(function (p) { return p[r.data[x].i] }).ToArray()[0][r.data[x].i];
                                var barLabel = Enumerable.From(bar).Select(function (p) { return p.Name }).ToArray();
                                var barData = Enumerable.From(bar).Select(function (p) { return p.Value }).ToArray();
                                var barPercentage = Enumerable.From(bar).Select(function (p) { return p.Percentage }).ToArray();
                                var barColors = [];
                                for (var p = 0; p < bar.length; p++) {
                                    var color = $s.randomColors();
                                    while (barColors.indexOf(color) > -1) {
                                        color = $s.randomColors();
                                    }
                                    barColors.push(color);
                                }
                                r.data[x].barData = {
                                    Label: barLabel,
                                    Data: barData,
                                    Percentage: barPercentage,
                                    Colors: barColors
                                }
                            }
                            $s.widgetLayout.push(r.data[x]);
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
                })
            }
            $s.computeWidgetContainerHeight();
        }
        $s.saveLayout = function () {
            $s.Request("SaveLayout", { layout: $s.widgetLayout, idsToRemove: JSON.stringify($s.widgetToRemove) }).then(function (ret) {
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
            $s.GetAnnouncements();
        })
        $s.closeDashHead = (function () {
            $(".dashboard-2").removeClass("open");
            $s.HasClass = false;
            $s.computeWidgetContainerHeight($(".dashboard-2").height() - 34);
        })
        $s.openDashHead = (function () {
            if ($s.Announcements.length > 0) {
                $(".dashboard-2").addClass("open");
                $s.HasClass = true;
            } else {
                $(".dashboard-2").removeClass("open");
                $s.HasClass = false;
            }
            setTimeout(function () {
                $s.computeWidgetContainerHeight();
            }, 300)
        });
        $s.FetchNewWidgets();
    });

    app.register.controller('Dashboard2', ['$scope', '$controller', 'DataService', '$state', Dashboard]);
})