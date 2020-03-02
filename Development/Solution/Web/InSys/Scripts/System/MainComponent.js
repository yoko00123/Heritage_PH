
'use strict';
define(['app'], function (app) {
    var defaultCtrl = function ($s, $c, $state, $md) {

        $c("BaseController", { $scope: $s });
        $c("MenuController", { $scope: $s });
        $s.vueLayout = null;

        $s.AppTitle = 'InSys';
        $s.IsSystem = parseFloat($s.Session('ID_UserGroup')) === 1;
        $s.DisplayPhoto = window.sessionStorage.DisplayPhoto == undefined ? $s.Session('ImageFile') : window.sessionStorage.DisplayPhoto;

        $s.Init = (function () {
             
            $s.AppTitle = $s.Session('Company');
			const cn = $s.Session('Company');
		    localStorage.setItem("cnv", cn);
            $(".bookmark-panel").addClass("toggle-bookmark");
            $(".star").addClass("toggle-star");

            setTimeout(function () {
                $(".bookmark-panel").removeClass("toggle-bookmark");
                $(".star").removeClass("toggle-star");
            }, 2000);

            $(".side-menu").removeClass("toggle-sidemenu");
        })

        $s.Publish = function () {
            $s.ToggleUser();
            //$s.Request("Publish", {}).then(function () {
            //    $s.Toast('Publish Successful.', 'Publish Website', 'success');
            //    window.location.reload();
            //}).fail(function (ex) {
            //    console.log(ex)
            //    $s.Toast(ex.data.Message, 'Publish Website', 'warning');
            //})

            var RequestID = vcl.Random.S4();
            $s.OverlayMessage('Publishing Menu');
            $s.Request('Publish', { RequestID: RequestID }).then(function () {
                $s.ShowOverlay();
                $s.CheckPublishStatus(RequestID)
            }).fail(function (ex) {
                console.log(ex)
                $s.Toast(ex.data.Message, 'Publish Website', 'warning');
            });
        }

        $s.CheckPublishStatus = (function (RequestID) {
            $s.Request('PublishStatus', { RequestID: RequestID }).then(function (d) {
                if (d.Status === 0) {
                    $s.OverlayMessage(d.Message);
                    $s.Task(null, 1000).then(function () {
                        $s.CheckPublishStatus(RequestID);
                    });
                } else {
                    $s.HideOverlay();
                    $s.Toast('Publish Successful.', 'Publish Website', 'success');
                    window.location.reload();
                }
            });
        })


        $s.LogOut = (function () {
            $s.ToggleUser();
            window.localStorage.clear();
            window.sessionStorage.clear();
            setTimeout(function () {
                $s.ActionUrl('LogOut', 'Account');
            }, 100);

        })

        $s.OnBlur = (function () {
            setTimeout(function () {
                $(".header-user").removeClass("toggle-user");
                $(".user-toggle-container").removeClass("toggle-user");
            }, 200)
        })

        $s.ToggleUser = (function () {
            $('.upload-toggle').css('display', 'initial');
            $(".header-user").toggleClass("toggle-user");
            $(".user-toggle-container").toggleClass("toggle-user");
            $s.ToggleUploadLeave();
        })

        $s.ToggleSideMenu = (function () {
            $(".side-menu").toggleClass("toggle-sidemenu");
            $(".bookmark-panel").removeClass("toggle-bookmark");
            $(".star").removeClass("toggle-star");

            if ($state.current.name == 'List') {
                setTimeout(function () {
                    var r_pos = ($(".table-footer").width() - $(".module-header").width()) + 10;
                    $(".paging").css("right", r_pos);
                }, 100)
            }

            if ($state.current.name == 'Dashboard') {
                setTimeout(function () {
                    $(".tab-content").find('table').each(function () {
                        var pid = $(this).attr('id').split("_")[$(this).attr('id').split("_").length - 1];
                        var rc = $(".expandor_" + pid).attr('rc');
                        var arrWidth = [];
                        var p = $("#tbl_head_" + pid);
                        var b = $("#tbl_body_" + pid);
                        var pw = p.parent()[0].clientWidth;
                        var tw = 0;
                        b.children().children().each(function (idx) {
                            var childs = $(this)[0].children;
                            for (var x = 0; x < childs.length; x++) {
                                var w = (childs[x].textContent.length * 10) + 20;
                                if (rc == 0) {
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
                        })

                        if (pw > tw) {
                            p[0].style.width = "100%";
                            b[0].style.width = "100%";
                        } else {
                            p.css("width", tw);
                            b.css("width", tw);
                        }
                        $("#div_body_" + pid).on("scroll", function () {
                            $("#div_head_" + pid).scrollLeft($(this)[0].scrollLeft)
                        });
                    });
                }, 500);
            } else {
                setTimeout(function () {
                    var cas = $(".MenuTable");
                    if (cas.length > 0) {
                        $(".MenuTable").each(function () {
                            var main = parseInt($(".main-content").css("width").replace("px", "")) - 5;
                            var _ = $(this);
                            if (parseInt(_.css("maxWidth").replace("px", "")) < main) {
                                _.css("maxWidth", main);
                                _.css("minWidth", main);
                                _.css("width", main);
                            }
                        })
                    }
                }, 500)
            }
        });

        $s.ToggleBookmark = (function () {
            $(".bookmark-panel").toggleClass("toggle-bookmark");
            $(".star").toggleClass("toggle-star");
        })

        $s.ToggleUpload = (function () {
            if ($(".user-toggle-container").hasClass("toggle-user") == true) {
                $(".upload-button").addClass("upload-toggle");
            }
        })
        $s.ToggleUploadLeave = (function () {
            $(".upload-button").removeClass("upload-toggle")
        })

        $s.SearchClear = function () {
            $('#auto_searchMain_value').val('');

            var e = jQuery.Event("keydown");
            e.which = 8;
            $("#auto_searchMain_value").trigger(e);
        }

        $s.menuSelect = (function (d) {
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
        })

        $s.GoToDashboard = (function () {
            var state = $s.ActiveApplicationType == 1 ? 'Dashboard2' : 'IONS';
            $state.go(state);
        })

        $s.PublishWidget = function () {
            $s.ToggleUser();
            $s.Request("PublishWidget", {}).then(function () {
                $s.Toast('Publish Successful.', 'Publish Widget', 'success');
                window.location.reload();
            }).fail(function (ex) {
                console.log(ex)
                $s.Toast(ex.data.Message, 'Publish Widget', 'warning');
            })
        }

        $s.toggleFav = (function (mnu) {
            if (mnu.HasFav)
                mnu.HasFav = false;
            else
                mnu.HasFav = true;

            if (mnu.HasFav) {
                var fv = {
                    ID: 0,
                    ID_ApplicationType: parseInt($s.ActiveApplicationType),
                    ID_Menu: mnu.ID,
                    Menu: mnu.Name,
                    SeqNo: $s.UserFav.length + 1,
                    ID_User: parseInt($s.Session("ID_User"))
                }
                vcl.Array.Remove($s.UserFav, function (x) { return x.ID_Menu === mnu.ID });
                $s.Request('AddUserFav', fv).then(function (d) {
                    $s.UserFav.push(fv);
                });
            } else {
                $s.Request('RemoveUserFav', { ID_User: parseInt($s.Session("ID_User")), ID_Menu: mnu.ID }).then(function (d) {
                    vcl.Array.Remove($s.UserFav, function (x) { return x.ID_Menu === mnu.ID });
                });
            }
        })

        $s.FavRemove = (function (d) {
            $s.Request('RemoveUserFav', { ID_User: parseInt($s.Session("ID_User")), ID_Menu: d.ID_Menu }).then(function () {
                vcl.Array.Remove($s.UserFav, function (x) { return x.ID_Menu === d.ID_Menu });

                $s.UpdateMenuItem(d.ID_Menu, 'HasFav', false);

            });
        })

        $s.FavFilter = (function (fv) {
            return fv.ID_ApplicationType === parseInt($s.ActiveApplicationType);
        })

        $s.FavMenuSelect = (function (fv) {
            var g = $s.GetMenuItem(fv.ID_Menu);
            $s.menuSelect(g);

            $(".bookmark-panel").removeClass("toggle-bookmark");
            $(".star").removeClass("toggle-star");
        })

        $s.Contents = (function () {
            $s.ToggleUser();
            $state.go('Contents');
        })

        $s.GoToThemes = function () {
            $s.ToggleUser();
            $state.go("Themes");
        }

        $s.SetOnline = (function () {
            $s.Request('SetUserOnline', { ID: $s.Session('ID_User'), ID_Employee: $s.Session('ID_Employee'), GUID: $s.Session("GUID") }).then(function () {
            })
        })

        $s.UploadDP = (function () {
            $('.upload-toggle').css('display', 'none');
            $s.UploadFile('UploadImage', null, false, 'image/*').then(function (d) { 
                $s.Request('UpdateWebImage', { Column: 'ImageFile', Value: d.GUID, ID: $s.Session('ID_User'), ID_Employee: $s.Session('ID_Employee') }).then(function () {
                    $s.Session('ImageFile', d.GUID);
                    $s.DisplayPhoto = d.GUID;
                    window.sessionStorage.DisplayPhoto = d.GUID;
                    $s.Toast('Display Photo has been Updated');
                })
            }).fail(function (msg) {
                $s.Toast(msg, document.title, 'warning');
            })
        })

        $s.menuContext = []

        var Construct = (function () { 
            $s.Implant();
             
            $s.LoadMenu();

            if ($s.Session('ID_User') <= 2) {
                $s.menuContext = [
                    {
                        text: "Menu",
                        click: function ($itemScope, $event, modelValue, text, $li) {
                            $md.Load($s, 36, $itemScope.menu.Parent.ID);
                        }
                    },
                    {
                        text: "Add to dashboard",
                        click: function ($itemScope, $event, modelValue, text, $li) { },
                        enabled: function ($itemScope, $event, modelValue, text, $li) {
                            return $s.checkMenu($itemScope.menu.Parent.ID);
                        },
                        children: [
                            {
                                text: "Create shortcut link",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 1, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(1, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout, 4);
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 2, "h": 4, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 1, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "MinW": 2, "MinH": 4, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    });
                                }
                            },
                            {
                                text: "Create Pie Chart",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 3, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(3, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout);
                                            var pieLabel = Enumerable.From(r.tbl).Select(function (p) { return p.Name }).ToArray();
                                            var pieData = Enumerable.From(r.tbl).Select(function (p) { return p.Value }).ToArray();
                                            var piePercentage = Enumerable.From(r.tbl).Select(function (p) { return p.Percentage }).ToArray();
                                            var pieColors = [];
                                            for (var p = 0; p < r.tbl.length; p++) {
                                                var color = $s.randomColors();
                                                while (pieColors.indexOf(color) > -1) {
                                                    color = $s.randomColors();
                                                }
                                                pieColors.push(color);
                                            }
                                            var pie = {
                                                Label: pieLabel,
                                                Data: pieData,
                                                Percentage: piePercentage,
                                                Colors: pieColors
                                            }
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 4, "h": 8, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 3, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "pieData": pie, "column": (r.selectedColumn.toLowerCase().indexOf('id_') > -1 ? r.selectedColumn.substr(3) : r.selectedColumn), "MinW": 4, "MinH": 8, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    });
                                }
                            },
                            {
                                text: "Create Bar Chart",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 4, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(4, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout);
                                            var barLabel = Enumerable.From(r.tbl).Select(function (p) { return p.Name }).ToArray();
                                            var barData = Enumerable.From(r.tbl).Select(function (p) { return p.Value }).ToArray();
                                            var barPercentage = Enumerable.From(r.tbl).Select(function (p) { return p.Percentage }).ToArray();
                                            var barColors = [];
                                            for (var p = 0; p < r.tbl.length; p++) {
                                                var color = $s.randomColors();
                                                while (barColors.indexOf(color) > -1) {
                                                    color = $s.randomColors();
                                                }
                                                barColors.push(color);
                                            }
                                            var bar = {
                                                Label: barLabel,
                                                Data: barData,
                                                Percentage: barPercentage,
                                                Colors: barColors
                                            }
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 8, "h": 8, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 4, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "barData": bar, "column": (r.selectedColumn.toLowerCase().indexOf('id_') > -1 ? r.selectedColumn.substr(3) : r.selectedColumn), "MinW": 8, "MinH": 8, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    })
                                }
                            }
                        ]
                    }
                ]
            } else {
                $s.menuContext = [
                    {
                        text: "Add to dashboard",
                        click: function ($itemScope, $event, modelValue, text, $li) { },
                        enabled: function ($itemScope, $event, modelValue, text, $li) {
                            return $s.checkMenu($itemScope.menu.Parent.ID);
                        },
                        children: [
                            {
                                text: "Create shortcut link",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 1, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(1, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout);
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 2, "h": 4, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 1, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "MinW": 2, "MinH": 4, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    });
                                }
                            },
                            {
                                text: "Create Pie Chart",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 3, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(3, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout);
                                            var pieLabel = Enumerable.From(r.tbl).Select(function (p) { return p.Name }).ToArray();
                                            var pieData = Enumerable.From(r.tbl).Select(function (p) { return p.Value }).ToArray();
                                            var piePercentage = Enumerable.From(r.tbl).Select(function (p) { return p.Percentage }).ToArray();
                                            var pieColors = [];
                                            for (var p = 0; p < r.tbl.length; p++) {
                                                var color = $s.randomColors();
                                                while (pieColors.indexOf(color) > -1) {
                                                    color = $s.randomColors();
                                                }
                                                pieColors.push(color);
                                            }
                                            var pie = {
                                                Label: pieLabel,
                                                Data: pieData,
                                                Percentage: piePercentage,
                                                Colors: pieColors
                                            }
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 4, "h": 8, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 3, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "pieData": pie, "column": (r.selectedColumn.toLowerCase().indexOf('id_') > -1 ? r.selectedColumn.substr(3) : r.selectedColumn), "MinW": 4, "MinH": 8, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    });
                                }
                            },
                            {
                                text: "Create Bar Chart",
                                enabled: function ($itemScope, $event, modelValue, text, $li) {
                                    return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 4, $itemScope.menu.Parent.ID_MenuType);
                                },
                                click: function ($itemScope, $event, modelValue, text, $li) {
                                    $s.showContextDialog(4, $itemScope.menu.Parent).then(function (r) {
                                        if (r.error != undefined) {
                                            $s.Toast(r.error, 'CreateShortcutLink', 'warning');
                                        } else {
                                            var maxData = $s.getMax($s.widgetLayout);
                                            var barLabel = Enumerable.From(r.tbl).Select(function (p) { return p.Name }).ToArray();
                                            var barData = Enumerable.From(r.tbl).Select(function (p) { return p.Value }).ToArray();
                                            var barPercentage = Enumerable.From(r.tbl).Select(function (p) { return p.Percentage }).ToArray();
                                            var barColors = [];
                                            for (var p = 0; p < r.tbl.length; p++) {
                                                var color = $s.randomColors();
                                                while (barColors.indexOf(color) > -1) {
                                                    color = $s.randomColors();
                                                }
                                                barColors.push(color);
                                            }
                                            var bar = {
                                                Label: barLabel,
                                                Data: barData,
                                                Percentage: barPercentage,
                                                Colors: barColors
                                            }
                                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": 8, "h": 8, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": 4, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "barData": bar, "column": (r.selectedColumn.toLowerCase().indexOf('id_') > -1 ? r.selectedColumn.substr(3) : r.selectedColumn), "MinW": 8, "MinH": 8, "ID_Menu": $itemScope.menu.Parent.ID });
                                            $s.vueLayout._data.resizable = true;
                                            $s.vueLayout._data.draggable = true;
                                            if ($("#appLayoutBar").length > 0) {
                                                $("#appLayoutBar").css("display", "block");
                                                $s.computeWidgetContainerHeight();
                                            }
                                        }
                                    })
                                }
                            }
                        ]
                    }
                ]
            }
            $s.checkMenu = function (id) {
                return (Enumerable.From($s.copyMenu).Where(function (x) { return x.ID == id }).ToArray().length > 0 ? true : false);
            }
            $s.checkMenuIfDashboard = function (id, type, menuType) {
                var ret = true;
                if (type == 1) {
                    ret = (Enumerable.From($s.widgetLayout).Where(function (x) { return x.ID_Menu == id && x.type == type }).ToArray().length > 0 ? true : false);
                } else {
                    if (menuType == 6) {
                        ret = true;
                    } else {
                        ret = false;
                    }
                }
                return ret;
                //return false;
            }
            $s.showContextDialog = function (widgetType, menu) {
                switch (widgetType) {
                    case 1:
                        var maxData = $s.getMax($s.widgetLayout);
                        return $s.Request('CreateWidget', { x: 0, y: (maxData.y + maxData.h), Type: 1, ID_Menu: menu.ID, name: menu.Name, ID_ApplicationType: parseInt($s.ActiveApplicationType), ID_MenuType: menu.ID_MenuType });
                        break;
                    case 3:
                        return $s.Dialog({
                            template: 'CreatePieChart.tmpl.html',
                            controller: 'CreatePieChart',
                            size: 'sm',
                            data: { menu: menu, widgetLayout: $s.widgetLayout, ID_ApplicationType: parseInt($s.ActiveApplicationType) }
                        }).result;
                        break;
                    case 4:
                        return $s.Dialog({
                            template: 'CreateBarChart.tmpl.html',
                            controller: 'CreateBarChart',
                            size: 'sm',
                            data: { menu: menu, widgetLayout: $s.widgetLayout, ID_ApplicationType: parseInt($s.ActiveApplicationType) }
                        }).result;
                        break;
                    default:
                        var maxData = $s.getMax($s.widgetLayout);
                        return $s.Request('CreateWidget', { x: 0, y: (maxData.y + maxData.h), Type: 1, ID_Menu: menu.ID, name: menu.Name, ID_ApplicationType: parseInt($s.ActiveApplicationType), ID_MenuType: menu.ID_MenuType });
                        break;
                }
            }
            $s.randomColors = function () {
                var letters = '0123456789ABCDEF'.split('');
                var color = '#';
                for (var i = 0; i < 6; i++) {
                    color += letters[Math.floor(Math.random() * 16)];
                }
                return color;
            }
            $s.computeWidgetContainerHeight = function (addHeight) {
                var layout = $("#appLayout");
                var height = (window.innerHeight - layout[0].offsetTop) + (addHeight == null ? 0 : addHeight);
                layout.css("max-height", height + "px");
                layout.css("height", height + "px");
                $s.tmpWidget = angular.copy($s.widgetLayout);
            }
            $s.getMax = function (arr) {
                var ret = {};
                var max;
                var h;
                if (arr.length > 0) {
                    for (var i = 0 ; i < arr.length ; i++) {
                        if (i == 0) {
                            max = arr[i];
                            ret.y = parseInt(arr[i]["y"]);
                            ret.h = parseInt(arr[i]["h"]);
                        } else {
                            if (parseInt(arr[i]["y"]) > parseInt(max["y"])) {
                                max = arr[i];
                                ret.y = parseInt(arr[i]["y"]);
                                ret.h = parseInt(arr[i]["h"]);
                            }
                        }
                    }
                } else {
                    ret.y = 0;
                    ret.h = 0;
                }

                return ret;
            }
        }($s));

        var offsetTop = $(".bookmark-panel")[0].offsetTop;
        var clientHeight = $(".bookmark-panel")[0].clientHeight;
        var offsetTop2 = $(".right-box")[0].offsetTop;
        var clientHeight2 = $(".right-box")[0].clientHeight;
        $(".bookmark-list").css("maxHeight", (clientHeight - 36) - offsetTop);
        $(".bookmark-list").css("minHeight", (clientHeight - 36) - offsetTop);
        $(".right-box-body").css("maxHeight", (clientHeight2 - 36) - offsetTop2);
        $(".right-box-body").css("minHeight", (clientHeight2 - 36) - offsetTop2);

        $s.selectedMenu = "";

        $s.openHelper = function () {
            $state.go('Helper', { reload: true });
            $s.ToggleUser();
        }
    }

    app.controller('MainComponent', ['$scope', '$controller', '$state', 'MenuDialog', defaultCtrl]);

    app.controller('CreatePieChart', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $modal, $data) {
        $c('BaseController', { $scope: $s });
        $s.columns = [];
        $s.cData = {};
        $s.cData.selectedColumn = null;
        $s.GetMenu($data.menu.ID).then(function (mm) {
            $s.columns = mm.tMenuTabField;
        })
        $s.widgetLayout = $data.widgetLayout;
        $s.Cancel = function () {
            $modal.dismiss();
        }
        $s.ID_MenuType = $data.menu.ID_MenuType
        $s.getMax = function (arr) {
            var ret = {};
            var max;
            var h;
            for (var i = 0 ; i < arr.length ; i++) {
                if (arr.length > 0) {
                    for (var i = 0 ; i < arr.length ; i++) {
                        if (i == 0) {
                            max = arr[i];
                            ret.y = parseInt(arr[i]["y"]);
                            ret.h = parseInt(arr[i]["h"]);
                        } else {
                            if (parseInt(arr[i]["y"]) > parseInt(max["y"])) {
                                max = arr[i];
                                ret.y = parseInt(arr[i]["y"]);
                                ret.h = parseInt(arr[i]["h"]);
                            }
                        }
                    }
                } else {
                    ret.y = 0;
                    ret.h = 0;
                }
            }
            return ret;
        }
        var maxData = $s.getMax($s.widgetLayout);
        $s.savePie = function () {
            $s.Request('CreateWidget', { x: 0, y: (maxData.y + maxData.h), name: $data.menu.Name, ID_Menu: $data.menu.ID, Type: 3, menu: $data.menu, column: $s.cData.selectedColumn, ID_ApplicationType: parseInt($data.ID_ApplicationType), ID_MenuType: $s.ID_MenuType }).then(function (r) {
                if (r.error != undefined) {
                    $s.Toast(r.error, 'CreateWidgetPie', 'warning');
                } else {
                    $s.Toast("Pie Created", 'CreateWidgetPie', 'success');
                    r.selectedColumn = $s.cData.selectedColumn;
                    $modal.close(r)
                }
            })
        }
    }]);

    app.controller('CreateBarChart', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $modal, $data) {
        $c('BaseController', { $scope: $s });
        $s.columns = [];
        $s.cData = {};
        $s.cData.selectedColumn = null;
        $s.GetMenu($data.menu.ID).then(function (mm) {
            $s.columns = mm.tMenuTabField;
        })
        $s.widgetLayout = $data.widgetLayout;
        $s.Cancel = function () {
            $modal.dismiss();
        }
        $s.ID_MenuType = $data.menu.ID_MenuType
        $s.getMax = function (arr) {
            var ret = {};
            var max;
            var h;
            for (var i = 0 ; i < arr.length ; i++) {
                if (arr.length > 0) {
                    for (var i = 0 ; i < arr.length ; i++) {
                        if (i == 0) {
                            max = arr[i];
                            ret.y = parseInt(arr[i]["y"]);
                            ret.h = parseInt(arr[i]["h"]);
                        } else {
                            if (parseInt(arr[i]["y"]) > parseInt(max["y"])) {
                                max = arr[i];
                                ret.y = parseInt(arr[i]["y"]);
                                ret.h = parseInt(arr[i]["h"]);
                            }
                        }
                    }
                } else {
                    ret.y = 0;
                    ret.h = 0;
                }
            }
            return ret;
        }
        var maxData = $s.getMax($s.widgetLayout);
        $s.saveBar = function () {
            $s.Request('CreateWidget', { x: 0, y: (maxData.y + maxData.h), name: $data.menu.Name, ID_Menu: $data.menu.ID, Type: 4, menu: $data.menu, column: $s.cData.selectedColumn, ID_ApplicationType: parseInt($data.ID_ApplicationType), ID_MenuType: $s.ID_MenuType }).then(function (r) {
                if (r.error != undefined) {
                    $s.Toast(r.error, 'CreateWidgetBar', 'warning');
                } else {
                    $s.Toast("Bar Created", 'CreateWidgetBar', 'success');
                    r.selectedColumn = $s.cData.selectedColumn;
                    $modal.close(r)
                }
            })
        }
    }]);
});