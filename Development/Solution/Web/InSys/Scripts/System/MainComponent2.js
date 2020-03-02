
'use strict';
define(['app'], function (app) {
    var defaultCtrl = function ($s, $c, $state, $md, $rootScope, $q, d) {
        $c("BaseController", { $scope: $s });
        var Construct = (function () {
            //$s.Implant();
            $s.Request('Session').then(function (dta) {
                $s.UserRow(dta);
                $c("MenuController", { $scope: $s });
                //console.log('session', dta);
                var menutype = 1;
                localStorage.setItem("menutypemo", menutype);
				const us = $s.PersonaName;
				var dfuc  = window.btoa(us);
				localStorage.setItem("us", dfuc);
                $s.vueLayout = null;
                $s.chartCollections = [];
				
			    const cn = $s.Session('Company')
			    var cnv  = window.btoa(cn);
				localStorage.setItem("cnv", cnv);

                $s.AppTitle = 'InSys';
                $s.IsSystem = false;  //parseInt($s.Session('ID_UserGroup')) === 1;
				
                const guid = dta.GUID;
				localStorage.setItem("gu", guid);

                $s.AppTitle = 'InSys';
                $s.IsSystem = parseFloat($s.Session('ID_UserGroup')) === 1;
                $s.DisplayPhoto = window.sessionStorage.DisplayPhoto == undefined ? $s.Session('ImageFile') : window.sessionStorage.DisplayPhoto;
                $s.HasClass = false;
                $s.Announcements = [];
                $s.Notifications = [];
                $s.NotificationCnt = 0;

                $s.computeWidgetContainerHeight = function (addHeight) {
                    var layout = $("#appLayout");
                    if (layout.length > 0) {
                        var height = (window.innerHeight - layout[0].offsetTop) + (addHeight == null ? 0 : addHeight);
                        layout.css("max-height", height + "px");
                        layout.css("height", height + "px");
                        $s.tmpWidget = angular.copy($s.widgetLayout);
                    }
                }

                $s.Restart = function () {
                    $s.Confirm('Are you sure you want to restart the website?', 'Restart').then(function () {
                        $s.Request('RestartWebsite', {});
                        setTimeout(function () {
                            $s.ToggleUser();
                            window.sessionStorage.clear();
                            setTimeout(function () {
                                $s.ActionUrl('LogOut', 'Account');
                            }, 100);
                        }, 5000)
                    }); 
                }

                $s.Init = function () {
                    $s.GetAnnouncements();

                    //console.log('sess',$s.Session('Company'));

                    $s.AppTitle = $s.Session('Company');
                    $(".bookmark-panel").addClass("toggle-bookmark");
                    $(".star").addClass("toggle-star");

                    setTimeout(function () {
                        $(".bookmark-panel").removeClass("toggle-bookmark");
                        $(".star").removeClass("toggle-star");
                    }, 2000);

                    $(".side-menu").removeClass("toggle-sidemenu");
                }
                
                $s.OpenCalendar = function () {
                    //console.log('Full Calendar', $s.Session('ID_Employee'), $s.Session);
                    $s.Dialog({
                        template: 'FullCalendar.tmpl.html',
                        controller: 'fullCalendar',
                        windowClass: 'custom-calendar',
                        size: 'lg',
                        data: {}
                    });
                }

                $s.computeWidgetContainerHeight = function (addHeight) {
                    var layout = $("#appLayout");
                    if (layout.length > 0) {
                        var height = (window.innerHeight - layout[0].offsetTop) + (addHeight == null ? 0 : addHeight);
                        layout.css("max-height", height + "px");
                        layout.css("height", height + "px");
                        $s.tmpWidget = angular.copy($s.widgetLayout);
                    }
                }
                $s.GetAnnouncements = (function () {
                    //console.log('ID_Company', parseInt($s.Session("ID_Company")));
                    $s.Request("getAnnouncements", { ID_ApplicationType: $s.ActiveApplicationType, ID_Company: parseInt($s.Session("ID_Company")) }).then(function (obj) {
                        $s.Announcements = obj;
                        setTimeout(function () {
                            $s.computeWidgetContainerHeight();
                        }, 1000);
                    });
                })
                $s.Init();
                $s.openDashHead = (function () {
                    $s.IsOpenAnnouncement = false;
                    if ($state.current.name != 'Dashboard') $("#crslAnnouncement").carousel('cycle');

                    if ($s.Announcements.length > 0) {
                        $(".dashboard-2").toggleClass("open");
                        $s.HasClass = true;
                        $s.IsOpenAnnouncement = true;
                        if ($(".dashboard-2").hasClass("open")) {
                            $(".dashboard-2-title").text("Announcements");
                            $s.IsOpenAnnouncement = true;
                        } else {
                            $(".dashboard-2-title").text("Dashboard");
                            $s.IsOpenAnnouncement = false;
                        }
                    } else {
                        $(".dashboard-2").removeClass("open");
                        $s.HasClass = false;
                        $s.IsOpenAnnouncement = false;
                    }

                    setTimeout(function () {
                        $s.computeWidgetContainerHeight();
                    }, 300)
                });

                $s.Publish = function (ID_Menu) {

                    if (!ID_Menu)
                        $s.ToggleUser();

                    var RequestID = vcl.Random.S4();
                    $s.OverlayMessage('Publishing Menu');
                    $s.Request('Publish', { RequestID: RequestID, ID_Menu: ID_Menu || null }).then(function () {
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
                    window.sessionStorage.clear();
					localStorage.removeItem("ipadd");
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
                    $(".header-user").toggleClass("toggle-user");
                    $(".user-toggle-container").toggleClass("toggle-user");
                    $s.ToggleUploadLeave();
                })

                $s.ToggleSideMenu = (function () {
                    $(".side-menu").toggleClass("toggle-sidemenu");
                    $(".untoggled-actions").toggleClass("toggle-actions");
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
                            if ($s.widgetLayout.length > 0) {
                                angular.forEach($s.widgetLayout, function (w) {
                                    var wid = Enumerable.From($s.widgetLayout).Where(function (xw) { return parseInt(xw.i) == parseInt(w.i) }).ToArray()[0];
                                    wid.x = w.x;
                                    wid.y = w.y;
                                    wid.w = w.w;
                                    wid.h = w.h;
                                    wid.IsRemove = false;
                                    wid.showType = true;
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
                                })
                            }
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
                    var state = $s.ActiveApplicationType == 1 ? 'Dashboard' : 'IONS';
                    $state.go(state);
                })

                $s.GotoHangFire = (function () {
                    window.location = '/HangFire';
                });

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


            
                    var exc = localStorage.getItem("ex");
                    var ex = window.atob(exc);
                    if (exc !== undefined && exc !== "" && exc !== null && exc !== "na" && exc !== 'bnVsbA==') {
                        $s.Request('SetUserOnline', { GUID: exc }).then(function () {
                            var exd = 'na';
                            localStorage.setItem("ex", exd);
                        });
                    }
          

                $s.UploadDP = (function () {
                    $(".upload-button").removeClass("upload-toggle")
                    $s.UploadFile('UploadImage', null, false, 'image/*').then(function (d) {
                        $s.Request('UpdateWebImage', { Column: 'ImageFile', Value: d.GUID, ID: $s.Session('ID_User'), ID_Employee: $s.Session('ID_Employee') }).then(function () {
                            $s.Session('ImageFile', d.GUID);
                            $s.DisplayPhoto = d.GUID;
                            window.sessionStorage.DisplayPhoto = d.GUID;
                            $s.Toast('Display Photo has been Updated');
                        })
                        $(".upload-button").removeClass("upload-toggle")
                    }).fail(function (msg) {
                        $(".upload-button").removeClass("upload-toggle")
                        $s.Toast(msg, document.title, 'warning');
                    })
                })

                $s.menuContext = []

                $s.tmp = function () {
                    $s.Dialog({
                        template: 'tmp.tmpl.html',
                        controller: 'tmp',
                        windowClass:'tmp-dlg',
                        size: 'lg',
                        data: {}
                    });
                }


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

                //$s.Request('CountOnlineUser', { GUID: $s.Session('GUID'), ID_User: $s.Session('ID_User')  }).then(function (dta) {

                //    var cou = dta.CountIDS[0].Column1;
                //    var userol = dta.data2[0].Name;
                //    //var useroll = dta.data2.length;

                //    $s.Onlineusers = cou;
                //    $s.OnlineusersName = userol;
                //   // $s.Onlineusersl = useroll;

                //});

                $s.Request("fetchInitNotifications", { appType: $s.ActiveApplicationType }).then(function (ret) {
                
                    if (ret.error == undefined) {
                        $s.Notifications = ret.data;
                        $s.NotificationCnt = ret.cnt;
                        
                    } else {
                        $s.Toast(ret.error, 'FetchInitNotifications', 'warning');
                        }
               
                });

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

                $('.dashboard-2').hover(function () {
                    $("#crslAnnouncement").carousel('pause');
                }, function () {
                    $("#crslAnnouncement").carousel('cycle');
                });

                $s.currentState = $state.current.name;
                $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
                    $s.currentState = toState.name;
                });

                $(document).ready(function () {
                    $('#noti_Button').click(function () {
                        $('#notifications').fadeToggle('fast', 'linear');
                        return false;
                    });

                    $(document).click(function () {
                        $('#notifications').hide();
                    });

                    $('#notifications').click(function () {
                        return false;
                    });
                });

                var isReady = true;
                $("#notifications_container").scroll(function () {
                    if (isReady) {
                        if ($("#notification_spinner").length == 0) {
                            if (($(this).scrollTop() + $(this).innerHeight()) >= $(this)[0].scrollHeight) {
                                $("#notifications_container").append('<div id="notification_spinner" style="padding: 5px;font-size: 20px;text-align: center;"><i class="fa fa-spinner fa-spin"></i></div>');
                                var ids = Enumerable.From($s.Notifications).Select(function (x) { return x.ID }).ToArray();
                                var appType = $s.ID_ApplicationType;
                                var _ = $(this);
                                $s.Request("fetchOldNotifications", { ids: ids, appType: appType }, 'Action', true).then(function (ret) {
                                    if (ret.error == undefined) {
                                        if (ret.length > 0) {
                                            Enumerable.From(ret).ForEach(function (a) {
                                                $s.Notifications.push(a);
                                            });
                                            $s.NotificationCnt = $s.NotificationCnt + (Enumerable.From(ret).Where(function (x) { return x.IsView == 0 }).ToArray().length);
                                        }
                                    } else {
                                        $s.Toast(ret.error, 'FetchOldNotifications', 'warning');
                                    }

                                    _.scrollTop(_[0].scrollHeight - (_.innerHeight() + 50));
                                    $("#notification_spinner").remove();
                                    isReady = false;
                                    setTimeout(function () {
                                        isReady = true;
                                    }, 5000);
                                });
                            }
                        }
                    }

                });

                $s.setNotificationTime = function (notifDateTime) {
                    var _MS_PER_DAY = 1000 * 60;
                    var a = new Date();
                    var b = new Date(notifDateTime); // Or any other JS date

                    var timeDiff = dateDiffInDays(b, a);
                    // a and b are javascript Date objects
                    function dateDiffInDays(a, b) {
                        // Discard the time and time-zone information.
                        var utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate(), a.getHours(), a.getMinutes());
                        var utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate(), b.getHours(), b.getMinutes());

                        return Math.floor((utc2 - utc1) / _MS_PER_DAY);
                    }

                    function formatDate(date) {
                        var hours = date.getHours();
                        var minutes = date.getMinutes();
                        var ampm = hours >= 12 ? 'pm' : 'am';
                        hours = hours % 12;
                        hours = hours ? hours : 12;
                        minutes = minutes < 10 ? '0' + minutes : minutes;
                        var strTime = hours + ':' + minutes + ' ' + ampm;
                        return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
                    }

                    var val = null;
                    if ((timeDiff / 60) >= 1) {
                        if (((timeDiff / 60) / 24) >= 1) {
                            if ((((timeDiff / 60) / 24) / 7) >= 1) {
                                if (((((timeDiff) / 60 / 24) / 7) / 4) >= 1) {
                                    return formatDate(b);
                                } else {
                                    //week
                                    val = Math.floor((((timeDiff) / 60 / 24) / 7));
                                    return (val > 1 ? val + " weeks ago..." : val + " week ago...");
                                }
                            } else {
                                //days
                                val = Math.floor((timeDiff / 60) / 24);
                                return (val > 1 ? val + " days ago..." : val + " day ago...");
                            }
                        } else {
                            //hours
                            val = Math.floor(timeDiff / 60);
                            return (val > 1 ? val + " hours ago..." : val + " hour ago...");
                        }
                    } else {
                        //minutes
                        val = Math.floor(timeDiff);
                        return (val > 1 ? val + " minutes ago..." : val <= 0 ? "just now..." : val + " minute ago...");
                    }
                }

                $s.generateNotificationLink = (function (itm) {
                    $s.Request('openNotification', { id: itm.ID, menu: itm.ID_Menu, rID: itm.rID, IsView: itm.IsView }).then(function (ret) {
                        console.log(itm.Menu)
                        if (ret.error == undefined) {
                            if (itm.IsView == 0) {
                                itm.IsView = 1;
                                $s.NotificationCnt = $s.NotificationCnt - 1;
                            }
                            $state.go('Info', {
                                Name: itm.Menu.replace(/ /g, '-'),
                                r: LZString.compressToEncodedURIComponent(itm.ID_Menu.toString() + '-' + itm.rID.toString() +
                                    (itm.ID_MenuType == 6 ? '-' + itm.Menu.replace(/ /g, '%20') + '-' + itm.ID_Menu.toString() : ''))
                                
                            });
                        } else {
                            if (ret.error == "The notification doesn't exist anymore.") {
                                if (itm.IsView == 0) {
                                    $s.NotificationCnt = $s.NotificationCnt - 1;
                                    vcl.Array.Remove($s.Notifications, function (n) {
                                        return n.ID == itm.ID;
                                    });
                                } else {
                                    vcl.Array.Remove($s.Notifications, function (n) {
                                        return n.ID == itm.ID;
                                    });
                                }
                                $s.Toast(ret.error, 'OpenNotification', 'warning');
                            } else {
                                $s.Toast(ret.error, 'OpenNotification', 'warning');
                            }

                        }
                    });
                    $s.startNotificationRemoval();
                });

                var startFlag = true;
                $s.startNotificationRemoval = function () {
                    startFlag = false;
                    var ids = Enumerable.From($s.Notifications).Select(function (x) { return x.ID }).ToArray();
                    if (ids.length > 0) {

                        $s.Request('startNotificationRemoval', { ids: ids }, 'Action', true).then(function (ret) {
                            if (ret.error == undefined) {
                                var distinct = Enumerable.From(ret.ids).Distinct().ToArray();
                                
                                if (JSON.stringify(ids) != JSON.stringify(distinct)) {
                                    for (var z = 0; z < distinct.length; z++) {
                                        $s.NotificationCnt = $s.NotificationCnt - 1;
                                        vcl.Array.Remove($s.Notifications, function (ii) {
                                            return ii.ID == distinct[z];
                                        });
                                    }
                                }
                                //console.log('running notification watcher');
                            } else {
                                $s.Toast(ret.error, 'StartNotificationRemoval', 'warning');
                            }

                            ids = Enumerable.From($s.Notifications).Select(function (x) { return x.ID }).ToArray();
                            $s.Request("fetchMoreNotifications", { ids: ids, appType: $s.ActiveApplicationType }, 'Action', true).then(function (ret) {
                                if (ret.error == undefined) {
                                    if (ret.length > 0) {
                                        Enumerable.From(ret).ForEach(function (a) {
                                            if (ids.indexOf(a.ID) < 0) {
                                                $s.Notifications.push(a);
                                            }
                                        });
                                        $s.NotificationCnt = $s.NotificationCnt + ret.length;
                                        if ($("#notification_count").hasClass("notification_bounce")) {
                                            $("#notification_count").removeClass("notification_bounce");
                                            setTimeout(function () {
                                                $("#notification_count").addClass("notification_bounce");
                                            }, 1000);
                                        } else {
                                            $("#notification_count").addClass("notification_bounce");
                                        }
                                    }
                                } else {
                                    $s.Toast(ret.error, 'FetchMoreNotifications', 'warning');
                                }
                                startFlag = true;
                            });
                        });
                    } else {
                        $s.Request("fetchMoreNotifications", { ids: ids, appType: $s.ActiveApplicationType }, 'Action', true).then(function (ret) {
                            if (ret.error == undefined) {
                                if (ret.length > 0) {
                                    Enumerable.From(ret).ForEach(function (a) {
                                        if (ids.indexOf(a.ID) < 0) {
                                            $s.Notifications.push(a);
                                        }
                                    });
                                    $s.NotificationCnt = $s.NotificationCnt + ret.length;
                                    if ($("#notification_count").hasClass("notification_bounce")) {
                                        $("#notification_count").removeClass("notification_bounce");
                                        setTimeout(function () {
                                            $("#notification_count").addClass("notification_bounce");
                                        }, 1000);
                                    } else {
                                        $("#notification_count").addClass("notification_bounce");
                                    }
                                }
                            } else {
                                $s.Toast(ret.error, 'FetchMoreNotifications', 'warning');
                            }
                            startFlag = true;
                        });
                    }
                }

                var startNotificationRemoval = setInterval(function () {
                    if (startFlag) {
                        $s.startNotificationRemoval();
                    }
                }, 120000);

                //var counter = 1;   
                //var updateUserAccess = setInterval(function () {
                //    var _this = new Date();
                //    var currHour = _this.getHours();
                //    console.log(currHour);
                //    if (currHour === 24 | currHour === 12 | currHour === 23 ) {
                //    if (counter === 1) {
                //        $s.Request("updateResignedUser", {}, 'Action', true);
                //        counter++;
                //    }
                //} else
                //{
                //    counter = 1;
                //}
                //}, 1000);

                $s.OpenAnnouncement = (function () {
                    d.GetMenu(2047).then(function (m) {
                        $state.go('List', {
                            Name: m.tMenu.Name.replace(/ /g, '-'),
                            r: LZString.compressToEncodedURIComponent(m.tMenu.ID.toString())
                        })
                    })
                })



                $s.ID_ApplicationType = $s.Session('ID_ApplicationType');

                if (window.sessionStorage.ActiveApplicationType == null || window.sessionStorage.ActiveApplicationType == typeof (undefined)) {
                    window.sessionStorage.setItem("ActiveApplicationType", ($s.ID_ApplicationType == null ? 1 : $s.ID_ApplicationType));
                }

                $s.PersonaName = $s.Session('Name');
                if (window.sessionStorage.ActiveApplicationType != null && window.sessionStorage.ActiveApplicationType != typeof (undefined)) {
                    $s.ActiveApplicationType = window.sessionStorage.ActiveApplicationType;
                    $s.AppType = ($s.ActiveApplicationType == 1 ? true : false);
                } else {
                    if ($s.ID_ApplicationType == null) {
                        $s.ActiveApplicationType = 1;
                        $s.AppType = true;
                    } else {
                        $s.ActiveApplicationType = $s.ID_ApplicationType;
                        $s.AppType = ($s.ActiveApplicationType == 1 ? true : false);
                    }
                }

                $s.IsSystem = parseInt($s.Session('ID_UserGroup')) === 1;
                $s.DisplayPhoto = $s.Session('ImageFile');

                $s.DisplayPhoto = $s.Session('ImageFile') // window.sessionStorage.DisplayPhoto == undefined ? $s.Session('ImageFile') : window.sessionStorage.DisplayPhoto;

                $s.LoadMenu();

                $s.initiateWidget = function ($itemScope, $event, modelValue, text, $li) {
                    $s.showContextDialog(0, $itemScope.menu.Parent).then(function (r) {
                        if (r.error != undefined) {
                            $s.Toast(r.error, 'Create Chart', 'warning');
                        } else {
                            var chartData = r.tbl;
                            var maxData = $s.getMax($s.widgetLayout);
                            var chartLabels = Enumerable.From(chartData).Select(function (x) { return x.GroupName }).ToArray();
                            var chartValues = [], chartPercentage = [], chartDataSet = [], chartSeries = [], chartColors = [];
                            var isShared = false;
                            if (chartData.length > 0) {
                                if (r.type == 3 || r.type == 5) {
                                    chartValues = Enumerable.From(chartData).Select(function (x) { return x.Value[0] }).ToArray();
                                    var value = Enumerable.From(chartData).Select(function (z) { return { "name": z.GroupName, y: z.Value[0] } }).ToArray();
                                    chartDataSet.push({
                                        type: (r.type == 3 ? "pie" : "doughnut"),
                                        indexLabelFontSize: 14,
                                        startAngle: 0,
                                        toolTipContent: "{name}: {y} - <strong>#percent%</strong>",
                                        indexLabel: "{name} #percent%",
                                        dataPoints: value
                                    });
                                    chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                    isShared = false;
                                } else if (r.type == 4 || r.type == 6 || r.type == 7) {
                                    if (chartData[0].SeriesName.length > 0) {
                                        for (var cs = 0; cs < chartData[0].SeriesName.length; cs++) {
                                            chartValues = Enumerable.From(chartData).Select(function (z) { return z.Value[cs] }).ToArray();
                                            var value = Enumerable.From(chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[cs] } }).ToArray();
                                            chartDataSet.push({
                                                type: (r.type == 4 ? "column" : (r.type == 6 ? "bar" : "line")),
                                                name: chartData[0].SeriesName[cs],
                                                legendText: chartData[0].SeriesName[cs],
                                                dataPoints: value
                                            });
                                        }
                                        chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                        isShared = true;
                                    } else {
                                        chartValues = Enumerable.From(chartData).Select(function (z) { return z.Value[0] }).ToArray();
                                        var value = Enumerable.From(chartData).Select(function (z) { return { "label": z.GroupName, y: z.Value[0] } }).ToArray();
                                        chartDataSet.push({
                                            type: (r.type == 4 ? "column" : (r.type == 6 ? "bar" : "line")),
                                            dataPoints: value
                                        });
                                        chartPercentage = Enumerable.From(chartData).Select(function (x) { return x.Percentage }).ToArray();
                                        isShared = false;
                                    }
                                }
                            }

                            var chartW, chartH;
                            switch (r.type) {
                                case 1:
                                    chartW = 2;
                                    chartH = 4;
                                    break;
                                case 3:
                                    chartW = 4;
                                    chartH = 8;
                                    break;
                                case 4:
                                    chartW = 8;
                                    chartH = 8;
                                    break;
                                case 5:
                                    chartW = 4;
                                    chartH = 8;
                                    break;
                                case 6:
                                    chartW = 8;
                                    chartH = 8;
                                    break;
                                case 7:
                                    chartW = 8;
                                    chartH = 8;
                                    break;
                            }
                            $s.widgetChartData = {};
                            $s.widgetChartData.labels = chartLabels;
                            $s.widgetChartData.datasets = chartDataSet;
                            $s.widgetChartData.colors = chartColors;
                            $s.widgetChartData.percentage = chartPercentage;
                            var t = (r.selectedColumn.indexOf("ID_") > -1 ? r.selectedColumn.replace("ID_", "").charAt(0).toUpperCase() + r.selectedColumn.replace("ID_", "").slice(1) : r.selectedColumn) + " Summary of " + $itemScope.menu.Parent.Name;
                            var title = {
                                text: t
                            }
                            $s.widgetLayout.push({ "IsRemove": false, "x": 0, "y": maxData.y + maxData.h, "w": chartW, "h": chartH, "i": r.index.toString(), "name": $itemScope.menu.Parent.Name, "type": r.type, "cnt": r.cnt, "menu": $itemScope.menu.Parent, "chartData": $s.widgetChartData, "column": r.selectedColumn, "column2": r.selectedColumn2, "MinW": chartW, "MinH": chartH, "ID_Menu": $itemScope.menu.Parent.ID, "title": title, "showType": true, "isShared": isShared })
                            if ($state.current.name == "Dashboard") {
                                $s.vueLayout._data.resizable = true;
                                $s.vueLayout._data.draggable = true;
                                if ($("#appLayoutBar").length > 0) {
                                    $("#appLayoutBar").css("display", "block");
                                    $s.computeWidgetContainerHeight();
                                }
                            }
                        }
                    });
                }

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
                                    text: "Create Chart",
                                    enabled: function ($itemScope, $event, modelValue, text, $li) {
                                        return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 3, $itemScope.menu.Parent.ID_MenuType);
                                    },
                                    click: function ($itemScope, $event, modelValue, text, $li) {
                                        $s.initiateWidget($itemScope, $event, modelValue, text, $li);
                                    }
                                }
                            ]
                        }, {
                            text: "Publish",
                            click: function ($itemScope, $event, modelValue, text, $li) {
                                // $md.Load($s, 36, $itemScope.menu.Parent.ID);
                                $s.Publish($itemScope.menu.Parent.ID);
                            }
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
                                    text: "Create Chart",
                                    enabled: function ($itemScope, $event, modelValue, text, $li) {
                                        return !$s.checkMenuIfDashboard($itemScope.menu.Parent.ID, 3, $itemScope.menu.Parent.ID_MenuType);
                                    },
                                    click: function ($itemScope, $event, modelValue, text, $li) {
                                        $s.initiateWidget($itemScope, $event, modelValue, text, $li);
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
                        case 0:
                            return $s.Dialog({
                                template: 'CreateChart.tmpl.html',
                                controller: 'CreateChart',
                                size: 'lg',
                                data: { menu: menu, widgetLayout: $s.widgetLayout, ID_ApplicationType: parseInt($s.ActiveApplicationType) }
                            }).result;
                            break;
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
                $s.getMax = function (arr) {
                    var ret = {};
                    var max;
                    var h;
                    if (arr.length > 0) {
                        for (var i = 0; i < arr.length; i++) {
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

            }).fail(function () {
                console.warn('fail to get session');
            });
        }($s));
        $s.BuildWebsite = function () {
            console.log(123)
            $s.Request('pBuildWebsite', {}).then(function () {
                window.location.reload();
            })
        }
    }

    app.controller('MainComponent', ['$scope', '$controller', '$state', 'MenuDialog', '$rootScope', '$q', 'DataService', defaultCtrl]);

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
            for (var i = 0; i < arr.length; i++) {
                if (arr.length > 0) {
                    for (var i = 0; i < arr.length; i++) {
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
            for (var i = 0; i < arr.length; i++) {
                if (arr.length > 0) {
                    for (var i = 0; i < arr.length; i++) {
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

    app.controller('CreateChart', ['$scope', '$controller', '$uibModalInstance', 'dData', '$compile', function ($s, $c, $modal, $data, $cp) {
        $c('BaseController', { $scope: $s });
        $s.currentWidget = 3;
        $s.columns = [];
        $s.columns2 = [];
        $s.cData = {};
        $s.cData.selectedColumn = null;
        $s.cData.selectedColumn2 = null;
        $s.menutabField = [];
        var mt = [];
        $s.GetMenu($data.menu.ID).then(function (mm) {
            $s.menutabField = mm.tMenuTabField;
            $s.Request('FetchSchemaTable', { tName: $data.menu.TableName }).then(function (sc) {
                var schema = sc.SchemaTable.Schema;
                mt = Enumerable.From($s.menutabField).Join(schema, '$.Name', '$.ColumnName', function (x, y) {
                    x.DataType = y.DataType
                    return x
                }).ToArray();
                $s.columns = Enumerable.From(mt).Where(function (x) { return x.DataType == 'int' && x.Name.indexOf('ID_') > -1 }).ToArray();
                $s.columns2 = angular.copy($s.columns);
                $s.selectWidget(3);
            });
        })
        $s.setSeries = function () {
            if ($s.cData.selectedColumn == $s.cData.selectedColumn2) $s.cData.selectedColumn2 = null;
        }
        $s.widgetLayout = $data.widgetLayout;
        $s.Cancel = function () {
            $modal.dismiss();
        }
        $s.ID_MenuType = $data.menu.ID_MenuType
        $s.getMax = function (arr) {
            var ret = {};
            var max;
            var h;
            for (var i = 0; i < arr.length; i++) {
                if (arr.length > 0) {
                    for (var i = 0; i < arr.length; i++) {
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
        $s.selectWidget = function (widgetID) {
            $("#previewChart").empty();
            $s.cData.selectedColumn = null;
            $s.cData.selectedColumn2 = null;
            $s.currentWidget = widgetID;
            if ($s.currentWidget == 3 || $s.currentWidget == 5) {
                $s.cData.selectedColumn2 = null;
                $s.columns = mt;
                $s.columns2 = angular.copy($s.columns);
                if (!$('.nav-tabs a[href="#Group"]').parents('li').hasClass('active')) {
                    setTimeout(function () {
                        $('.nav-tabs a[href="#Group"]').click();
                        if (!$("#Group").hasClass("active")) {
                            $("#Group").addClass("active in");
                        }
                    }, 300);
                }
            } else {
                $s.columns = Enumerable.From(mt).Where(function (x) { return x.DataType == 'int' && x.Name.indexOf('ID_') > -1 }).ToArray();
                $s.columns2 = angular.copy($s.columns);
                if (!$('.nav-tabs a[href="#Group"]').parents('li').hasClass('active')) {
                    setTimeout(function () {
                        $('.nav-tabs a[href="#Group"]').click();
                        if (!$("#Group").hasClass("active")) {
                            $("#Group").addClass("active in");
                        }
                    }, 300);
                }
            }
        }
        $s.showPreviewChart = function () {
            $("#previewChart").empty();
            if ($s.cData.selectedColumn != null) {
                $s.Request('PreviewChartData', { widgetType: $s.currentWidget, column1: $s.cData.selectedColumn, column2: $s.cData.selectedColumn2, ds: $data.menu.DataSource }).then(function (ret) {
                    $s.chartData = ret.data;
                    angular.element($("#previewChart").append($cp("<div ros-chart widget-type='currentWidget' chart-data='chartData'></div>")($s)));
                })
            } else {
                $s.Toast('No column selected.', 'Preview Chart', 'warning');
            }
        }
        $s.saveChart = function () {
            $s.Request('CreateNewWidget', { x: 0, y: (maxData.y + maxData.h), name: $data.menu.Name, ID_Menu: $data.menu.ID, Type: $s.currentWidget, menu: $data.menu, column: $s.cData.selectedColumn, column2: $s.cData.selectedColumn2, ID_ApplicationType: parseInt($data.ID_ApplicationType), ID_MenuType: $s.ID_MenuType }).then(function (r) {
                if (r.error != undefined) {
                    $s.Toast(r.error, 'CreateWidget', 'warning');
                } else {
                    $s.Toast("Widget Created", 'CreateWidget', 'success');
                    r.selectedColumn = $s.cData.selectedColumn;
                    r.selectedColumn2 = $s.cData.selectedColumn2;
                    $modal.close(r);
                }
            })
        }
        $s.checkIfHasColumns = function (cc) {
            var q = Enumerable.From(cc).Where(function (x) { return x.Name.toLowerCase() != 'id' }).ToArray();
            if (q.length > 0) return true; else return false;
        }
    }]);

    var fullCalendar = function ($s, $c, ds, $modal) {
        $c("BaseController", { $scope: $s });
        $s.SetController("Action");
        //var dialogPath = (app.PublishID == 0 || app.PublishID == undefined || app.AllowDebugging ? 'Dialogs' : 'Build/' + app.PublishID + '/Dialogs');

        $s.renderData = function (start, end, id_emp) {
            var dsd = $.Deferred();

            $s.Request('GetCalendarSource2', { 'StartDate': start, 'EndDate': end, 'ID_Employee': id_emp }).then(function (data) {
                var eventData = [];
                var eventSchedData = [];
                var eventLogsData = [];
                var eventLeaveData = [];
                var eventOBData = [];
                var eventOTData = [];
                var eventCOSData = [];
                var eventUTData = [];
                Enumerable.From(data).ForEach(function (obj, idx) {
                    //sched
                    var ev = {};
                    if (Enumerable.From(eventSchedData).Where(function (x) { return x.Date == new Date(obj.Date) }).ToArray().length == 0) {
                        if (obj.ID_DayType == 2) {
                            ev.title = 'Rest Day'
                        } else if (obj.ID_DayType == 1) {
                            ev.title = obj.Sched
                        } else {
                            if (obj.Holiday != null) ev.title = obj.Holiday
                            else
                                ev.title = obj.Sched
                        }
                        ev.Date = new Date(obj.Date);
                        ev.start = new Date(obj.Date);
                        ev.allDay = true;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.Tooltip = "Schedule";
                        if (obj.ID_DayType != 1 && obj.ID_DayType != 2) {
                            ev.color = "#e600ac";
                            ev.Tooltip = obj.DayType;
                        }
                        eventSchedData.push(ev);

                        //Geo Location
                        if (obj.Location != null) {
                            var xLocations = obj.Location.split("<br/>");
                            for (var xL = 0; xL < (xLocations.length - 1); xL++) {
                                var xLTime = xLocations[xL].substr(0, 8);
                                var xLTime2 = xLocations[xL].split(")")[0];
                                ev = {};
                                ev.title = xLTime2.toString() + ") - Geo Location"; //xLocations[xL];
                                ev.Date = new Date(obj.Date);
                                ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + xLTime).format("hh:mm A"));
                                ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + xLTime).format("hh:mm A"));
                                ev.allDay = false;
                                ev.ID_DayType = obj.ID_DayType;
                                ev.DayType = obj.DayType;
                                ev.color = "gray";
                                var aa = xLocations[xL].split(")");
                                aa.splice(0, 1);
                                var bb = aa.join(")");
                                ev.Tooltip = bb;
                                eventSchedData.push(ev);
                            }
                        }

                        //attendance
                        var currDate = new Date();
                        if (new Date(moment(currDate).format("YYYY-MM-DD")) >= new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD"))) {
                            ev = {};
                            ev.start = obj.IN;
                            ev.end = obj.OUT;
                            ev.allDay = false;
                            ev.ID_DayType = obj.ID_DayType;
                            ev.DayType = obj.DayType;
                            ev.Tooltip = "Attendance Date: " + moment(new Date(obj.Date)).format("MMM D, YYYY");
                            if (obj.IN != null && obj.OUT != null) {
                                if (obj.IN == obj.OUT) {
                                    ev.title = moment((obj.IN != null ? obj.IN : obj.OUT)).format("hh:mm A") + " (Missing Log)";
                                    ev.color = "#b10a0a";
                                } else {
                                    ev.title = moment(obj.IN).format("hh:mm A") + " - " + moment(obj.OUT).format("hh:mm A");
                                    ev.color = "#ef7e1c";                                         
                                }
                            } else {
                                var s = obj.Sched.split("-");
                                var s2 = parseInt(s[1].replace(" PM ", "").replace(":00", ""));
                                if (s2 >= 1 && s2 <= 11) {
                                    s2 = s2 + 12;
                                } else {
                                    s2 = s2;
                                }
                                ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + "T" + s[0].replace(" AM ", ""));
                                ev.allDay = true;
                                ev.title = "No Logs";
                                ev.color = "#b10a0a";
                            }
                            if (obj.ID_DayType != 1 && ev.title != "No Logs") {
                                eventLogsData.push(ev);
                            } else if (obj.ID_DayType == 1) {
                                eventLogsData.push(ev);
                            }
                        }
                    }
                    //Leave
                    if (obj.Leave != null && Enumerable.From(eventLeaveData).Where(function (x) { return x.ID == obj.Leave_ID }).ToArray().length == 0) {
                        ev = {};
                        ev.ID = obj.Leave_ID;
                        ev.Date = new Date(obj.Date)
                        ev.title = obj.Leave;
                        if (obj.Leave.toString().toLowerCase().replace(/ /g, "").indexOf("firsthalf") > -1) {
                            ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.FirstTimeIn).format("hh:mm:ss A"));
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.FirstHalfTimeOut).format("hh:mm:ss A"));
                        } else if (obj.Leave.toString().toLowerCase().replace(/ /g, "").indexOf("secondhalf") > -1) {
                            ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.SecondHalfTimeIn).format("hh:mm:ss A"));
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.SecondTimeOut).format("hh:mm:ss A"));
                        } else {
                            ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.FirstTimeIn).format("hh:mm:ss A"));
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.SecondTimeOut).format("hh:mm:ss A"));
                        }
                        ev.allDay = false;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.color = "#B10DC9"
                        ev.Tooltip = "Leave";
                        eventLeaveData.push(ev);
                    }
                    //OB
                    if (obj.OB != null && Enumerable.From(eventOBData).Where(function (x) { return x.ID == obj.OB_ID }).ToArray().length == 0) {
                        ev = {};
                        ev.ID = obj.OB_ID;
                        ev.Date = new Date(obj.Date)
                        ev.title = obj.OB;
                        var ob = obj.OB.split(" - ");
                        var obStart = ob[0], obEnd = ob[1];
                        ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obStart).format("hh:mm:ss A"));
                        if (obEnd.indexOf("AM") > -1) {
                            var z = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obEnd).format("H"))
                            var z2 = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obStart).format("H"))
                            if (z < z2) {
                                ev.end = new Date(moment(new Date(obj.Date)).add(1, "days").format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obEnd).format("hh:mm:ss A"));
                            } else {
                                ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obEnd).format("hh:mm:ss A"));
                            }
                        } else {
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + obEnd).format("hh:mm:ss A"));
                        }
                        ev.allDay = false;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.color = "#b37700";
                        ev.Tooltip = "Official Business";
                        eventOBData.push(ev);
                    }
                    //OT
                    if (obj.OT != null && Enumerable.From(eventOTData).Where(function (x) { return x.ID == obj.OT_ID }).ToArray().length == 0) {
                        ev = {};
                        ev.ID = obj.OT_ID;
                        ev.Date = new Date(obj.Date);
                        ev.title = obj.OT;
                        var ot = obj.OT.split(" - ");
                        var otStart = ot[0], otEnd = ot[1];
                        ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otStart).format("hh:mm:ss A"));
                        if (otEnd.indexOf("AM") > -1) {
                            var z = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otEnd).format("H"))
                            var z2 = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otStart).format("H"))
                            if (z < z2) {
                                ev.end = new Date(moment(new Date(obj.Date)).add(1, "days").format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otEnd).format("hh:mm:ss A"));
                            } else {
                                ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otEnd).format("hh:mm:ss A"));
                            }
                        } else {
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + otEnd).format("hh:mm:ss A"));
                        }
                        ev.allDay = false;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.color = "#3333cc";
                        ev.Tooltip = "Overtime";
                        eventOTData.push(ev);
                    }
                    //UT
                    if (obj.UT != null && Enumerable.From(eventUTData).Where(function (x) { return x.ID == obj.UT_ID }).ToArray().length == 0) {
                        ev = {};
                        ev.ID = obj.UT_ID;
                        ev.Date = new Date(obj.Date)
                        ev.title = obj.UT;
                        var ut = obj.UT.split(" - ");
                        var utStart = ut[0], utEnd = ut[1];
                        ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utStart).format("hh:mm:ss A"));
                        if (utEnd.indexOf("AM") > -1) {
                            var z = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utEnd).format("H"))
                            var z2 = parseInt(moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utStart).format("H"))
                            if (z < z2) {
                                ev.end = new Date(moment(new Date(obj.Date)).add(1, "days").format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utEnd).format("hh:mm:ss A"));
                            } else {
                                ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utEnd).format("hh:mm:ss A"));
                            }
                        } else {
                            ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + utEnd).format("hh:mm:ss A"));
                        }
                        ev.allDay = false;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.color = "#86592d";
                        ev.Tooltip = "Undertime";
                        eventUTData.push(ev);
                    }
                    //COS
                    if (obj.COS != null && Enumerable.From(eventCOSData).Where(function (x) { return x.ID == obj.COS_ID }).ToArray().length == 0) {
                        ev = {};
                        ev.ID = obj.COS_ID;
                        ev.Date = new Date(obj.Date);
                        ev.title = obj.COS;
                        ev.start = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.COSTimeIn).format("hh:mm:ss A"));
                        ev.end = new Date(moment(new Date(obj.Date)).format("YYYY-MM-DD") + " " + moment(obj.COSTimeOut).format("hh:mm:ss A"));
                        ev.allDay = false;
                        ev.ID_DayType = obj.ID_DayType;
                        ev.DayType = obj.DayType;
                        ev.color = "#5c5c8a";
                        ev.Tooltip = "Change of Schedule";
                        eventCOSData.push(ev);
                    }
                });
                dsd.resolve(eventSchedData.concat(eventLogsData).concat(eventLeaveData).concat(eventOBData).concat(eventOTData).concat(eventCOSData).concat(eventUTData));
            });
            return dsd.promise();
        }

        $s.getCurrentYear = new Date().getFullYear();
        $s.getCurrentMonth = new Date().getMonth() + 1;
        $s.getCurrentEmployee = $s.Session('ID_Employee');
        $s.months = [
            { ID: 1, Name: "January" },
            { ID: 2, Name: "February" },
            { ID: 3, Name: "March" },
            { ID: 4, Name: "April" },
            { ID: 5, Name: "May" },
            { ID: 6, Name: "June" },
            { ID: 7, Name: "July" },
            { ID: 8, Name: "August" },
            { ID: 9, Name: "September" },
            { ID: 10, Name: "October" },
            { ID: 11, Name: "November" },
            { ID: 12, Name: "December" }
        ];
        $s.years = [];
        for (var i = 0; i < 41; i++) {
            $s.years.push({ ID: ($s.getCurrentYear - 20) + i, Name: String(($s.getCurrentYear - 20) + i) });
        }

        

        $s.updateCalendar = function (v1, v2, v3) {

            var fcDate = new Date(moment([v2, v1 - 1]).toDate());
            $s.getCurrentEmployee = v3;
            $("#fullCalendar").fullCalendar('gotoDate', fcDate);
        }

        $s.refetchCalendar = function (v1, v2, v3) {
            $s.getCurrentEmployee = v3;
            $("#fullCalendar").fullCalendar('refetchEvents');
        }

        $s.EmployeeSource = [];

        $s.Request('GetApproverEmployee', { 'eID': $s.Session('ID_Employee') }).then(function (result) {
            $s.EmployeeSource = result;
        });
        setTimeout(function () {
            $("#fullCalendar").fullCalendar({
                height: 700,
                header: {
                    left: '',
                    center: '',
                    right: 'today prev,next month,agendaWeek'
                },
				nextDayThreshold:'00:00:00',
                eventRender: function (event, element) {
                    $(element).tooltip({ title: event.Tooltip });
                    $(element).html(event.title);
                },
                events: function (start, end, timezone, callback) {
                    $s.renderData(moment(start._d).toDate(), moment(end._d).toDate(), $s.getCurrentEmployee).then(function (cdata) {
                        callback(cdata);
                    });
                },
                eventLimit: true,
                views: {
                    month: {
                        eventLimit: 4
                    }
                }
            });

            $('.fc-prev-button').click(function () {
                var date = $("#fullCalendar").fullCalendar('getDate');
                var month_int = date._d.getMonth();
                var year_int = date._d.getFullYear();
                $s.getCurrentMonth = month_int + 1;
                $s.getCurrentYear = year_int;
                $("#cmonth").val($s.getCurrentMonth - 1);
                var cy = Enumerable.From($s.years).Select(function (x) { return x.ID }).IndexOf(year_int);
                $("#cyear").val(cy);
            });

            $('.fc-next-button').click(function () {
                var date = $("#fullCalendar").fullCalendar('getDate');
                var month_int = date._d.getMonth();
                var year_int = date._d.getFullYear();
                $s.getCurrentMonth = month_int + 1;
                $s.getCurrentYear = year_int;
                $("#cmonth").val($s.getCurrentMonth - 1);
                var cy = Enumerable.From($s.years).Select(function (x) { return x.ID }).IndexOf(year_int);
                $("#cyear").val(cy);
            });
        }, 500);
        
        //$s.showLegend = function () {
        //    var legend = dlg.create(dialogPath + '/CalendarLegend.html', '', {}, { size: 'md', keyboard: true, backdrop: true, windowClass: 'my-class' });
        //}
        $s.closeCalendar = function () {
            $modal.close();
        }
    }
    app.controller('fullCalendar', ['$scope', '$controller', 'DataService', '$uibModalInstance', fullCalendar]);

    var tmp = function ($s, $c,$modal) {
        $c('BaseController', { $scope: $s });
        $s.SetController('Action');

        $s.Close = function () {
            if ($s.GUID) {
                $s.Request('DeleteFile', { FileName: $s.GUID }).then(function () {
                    $modal.close();
                });
            } else {
                $modal.close();
            }   
        }
        $s.qry = '';
        $s.data = [];
        $s.AffectedRows = 0;
        $s.IsSelect = true;
        $s.FileName = '';
        $s.GUID = '';
        $s.Database = '';
        $s.Init = function () {
            $s.Request('GetDatabase', {},'Action').then(function (ret) {
                $s.Database = ret.Name;
            });
        }
        $s.ExecScript = function () {
            console.log($s.Database)
            $s.Confirm('Confirm to execute \'' + $s.FileName + '\' to ' + $s.Database, 'Run').then(function () {
                $s.Request('tmp', { FileName: $s.GUID }).then(function (ret) {
                    if (ret.IsComplete) {
                        $s.Toast('Script has been executed.','Success','success');
                    }
                });
                $s.FileName = '';
                $s.GUID = '';
                $s.$apply();
            });
        }
        $s.SelectScript = function () {
            $s.UploadFile('Uploadtmp', null, false).then(function (ret) {
                $s.FileName = ret.OrigFileName;
                $s.GUID = ret.GUID;
                $s.$apply();
            })
        }
        $s.Execute = function () {
            if ($s.qry) {
                $s.Request('Qtmp', { cmd: $s.qry }).then(function (ret) {
                    if (ret) {
                        $s.data = ret;
                    } else {
                        $s.Toast('Query has been successfully executed','Success','success');
                    }
                    
                });
            } else {
                $s.Toast('No query detected','Warning','warning');
            }
            
        }
    }
    app.controller('tmp', ['$scope', '$controller', '$uibModalInstance', tmp]);
});