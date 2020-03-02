
define(['app'], function (app) {

    var List = (function ($s, $c, $r, $st, $inv) {
        $c('BaseController', { $scope: $s });
        $s.g = $inv.group($s.Session('GUID'));
        $s.SetController('Action');
        $s.child = [];
        
        for (var r in $r) $s[r] = $r[r];
        $s.grid = {
            Skip: 1,
            Take: 30,
            filter: null,
            Pages: [1],
            OrderBy: "ID DESC"
        };
        $s.sortDirection = "DESC";
        $s.IsDataLoading = true;
        $s.FilterColumns = null;
        $s.Menu = $s.tMenu;
        $s.MenuHeader = $s.Menu.Name;
        $s.IsAllowDelete = $s.Menu.AllowDelete;
        $s.IsAllowNew = $s.Menu.AllowNew;
        $s.AllowEdit= true;
        if ($s.Menu.ID_MenuType == 1) {
     
            $s.Request('FetchChild', { ID_Menu: $s.Menu.ID, ID_UserGroup: $s.Session('ID_UserGroup') }).then(function (ret) {
               // console.log('tama eh d2 sa gets', ret.data[0].allownew,ret.data);
                if (ret.error != undefined) {
                    $s.toast(ret.error, 'GetChild', 'warning');
                } else {
                    
                    //$s.child = ret.data;
                    //$s.request('getmenu', { id_menu: $s.child[0].id }).then(function (ret2) {

                    //    for (var r in ret2) $s[r] = ret2[r];
                    //    $s.grid = {
                    //        skip: 1,
                    //        take: 30,
                    //        filter: null,
                    //        pages: [1],
                    //        orderby: "id desc",
                    //    };

                    //    $s.sortdirection = "desc";
                    //    $s.isdataloading = true;
                    //    $s.filtercolumns = null;
                    //    $s.isallowdelete = ret.data[0].allowdelete//$s.tmenu.allowdelete;
                    //    $s.isallownew = ret.data[0].allownew; //$s.tmenu.allownew;
                    //    $s.allowedit = ret.data[0].allowedit; //new
                        
                    //    $s.g.invoke('rtitle', 'list filter');
                    //    $s.loadgrid().then(function () {
                    //        $s.loaddata();
                    //    });

                    //    $s.fixpaging();
                    //})
                    $s.IsAllowDelete = ret.data[0].AllowDelete//$s.tmenu.allowdelete;
                    $s.IsAllowNew = ret.data[0].AllowNew; //$s.tmenu.allownew;
                    $s.AllowEdit = ret.data[0].AllowEdit; //new
                }
            })
        } 

        $s.setDataSource = function (id) {
            $s.Request('GetMenu', { ID_Menu: id }).then(function (ret2) {
                for (var r in ret2) $s[r] = ret2[sr];
                $s.grid = {
                    Skip: 1,
                    Take: 30,
                    filter: null,
                    Pages: [1],
                    OrderBy: "ID DESC"
                };

                $s.sortDirection = "DESC";
                $s.IsDataLoading = true;
                $s.FilterColumns = null;
                $s.IsAllowDelete = $s.tMenu.AllowDelete;
                $s.IsAllowNew = $s.tMenu.AllowNew;
                
                $s.g.invoke('RTitle', 'List Filter');
                $s.LoadGrid().then(function () {
                    $s.LoadData();
                });

                $s.FixPaging();
            })

            var pch = $('.module-tab p');
            var ch = $('#ch_' + id);
            pch.removeClass('active');
            ch.addClass('active');
        }
        
        $s.Init = (function () {
            //console.log($s)
            if ($s.Menu.ID_MenuType != 6) {
                $s.g.invoke('RTitle', 'List Filter');
		        $s.g.invoke('ClearFilter');
                $s.LoadGrid().then(function () {
                    $s.LoadData();
                });

                $s.FixPaging();
            }
        })


        
        $s.IsLoadDefault = false;
        $s.HasGroup = false;
        $s.idx = 0;
        $s.LoadGrid = (function () {
            
            return $s.Request('MenuGridColumns', { ID_User: $s.Session('ID_User'), ID_Menu: $s.tMenu.ID }).then(function (d) {
                $s.grid.Columns = (d.length === 0) ? $s.DefaultColumns() : d;
                for (var xx = 0; xx < $s.grid.Columns.length; xx++) {
                    if (Enumerable.From($s.tMenuTabField).Where(function (x) { return x.Name == $s.grid.Columns[xx].Name }).ToArray().length > 0) {
                        $s.grid.Columns[xx].DataType = Enumerable.From($s.tMenuTabField).Where(function (x) { return x.Name == $s.grid.Columns[xx].Name }).Select(function (x) { return x.DataType }).ToArray()[0];
                    } else {
                        $s.grid.Columns[xx].DataType = null;
                    }
                }
                $s.g.invoke('RInject', '<div idx="idx" control-filter="grid.Columns" on-filter="ListFilter"></div>', $s);
                if ($s.grid.Columns.filter(function (x) { return x.ID > 0 && x.SeqNo > 0 }).length == 0) {
                    $s.IsLoadDefault = true;
                } else {
                    $s.IsLoadDefault = false;
                }
                if ($s.grid.Columns.filter(function (x) { return x.GroupSeqNo > 0 && x.ID > 0 }).length == 0) {
                    $s.HasGroup = false;
                } else {
                    $s.HasGroup = true;
                }
            })
        })

        $s.LoadData = (function () {
            var dSource = $s.PassParameter($s.tMenu.DataSource);
            dSource = $s.SetFromSystemQueryParameter(dSource);
            $s.grid.Rows = [];
            $s.grid.TotalItems = 0;
            $s.IsDataLoading = true;

            $s.g.invoke('RFilter').then(function (d) {
                for (var x in d) {
                    if (d[x] != null) {
                        $s.grid.filter = d[x];
                    }
                }
                return $s.Request('LoadList', {
                    DataSource: dSource, Skip: $s.grid.Skip, Where: $s.grid.filter, TableName: $s.tMenu.TableName,
                    FilterColumns: $s.FilterColumns,
                    Take: $s.grid.Take,
                    OrderBy: $s.grid.OrderBy
                })
                .then(function (data) {
                    $s.IsDataLoading = false;
                    if (data.rows.length === 0) {
                        $s.grid.Rows = [];
                        $s.grid.TotalItems = 0;
                    } else {
                        $s.grid.Rows = data.rows;
                        $s.grid.TotalItems = data.count;
                    }
                    $s.grid.Pages = Enumerable.Range(1, Math.ceil($s.grid.TotalItems / $s.grid.Take)).ToArray();
                });
            })  
        })

        //Actions

        $s.OpenInfo = (function (ID) {
            sessionStorage.setItem("YHNUJMJH", '')
            sessionStorage.setItem("TGBYHNHG", '')
            $st.go('Info', {
                Name: $s.tMenu.Name.replace(/ /g, '-'),
                r: LZString.compressToEncodedURIComponent($s.tMenu.ID.toString() + '-' + ID.toString() +
                    ($s.Menu.ID_MenuType == 6 ? '-' + $s.Menu.Name.replace(/ /g, '%20') + '-' + $s.Menu.ID.toString() : ''))
            })
        })

        $s.ListFilter = (function (d) {
            $s.grid.filter = d;

            console.log('glf', d);

            $s.LoadData();
        })

        $s.FilterClick = (function () { 
            $s.g.invoke('RTogglePanel');
        })

        $s.g.on('RPanelClose', function (d) {
            if (!d) {
                $s.grid.filter = null;
            }
        })

        $s.PrintData = (function (d) {
            var dSource = $s.PassParameter($s.tMenu.DataSource);
            dSource = $s.SetFromSystemQueryParameter(dSource);

            switch (doc.Type) {
                case 1: //Download
                    $s.Download('PrintListInfo', {
                        ID_Menu: $s.tMenu.ID,
                        Data: {
                            DataSource: dSource, Skip: $s.grid.Skip, Where: $s.grid.filter, TableName: $s.tMenu.TableName,
                            FilterColumns: $s.FilterColumns,
                            Take: $s.grid.Take,
                            OrderBy: $s.grid.OrderBy
                        },
                        Doc: d
                    }).then(function () {
                        $s.Status = 'Ready';
                        $s.StatusColor = 2;
                    });
                    break;
            } 
        })

        //$s.setColumns = function () {
        //    var sortedArray = {};
        //    $s.columnsSet = {};
        //    $s.columnsSet.A = angular.copy(Enumerable.From($s.grid.Columns).Where(function (x) { return x.GroupSeqNo == 0 }).ToArray());
        //    $s.columnsSet.B = angular.copy(Enumerable.From($s.grid.Columns).Where(function (x) { return x.GroupSeqNo != 0 }).ToArray());
        //    //SET A
        //    sortedArray.A = $s.columnsSet.A.filter(function (x) { return x.SeqNo != 0 });
        //    angular.forEach($s.columnsSet.A.filter(function (x) { return x.SeqNo == 0 }), function (obj, idx) {
        //        idx++;
        //        while (sortedArray.A.filter(function (x) { return x.SeqNo == idx }).length > 0) {
        //            idx++;
        //        }
        //        obj.SeqNo = idx;
        //        sortedArray.A.push(obj);
        //    });

        //    $s.columnsSet.A = sortedArray.A;
        //    $s.columnsSet.A.sort(function (a, b) {
        //        return a.SeqNo - b.SeqNo;
        //    });
        //    angular.forEach($s.columnsSet.A, function (cols) {
        //        if (cols.ID > 0) {
        //            cols.visible = true;
        //        }
        //    });

        //    //SET B
        //    sortedArray.B = $s.columnsSet.B.filter(function (x) { return x.SeqNo != 0 });
        //    angular.forEach($s.columnsSet.B.filter(function (x) { return x.SeqNo == 0 }), function (obj, idx) {
        //        idx++;
        //        while (sortedArray.B.filter(function (x) { return x.SeqNo == idx }).length > 0) {
        //            idx++;
        //        }
        //        obj.SeqNo = idx;
        //        sortedArray.B.push(obj);
        //    });

        //    $s.columnsSet.B = sortedArray.B;
        //    $s.columnsSet.B.sort(function (a, b) {
        //        return a.GroupSeqNo - b.GroupSeqNo;
        //    });

        //    angular.forEach($s.columnsSet.B, function (cols) {
        //        if (cols.ID > 0) {
        //            cols.visible = true;
        //        }
        //    });

        //    $s.check = false;
        //    $s.checkAll = function () {
        //        $s.check = !$s.check;
        //        angular.forEach($s.columnsSet.A, function (cols) {
        //            cols.visible = $s.check;
        //        });
        //    }

        //    $s.dropItem = function (item) {
        //        item.visible = false;
        //    }

        //    $s.Load = function () {
        //        $s.columns = [];
        //        angular.forEach($s.columnsSet.A, function (obj, idx) {
        //            $s.columnsSet.A[idx].SeqNo = idx + 1;
        //            $s.columnsSet.A[idx].GroupSeqNo = 0;
        //            $s.columns.push($s.columnsSet.A[idx]);
        //        });
        //        angular.forEach($s.columnsSet.B, function (obj, idx) {
        //            $s.columnsSet.B[idx].GroupSeqNo = idx + 1;
        //            $s.columnsSet.B[idx].SeqNo = 0;
        //            $s.columns.push($s.columnsSet.B[idx]);
        //        });
        //        $s.ret = {};
        //        if ($s.columnsSet.B.length == 0) {
        //            $s.ret.cols = $s.columns.filter(function (col) { return col.visible === true; });
        //            $s.ret.grpItm = [];
        //        } else {
        //            $s.ret.cols = $s.columns.filter(function (col) { return col.visible === true; });
        //        }

        //        $s.Request('SaveColumnSelection', { ID: $s.tMenu.ID, Cols: JSON.stringify($s.ret.cols) }).then(function (results) {
        //            if (results.error != undefined) {
        //                $s.Toast(results.error, 'SaveColumnSelection', 'warning');
        //            } else {
        //                if ($s.Menu.ID_MenuType != 6) {
        //                    $s.Init();
        //                } else {
        //                    $s.Request('GetMenu', { ID_Menu: $s.child[0].ID }).then(function (ret2) {
        //                        for (var r in ret2) $s[r] = ret2[r];
        //                        $s.grid = {
        //                            Skip: 1,
        //                            Take: 30,
        //                            filter: null,
        //                            Pages: [1],
        //                            OrderBy: "ID DESC"
        //                        };
        //                        $s.sortDirection = "DESC";
        //                        $s.IsDataLoading = true;
        //                        $s.FilterColumns = null;
        //                        $s.IsAllowDelete = $s.tMenu.AllowDelete;
        //                        $s.IsAllowNew = $s.tMenu.AllowNew;
                                
        //                        $s.g.invoke('RTitle', 'List Filter');
        //                        $s.LoadGrid().then(function () {
        //                            $s.LoadData();
        //                        });

        //                        $s.FixPaging();
        //                    })
        //                }
        //            }
        //        });
        //        $s.resetColumns();
        //        $s.GroupItems();
        //    };
        //}

        //$s.$watch('grid.Columns', function () {
        //    if ($s.grid.Columns != undefined && $s.grid.Columns.length > 0) {
        //        $s.setColumns();
        //    }
        //})
        //$s.setHeight = function () {
        //    $s.setColumns();
        //    setTimeout(function () {
        //        var h = $(".setA").height();
        //        $(".setB").attr("style", "min-height:" + h + "px");
        //    }, 200)
        //}

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
        $s.$watch('grid.Rows', function () {
            $s.GroupItems();
        });
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

        $s.IsExpand = true;
        $s.toggleAllGroup = function (type) {
            $s.IsExpand = type;
            if (type) {
                $(".group-table").each(function () {
                    var a = $(this).attr("a");
                    if ($('.groupIndx_' + a).hasClass('hide')) {
                        $(".groupIndx_" + a).removeClass("hide");
                    }
                })
            } else {
                $(".group-table").each(function () {
                    var a = $(this).attr("a");
                    if (!$('.groupIndx_' + a).hasClass('hide')) {
                        $(".groupIndx_" + a).addClass("hide");
                    }
                })
            }

        }

        $s.checkChildren = function (id, pid) {
            var cnt = $("[targetid$=_" + id + "]").not("[targetid=parent_" + id + "]");
            cnt.each(function () {
                var _ = $(this);
                var p = $("[targetid^=parent_" + id + "]");

                if (p.is(":checked")) {
                    _.prop("checked", true).change();
                    var id2 = "";
                    if (_.attr("targetid").split("_").length > 2) {
                        id2 = _.attr("targetid").split("_")[_.attr("targetid").split("_").length - 2];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            $s.checkChildren(id2, id);
                        }
                    } else {
                        id2 = _.attr("targetid").split("_")[1];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            $s.checkChildren(id2, id);
                        }
                    }
                } else {
                    var _ = $(this);
                    _.prop("checked", false).change();
                    var id2 = "";
                    if (_.attr("targetid").split("_").length > 2) {
                        id2 = _.attr("targetid").split("_")[_.attr("targetid").split("_").length - 2];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            $s.checkChildren(id2, id);
                        }
                    } else {
                        id2 = _.attr("targetid").split("_")[1];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            $s.checkChildren(id2, id);
                        }
                    }
                }
            });
        }

        $s.DeleteRecord = function () {
            var ids = Enumerable.From($s.grid.Rows).Where(function (x) { return x.IsChecked }).Select(function (x) { return x.ID }).ToArray()
            if (ids.length != 0) {
                $s.Confirm('Are you sure?', document.title).then(function () {
                    $s.Request("RemoveSelectedInfo", { TableName: $s.tMenu.TableName, IDS: JSON.stringify(ids) }).then(function () {
                        $s.Toast('Record deleted.', 'Delete Record', 'info');
                        $s.LoadData();
                    });
                });
            } else {
                $s.Toast('No record selected.', 'Delete Record', 'warning');
            }
        }
        $s.sortRecord = function (name, e) {
            if (e.target.className == "fa fa-sort") {
                $s.sortDirection = "asc";
            } else if (e.target.className == "fa fa-sort-asc") {
                $s.sortDirection = "desc";
            } else if (e.target.className == "fa fa-sort-desc") {
                $s.sortDirection = "asc";
            }
            $s.grid.OrderBy = name + " " + $s.sortDirection;
            $s.LoadData();
            $s.resetColumns();
        }
        $s.sortIcon = function (name) {
            name = name.toString().toLowerCase();
            $s.grid.OrderBy = $s.grid.OrderBy.toString().toLowerCase();
            $s.sortDirection = $s.sortDirection.toString().toLowerCase();
            var n = $s.grid.OrderBy.split(" ");
            if (n.indexOf(name) == -1) {
                return "fa fa-sort";
            } else if (n.indexOf(name) > -1 && $s.sortDirection == "asc") {
                return "fa fa-sort-asc";
            } else if (n.indexOf(name) > -1 && $s.sortDirection == "desc") {
                return "fa fa-sort-desc";
            }
        }
        $s.resetColumns = function () {
            var c = $s.grid.Columns;
            $s.grid.Columns = [];
            $s.grid.Columns = c;
        }

        $s.FixPaging = (function () {
            var r_pos = ($(".table-footer").width() - $(".module-header").width()) + 10;
            $(".paging").css("right", r_pos);
        })

        var offsetTop = $(".main-content")[0].offsetTop;
        var clientHeight = $(".main-content")[0].clientHeight;
        $(".table-container").css("maxHeight", (clientHeight - 65) - offsetTop);
        $(".table-container").css("minHeight", (clientHeight - 65) - offsetTop);
    })


    app.register.controller('List', ['$scope', '$controller', 'resources', '$state', '$Invoker', List]);
})