'use strict';
define(['app'], function (app) {

    var BaseController = function ($s, $r, $sce, $ss, $t, $uibModal) {
        $s.currentVersion = parseFloat($ss.Session('currentVersion'));
        $s.Controller = $r.Controller;
        $s.CurrentController = null;
        $s.widgetLayout = [];

        $s.SetController = (function (value) {
            $s.CurrentController = value;
            $r.SetController(value);
        })

        $s.SetEncryption = (function (token, salt) {
            $r.SetEncryption(token, salt);
        });

        $s.UserRow = function (data) {
            $ss.UserRow(data);
           
        }

        //$s.Implant = function () {

        //    var insys = document.querySelector("insys");
        //    var auth = insys.innerHTML.trim();
        //    var dot = auth.indexOf('.');
        //    var k = auth.substr(0, dot);
        //    var s = auth.substr(dot + 1);

        //    $s.SetEncryption(k, s);

        //    insys.parentNode.removeChild(insys);
        //}

        $s.Dialog = (function (opt) {

            return $uibModal.open({
                animation: true,
                templateUrl: opt.templateUrl || ('/Web/Template/' + opt.template),
                controller: opt.controller,
                size: opt.size || 'lg',
                appendTo: angular.element(document.body),
                backdrop: 'static',
                keyboard: false,
                windowClass: 'custom-info-dialog ' + opt.windowClass,
                resolve: opt.resolve || { dData: opt.data || null }
            });

            /*
                close(result) (Type: function) - Can be used to close a modal, passing a result. 
                dismiss(reason) (Type: function) - Can be used to dismiss a modal, passing a reason. 
                result (Type: promise) - Is resolved when a modal is closed and rejected when a modal is dismissed. 
                opened (Type: promise) - Is resolved when a modal gets opened after downloading content's template and resolving all variables. 
                closed (Type: promise) - Is resolved when a modal is closed and the animation completes. 
                rendered (Type: promise) - Is resolved when a modal is rendered.
            */
        })

        $s.Confirm = (function (message, title) {
            var def = new $.Deferred();
            var dd = $s.Dialog({
                controller: ['$scope', 'dData', '$uibModalInstance', function ($s, $data, $d) {

                    $s.Message = $data.message;
                    $s.Title = $data.title;

                    $s.Cancel = (function () {
                        $d.close(1);
                    })

                    $s.OK = (function () {
                        $d.close(0);
                    })
                }],
                template: 'Confirm.tmpl.html',
                size: 'sm',
                data: { message: message, title: title || document.title }
            })
            dd.result.then(function (x) {
                if (x == 0)
                    def.resolve(x);
                else {
                    def.reject();
                }
            })

            return def.promise();
        })

        $s.Alert = (function (message, title) {
            //$d.show($d.alert({ skipHide: true })
            //        .parent(angular.element(document.body))
            //        .clickOutsideToClose(true)
            //        .title(title || '')
            //        .textContent(message)
            //        .ariaLabel('Alert Dialog')
            //        .ok('OK')
            //        .targetEvent(window.event)
            //    ); 
        })

        $s.Toast = (function (msg, title, opt) {
            //warning, error, info
            switch (opt) {
                case 'info':
                    $t.info(msg, title || document.title);
                    break;
                case 'warning':
                    $t.warning(msg, title || document.title);
                    break;
                case 'error':
                    $t.error(msg, title || document.title);
                    break;
                default:
                    $t.success(msg, document.title);
                    break;
            }
        })

        $s.Request = function (Name, Param, disableInterceptor) {
            return $r.Post(Name, Param, $s.Controller, disableInterceptor).fail(function (e) {
                var msg = e.toString().substr(0, 40);
                if (msg.length < e.toString().length) msg = msg + '...';

                $t.error(e.toString(), document.title);

                return e;
            });
        }

        $s.ByteLength = (function (obj) {
            if (typeof obj === 'object')
                obj = JSON.stringify(obj);
            return $r.ByteLength(obj);
        })

        $s.Download = (function (Name, Param) {
            return $r.Download(Name, Param, $s.Controller).fail(function (e) {
                var msg = e.toString().substr(0, 40);
                if (msg.length < e.toString().length) msg = msg + '...';

                $t.error(e.toString(), document.title);
            });
        })

        $s.DownloadSlim = (function (mFile, Container) {
            return $r.DownloadSlim(LZString.compressToEncodedURIComponent(mFile), Container).catch(function (data, e) {
                var msg = e.toString().substr(0, 40);
                if (msg.length < e.toString().length) msg = msg + '...';

                $t.error(e.toString(), document.title);
            });;
        })

        $s.UrlApi = (function (Name) {
            return $r.UrlApi(Name);
        });

        $s.ActionUrl = function (action, controller, param) {
            var url;
            if (controller.toLowerCase().indexOf("controller") >= 0) {
                url = '/' + controller.replace('Controller', '') + '/' + action;
            } else {
                url = '/' + controller + '/' + action;
            }
            if (vcl.String.StartsWith(url, '/http'))
                return url.substr(1);

            if (param) {
                var h = [];
                var jh = Object.keys(param);
                for (var kk in jh)
                    h.push(jh[kk] + '=' + param[jh[kk]]);
                url = url + '?' + h.join('&');
            }

            window.location = url;
        };

        $s.IsNull = (function (inp, oup) {
            return inp == null || typeof inp == typeof undefined ? oup : inp;
        });

        $s.Goto = (function (path) {
            window.location = '#!' + path; // add excalamation on angular 1.6
        })

        $s.UploadFile = function (Name, Param, IsMultiple, Accept) {
            try {
                var def = $.Deferred();
                var accp = "";

                if (Accept)
                    accp = ' accept="' + Accept + '" ';

                var fle = $('<input type="file" ' + (IsMultiple ? 'multiple' : '') + accp + ' />');

                fle.change(function (d) {
                    try {

                        if (fle[0].files.length == 0) {
                            def.reject();
                            return;
                        }

                        if (Accept) {
                            var b = false;
                            var acc = Accept.split(',');
                            for (var c = 0; c < acc.length; c++) {
                                var kgb = acc[c].trim().indexOf('*') !== -1;
                                if (kgb) {
                                    var gg = acc[c].trim().split('/');
                                    if (fle[0].files[0].type.toLowerCase().startsWith(gg[0].toLowerCase())) {
                                        b = true;
                                        break;
                                    }
                                } else
                                    if (acc[c].trim() === fle[0].files[0].type) {
                                        b = true;
                                        break;
                                    }
                            }

                            if (!b) {
                                def.reject("Invalid File");
                                return;
                            }
                        }

                        var fd = new FormData();
                        if (Param) {
                            var ky = Object.keys(Param);
                            for (var j in ky)
                                fd.append(ky[j], Param[ky[j]]);
                        }

                        for (var fi = 0; fi < fle[0].files.length; fi++) {
                            fd.append("fileToUpload_" + fi.toString(), fle[0].files[fi]);
                        }

                        $r.Upload(Name, fd).then(function (d, s) {
                            def.resolve(d, s);
                        }).fail(function (e) {
                            var msg = e.toString().substr(0, 40);
                            if (msg.length < e.toString().length) msg = msg + '...';

                            $t.error(e.toString(), document.title);
                            def.reject();
                        });
                    } catch (ex) {
                        console.error(ex);
                    }
                });

                fle.trigger('click');
                return def.promise();
            } catch (ex) {
                console.error(ex);
            }
        }
       

        $s.UploadFileSlim = (function (Container, Param) {
            var p = Param || {};

            p.Container = Container;

            if (!p.UseOriginalName) p.UseOriginalName = false;

            return $s.UploadFile('UploadFile', p);
        })

        $s.Task = (function (action, timeout) {
            var def = $.Deferred();
            if (action)
                action(def);
            else
                setTimeout(function () { def.resolve(); }, timeout || 100);

            return def.promise();
        });

        $s.WaitForElement = (function (query) {
            var def = $.Deferred();
            var retry = 0;
            var act = function () {
                var j = setTimeout(function () {
                    var k = $(query);
                    clearTimeout(j);
                    if (k.length > 0)
                        def.resolve(k);
                    else {
                        retry++;

                        if (retry < 10)
                            act();
                        else
                            def.reject('Could not find Element')
                    }
                }, 100);
            }

            act();

            return def.promise();
        })

        $s.GetFileNameWithOutExtension = (function (file) {
            return vcl.Path.GetFileName(file);
        })

        $s.TrustSrc = function (src) {
            return $sce.trustAsResourceUrl(src);
        }

        $s.ModelToDate = function (value) {
            if (!value) return null;
            if (value && !(value instanceof Date)) {
                if (typeof value === "string")
                    value = new Date(value);
                if (Object.prototype.toString.call(value) !== "[object Date]")
                    throw Error('The ng-model for md-datepicker must be a Date instance. ' +
                        'Currently the model is a: ' + (typeof value));
            }

            return value;
        }

        $s.StringFormat = function (text) {
            var args = Array.prototype.slice.call(arguments, 1);
            return text.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                    ? args[number]
                    : match;
            });
        };

        $s.SQLFormat = function (obj, suffix) {
            var s = obj;
            if (obj === null || typeof obj === typeof undefined) {
                s = "NULL";
            }
            else {
                if (obj === true || obj === false || obj === 'true' || obj === 'false') s = (obj === true || obj === 'true') ? 1 : 0;
                else if (isNaN(obj)) s = "'" + obj + ((suffix) ? '%' : '') + "'";
            }
            return s;
        }

        $s.DataBank = function (Name, Value) {
            if (Value)
                $ss.DataBank(Name, Value);
            else
                return $ss.DataBank(Name);
        }

        $s.Title = function (Name) {
            document.title = Name + ' - ' + $s.Session('Company'); //$r.AppName;
        }

        $s.Session = (function (Name) {
            return $ss.Session(Name);
        })

        /**
            fixcolumn = ['@.Column']  
        */
        $s.PassParameter = (function (cmd, row, passkey, fixColumn) {
            try {
                if (cmd === undefined || cmd === null) return;

                var rfmrt = function (data) {
                    return (data == 'NULL') ? data.toLowerCase() : data;
                }
                var urow = row || $ss.UserRow();
                passkey = passkey || '@';
                var keys = Object.keys(urow);

                if (fixColumn) {
                    for (var i = 0; i < fixColumn.length; i++) {
                        var gg = fixColumn[i].split('.');
                        cmd = cmd.replace(new RegExp('\\' + gg[0] + '\\b' + gg[1] + '\\b', 'g'), rfmrt($s.SQLFormat(urow[gg[1]])));
                    }
                }

                for (var i = 0; i < keys.length; i++) {
                    if (passkey instanceof Array) {
                        passkey.forEach(function (k) {
                            cmd = cmd.replace(new RegExp('\\' + k + '\\b' + keys[i] + '\\b', 'g'), rfmrt($s.SQLFormat(urow[keys[i]])));
                        });
                    } else
                        cmd = cmd.replace(new RegExp('\\' + passkey + '\\b' + keys[i] + '\\b', 'g'), rfmrt($s.SQLFormat(urow[keys[i]])));
                }
                return cmd;
            } catch (ex) {
                console.error(ex.message, cmd);
            }
        })

        $s.hasValue = function (data) {
            if (!data) return false;
            if (typeof data == "string") if (data.trim() === "") return false;
            return true;
        }

        $s.SetFromSystemQueryParameter = function (data) {
            if (!$s.hasValue(data)) return data;

            var systemParam = vcl.Options.SystemParameters;

            for (var i = 0; i < systemParam.length; i++) {
                var row = systemParam[i];
                var key = '@' + row.Key;
                if (row.Value) {
                    var value = row.Value.toString();
                    data = data.replace(key, value);
                }
            }

            return data;
        }

        $s.FormatColumn = function (colname, row) {
            var r = row[colname];
            if (typeof r == 'string' || r instanceof String) {
                if (colname.toLowerCase().indexOf('datetime') !== -1 && vcl.DateTime.IsNewtonFormat(r))
                    r = vcl.DateTime.Format(r, 'mmm dd, yyyy' + ' - ' + 'hh:MM TT');
                else if (colname.toLowerCase().indexOf('date') !== -1 && vcl.DateTime.IsNewtonFormat(r))
                    r = vcl.DateTime.Format(r, 'mmm dd, yyyy');
                else if (vcl.DateTime.IsNewtonFormat(r))
                    r = vcl.DateTime.Format(r, 'hh:MM TT');

            } else if (r && !isNaN(r) && colname.toLowerCase() !== 'id' && colname.toLowerCase() !== 'year') {
                r = $s.FormatNumber(r); // parseFloat(r).toFixed(2).toLocaleString();
            }
            return r;
        }

        $s.FormatNumber = function (x, decimal) {
            var parts = parseFloat(x).toFixed(decimal || 2).toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }

        $s.Compress = (function (obj) {
            return LZString.compressToEncodedURIComponent(JSON.stringify(obj));
        })

        $s.Decompress = (function (obj) {
            return JSON.parse(LZString.decompressFromEncodedURIComponent(obj));
        })

        $s.GetMenu = function (ID) {
            return $r.GetMenu(ID);
        }

        $s.convertDataURIToBinary = function (dataURI) {
            var BASE64_MARKER = ';base64,';
            var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
            var base64 = dataURI.substring(base64Index);
            var raw = window.atob(base64);
            var rawLength = raw.length;
            var array = new Uint8Array(new ArrayBuffer(rawLength));

            for (var i = 0; i < rawLength; i++) {
                array[i] = raw.charCodeAt(i);
            }
            return array;
        }

        $s.DefaultColumns = (function () {
            return [
                { 'ID': 0, 'Name': 'ID', 'EffectiveLabel': 'Reference ID' },
                { 'ID': 0, 'Name': 'Name', 'EffectiveLabel': 'Name' },
            ]
        })

        $s.ShowOverlay = (function () {
            var ov = $('#window-screen');
            $('.bg-screen', ov).css({ opacity: 0.6 });
            ov.fadeIn();
        });

        $s.HideOverlay = (function () {
            var ov = $('#window-screen');
            $('#LoadingMessage', ov).html('');
            ov.fadeOut();
        })

        $s.OverlayMessage = (function (Message) {
            var ov = $('#window-screen');
            $('#LoadingMessage', ov).html(Message);
        })

        $s.LongDate = function (d) {
            var dateTime = vcl.DateTime;
            return (d != null) ? dateTime.Format(d, dateTime.masks.inSysDateTime) : null;
        }

        $s.LoadCombo = function (DataSource, Name, FixedFilter,Sort) {
            return $s.Request('LoadCombo', { DataSource: DataSource, filter: FixedFilter, Name: Name, Sort: Sort });
        }

        $s.SessionReady = (function () {
            var def = $.Deferred();
            var retry = 0;
            var act = function () {
                var j = setTimeout(function () {

                    clearTimeout(j);
                    if ($ss.Length() > 0)
                        def.resolve($ss);
                    else {
                        retry++;
                        if (retry < 100)
                            act();
                        else
                            def.reject('Waiting for session exceeded');
                    }
                }, 100);
            }

            act();

            return def.promise();
        })
    }

    app.controller('BaseController', ['$scope', 'DataService', '$sce', 'Session', 'toastr', '$uibModal', BaseController]);

    var MenuController = function ($s, $ds, $Session, $state) {
        $s.Menus = [];
        $s.MenuBare = [];
        $s.UserFav = null;
        $s.completeMenu = [];
        $s.copyMenu = [];
        $s.ID_ApplicationType = $s.Session('ID_ApplicationType');
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

        $s.Prepare = (function () {
            //var tble = $ds.GetMenu('tMenu');
            var tble = $ds.Post('LoadUserMenu', { ID_User: $s.Session('ID_User') });

            if (parseInt($s.Session("ID_User")) > 2) {
                var ids = Enumerable.From(JSON.parse(LZString.decompressFromUTF16($s.Session('UserMenuIDList')))).Select(function (x) { return { ID: x.Item1, IsVisible: x.Item2 }; }).ToArray();

                return tble.then(function (d) {
                    $s.MenuBare = $s.Menus = Enumerable.From(d.Menu)
                        .Join(ids, '$.ID', '$.ID', function (a, b) {
                            a.IsVisible = b.IsVisible;
                            return a;
                        })
                        .Where(function (r) {
                            return r.IsVisible && r.ID_ApplicationType == $s.ActiveApplicationType;
                        })
                        .ToArray();
                    $s.completeMenu = angular.copy($s.MenuBare);
                    $s.copyMenu = angular.copy($s.completeMenu);
                    $s.toRemove = [];
                    angular.forEach($s.completeMenu, function (cm, idx) {
                        var cnt = Enumerable.From($s.completeMenu).Where(function (x) { return x.ID_Menu == cm.ID && cm.ID_MenuType != 6 }).ToArray().length;
                        if (cnt > 0) {
                            var idx2 = $s.copyMenu.indexOf(cm);
                            vcl.Array.Remove($s.copyMenu, function (a) {
                                return a.ID == cm.ID
                            });
                        }
                    });
                });
            } else
                return tble.then(function (d) {

                    $s.UserFav = d.UserFav;
                    $s.MenuBare = $s.Menus = Enumerable.From(d.Menu)
                        .Where(function (r) { return r.IsVisible && r.ID_ApplicationType == $s.ActiveApplicationType })
                        .Select(function (r) {
                            r.HasFav = Enumerable.From(d.UserFav).Where(function (kx) { return kx.ID_Menu === r.ID }).Any();
                            return r;
                        })
                        .ToArray();
                    $s.completeMenu = angular.copy($s.MenuBare);
                    $s.copyMenu = angular.copy($s.completeMenu);
                    $s.toRemove = [];
                    angular.forEach($s.completeMenu, function (cm, idx) {
                        var cnt = Enumerable.From($s.completeMenu).Where(function (x) { return x.ID_Menu == cm.ID && cm.ID_MenuType != 6 }).ToArray().length;
                        if (cnt > 0) {
                            var idx2 = $s.copyMenu.indexOf(cm);
                            vcl.Array.Remove($s.copyMenu, function (a) {
                                return a.ID == cm.ID
                            });
                        }
                    });

                });
        });

        $s.LoadMenu = (function () {

            $s.Distinct = function (menu) {
                var tmp = [], id = [];
                Enumerable.From(menu).ForEach(function (x) {
                    if (id.indexOf(x.ID) < 0) { id.push(x.ID); tmp.push(x); }
                });
                return menu;
            }

            $s.MenuTree = function (menu) {
                var tree = [];

                function getChild(i, a) {
                    var toFill = [];
                    Enumerable.From(a).Where(function (r) { return r.ID_Menu === parseInt(i) }).OrderBy(function (x) { return x.SeqNo }).ForEach(function (x) {
                        toFill.push({ Parent: x, Children: (x.ID_MenuType == 6 ? [] : getChild(x.ID, a)) });
                    });
                    return toFill;
                }

                Enumerable.From(menu).OrderBy(function (x) { return x.SeqNo }).ForEach(function (x) {
                    if (x.ID_Menu === null) {
                        tree.push({ Parent: x, Children: (x.ID_MenuType == 6 ? [] : getChild(x.ID, menu)) });
                    }
                });
                return tree;
            }

            $s.Prepare().then(function () {
                $s.Distinct = function (menu) {
                    var tmp = [], id = [];
                    Enumerable.From(menu).ForEach(function (x) {
                        if (id.indexOf(x.ID) < 0) { id.push(x.ID); tmp.push(x); }
                    });
                    return menu;
                }
                //$s.MenuTree = function (menu) {
                //    var tree = [];

                //    function getChild(i, a) {
                //        var toFill = [];
                //        Enumerable.From(a).Where(function (r) { return r.ID_Menu === parseInt(i) }).OrderBy(function (x) { return x.SeqNo }).ForEach(function (x) {
                //            toFill.push({ Parent: x, Children: getChild(x.ID, a) });
                //        });
                //        return toFill;
                //    }

                //    Enumerable.From(menu).OrderBy(function (x) { return x.SeqNo }).ForEach(function (x) {
                //        if (x.ID_Menu === null) {
                //            tree.push({ Parent: x, Children: getChild(x.ID, menu) });
                //        }
                //    });
                //    return tree;
                //}

                //Rebuild Parenting for Single Parents
                $s.RebuildParentMenu = (function (ID) {
                    var j = Enumerable.From($s.Menus).Where(function (xs) { return xs.ID_Menu === ID });
                    if (j.Count() == 1) {
                        var nID = j.SingleOrDefault().ID;
                        $s.RebuildParentMenu(nID);
                        vcl.Array.Remove($s.Menus, function (xys) { return xys.ID === nID });
                    } else {
                        j.ForEach(function (x) {
                            x.ID_Menu = null; //make a child as root menu
                        })
                    }
                })

                var j = Enumerable.From($s.Menus).Where('$.ID_Menu === null');
                if (j.Count() == 1) {
                    var nID = j.SingleOrDefault().ID;
                    $s.RebuildParentMenu(nID);
                    vcl.Array.Remove($s.Menus, function (xys) { return xys.ID === nID });
                }
                //->

                $s.Menus = $s.Distinct($s.Menus);
                $s.Menus = $s.MenuTree($s.Menus);

                $s.ReCreateParent = function (menus) {
                    if (menus.length == 1) {
                        menus = menus;
                    } else {
                        if (menus.length != 0) {
                            angular.forEach(menus, function (m) {
                                if (m.Children.length == 1) {
                                    m = m.Children[0];
                                } else {
                                    m = $s.ReCreateChild(m);
                                }
                            });
                        }
                    }
                    return menus;
                }
                $s.ReCreateChild = function (ds) {
                    if (ds.length == 1) {
                        ds = ds[0].Children;
                    } else {
                        if (ds.length != 0) {
                            angular.forEach(ds.Children, function (m) {
                                if (m.Children.length == 1) {
                                    m = m.Children[0];
                                } else {
                                    m = $s.ReCreateChild(m);
                                }
                            });
                        }
                    }
                    return ds;
                }

                $s.Menus = $s.ReCreateParent($s.Menus);

            });
        });
        $s.AppType = $s.Session('ID_ApplicationType') == null ? true : false;
        $s.ChangeDashboard = function () {
            if ($s.AppType != undefined) {
                if ($s.AppType) {
                    $s.ActiveApplicationType = 1;
                    window.sessionStorage.ActiveApplicationType = 1;
                    window.location = '#!/Dashboardv3';
                    window.location.reload();
                } else {
                    $s.ActiveApplicationType = 2;
                    window.sessionStorage.ActiveApplicationType = 2;
                    window.location = '#!/IONSDashboard';
                    window.location.reload();
                    
                }
            }
        }

        $s.IONSHome = (function () {
            //if ($state.current.controller == "IONSDashboard")
            //    return false
            //else
            //    return true
            if (window.sessionStorage.ActiveApplicationType == 2)
                return false
            else
                return true
        })

        $s.UpdateMenuItem = (function (ID, Column, Value) {

            var MenuChildren = (function (mnu, ID) {
                for (var i = 0; i < mnu.Children.length; i++) {
                    if (mnu.Children[i].Parent.ID === ID) {
                        mnu.Children[i].Parent[Column] = Value;
                        return;
                    }
                    MenuChildren(mnu.Children[i], ID);
                }
            })

            for (var i = 0; i < $s.Menus.length; i++) {
                if ($s.Menus[i].Parent.ID === ID) {
                    $s.Menus[i].Parent[Column] = Value;
                    return;
                }
                MenuChildren($s.Menus[i], ID);
            }
        })

        $s.GetMenuItem = (function (ID) {
            var mItem = (function (mnu, ID) {
                for (var i = 0; i < mnu.Children.length; i++) {
                    if (mnu.Children[i].Parent.ID === ID)
                        return mnu.Children[i].Parent;
                    var j = mItem(mnu.Children[i], ID);
                    if (j)
                        return j;
                }
            })

            for (var i = 0; i < $s.Menus.length; i++) {
                if ($s.Menus[i].Parent.ID === ID)
                    return $s.Menus[i].Parent;
                var j = mItem($s.Menus[i], ID);
                if (j)
                    return j;
            }
        })

    }

    app.controller('MenuController', ['$scope', 'DataService', 'Session', '$state', MenuController]);

    var RightPanelController = (function ($s, $compile, $inv, $c) {
        $c('BaseController', { $scope: $s });

        $s.showbox = false;
        $s.RPanelTitle = "Filter";

        $s.TogglePanel = (function () {
            $s.showbox = !$s.showbox;

            //$s.$apply();

            $s.g.invoke('RPanelClose', $('.right-box').hasClass('show-box'));
        })

        $s.PanelTitle = (function (title) {
            $s.RPanelTitle = title;
        })

        $s.Inject = (function (h, $scaller) {
            $('.right-box-body').empty();
            angular.element($('.right-box-body').append($compile(h)($scaller)));
        });

        $s.SessionReady().then(function (ss) {
            $s.g = $inv.group($s.Session('GUID'));
            $s.g.on('RTitle', $s.PanelTitle);
            $s.g.on('RTogglePanel', function () {
                $s.TogglePanel();
            });
            $s.g.on('RInject', $s.Inject);
        });
    })

    app.controller('RightPanelController', ['$scope', '$compile', '$Invoker', '$controller', RightPanelController]);

    var PrintDialogController = (function ($s, $c, $d) {
        $c('BaseController', { $scope: $s });

        $s.DocType = 1; //PDF by default
        $s.PrButtons = [
            { Name: 'Download', Type: 1, Icon: 'fa-download' },
            { Name: 'Print', Type: 2, Icon: 'fa-print' },
            { Name: 'Email', Type: 3, Icon: 'fa-envelope-o' },
            { Name: 'Save to OneDrive', Type: 4, Icon: 'fa-cloud' },
        ]
        $s.PrOptions = {
            PageSelection: 1,
            Layout: 1,
            Page: null
        }

        $s.btnClick = (function (pr) {
            $d.close({ DocType: $s.DocType, Type: pr.Type, Layout: $s.PrOptions.Layout, Page: ($s.PrOptions.PageSelection === 1) ? null : $s.PrOptions.Page });
        })

        $s.Cancel = (function () {
            $d.dismiss();
        })

        $s.OK = (function () {
            $d.close(0);
        })

    })

    app.controller('PrintDialogController', ['$scope', '$controller', '$uibModalInstance', PrintDialogController]);

    var EmailDialogController = (function ($s, $c, $d) {
        $c('BaseController', { $scope: $s });

        $s.Cancel = (function () {
            $d.dismiss();
        })

        $s.OK = (function () {
            $d.close(0);
        })

        $s.ToggleBCC = (function () {
            $("#bcc-frm").toggleClass("toggled-bcc");
        })

        $s.ToggleCC = (function () {
            $("#cc-frm").toggleClass("toggled-cc");
        })

    })

    app.controller('EmailDialogController', ['$scope', '$controller', '$uibModalInstance', EmailDialogController]);

    var lzyGridController = function ($s, $elem, $attr, $t) {
        var lastScrollTop = 0, lastScrollLeft = 0;
        var defaults = {
            tableData: [],
            viewCount: 50,
            rowHeight: 30,
            tableWidth: '100%',
            tableHeight: '500px',
            multiSelect: false,
            columns: [],
            hasPaging: false,
            increment: 50,
            pages: [],
            pageView: [50, 120, 150],
            selectedPage: 1,
            columnSortName: 'first_name',
            columnSortOrder: 'asc',
            onViewChange: function () { }, //make your own view change
            onPageChange: function () { }, //make your own page change
            onSortChange: function (colName, order) { }, //make your own sort change
            onRowDoubleClick: function (row, index) { }, //make your own open
            sortData: null,
            getRowSelected: null,
            hasOpenIcon: false,
            hasOpenIconClick: function (row, index) { },
            Previous: null,
            Next: null
        }
        $s.tableOptions = $.extend(defaults, $s.tableOptions);
        $elem.css('width', $s.tableOptions.tableWidth);
        $elem.find('.lzy_body').css('height', $s.tableOptions.tableHeight);

        $s.hTop = 0, $s.hBottom = 0;
        $s.cb = {};
        $s.checkAllBox = function () {
            for (var x = 0; x < $s.records.length; x++) {
                $s.records[x].IsChecked = ($s.cb.checkAll == false ? false : true);
            }
        }

        $s.sortDirection = function () {
            return ($s.tableOptions.columnSortOrder == 'asc' ? 'lzy_chevron lzy_chevron_up' : 'lzy_chevron lzy_chevron_down');
        }

        var fp_top = $elem.find('.lzy_fp_Top');
        var fp_bottom = $elem.find('.lzy_fp_Bottom');
        var lzy_top = $elem.find('.lzy_Top');
        var lzy_bottom = $elem.find('.lzy_Bottom');

        $s.setHeightStyle = function () {
            fp_top.css('height', $s.hTop);
            fp_bottom.css('height', $s.hBottom);
            lzy_top.css('height', $s.hTop);
            lzy_bottom.css('height', $s.hBottom);
        }

        $s.$watch('tableOptions.tableData', function (nv, ov) {
            $elem.find('.lzy_body').scrollTop(0);
            $elem.find('.lzy_fp_body').scrollTop(0);

            $elem.find('.lzy_column').each(function (idx, c) {
                $(c).css('minWidth', '');
            });

            if ($s.tableOptions.tableData.length > 0) {
                $s.data = [], $s.FreezeColumns = [], $s.records = [], $s.start = 0, $s.end = $s.tableOptions.increment, $s.cb.checkAll = false;
                $s.hBottom = ($s.tableOptions.tableData.length - $s.end) * $s.tableOptions.rowHeight;
                $s.hTop = 0;

                $s.setHeightStyle();
                $s.orderByColumns();

                $s.data = angular.copy($s.tableOptions.tableData);
                $s.records = $s.data.slice($s.start, $s.end);

                $s.hBottom = ($s.data.length - $s.end) * $s.tableOptions.rowHeight;

            } else {
                $s.records = [];
            }
            $s.updateFreezeColumn();
            $t(function () {
                resizeContainer();
            }, 200);
        });
        $s.oldAddValue = 0;
        $s.oldStartValue = 0;
        $s.onScrollTopStop = function (e) {
            var currentScrollTop = e.target.scrollTop;
            var a = Math.floor((e.target.scrollTop - $s.hTop) / $s.tableOptions.rowHeight);
            var b = Math.floor(($s.hBottom - e.target.scrollTop) / $s.tableOptions.rowHeight) + $s.tableOptions.increment;

            if (currentScrollTop < lastScrollTop) {
                //scroll up
                if ($s.hTop > 0) {
                    a = (a * -1);

                    var tmpStart = $s.start;

                    $s.start = tmpStart - a,
                        $s.end = $s.end - a;

                    if ($s.start < 0) {
                        $s.start = 0,
                            $s.end = $s.tableOptions.increment;
                    }

                    $s.records = $s.data.slice($s.start, $s.end);

                    $s.hTop = ($s.start) * $s.tableOptions.rowHeight,
                        $s.hBottom = (($s.data.length - $s.tableOptions.increment) * $s.tableOptions.rowHeight) - $s.hTop;
                }
            } else {
                //scroll bottom
                if ($s.hBottom > 0) {
                    var tmpStart = $s.start;

                    $s.start = (($s.data.length - b) - tmpStart) - 5,
                        $s.end = $s.start + $s.tableOptions.increment;

                    if ($s.start < 0) {
                        $s.start = 0,
                            $s.end = $s.tableOptions.increment;
                    }

                    if ($s.end > $s.data.length) {
                        $s.start = $s.data.length - $s.tableOptions.increment,
                            $s.end = $s.data.length;
                    }

                    $s.records = $s.data.slice($s.start, $s.end);

                    $s.hTop = (($s.start - tmpStart) * $s.tableOptions.rowHeight) + $s.hTop,
                        $s.hBottom = (($s.data.length - $s.tableOptions.increment) * $s.tableOptions.rowHeight) - $s.hTop;
                }
            }
            lastScrollTop = currentScrollTop;
            $s.$apply();
            $s.setHeightStyle();
        }
        $s.onScrollLeftStop = function (e) {
            //var currentScrollLeft = e.target.scrollLeft;

            //var a = Math.floor(currentScrollLeft / 150);
            //if (a > 0) {
            //    console.log(a, $s.HiddenRight.length, currentScrollLeft)

            //    if (a >= $s.HiddenRight.length) {
            //        if ($s.HiddenRight.length > 0) {
            //            angular.forEach($s.tableOptions.columns, function (col) {
            //                var c = Enumerable.From($s.HiddenRight).Where(function (x) { return x.Name == col.Name }).ToArray();
            //                if (c.length > 0) {
            //                    col.isShow = true;
            //                }
            //            })
            //            console.log(currentScrollLeft)
            //            $s.HiddenRight = [];
            //            $t(function () {

            //            }, 100);
            //        }

            //    } else {

            //    }
            //}

            //lastScrollTop = currentScrollLeft;
            //$s.$apply();

        }
        $s.updateFreezeColumn = function () {
            $s.FreezeColumns = Enumerable.From($s.tableOptions.columns).Where(function (x) { return (x.IsFreeze == undefined ? false : x.IsFreeze) == true }).ToArray();
        }

        $s.$watch('tableOptions.columns', function (nv, ov) {
            var right = 0;
            $s.updateFreezeColumn();
            $t(function () {
                resizeContainer();
            }, 200);
        });
        $s.hover = function (idx) {
            $($elem.find('.lzy_fp_row')[idx]).addClass('lzy_hover');
            $($elem.find('.lzy_row')[idx]).addClass('lzy_hover');
        }
        $s.hoverOut = function (idx) {
            $($elem.find('.lzy_fp_row')[idx]).removeClass('lzy_hover');
            $($elem.find('.lzy_row')[idx]).removeClass('lzy_hover');
        }
        $s.setRowHeight = function () {
            return { 'minHeight': $s.tableOptions.rowHeight, 'maxHeight': $s.tableOptions.rowHeight };
        }
        $s.compare = function (a, b) {
            if (a[$s.tableOptions.columnSortName] < b[$s.tableOptions.columnSortName])
                return ($s.tableOptions.columnSortOrder == 'asc' ? -1 : 1);
            if (a[$s.tableOptions.columnSortName] > b[$s.tableOptions.columnSortName])
                return ($s.tableOptions.columnSortOrder == 'asc' ? 1 : -1);
            return 0;
        }
        $s.getRowSelected = function () {
            return Enumerable.From($s.records).Where(function (x) { return x.IsChecked == true }).ToArray();
        }
        $s.tableOptions.sortData = $s.compare;
        $s.tableOptions.getRowSelected = $s.getRowSelected;
        $s.orderByColumns = function () {
            $s.tableOptions.columns.sort(function (a, b) {
                return a.SeqNo - b.SeqNo;
            });
            $s.FreezeColumns.sort(function (a, b) {
                return a.SeqNo - b.SeqNo;
            });
        }
        $s.formatDate = function (d, f) {
            if (d == null) return null;
            return moment(new Date(d)).format(f);
        }
        $s.replaceNull = function (val) {
            if (val == null) {
                return '-';
            } else {
                return val;
            }
        }
        $s.tableOptions.Previous = function () {
            $s.tableOptions.selectedPage -= 1;
            $s.tableOptions.onPageChange();
        }
        $s.tableOptions.Next = function () {
            $s.tableOptions.selectedPage += 1;
            $s.tableOptions.onPageChange();
        }
        var timer;
        $(window).resize(function () {
            $t(function () {
                resizeContainer();
            }, 200);
        });

        function resizeContainer() {
            $elem.find('.lzy_header').css('minWidth', $elem.find('.lzy_body')[0].clientWidth);
            if ($elem.find('.lzy_body').get(0).scrollWidth > $elem.find('.lzy_body').width()) {
                $elem.find('.lzy_freeze_pane').css('height', $elem.find('.lzy_header')[0].clientHeight + $elem.find('.lzy_body')[0].clientHeight);
                $elem.find('.lzy_body').scrollLeft(0);
            } else {
                $elem.find('.lzy_freeze_pane').css('height', ($elem[0].clientHeight - ($elem.find('.lzy_footer')[0].clientHeight + 20)) - 1);
            }
            if ($s.tableOptions.multiSelect || $s.FreezeColumns.length > 0 || $s.tableOptions.hasOpenIcon) {
                if ($elem.find('.lzy_body').get(0).scrollWidth > $elem.find('.lzy_body').width()) {
                    $elem.find('.lzy_fp_body').css('height', $elem.find('.lzy_freeze_pane').height() - $elem.find('.lzy_header').height());
                } else {
                    $elem.find('.lzy_fp_body').css('height', $elem.find('.lzy_freeze_pane').height() - $elem.find('.lzy_header').height() - 1);
                }
            }
            if ($elem.find('.lzy_body').get(0).scrollHeight > $elem.find('.lzy_body').height()) {
                $elem.find('.lzy_header').css('width', 'calc(100% - 30px)');
            }
        }

        $elem.find('.lzy_fp_body').bind('mousewheel DOMMouseScroll', function (e) {
            var scrollTo = 0;
            e.preventDefault();
            if (e.type == 'mousewheel') {
                scrollTo = (e.originalEvent.wheelDelta * -1);
            }
            else if (e.type == 'DOMMouseScroll') {
                scrollTo = 40 * e.detail;
            }
            var y = (scrollTo + $(this).scrollTop()) - 20;
            $(this).scrollTop(y);
            $elem.find('.lzy_body').scrollTop(y);
        });
        $elem.find('.lzy_body').on('scroll', function (e) {
            $elem.find('.lzy_header').scrollLeft($(this).scrollLeft());
            $elem.find('.lzy_fp_body').scrollTop($(this).scrollTop());
            if (!$elem.find('.lzy_freeze_pane').hasClass('scrolling')) {
                $elem.find('.lzy_freeze_pane').addClass('scrolling');
            }
            if (timer) clearTimeout(timer);
            timer = $t(function () {
                $elem.find('.lzy_freeze_pane').removeClass('scrolling');
                var documentScrollTop = $('.lzy_body').scrollTop();
                var documentScrollLeft = $('.lzy_body').scrollLeft();
                if (lastScrollLeft != documentScrollLeft) {
                    //$s.onScrollLeftStop(e)
                }
                if (lastScrollTop != documentScrollTop) {
                    $s.onScrollTopStop(e);
                }
            }, 150);
        });

        $s.resizeColumn = function (e, i) {
            if ($s.tableOptions.multiSelect == true) i += 1;
            if ($s.tableOptions.hasOpenIcon == true) i += 1;
            //if ($s.tableOptions.multiSelect == true && $s.tableOptions.hasOpenIcon == true) i -= 1;

            var _ = $(e.currentTarget);
            var isResizing = false,
                lastDownX = 0,
                container = _.parent().parent(),
                lastDownX = e.clientX,
                isResizing = true,
                currentWidth;

            if (_.attr('oldWidth') == undefined) {
                currentWidth = parseInt(container.css('minWidth').replace('px', ''));
                _.attr('oldWidth', currentWidth);
            } else {
                currentWidth = _.attr('oldWidth');
            }

            container.css('borderRight', '1px dashed #ddd');
            $(document).on('mousemove', function (e) {
                if (!isResizing)
                    return;
                var ww = e.clientX - container.offset().left;
                if (ww < currentWidth) {
                    container.css('minWidth', currentWidth);
                    resizeRows(currentWidth, i);
                } else {
                    container.css('minWidth', ww);
                    resizeRows(ww, i);
                }
                //resizeContainer();
            }).on('mouseup', function (e) {
                isResizing = false;
                container.css('borderRight', '');
                removeBorder(i);
            });
        }

        function resizeRows(ww, idx) {
            $elem.find('.lzy_row').each(function () {
                var _ = $(this).find('.lzy_row_column')[idx];
                $(_).css('minWidth', ww);
                $(_).css('borderRight', '1px dashed #ddd');
            });
        }
        function removeBorder(idx) {
            $elem.find('.lzy_row').each(function () {
                var _ = $(this).find('.lzy_row_column')[idx];
                $(_).css('borderRight', '');
            });
        }
    }
    app.controller('lzyGridController', ['$scope', '$element', '$attrs', '$timeout', lzyGridController]);

});