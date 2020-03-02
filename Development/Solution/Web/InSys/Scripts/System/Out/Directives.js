'use strict';
define(['app'], function (app) {
    var vwLoadingBar = (function (q, t, w, $http, cfp) {
        return {
            restrict: 'EA',
            s: {
                wcOverlayDelay: "@"
            },
            link: function (s, element, A) {
                var
                    timerPromise = null,
                    timerPromiseHide = null,
                    inSession = false,
                    ngRepeatFinished = true,
                    queue = [];

                function processRequest() {
                    queue.push({});
                    if (queue.length == 3) {
                        $('#loading-bar').append('<div></div>');
                        $('#loading-bar').append('<div></div>');
                        $('#loading-bar').append('<div></div>');
                    }
                    if (queue.length == 1) {
                        timerPromise = t(function () {
                            if (queue.length) cfp.start(); //, s.ShowBooks();
                        }, s.wcOverlayDelay ? s.wcOverlayDelay : 300);
                    }
                }

                function processResponse() {
                    queue.pop();
                    if (queue.length == 0) {
                        timerPromiseHide = t(function () {
                            if (queue.length == 0) {
                                cfp.complete(); //, s.HideBooks();
                                if (timerPromiseHide) t.cancel(timerPromiseHide);
                            }
                        }, s.wcOverlayDelay ? s.wcOverlayDelay : 300);
                    }
                }

                $http.request = function (config) {
                    if (config.disableInterceptor == undefined || config.disableInterceptor == false) processRequest();
                    return config || q.when(config);
                };

                $http.response = function (response) {
                    processResponse();
                    if (response.data.Status === 1) {
                        //var statusmsg = response.data.ErrorMsg || "Error occurred. Contact system administrator.";
                        response.data.ErrorMsg = response.data.ErrorMsg || "Error occurred. Contact system administrator.";

                        return response || q.when(response); //q.reject(statusmsg);
                    } else
                        return response || q.when(response);
                };

                $http.responseError = function (rejection) {
                    processResponse();
                    //console.log('rj', rejection);

                    if (rejection.status === 401) {
                        window.location = '/Account';
                    }

                    return q.reject(rejection);
                };
            }
        }
    })

    //Empty factory to hook into $httpProvider.interceptors
    //Directive will hookup request,response,and responseError interceptors
    app.factory('httpInterceptor', function () {
        return {
        }
    });

    //Hook httpInterceptor factory into the $httpProvider interceptors so that we can monitor XHR calls
    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('httpInterceptor');
    }]);

    app.directive('vwLoadingBar', ['$q', '$timeout', '$window', 'httpInterceptor', 'cfpLoadingBar', vwLoadingBar]);
    //Handsontable
    app.directive('detailGrid', ['$controller', '$Invoker', '$compile', function ($c, $inv, $cpl) {
        return {
            restrict: 'A',
            scope: {
                detailGrid: '=',
                buttonClick: '=',
                vIf: "="
            },
            //replace: true,
            template: '<div ng-include="tempUrl"></div>',
            link: function ($s, $e, A) {
                $c('BaseController', { $scope: $s });
                $s.g = $inv.group($s.detailGrid.GUID);
                if (A.typeView != undefined) {
                    if (A.typeView == 1) {
                        $s.tempUrl = '/Web/Template/detail-grid.tmpl.html';
                    } else if (A.typeView == 2) {
                        $s.tempUrl = '/Web/Template/detail-form.tmpl.html';
                    } else {
                        $s.tempUrl = '/Web/Template/detail-grid.tmpl.html';
                    }
                } else {
                    $s.tempUrl = '/Web/Template/detail-grid.tmpl.html';
                }
                $s.VisibleIf = $s.vIf;
                $s.RowData = [];
                $s.RowIndex = 0;
                $s.grid = {
                    Skip: 1,
                    Take: 30,
                    Pages: [1]
                };
                $s.Combos = {};
                $s.RowsRemoved = [];
                $s.CurentEditorList = {};

                $s.SetCurEditorList = (function (row, col, data) {
                    $s.CurentEditorList[row + '-' + col] = data;
                })

                var __Construct = (function () {
                    $s.dtData = $s.detailGrid.DetailTab;
                    $s.Schema = $s.detailGrid.TableSchema.Schema;
                    $s.Type = $s.detailGrid.TableSchema.Type;
                    $s.Columns = $s.detailGrid.DetailTabField;
                    $s.Buttons = $s.detailGrid.Buttons;
                }($s))

                $s.Init = (function () {
                    try {
                        var fixedFilter;
                        Enumerable.From($s.Columns)
                            .Where(function (x) { return x.ShowInInfo === true && x.Name.substr(0, 3) === "ID_" && $s.IsNull(x.ID_Menu, 0) === 0 })
                            .ForEach(function (mtf) {
                                fixedFilter = mtf.FixedFilter
                                fixedFilter = $s.PassParameter(fixedFilter);
                                $s.LoadCombo($s.StringFormat("V{0}", mtf.Name.substr(3)), mtf.Name, fixedFilter, mtf.Sort).then(function (r) {

                                    $s.Combos[r.Name] = r.DataList;
                                });
                            });

                        $s.HasImportFile = $s.IsNull($s.dtData.ImportFile, "") !== "";

                        $s.Task(function () {
                            // $s.hot = new Handsontable($('.mdt-table', $e)[0], setting);
                            $s.g.invoke('InitReady', $s.dtData.ID);
                        });
                    } catch (Ex) {
                        console.error(Ex);
                    }
                 })

                $s.OpenDetail = (function (val) {
                    if (val == null || val == 0) return;
                    $s.g.invoke('DetailOpen', $s.dtData.ID_DetailMenu, val, $s.dtData.ID);
                })

                //$s.BeforeRemoveRow = (function (a, b, c) {
                //    for (var i = a; i < a + b; i++) {
                //        var row = $s.hot.getSourceDataAtRow(i);
                //        $s.RowsRemoved.push(row.ID || 0);

                //        vcl.Array.Remove($s.RowData, function (x) { return x.XXX_ROWID === row.XXX_ROWID });
                //    }
                //    //   $s.UpdateModel();
                //})

                //for rev
                //$s.AfterCreateRow = (function (a, b, c) {
                //    for (var i = a; i < a + b; i++) {
                //        //must add last count 
                //        if ($s.hot && $s.RowData) {
                //            $s.hot.getSourceDataAtRow(i).XXX_ROWID = i + b;

                //            console.log($s.hot.getSourceDataAtRow(i));
                //            //if ($s.hot.getSourceDataAtRow(i).ID == null) {
                //            //    var jjj = Enumerable.From($s.RowData);
                //            //    if (jjj.Any())
                //            //        $s.hot.getSourceDataAtRow(i).ID = jjj.Min(function (x) { return x.ID }) - 1;
                //            //    else
                //            //        $s.hot.getSourceDataAtRow(i).ID = 0;
                //            //}
                //        }
                //    }
                //})

                //lookup extra fields
                $s.OnCellEditorClosed = (function (row, col, prop, value) {
                    var j = Enumerable.From($s.Columns).Where(function (x) { return x.ParentLookUp === prop.prop; });

                    if (j.Any() && prop.DataRow) {
                        j.ForEach(function (x) {
                            $s.hot.setDataAtRowProp(row, x.Name, prop.DataRow[x.Name]);
                        })
                    }
                })

                //$s.Clear = (function () {
                //    //  $s.hot.loadData([]);
                //})

                //$s.UpdateModel = (function () {
                //    var gdt = $s.GetData();
                //    if (gdt.length > 0) {
                //        for (var k in gdt) {
                //            var h = Enumerable.From($s.RowData).Where(function (x) { return x.XXX_ROWID === gdt[k].XXX_ROWID });
                //            if (h.Any()) {
                //                var gg = h.SingleOrDefault();
                //                var ggc = Object.keys(gdt[k]);
                //                for (var jh in ggc) {
                //                    gg[ggc[jh]] = gdt[k][ggc[jh]];
                //                }
                //            } else {
                //                if (!$s.RowData) {
                //                    $s.RowData = [];
                //                    gdt[k].XXX_ROWID = 1;
                //                }
                //                else
                //                    gdt[k].XXX_ROWID = $s.RowData.length + 1;

                //                gdt[k].ID = 0;
                //                $s.RowData.push(gdt[k]);
                //            }
                //        }
                //    }
                //})

                $s.g.on('ValidateDetailData', function () {
                    return { ID: $s.dtData.ID, Name: $s.dtData.Name, Tag: $s.ValidateData() };
                })

                $s.ValidateData = (function () {
                    var dt = $s.RowData;
                    var gcols = Enumerable.From($s.Columns).Where('$.IsActive === true && $.ShowInInfo === true');
                    var cols = gcols.Join($s.Schema, '$.Name', '$.ColumnName', function (x, y) {
                        x.DataType = y.DataType;
                        x.AllowDBNull = y.AllowDBNull;
                        x.DefaultValue = y.DefaultValue;
                        return x;
                    }).Where(function (x) { return x.IsRequired || (!x.AllowDBNull && x.DefaultValue == null) }).ToArray();


                    for (var i = 0; i < dt.length; i++) {
                        if (!$s.IsEmptyRow(i, dt)) {
                            for (var c in cols) {
                                if (cols[c].Name !== 'ID' && $s.IsNull(dt[i][cols[c].Name], '') === '') {
                                    //console.log('Field Validator: ' + cols[c].Name);
                                    return { Field: cols[c].Name, Line: (i + 1) };
                                }
                            }
                        }
                    }

                    return false;
                })

                $s.g.on('GetDetailData', function (noTran) {
                    if (noTran && noTran.indexOf($s.dtData.TableName) !== -1) {
                        return null;
                    }

                    //$s.UpdateModel();

                    return {
                        ID: $s.dtData.ID,
                        Name: $s.dtData.Name,
                        Data: $s.RowData, //$s.GetData(),
                        Deleted: $s.RowsRemoved,
                        TableName: $s.dtData.TableName
                    };
                })

                //$s.GetData = (function () {
                //    var kk = $s.hot.getSourceData();
                //    if ($s.dtData.AllowNewRow)
                //        kk.pop();
                //    return kk;
                //})

                $s.g.on('DetailActivate', function (mdtab) {
                    //if (mdtab.ID === $s.dtData.ID)
                    // console.log(mdtab.Name);
                    //$s.hot.validateCells(),
                    //$s.hot.render();
                })

                $s.g.on('ListMenuSelection', function (ID, rows) {
                    try {
                        if ($s.dtData.ID === ID) {

                            //get ListKey
                            var nkey = Enumerable.From($s.Columns).Where(function (zz) { return zz.ListKey === true });
                            var key = null;
                            if (nkey.Any())
                                key = nkey.SingleOrDefault();
                            else
                                throw new Error('System Error: List key is not defined');

                            var errtr = [];

                            rows.forEach(function (zz) {

                                if ($s.dtData.AllowDuplicateList === false) {
                                    //search for existing Data
                                    if (Enumerable.From($s.RowData).Where(function (xx) { return xx[key.Name] === zz[key.ListColumn] }).Any()) {
                                        errtr.push(zz);
                                        return;
                                    }
                                }

                                var g = $s.NewRowSchema();
                                Enumerable.From($s.Columns).Where(function (x) { return x.ListColumn !== null }).ForEach(function (x) {
                                    g[x.Name] = zz[x.ListColumn];
                                })

                                $s.AddRow(g);
                            })

                            if (errtr.length > 0) {
                                $s.Toast('Some selected items already exists', 'Menu Selection', 'warning');
                            }

                            // $s.hot.loadData($s.RowData);
                        }
                    } catch (ex) {
                        $s.Toast(ex.message, document.title, 'warning');
                    }
                })

                $s.g.on('RefreshDetailControls', function (row) {
                    var j = Enumerable.From([row]).Where($s.PassParameter($s.dtData.DisableButtonsIf)).Any();
                    if (j && $s.dtData.DisableButtonsIf)
                        $s.DetailButtonEnabled = false;
                    else
                        $s.DetailButtonEnabled = true;
                })

                $s.AddRow = (function (row) {
                    //console.log('row', row);
                    var j = $s.RowData.length - 1;
                    if ($s.IsEmptyRow(j)) {
                        for (var n in row)
                            $s.RowData[j][n] = row[n];
                    } else
                        $s.RowData.push(row);

                    //console.log('row added', $s.RowData);
                })

                $s.IsEmptyRow = function (i, rows) {
                    if (!$s.dtData.AllowNewRow)
                        return false;

                    var rowData = (rows || $s.RowData)[i];
                    if (rowData) {
                        var k = Object.keys(rowData);
                        for (var j in k) {
                            if (rowData[k[j]] !== null && k[j] !== 'XXX_ROWID') return false;
                        }
                    } else
                        return false;
                    return true;
                }

                $s.g.on('GetTable', function (TableName) {
                    if (TableName === $s.dtData.TableName)
                        return $s;
                })

                $s.ClearDetail = function () {
                    $s.RowIndex = 0;
                    $s.RowData = [];
                    $s.UpdateView();
                }

                $s.g.on('ParentCurrentRow', function (TableName) {
                    if ($s.dtData.TableName === TableName) {
                        return $s.RowData[$s.RowIndex]; //   $s.data.RowIndex
                    }
                })

                $s.GetParentCurrentID = (function () {
                    return $s.g.invoke('ParentCurrentRow', $s.dtData.ParentTableName).then(function (row) {
                        return Enumerable.From(row).Where('$ != null').Select(function (x) { return x[$s.dtData.ParentColumn]; }).SingleOrDefault();
                    })
                })

                $s.bGenerateTemplate = (function () {
                    $s.GetParentCurrentID().then(function (x) {
                        $s.Download("DownloadExcelTemplate", {
                            ImportFile: $s.dtData.ImportFile,
                            DataSource: $s.PassParameter($s.PassParameter($s.dtData.FileReferenceDataSource), { ID: x }),
                            Sort: $s.dtData.FileReferenceSort,
                            IDTab: $s.dtData.ID,
                            ID_Menu: $s.dtData.ID_Menu
                            //Description: $s.IsNull($s.PassParameter($s.PassParameter($s.PassParameter($s.dtData.Description), { ID_Department: $s.Row.ID_DEPARTMENT }, "#"), { ID: x }), "")
                        });
                    })
                })

                $s.bImportFile = (function () {
                    $s.GetParentCurrentID().then(function (PID) {
                        $s.Confirm('Do you want to delete Current Detail', document.title).then(function () {
                            $s.UploadExcelFile(1, PID);
                        }).fail(function () {
                            $s.UploadExcelFile(0, PID);
                        })
                    });
                })

                $s.UploadExcelFile = (function (opt, PID) {
                    $s.UploadFile("UploadExcelTemplate", {
                        ID_Menu: $s.dtData.ID_Menu,
                        ID_User: $s.Session("ID_User"),
                        TableName: $s.dtData.TableName,
                        DataSource: $s.PassParameter($s.PassParameter($s.dtData.FileReferenceDataSource), { ID: PID }),
                        Sort: $s.dtData.FileReferenceSort,
                        ID: PID
                    },
                        false,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                    ).then(function (d) {
                        if (opt == 1) {
                            $s.ClearDetail()
                        }
                        d.forEach(function (x) {
                            var g = $s.NewRowSchema();
                            Enumerable.From(Object.keys(x)).Where(function (xs) { return xs !== 'ID' }).ForEach(function (c) {
                                g[c] = x[c];
                            });
                            $s.AddRow(g);
                        });

                        $s.grid = {
                            Skip: 1,
                            Take: 30
                        };

                        $s.UpdateView();
                    }).fail(function (ex) {
                        $s.Toast(ex, 'Upload Excel Template', 'error');
                        //  console.error(ex);
                    });
                })

                $s.NewRowSchema = (function () {
                    var d = {};
                    //console.log('schema', $s.Schema);
                    $s.Schema.forEach(function (obj) {
                        
                            d[obj.ColumnName] = null;

                        var k = Enumerable.From($s.Columns).Where(function (x){ return x.Name === obj.ColumnName});
                            
                            if (k.Any()) {
                                var j = k.Single();
                                if (j.Defaultvalue && j.Defaultvalue !== "") {
                                    d[obj.ColumnName] = vcl.String.Trim($s.PassParameter(j.Defaultvalue), "'");
                                }
                                else {
                                    // console.log('dito nalang', obj.DefaultValue, obj.ColumnName);
                                    if (obj.DefaultValue != null && obj.DataType === "int") {
                                        d[obj.ColumnName] = obj.DefaultValue.replace('((', '').replace('))', '');
                                    }
                                }
                            }

                            //Default Value
                            if ($s.IsNull(obj.DefaultValue, "").toLowerCase() === "(getdate())") {
                                if (obj.ColumnName.toLowerCase().indexOf("time"))
                                    d[obj.ColumnName] = $s.LongDate(new Date());
                                else
                                    d[obj.ColumnName] = vcl.DateTime.ShortDate2();
                            }
                        })

                        d.ID = 0;
                        if (!$s.RowData)
                            d.XXX_ROWID = 1;
                        else
                            d.XXX_ROWID = $s.RowData.length + 1;
                    

                    //console.log('new schema', d);

                    return d;
                })

                //Detail List Menu

                $s.idx = 0;

                $s.DetailMenuSet = (function (dr) {
                    //console.log('dms', dr);

                    try {
                        var mk = Enumerable.From($s.Columns).Where(function (x) { return x.ListColumn !== null });
                        var lkey = mk.Where(function (x) { return x.ListKey === true }).SingleOrDefault(null);
                        if (!$s.dtData.AllowDuplicateList && !lkey)
                            throw new Error("List Key Not Set");

                        dr = typeof dr == 'object' ? [dr] : dr;
                        for (var j = 0; j < dr[0].length; j++) {

                            if (!$s.dtData.AllowDuplicateList) {
                                if (Enumerable.From($s.RowData).Where(function (x) { return x[lkey.Name] === dr[0][j][lkey.ListColumn]; }).Any())
                                    continue;
                            }
                            var d = $s.NewRowSchema();
                            mk.ForEach(function (c) {
                                d[c.Name] = dr[0][j][c.ListColumn];
                            })

                            // console.log('dd', d);

                            $s.AddRow(d);
                            $s.UpdateView();
                        }
                    } catch (ex) {
                        $s.Toast(ex.message, "Detail Menu Set", "error");
                        console.error(ex);
                    }
                })

                //New

                $s.Disabled = {};
                $s.RowAdded = false;

                $s.ColumnFilter = (function (col) {
                    return col.IsActive === true && col.ShowInInfo === true
                })

                $s.PageBreak = (function (row) {
                    if ($s.grid.Skip == null) $s.grid.Skip = 1;
                    $s.grid.Pages = Enumerable.Range(1, Math.ceil($s.RowData.length / $s.grid.Take)).ToArray();

                    var d = Enumerable.From($s.RowData).Where(function (row) {
                        return row.XXX_ROWID >= (($s.grid.Skip * $s.grid.Take) - $s.grid.Take) + 1
                            && row.XXX_ROWID <= $s.grid.Skip * $s.grid.Take;
                    }).ToArray();

                    if ($s.RowIndex == null) $s.RowIndex = 0;

                    //return row.XXX_ROWID >= (($s.data.PageIndex * $s.data.PageCount) - $s.data.PageCount) + 1 && row.XXX_ROWID <= $s.data.PageIndex * $s.data.PageCount;
                    return row.XXX_ROWID >= (($s.grid.Skip * $s.grid.Take) - $s.grid.Take) + 1 && row.XXX_ROWID <= $s.grid.Skip * $s.grid.Take;
                })

                $s.ReadOnly = function (d, row) {
                    try {
                        var h = false;
                        var mf = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName.toUpperCase() === d.Name.toUpperCase(); }).SingleOrDefault(null);
                        if (d.ReadOnly === true) h = true;
                        else if (mf && (mf.Computed === true || parseInt(mf.ID_ColumnSource) === 2)) h = true;

                    } catch (ex) {
                        $s.Toast(ex.message, 'Read Only', 'error');
                        console.error('ReadOnlyIf', ex);
                    }
                    return h;
                }

                $s.Required = function (tabField) {
                    var h = false;
                    var mf = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName.toUpperCase() === tabField.Name.toUpperCase() }).SingleOrDefault(null);
                    if (tabField.IsRequired === true) h = true;
                    else if (mf && mf.AllowDBNull === 0) h = true;
                    return h;
                }

                $s.g.on('LoadDetailData', function (row, noTran) {
                    if (noTran && noTran.indexOf($s.dtData.TableName) !== -1) {
                        return;
                    }
                    $s.LoadData(row);
                });

                $s.LoadData = (function (row) {
                    $s.Request('DetailInfo', {
                        TableName: $s.dtData.TableName,
                        ParentColumn: $s.dtData.ParentColumn,
                        ChildColumn: $s.dtData.ChildColumn,
                        Row: row,
                        Order: $s.dtData.Sort,
                        Filter: $s.dtData.DetailTabFilter
                    }).then(function (d) {
                        $s.RowIndex = 0;

                        $s.RowData = d;

                        Enumerable.From($s.RowData).ForEach(function (x) {
                            Enumerable.From($s.Columns).Where(function (y) { return y.WebReadOnlyif !== "" }).ForEach(function (y) {
                                if (y.WebReadOnlyif) {
                                    $s.Disabled[x.ID] = Enumerable.From([row]).Where($s.PassParameter(y.WebReadOnlyif)).Any();
                                }
                                else {
                                    $s.Disabled[x.ID] = false
                                }
                            })
                        })
                    })
                })

                $s.evBlur = (function (d, row) {
                    var h = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName.toUpperCase() === d.Name.toUpperCase() }).SingleOrDefault(null);
                    if (d.ID_SystemControlType === 1 && (h.DataType === "decimal" || h.DataType === "int"))
                        if (isNaN(row[d.Name]))
                            row[d.Name] = null;
                    $s.RowAdded = false;
                })

                $s.FieldLength = (function (d) {

                    if (d.Name.toLowerCase().indexOf('date') != - 1) return 100;

                    var h = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName.toUpperCase() === d.Name.toUpperCase() && x.DataType == 'varchar' }).SingleOrDefault(null);

                    if (h == null)
                        return 50;

                    if (h.Length)
                        return h.Length;

                    return 50;
                })

                $s.UpdateView = (function () {

                })

                $s.AddNewRow = (function () {
                    var tn = $s.dtData.TableName;
                    
                 //Added by Yoku 02282019
                 $s.AddRow($s.NewRowSchema());
                 $s.RowAdded = true;
                    
                });
             
                $s.DeleteRow = (function (row, i) {
                    try {
                        if ($s.RowData.length - 1 === i) $s.RowAdded = false;
                        vcl.Array.Remove($s.RowData, function (x) { return x.$$hashKey === row.$$hashKey }); //because ID is too mainstream 
                        $s.RowsRemoved.push(row.ID || 0);
                    } catch (ex) {
                        console.error("DeleteRow", ex.message);
                    }
                })

                $s.UploadFileDetail = function (data, col) {
                    $s.UploadFile('UploadFileDetail', null, false,'application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/pdf,image/jpg,image/jpeg,image/png').then(function (ret) {
                        if (ret.error != undefined) {
                            $s.Toast(ret.error, 'danger');
                        } else {
                            data[col] = ret.OrigFileName;
                            data[col + '_GUID'] = ret.GUID;
                            $s.$apply();
                        }
                    })
                }

                $s.DownloadFileDetail = function (data, col) {
                    var p = { FileName: data[col + '_GUID'], OrigName: data[col] };
                    $s.Download('DownloadFileDetail', p);
                }

                var __Construct = (function () {
                    $s.Init();
                }())
            }
        }
    }])
    app.directive('detailTree', ['$controller', '$Invoker', function ($c, $inv) {
        return {
            restrict: 'A',
            scope: {
                detailTree: '=',
            },
            replace: true,
            templateUrl: '/Web/Template/detail-tree.tmpl.html',
            //template: '<div class="detail-tree"><div id="tree-{{ detailTree.DetailTab.ID }}"></div>',
            link: function ($s, $e, A) {
                $c('BaseController', { $scope: $s });
                $s.g = $inv.group($s.detailTree.GUID);

                $s.mTree = {};

                $s.data = {
                    MenuDetailTabField: $s.detailTree.DetailTabField,
                    GroupList: null,
                    TreeBase: [],
                    treeData: [],
                    treeJson: { id: 0, text: "All", item: [], child: 1 },
                    ListSource: null,
                    pID: null,
                    TreeKeyColumn: null
                }

                var __Construct = (function () {
                    $s.dtData = $s.detailTree.DetailTab;

                    $s.Schema = $s.detailTree.TableSchema.Schema;
                    $s.Type = $s.detailTree.TableSchema.Type;
                    $s.Columns = $s.detailTree.DetailTabField;
                    //$s.Buttons = $s.detailTree.Buttons;
                }($s))

                $s.Init = (function () {
                    $s.WaitForElement("#tree-" + $s.dtData.ID).then(function () {
                        $s.mTree = new dhtmlXTreeObject("tree-" + $s.dtData.ID, "100%", "100%", 0);
                        $s.mTree.setImagePath("/Scripts/Pack/dhtmlx/dhxtree_material/");
                        $s.mTree.enableCheckBoxes(1);
                        $s.mTree.enableThreeStateCheckboxes(true);

                        $s.data.GroupList = Enumerable.From($s.data.MenuDetailTabField).Where(function (x) { return x.IsGroup }).OrderBy(function (x) { return x.ListKey }).ThenBy(function (x) { return x.SeqNo }).ToArray();
                        for (var i in $s.data.GroupList)
                            $s.data.GroupList[i].Index = parseInt(i);

                        if ($s.data.GroupList.length === 0) throw new Error("Tab " + $s.dtData.Name + " Missed the GroupKey");

                        $s.data.TreeKeyColumn = Enumerable.From($s.data.MenuDetailTabField).Where(function (x) { return x.ListKey }).Select(function (x) { return x.Name }).SingleOrDefault(null);

                        $s.g.invoke('InitReady', $s.dtData.ID);
                    }, 500);
                })

                $s.g.on('LoadDetailData', function (row) {
                    $s.LoadData(row);
                })

                $s.LoadData = (function (row) {
                    return $s.Request("ListSource", { TableName: 'v' + $s.dtData.TableName.substr(1), childcolumn: $s.dtData.ChildColumn, value: row.ID })
                        .then(function (d) {
                            $s.data.pID = row.ID;
                            try {
                                $s.data.TreeBase = d;
                                $s.data.treeJson = { id: 0, text: "All", item: [], child: 1 };
                                $s.mTree.deleteChildItems(0);
                                $s.PopulateTreeView($s.data.treeJson, '', null, true);
                                $s.mTree.parse($s.data.treeJson, "json");
                                $s.data.treeData.forEach(function (x) {
                                    $s.mTree.setCheck(x, true);
                                });
                            } catch (ex) {
                                console.error(ex);
                            }
                        })
                })

                $s.LoadListSource = (function () {
                    return $s.Request('LoadTreeListSource', {
                        TableName: $s.dtData.TableName,
                        pListSource: $s.PassParameter($s.dtData.ListSource),
                        ChildColumn: $s.dtData.ChildColumn,
                        pID: $s.data.pID,
                        ID_Session: $s.Session('ID_Session'),
                        //ID_Company: $s.Session('ID_Company'),
                        ListKeyRow: JSON.stringify(Enumerable.From($s.data.MenuDetailTabField).Where(function (x) { return x.ListKey }).Select(function (x) { return { Name: x.Name, ListColumn: x.ListColumn } }).ToArray())
                    }).then(function (d) {
                        //$s.data.TreeBase = [];
                        try {
                            $s.data.ListSource = d;

                            var k = Enumerable.From($s.data.MenuDetailTabField).Where(function (x) { return x.CopyFromList && x.Name !== 'ID' });
                            d.forEach(function (x) {
                                var nRow = {};
                                k.ForEach(function (c) {
                                    var n = c["Name"],
                                        a = c["ListColumn"];
                                    if ($s.IsNull(a, '') === '') a = n;

                                    nRow[n] = x[a];
                                });
                                if (!nRow.ID)
                                    nRow.ID = $s.GetMinZeroValue();

                                //dont duplicate list
                                var j = Enumerable.From($s.data.TreeBase).Where(function (sx) { return sx[$s.data.TreeKeyColumn] === nRow[$s.data.TreeKeyColumn] });
                                if (!j.Any())
                                    $s.data.TreeBase.push(nRow);
                            });

                            $s.data.treeJson = { id: 0, text: "All", item: [], child: 1 };
                            $s.mTree.deleteChildItems(0);
                            $s.PopulateTreeView($s.data.treeJson, '', null);
                            $s.mTree.parse($s.data.treeJson, "json");

                            $s.data.treeData.forEach(function (x) {
                                $s.mTree.setCheck(x, true);
                            });
                        } catch (Ex) {
                            console.error(Ex);
                        }
                    });
                });

                $s.GetMinZeroValue = (function () {
                    var g = Enumerable.From($s.data.TreeBase)
                    if (g.Any())
                        g = g.Min(function (x) { return x.ID; });
                    else
                        g = 0;
                    return g > 0 ? 0 : g - 1;
                });

                $s.g.on('GetDetailData', function () {
                    return $s.Task().then(function () {
                        try {
                            var f = Enumerable.From($s.mTree.getAllChecked().split(",")).Where(function (x) { return !isNaN(x) && parseFloat(x) > 0 }).Select(function (x) {
                                var g = { ID: 0 };
                                g[$s.data.TreeKeyColumn] = parseFloat(x)
                                return g
                            }).ToArray();
                            return {
                                TableName: $s.dtData.TableName,
                                Data: f,
                                Deleted: Enumerable.From($s.data.TreeBase).Where(function (x) { return $s.IsNull(x.ID, 0) > 0 }).Select(function (x) { return x.ID }).ToArray()
                            };

                        } catch (Ex) {
                            console.error('Get Tree', Ex);
                        }
                    });
                });

                //@LJ 20160928
                //--> rev: v2 : from GSCOM
                $s.PopulateTreeView = (function (nd, pFilter, pGroupRow, CheckSet) {
                    try {
                        var dr = {};

                        if (pGroupRow)
                            dr = $s.data.GroupList[pGroupRow.Index + 1];
                        else {
                            if ($s.data.GroupList.length === 0) return;
                            dr = $s.data.GroupList[0];
                        }

                        var a = $s.GetDistinctObjects(Enumerable.From($s.data.TreeBase).Where(pFilter).OrderBy('$.' + dr.Text).ToArray(), dr.Name);
                        var b, drx, s;
                        a.forEach(function (ctr) {
                            var n = { id: 0, text: null, item: [], child: 0 };
                            if (ctr === null)
                                b = '$.' + dr.Name + " === null",
                                    n.text = "(Unspecified)";
                            else
                                b = '$.' + dr.Name + " === " + $s.SQLFormat(ctr)

                            drx = Enumerable.From($s.data.TreeBase).Where(b).FirstOrDefault(null);

                            if (ctr !== null) n.text = drx[dr.Text];

                            if (dr.Index === $s.data.GroupList.length - 1) {
                                n.id = $s.IsNull(drx[$s.data.TreeKeyColumn], drx[dr.Name]); //for rev:

                                if (CheckSet && $s.data.treeData.indexOf(n.id) === -1)
                                    $s.data.treeData.push(n.id);

                            } else {
                                n.id = "g" + dr.Name + drx[dr.Name];
                            }

                            nd.child = 1;
                            nd.item.push(n);

                            s = "";
                            if (pFilter !== "")
                                s = pFilter + " && ";
                            s += b;

                            if (dr.Index < $s.data.GroupList.length - 1)
                                $s.PopulateTreeView(n, s, dr, CheckSet);
                        })
                    } catch (Ex) {
                        console.error('Populate Tree:', Ex);
                    }
                })

                $s.GetDistinctObjects = (function (pRows, pColumnName) {
                    var a = [];
                    pRows.forEach(function (x) {
                        if (a.indexOf(x[pColumnName]) !== -1) return;
                        a.push(x[pColumnName]);
                    });
                    return a;
                })

                var __Construct = (function () {
                    $s.Init();
                }($s))
            }
        }
    }])

    app.directive('detailList', ['$controller', '$Invoker', function ($c, $inv) {
        return {
            restrict: 'A',
            scope: {
                detailList: '='
            },
            replace: true,
            templateUrl: '/Web/Template/detail-list.tmpl.html',
            link: function ($s, $e, A) {
                $c('BaseController', { $scope: $s });
                $s.g = $inv.group($s.detailList.GUID);

                $s.RowData = [];
                $s.RowIndex = 0;
                $s.grid = {
                    Skip: 1,
                    Take: 30,
                    Pages: [1]
                };

                var __Construct = (function () {
                    $s.dtData = $s.detailList.DetailTab;
                    $s.Columns = $s.detailList.DetailTabField;
                }($s))

                $s.Init = (function () {
                    $s.Request('MenuGridColumns', { ID_User: $s.Session('ID_User'), ID_Menu: $s.dtData.ID_Menu }).then(function (d) {
                        try {
                            $s.grid.Columns = (d.length === 0) ? $s.DefaultColumns() : d;
                            $s.g.invoke('RInject', '<div control-filter="grid.Columns" on-filter="ListFilter"></div>', $s);

                            var setting = {
                                data: $s.RowData,
                                minSpareRows: 0,
                                allowRemoveRow: false,
                                rowHeaders: false,
                                width: $e.parent().width() || $('.panel-body').width(),
                                height: $e.parent().height() || 400,
                                //autoWrapRow: true,
                                colHeaders: Enumerable.From($s.grid.Columns).Select(function (y) { return y.EffectiveLabel; }).ToArray(),
                                columns: Enumerable.From($s.grid.Columns).Select(function (y) {
                                    return {
                                        data: y.Name,
                                        readOnly: true,
                                    }
                                }).ToArray(),
                                contextMenu: false,
                                stretchH: 'all',
                                autoColumnSize: true,
                                manualColumnResize: true,
                                readOnly: true
                            };

                            $s.hot = new Handsontable($('.mdt-table', $e)[0], setting);

                            //console.log('list ready');

                            $s.g.invoke('InitReady', $s.dtData.ID);
                        } catch (Ex) {
                            console.error(Ex);
                        }
                    })
                })

                $s.g.on('LoadDetailData', function (row) {
                    $s.LoadData(row);
                });

                $s.LoadData = (function (row) {
                    //$s.Request('DetailInfo', {
                    //    TableName: $s.dtData.TableName,
                    //    ParentColumn: $s.dtData.ParentColumn,
                    //    ChildColumn: $s.dtData.ChildColumn,
                    //    Row: row,
                    //    Order: $s.dtData.Sort
                    //}).then(function (d) {
                    //    $s.RowIndex = 0;
                    //    $s.RowData = d;
                    //    $s.UpdateView();
                    //})
                    var dSource = $s.PassParameter($s.dtData.TableName);
                    dSource = $s.SetFromSystemQueryParameter(dSource);
                    $s.grid.Rows = [];
                    $s.grid.TotalItems = 0;
                    $s.IsDataLoading = true;

                    return $s.GetParentCurrentID().then(function (ID_Parent) {
                        return $s.Request('LoadList', {
                            DataSource: dSource, Skip: $s.grid.Skip, Where: [
                                {
                                    Name: $s.dtData.ChildColumn,
                                    Value: [ID_Parent],
                                    Type: 2
                                }
                            ], //, TableName: $s.tMenu.TableName,
                            // FilterColumns: $s.FilterColumns,
                            Take: $s.grid.Take,
                            OrderBy: $s.grid.OrderBy
                        })
                            .then(function (data) {
                                if (data.rows.length === 0) {
                                    $s.RowData = [];
                                } else {
                                    $s.RowData = data.rows;
                                    $s.grid.TotalItems = data.count;
                                }
                                $s.UpdateView();
                            });
                    })
                })

                $s.UpdateView = function () {
                    if ($s.grid.Skip == null) $s.grid.Skip = 1;
                    $s.grid.Pages = Enumerable.Range(1, Math.ceil($s.RowData.length / $s.grid.Take)).ToArray();

                    var d = Enumerable.From($s.RowData).Where(function (row) {
                        return row.XXX_ROWID >= (($s.grid.Skip * $s.grid.Take) - $s.grid.Take) + 1
                            && row.XXX_ROWID <= $s.grid.Skip * $s.grid.Take;
                    }).ToArray();

                    if ($s.RowIndex == null) $s.RowIndex = 0;

                    $s.hot.loadData(d);
                    $s.hot.validateCells();
                }

                $s.GetParentCurrentID = (function () {
                    return $s.g.invoke('ParentCurrentRow', $s.dtData.ParentTableName).then(function (row) {
                        for (var i in row) {
                            if (row[i]) {
                                return row[i][$s.dtData.ParentColumn];
                            }
                        }
                    })
                })

                var __Construct = (function () {
                    $s.Init();
                }($s))
            }
        }
    }])

    //LookUp

    app.directive('lookUp', function () {
        return {
            restrict: 'A',
            scope: {
                field: '=lookUp',
                row: '=lookUpData',
                fields: '=lookUpTabFields',
                getWidth: '=lookUpWidth',
                type: '=lookUpType',
                onSet: '=lookUpSet'
            },
            link: function ($s, $e) {
                $s.elem = $e;
            },
            controller: ['$scope', '$controller', '$compile', '$rootScope', function ($s, $c, $cpl, $rs) {
                $c('BaseController', { $scope: $s });
                var _ = this;

                $s.grid = {
                    Columns: null,
                    Skip: 1,
                    Take: 30,
                    filter: null,
                    Pages: [1]
                };

                _.hot = null; //grid object
                $s.FilterColumns = [];

                //@Overrides
                this.OnLookupOpen = (function () { })
                this.InitGrid = (function (labels, cols) { })

                if ($s.field.ID_Menu == null) {
                    $s.Toast('System Error: Field ' + $s.field.Name + ' ID_Menu Required.', 'Field Required', 'warning');
                    return;
                }

                //Overridable
                this.Init = (function () {

                    var Columns = $s.Request('MenuGridColumns', { ID_User: $s.Session('ID_User'), ID_Menu: $s.field.ID_Menu });
                    var Menu = $s.Request('GetMenu', { ID_Menu: $s.field.ID_Menu });

                    $.when(Columns, Menu).then(function (c, m) {
                        $s.Columns = (c[0].length === 0) ? $s.DefaultColumns() : c[0];

                        for (var r in m[0]) $s[r] = m[0][r]; //Menu

                        var labels = ['ID'];
                        var cols = [{
                            data: 'ID',
                            readOnly: true,
                            renderer: function (instance, td, row, col, prop, value, cellProperties) {
                                var el = $cpl('<a href="javascript:;" style="text-decoration:none!important;" ng-click="cellClick(' + value + ')"><i class="fa fa-pencil"></i> <span>' + value + '</span></a>')($s);

                                if (!td.firstChild)
                                    td.appendChild(el[0]);

                                return td;
                            }
                        }];

                        Enumerable.From($s.Columns).Where(function (xx) { return xx.Name !== 'ID' }).ForEach(function (y) {
                            labels.push(y.EffectiveLabel)
                            cols.push({
                                data: (y.ID_SystemControlType === 2 || y.ID_SystemControlType == 4 ? y.Name.substr(3) : y.Name),
                                readOnly: true,
                            });

                            //var g = y.Name;
                            //if (y.ID_SystemControlType === 2 || y.ID_SystemControlType == 4) {
                            //    g = g.substr(3);
                            //}

                            //$s.FilterColumns.push(g);

                        })

                        _.InitGrid(labels, cols);

                    });
                })

                $s.LoadData = (function () {
                    var dSource = $s.PassParameter($s.tMenu.DataSource);
                    dSource = $s.SetFromSystemQueryParameter(dSource);
                    $s.grid.Rows = [];
                    $s.grid.TotalItems = 0;
                    $s.IsDataLoading = true;
                    _.hot.loadData([]);

                    //console.log($s.field);

                    return $s.Request('LoadList', {
                        DataSource: dSource, Skip: $s.grid.Skip, Where: $s.grid.filter, TableName: $s.tMenu.TableName,
                        FilterColumns: $s.FilterColumns,
                        FixedFilter: $s.PassParameter($s.PassParameter($s.field.FixedFilter, $s.row || null, '$')),
                        Take: $s.grid.Take
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

                            _.hot.loadData(data.rows);

                            $s.grid.Pages = Enumerable.Range(1, Math.ceil($s.grid.TotalItems / $s.grid.Take)).ToArray();
                        });
                })

                $s.cellClick = (function (ID) {

                    var nRow = Enumerable.From($s.grid.Rows).Where(function (x) { return x.ID === ID }).SingleOrDefault();
                    switch (parseInt($s.type)) {
                        case 1: //MenuTabField
                            $s.row[$s.field.Name.trim().substr(3)] = nRow['Name'];
                            $s.row[$s.field.Name] = ID;
                            $s.LookUpExtraFields(nRow);
                            break;
                        case 2: //DetailTabField
                            $s.onSet(nRow);
                            break;
                    }
                    $s.field.IsLookUpOpen = false;
                })

                $s.LookUpExtraFields = (function (row) {
                    var g = Enumerable.From($s.fields).Where(function (x) {
                        return (x.ParentLookUp || "") === $s.field.Name
                    });
                    if (g.Any()) {
                        var j = Enumerable.From(g).Where(function (x) { return x.ListColumn.startsWith("ID_"); }).Select(function (x) { return x.ListColumn.substr(3); }).ToArray();
                        $s.Request("LookUpExtraFields", {
                            Columns: g.Select(function (x) { return x.ListColumn; }).Union(j).ToArray().join(","),
                            Tablename: "v" + $s.field.Name.substr(3),
                            ID: row.ID
                        }).then(function (h) {
                            g.ForEach(function (y) {
                                $s.row[y.Name] = h[y.ListColumn];
                                if (y.ListColumn.startsWith("ID_"))
                                    $s.row[y.Name.substr(3)] = h[y.ListColumn.substr(3)];
                            });
                            $rs.$apply();
                        })
                    }
                })

                //public

                this.Open = (function () {
                    _.OnLookupOpen();
                    //console.log('So eto pala');
                    $s.LoadData().then(function () {
                        if ($s.grid.Rows.length === 1) {
                            switch (parseInt($s.type)) {
                                case 1: //MenuTabField
                                    $s.row[$s.field.Name.trim().substr(3)] = $s.grid.Rows[0]['Name'];
                                    $s.row[$s.field.Name] = $s.grid.Rows[0].ID;
                                    $s.LookUpExtraFields($s.grid.Rows[0]);
                                    break;
                                case 2: //DetailTabField
                                    $s.onSet($s.grid.Rows[0]);
                                    break;
                            }
                            $s.field.IsLookUpOpen = false;
                        } else {
                            $s.field.IsLookUpOpen = true;
                        }
                    });
                })

                this.Close = (function () {
                    $s.field.IsLookUpOpen = false;
                })

                this.GetWidth = (function () {
                    if ($s.getWidth)
                        return $s.getWidth();
                    else
                        return $s.elem.width();
                })

                this.GetName = (function () {
                    return $s.tMenu.Name;
                })

                $s.field.OnLookUpOpen = (function (open) {
                    if (open) {
                        $s.LoadData().then(function () {
                            _.OnLookupOpen();
                        });
                    }
                })

                this.SetFilter = (function (value) {
                    $s.grid.filter = [
                        {
                            Name: 'Name',
                            Value: [value],
                            Type: 1
                        }
                    ];
                });

                this.ClearFilter = (function () {
                    $s.grid.filter = null;
                });

                this.ResetModel = (function () {
                    $s.row[$s.field.Name.trim().substr(3)] = null;
                    $s.row[$s.field.Name] = null;
                })

                this.Refresh = (function () {
                    $s.grid.filter = null;
                    $s.LoadData();
                })

            }]
        }
    })

    app.directive('lookUpInput', ['$rootScope', '$controller', function ($rs, $c) {
        return {
            restrict: 'A',
            require: ['^lookUp', 'ngModel'],
            link: function ($s, $e, $a, $p) {
                $c('BaseController', { $scope: $s });
                $e.bind('keydown', function (ev) {
                    switch (ev.keyCode) {
                        case 9://Tab
                            ev.preventDefault();
                            if ($s.IsNull($p[1].$viewValue, '') !== '') {
                                $p[0].SetFilter($p[1].$viewValue);
                                $p[0].Open();
                            } else {
                                $p[0].ClearFilter();
                            }
                            break;
                    }
                })

                $e.change(function () {
                    if ($s.IsNull($p[1].$viewValue, '') === '') {
                        $p[0].ClearFilter();
                        $p[0].ResetModel();
                    } else {
                        //lj for test
                        $p[0].SetFilter($p[1].$viewValue);
                        $p[0].Open();
                    }
                })
            }
        }
    }])

    app.directive('lookUpDiv', ['$rootScope', function ($rs) {
        return {
            restrict: 'E',
            require: '^^lookUp',
            templateUrl: '/Web/Template/look-up.tmpl.html',
            replace: true,
            link: function ($s, $e, $a, $p) {

                $s.Init = (function () {
                    $s.Width = $p.GetWidth();
                })

                //override
                $p.OnLookupOpen = (function () {
                    $s.Width = $p.GetWidth();
                })

                $p.InitGrid = (function (labels, cols) {

                    $s.lookupName = $p.GetName();

                    $p.hot = new Handsontable($e.find('#lookUpGrid')[0], {
                        data: [],
                        rowHeaders: false,
                        width: $p.GetWidth(),
                        height: 300,
                        autoWrapRow: true,
                        colHeaders: labels,
                        columns: cols,
                        onSelection: function (r, c, r2, c2) {
                            this.deselectCell();
                        }
                    });
                })

                $s.CloseLookUp = (function () {
                    $p.Close();
                })

                $s.Refresh = (function () {
                    $p.Refresh();
                })

                var _Construct = (function () {
                    $p.Init();
                }($s, $p))

            }
        }
    }])

    //end lookup

    app.directive('lazyLoadImage', ['DataService', function ($ds) {
        return {
            restrict: 'A',
            scope: { lazyLoadImage: '=' },
            link: function (s, e, a) {
                s.$watch('lazyLoadImage.Image', function (c, v) {

                    e.css({
                        'background-image': "url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA)",
                        'background-repeat': 'no-repeat',
                        'background-position': 'center',
                        //opacity: 1
                    });

                    if (!s.lazyLoadImage.Image) {
                        //e.css({ 'background-image': "url('/CompanyLogo5.png')", 'background-size': 'initial', opacity: 1 });
                        e.css({ 'background-image': "url('/noimage.png')", 'background-size': 'initial' });
                        return;
                    }

                    $ds.Post('LoadImage', { ImgFile: s.lazyLoadImage.Image, Container: s.lazyLoadImage.Container || null, Path: s.lazyLoadImage.Path || null, Size: s.lazyLoadImage.Size || null }).then(function (d) {
                        //e.css({ opacity: 0 });
                        var a = setTimeout(function () {
                            clearTimeout(a);
                            if (d) {

                                e.css({ 'background-image': "url(" + d + ")", 'background-size': 'contain' });

                                var b = setTimeout(function () {
                                    clearTimeout(b);
                                    //e.css({ opacity: 1 });
                                }, 1000)
                            } else {
                                //e.css({ 'background-image': "url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA)", 'background-size': 'contain', opacity: 1 });
                                e.css({ 'background-image': "url('/noimage.png')", 'background-size': 'contain' });
                            }
                        });
                    }).fail(function () {
                        //e.css({ 'background-image': "url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA)", 'background-size': 'initial', opacity: 1 });
                        e.css({ 'background-image': "url('/noimage.png')", 'background-size': 'initial' });
                    });
                })
            }
        }
    }]);

    app.directive('listMenu', ['$controller', 'DataService', '$compile', '$Invoker', function ($c, $ds, $cpl, $inv) {
        return {
            restrict: 'E',
            scope: {
                tab: '='
            },
            replace: true,
            templateUrl: '/Web/Template/list-menu.tmpl.html',
            link: function ($s, $e, $a) {
                $c('BaseController', { $scope: $s });

                $s.grid = {
                    Skip: 1,
                    Take: 30,
                    Where: null
                };

                $s.fltrText = null;
                $s.ShowFilter = false;
                $s.FilterColumns = [];
                $s.checkList = {};

                var Columns = $s.Request('MenuGridColumns', { ID_User: $s.Session('ID_User'), ID_Menu: $s.tab.ID_ListMenu });
                var Menu = $ds.GetMenu($s.tab.ID_ListMenu)

                $.when(Columns, Menu).then(function (c, m) {
                    $s.Columns = (c[0].length === 0) ? $s.DefaultColumns() : c[0];
                    for (var r in m) $s[r] = m[r];

                    var labels = [''];
                    var cols = [{
                        data: 'ID',
                        readOnly: false,
                        renderer: function (instance, td, row, col, prop, value, cellProperties) {
                            var el = $cpl('<input type="checkbox" ng-model="checkList[' + value + ']" />')($s);
                            $(td).empty();
                            td.appendChild(el[0]);
                            td.style.textAlign = 'center';
                            return td;
                        }
                    }];

                    Enumerable.From($s.Columns).ForEach(function (y) {
                        labels.push(y.EffectiveLabel)
                        cols.push({
                            data: y.Name,
                            readOnly: true,
                        });

                        var g = y.Name;
                        if (y.ID_SystemControlType === 2 || y.ID_SystemControlType == 4) {
                            g = g.substr(3);
                        }

                        $s.FilterColumns.push(g);

                    })

                    $s.hot = new Handsontable($e.find('#listMenuGrid')[0], {
                        data: [],
                        rowHeaders: false,
                        width: $e.width(),
                        height: $e.height() - 100,
                        //autoWrapRow: true,
                        colHeaders: labels,
                        columns: cols,
                        onSelection: function (r, c, r2, c2) {
                            this.deselectCell();
                        }
                    });
                })

                $s.tab.OnLookUpOpen = (function (ss) {
                    if (ss)
                        $s.LoadData();
                })

                $s.tab.OnLookUpKeyUp = (function ($event) {
                    if ($event.keyCode === 13) {
                        $s.tab.IsLookUpOpen = true;
                    }
                })

                $s.Filter = (function () {
                    $s.ShowFilter = !$s.ShowFilter;
                })

                $s.fltrClick = (function ($event) {
                    if ($event.keyCode === 13) {
                        $s.LoadData();
                    }
                })

                $s.Refresh = (function () {
                    $s.ShowFilter = false;
                    $s.fltrText = null;
                    $s.grid.Skip = 1;
                    $s.LoadData();
                })

                $s.LoadData = (function (v) {
                    var dSource = $s.PassParameter($s.tMenu.DataSource);
                    dSource = $s.SetFromSystemQueryParameter(dSource);
                    $s.grid.Rows = [];
                    $s.grid.TotalItems = 0;
                    $s.IsDataLoading = true;

                    if ($s.ShowFilter) {
                        if ($s.fltrText)
                            $s.grid.Where = { '*': $s.fltrText };
                        else
                            $s.grid.Where = null;
                    }
                    else {
                        if ($s.lookUpName)
                            $s.grid.Where = { Name: $s.lookUpName };
                        else
                            $s.grid.Where = null;
                    }

                    return $s.Request('LoadList', {
                        DataSource: dSource, Skip: v ? (v.Skip || $s.grid.Skip) : $s.grid.Skip, Where: $s.grid.Where || null, TableName: $s.tMenu.TableName,
                        FilterColumns: $s.FilterColumns
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

                            $s.hot.loadData(data.rows);

                            $s.grid.Skip = v ? (v.Skip || $s.grid.Skip) : $s.grid.Skip;
                        });
                })

                $s.OnPageChanged = (function (page) {
                    $s.LoadData({ Skip: page });
                })

                $s.OpenItem = (function () {
                    var jj = [];
                    Object.keys($s.checkList).forEach(function (x) {
                        if ($s.checkList[x] === true)
                            jj.push(parseFloat(x));
                    })

                    var kk = Enumerable.From($s.grid.Rows).Join(jj, '$.ID', '$', function (x, y) { return x; }).ToArray();

                    //reset
                    $s.checkList = {};
                    $s.tab.IsLookUpOpen = false;
                    $s.grid = {
                        Skip: 1,
                        Take: 30,
                        Where: null
                    };
                    $s.fltrText = null;
                    $s.ShowFilter = false;
                    $s.FilterColumns = [];

                    $inv.invoke('ListMenuSelection', $s.tab.ID, kk);
                })
            }
        }
    }])

    app.directive('dateToIso', function () {
        return {
            restrict: "A",
            require: 'ngModel',
            scope: {
                ngModel: '='
            },
            link: function (scope, element, attrs, ngModel) {
                element.on('blur', function () {
                    if (scope.ngModel != '') {
                        var t;
                        function TimeParser(a) {
                            var str = a
                            return Date.parse(str)
                        }

                        t = TimeParser(scope.ngModel)
                        ngModel.$setViewValue(vcl.DateTime.Format(t, vcl.DateTime.masks.mediumDate));
                        ngModel.$render();
                    }
                });

                scope.$watch(function () {
                    return ngModel.$modelValue;
                }, function (newValue) {
                    if (newValue && vcl.DateTime.IsNewtonFormat(newValue)) {
                        ngModel.$setViewValue(vcl.DateTime.Format(newValue, vcl.DateTime.masks.mediumDate)); //console.log(new Date(newValue));
                        ngModel.$render();
                    }
                });
            }
        };
    })

    app.directive('timeToIso', function () {
        return {
            restrict: "A",
            require: 'ngModel',
            scope: {
                ngModel: '='
            },
            link: function (scope, element, attrs, ngModel) {
                //element.on('blur', function () {
                //    console.log(scope.ngModel, 1)
                //    if (scope.ngModel != '') {
                //        var t;
                //        function TimeParser(a) {
                //            var str = a
                //            return Date.parse(str)
                //        }
                //        if (scope.ngModel === '12p' || scope.ngModel === '12pm' || scope.ngModel === '12:00 PM') {
                //            t = TimeParser('12:00')
                //            ngModel.$setViewValue(vcl.DateTime.Format(t, vcl.DateTime.masks.shortTime));
                //        }
                //        else if (scope.ngModel === '12a' || scope.ngModel === '12am' || scope.ngModel === '12:00 AM') {
                //            t = TimeParser('00:00')
                //            ngModel.$setViewValue(vcl.DateTime.Format(t, vcl.DateTime.masks.shortTime));
                //        } else if (scope.ngModel === '12:30a' || scope.ngModel === '12:30am' || scope.ngModel === '12:30 am' || scope.ngModel === '12:30 AM' || scope.ngModel === '12:30AM') {
                //            t = TimeParser('00:30')
                //            ngModel.$setViewValue(vcl.DateTime.Format(t, vcl.DateTime.masks.shortTime));
                //        } else if (scope.ngModel === undefined || scope.ngModel === null) {
                //            ngModel.$setViewValue(null);
                //        }
                //        else {
                //            //t = TimeParser(scope.ngModel)
                //            //ngModel.$setViewValue(vcl.DateTime.Format(t, vcl.DateTime.masks.shortTime));
                //        }
                //        ngModel.$render();
                //    }
                //});
                element.on('keydown', function () {
                    return false;
                })
                scope.$watch(function () {
                    return ngModel.$modelValue;
                }, function (newValue) {
                    if (newValue && vcl.DateTime.IsNewtonFormat(newValue)) {
                        ngModel.$setViewValue(vcl.DateTime.Format(newValue, vcl.DateTime.masks.shortTime)); //console.log(new Date(newValue));
                        ngModel.$render();
                    }
                });
            }
        };
    });

    app.directive('ngRecursiveMenu', ['$compile', function (c) {
        return {
            restrict: 'A',
            replace: true,
            transclude: true,
            scope: {
                menus: '=',
                itemSelect: '=',
                itemToggleFav: '=',
                menuContext: '='
            },
            templateUrl: '/Web/Template/ngRecursiveMenu.html',
            compile: function (E, A, transclude) {
                var contents = E.contents().remove();
                var compiledContents;
                return function ($s, iE, iA) {
                    if (!compiledContents) {
                        compiledContents = c(contents, transclude);
                    };

                    compiledContents($s, function (clone, s) {
                        iE.append(clone);
                    });

                    $s.SetIcon = (function (str) {
                        var abbr = "";
                        str = str.split(" ");
                        for (var i = 0; i < str.length; i++) {
                            abbr += str[i].substr(0, 1);
                        }

                        if (abbr.length > 2) {
                            abbr = abbr.substr(0, 2);
                        }

                        return abbr.toLowerCase();
                    })

                    $s.SetSlide = (function (Id) {
                        var $this = $("#" + Id);
                        var span = $("span", $this);
                        var pwidth = $($this).width();
                        var swidth = $("span", $this).width();

                        if (pwidth < swidth) {
                            $this.addClass("text-slide");
                            span.css("marginLeft", (swidth + 5) * (-1));
                            $(span).bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
                                $s.DeSetSlide();
                            });
                        }
                    })

                    $s.DeSetSlide = (function () {
                        $(".ISAzureMenu_nav_wrap ul a p").removeClass("text-slide");
                        $(".ISAzureMenu_nav_wrap ul a p span").css("marginLeft", 0);
                    })
                };
            },
        };
    }]);

    app.directive('ngRecursiveMenu2', ['$compile', function (c) {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                menus: '=',
                itemSelect: '=',
                itemToggleFav: '=',
                menuContext: '='
            },
            templateUrl: '/Web/Template/ngRecursiveMenu2.html',
            link: function ($s) {
                $s.SetIcon = (function (str) {
                    var abbr = "";
                    str = str.split(" ");
                    for (var i = 0; i < str.length; i++) {
                        abbr += str[i].substr(0, 1);
                    }

                    if (abbr.length > 2) {
                        abbr = abbr.substr(0, 2);
                    }

                    return abbr.toLowerCase();
                })

                $s.SetSlide = (function (Id) {
                    var $this = $("#" + Id);
                    var span = $("span", $this);
                    var pwidth = $($this).width();
                    var swidth = $("span", $this).width();

                    if (pwidth < swidth) {
                        $this.addClass("text-slide");
                        span.css("marginLeft", (swidth + 5) * (-1));
                        $(span).bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
                            $s.DeSetSlide();
                        });
                    }
                })

                $s.DeSetSlide = (function () {
                    $(".ISAzureMenu_nav_wrap ul a p").removeClass("text-slide");
                    $(".ISAzureMenu_nav_wrap ul a p span").css("marginLeft", 0);
                    $(".covering_levels ul a p").removeClass("text-slide");
                    $(".covering_levels ul a p span").css("marginLeft", 0);
                    $(".menu-cover ul a p").removeClass("text-slide");
                    $(".menu-cover ul a p span").css("marginLeft", 0);
                })

                $s.openMenu = function (menu) {
                    if (menu.Children.length == 0) return false;
                    var div = $("<nav/>");
                    div[0].id = "menu-cover_" + menu.Parent.ID;
                    div.addClass("menu-cover");

                    var back = $("<button/>");
                    back.append("Back");
                    back.addClass('menu-back')
                    back[0].style.cursor = "pointer";
                    div.append(back)


                    back.on('click', function () {
                        $("#menu-cover_" + menu.Parent.ID).removeClass("open-cover");
                    });
                    $s.m = menu;
                    if ($("#menu-cover_" + menu.Parent.ID).length == 0) {
                        $(".menu-items").append(div);
                        $("#menu-cover_" + menu.Parent.ID).append(c('<div ng-recursive-menu2 menus="m.Children" item-select="itemSelect" item-toggle-fav="itemToggleFav" menu-context="menuContext"><div ng-transclude></div></div>')($s));
                        setTimeout(function () {
                            div.addClass("open-cover")
                        }, 100);
                    } else {
                        $("#menu-cover_" + menu.Parent.ID).addClass("open-cover");
                    }

                }
            }
        };
    }]);

    app.directive('ngRecursiveMenu3', ['$compile', function (c) {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                menus: '=',
                itemSelect: '=',
                itemToggleFav: '=',
                menuContext: '='
            },
            templateUrl: '/Web/Template/ngRecursiveMenu3.html',
            link: function ($s, $e, $a) {
                $s.SetIcon = (function (str) {
                    var abbr = "";
                    str = str.split(" ");
                    for (var i = 0; i < str.length; i++) {
                        abbr += str[i].substr(0, 1);
                    }

                    if (abbr.length > 2) {
                        abbr = abbr.substr(0, 2);
                    }

                    return abbr.toLowerCase();
                })

                $s.SetSlide = (function (Id) {
                    var $this = $("#" + Id);
                    var span = $("span", $this);
                    var pwidth = $($this).width();
                    var swidth = $("span", $this).width();

                    if (pwidth < swidth) {
                        $this.addClass("text-slide");
                        span.css("marginLeft", (swidth + 5) * (-1));
                        $(span).bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
                            $s.DeSetSlide();
                        });
                    }
                })

                $s.DeSetSlide = (function () {
                    $(".ISAzureMenu_nav_wrap ul a p").removeClass("text-slide");
                    $(".ISAzureMenu_nav_wrap ul a p span").css("marginLeft", 0);
                    $(".covering_levels ul a p").removeClass("text-slide");
                    $(".covering_levels ul a p span").css("marginLeft", 0);
                    $(".menu-cover ul a p").removeClass("text-slide");
                    $(".menu-cover ul a p span").css("marginLeft", 0);
                })
                $s.idx = parseInt($a.idx) + 1;
                $s.openMenu = function (menu, e, idx) {
                    $(e.currentTarget).find("i").removeClass("fa-chevron-right");
                    $(e.currentTarget).find("i").addClass("fa-chevron-down");
                    var ch = $(".child_" + menu.Parent.ID);
                    if (ch.hasClass("hide-cover")) {
                        if (idx == 1) {
                            $(".menu-items").children().children().children().children().each(function () {
                                if ($(e.currentTarget).attr("id") != $($(this).children()[0]).attr("id")) {
                                    if ($(this).children().find("i").length > 0) {
                                        if ($(this).children().find("i").hasClass("fa-chevron-down")) {
                                            $(this).children().find("i").removeClass("fa-chevron-down");
                                            $(this).children().find("i").addClass("fa-chevron-right");
                                        }
                                    }
                                }

                            })
                            $(".menu-parent-ul").each(function () {
                                var me = $(this);
                                if (!me.hasClass("hide-cover")) {
                                    me.addClass("hide-cover");
                                    me.children().each(function () {
                                        var child = $(this);
                                        if (child.children().find("i").length > 0) {
                                            if (child.children().find("i").hasClass("fa-chevron-down")) {
                                                child.children().find("i").removeClass("fa-chevron-down");
                                                child.children().find("i").addClass("fa-chevron-right");
                                            }
                                        }
                                    })
                                }
                            })
                        }

                        $(".child_" + menu.Parent.ID).removeClass("hide-cover");
                        var idx = parseInt($(".child_" + menu.Parent.ID).attr("idx"));
                        $(".child_" + menu.Parent.ID).each(function () {
                            var _ = $(this);
                            _[0].style.marginLeft = (idx > 1 ? (idx * 15) - 10 : (idx * 15)) + "px";
                        })
                    } else {
                        if (idx == 1) {
                            $(".menu-items").children().children().children().children().each(function () {
                                if ($(e.currentTarget).attr("id") != $($(this).children()[0]).attr("id")) {
                                    if ($(this).children().find("i").length > 0) {
                                        if ($(this).children().find("i").hasClass("fa-chevron-down")) {
                                            $(this).children().find("i").removeClass("fa-chevron-down");
                                            $(this).children().find("i").addClass("fa-chevron-right");
                                        }
                                    }
                                }

                            })
                            $(".menu-parent-ul").each(function () {
                                var me = $(this);
                                if (!me.hasClass("hide-cover")) {
                                    me.addClass("hide-cover");
                                    me.children().each(function () {
                                        var child = $(this);
                                        if (child.children().find("i").length > 0) {
                                            if (child.children().find("i").hasClass("fa-chevron-down")) {
                                                child.children().find("i").removeClass("fa-chevron-down");
                                                child.children().find("i").addClass("fa-chevron-right");
                                            }
                                        }
                                    })
                                }
                            })
                        }

                        $(e.currentTarget).find("i").addClass("fa-chevron-right");
                        $(e.currentTarget).find("i").removeClass("fa-chevron-down");
                        $(".child_" + menu.Parent.ID).addClass("hide-cover");
                        $(".child_" + menu.Parent.ID).each(function () {
                            var _ = $(this);
                            _[0].style.marginLeft = "";
                        })
                    }
                }
            }
        };
    }]);

    app.directive('ngEnter', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.bind("keydown keypress", function (event) {
                    if (event.which === 13) {
                        scope.$apply(function () {
                            scope.$eval(attrs.ngEnter);
                        });
                        event.preventDefault();
                    }
                });
            }
        }
    });

    app.directive('ngUpdateModel', function () {
        return {
            restrict: 'A',
            scope: {
                ngUpdateModel: '=',
                ngModel: '='
            },
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                element.bind("keyup keypress change blur", function (event) {
                    scope.ngUpdateModel.model[scope.ngUpdateModel.name] = scope.ngModel;
                    scope.$apply();
                });
            }
        }
    })

    /**
     * Incomplete *
     * 
     */
    app.directive('controlFilter', ['$rootScope', '$controller', '$Invoker', function ($rs, $c, $inv) {
        return {
            restrict: 'A',
            scope: {
                controlFilter: '=',
                onFilter: '=',
                idx: '=',
            },
            templateUrl: '/Web/Template/Control-Filter.tmpl.html',
            link: (function ($s, $e, $a) {
                $c("BaseController", { $scope: $s });
                $s.data = {};
                $s.dropdownSource = {};
                $s.nFltr = null;
                $s.Init = (function () {
                    $s.nFltr = [];
                    if ($s.controlFilter.length > 0) {
                        $s.controlFilter.sort(function (a, b) {
                            return a.SeqNo - b.SeqNo
                        })
                    }
                    if (Enumerable.From($s.controlFilter).Where(function (x) { return x.ID > 0 }).ToArray().length > 0) {
                        $s.HasUserColumn = true;
                    } else {
                        $s.HasUserColumn = false;
                    }
                    $s.controlFilter.forEach(function (x) {
                        //  console.log(x)
                        x.CType = $s.ValidateTextBoxControl(x) || 2;
                        $s.nFltr.push(x);
                    })

                    if ($s.nFltr.length > 0) {
                        $s.nFltr.sort(function (a, b) {
                            return a.SeqNo - b.SeqNo
                        })
                    }

                    //console.log('filter', $s.nFltr);

                })

                var comboControl = Enumerable.From($s.controlFilter).Where(function (x) { return x.ID_SystemControlType == 2 }).ToArray();

                $s.Request('fetchFilterSource', { comboData: JSON.stringify(comboControl) }).then(function (obj) {
                    $s.dropdownSource = obj.data.dropdown_source;
                });

                $s.ValidateTextBoxControl = function (d) {
                    //console.log(d)
                    if (d.ID_SystemControlType == 1 && d.DataType == 'int') {
                        //NUMERIC ONLY 
                        return 1;
                    } else if (d.ID_SystemControlType == 1 && (d.DataType == 'varchar' || d.DataType == 'decimal' || d.DataType == 'text')) {
                        //STRING, DECIMAL 
                        return 2;
                    } else if (d.ID_SystemControlType === 2) {
                        return 9;
                    } else if (d.ID_SystemControlType === 3) {
                        return 10;
                    } else if (d.ID_SystemControlType === 4) {
                        return 11;
                    } else if (d.ID_SystemControlType == 11 && d.DataType == 'datetime' && (d.Name.toLowerCase().indexOf('start') > -1 || d.Name.toLowerCase().indexOf('end') > -1) && d.Name.toLowerCase().indexOf('date') > -1) {
                        //DATE PICKER 
                        if (d.Name.toLowerCase().indexOf('start') > -1) {
                            return 12;
                        } else {
                            return 13;
                        }
                    } else if (d.ID_SystemControlType == 11 && (d.DataType == 'datetime' || d.Name.toLowerCase().indexOf('date') > -1)) {
                        //DATE PICKER 
                        return 8;
                    } else if (d.ID_SystemControlType == 12 && (d.DataType == 'datetime' && (d.Name.toLowerCase().indexOf('start') > -1 || d.Name.toLowerCase().indexOf('end') > -1) && d.Name.toLowerCase().indexOf('time') > -1)) {
                        //TIME PICKER 
                        if (d.Name.toLowerCase().indexOf('start') > -1) {
                            return 14;
                        } else {
                            return 15;
                        }
                    } else if (d.ID_SystemControlType == 12 && d.DataType == 'datetime' && d.Name.toLowerCase().indexOf('time') > -1) {
                        //TIME PICKER 
                        return 4;
                    } else if (d.ID_SystemControlType == 17 && d.DataType == 'datetime' && (d.Name.toLowerCase().indexOf('start') > -1 || d.Name.toLowerCase().indexOf('end') > -1) && d.Name.toLowerCase().indexOf('datetime') > -1) {
                        //DATETIME PICKER 
                        return 3;
                    } else if (d.ID_SystemControlType == 17 && d.DataType == 'datetime' && d.Name.toLowerCase().indexOf('datetime') > -1) {
                        //DATETIME PICKER 
                        return 3;
                    }
                }

                $s.ExecuteFilter = (function () {
                    var fltr = [];
                    $s.nFltr.forEach(function (x) {
                        switch (x.CType) {
                            case 1:
                            case 4:
                            case 6:
                            case 8:
                                if ($s.data['From_' + x.Name]) {
                                    var tro = $s.data['To_' + x.Name] || $s.data['From_' + x.Name];
                                    fltr.push({ Name: x.Name, Value: [$s.data['From_' + x.Name], tro], Type: 3 });
                                }
                                break;
                            case 0:
                            case 2:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 1 });
                                break;
                            case 11:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 4 });
                                break;
                            case 12:
                            case 14:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 5 });
                                break;
                            case 13:
                            case 15:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 6 });
                                break;
                            default:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 2 });
                                break;
                        }
                    })

                    $s.onFilter(fltr);
                })

                $s.g = $inv.group($s.Session('GUID'));
                $s.ReturnFilter = function () {
                    var fltr = [];
                    $s.nFltr.forEach(function (x) {
                        switch (x.CType) {
                            case 1:
                            case 4:
                            case 6:
                            case 8:
                                if ($s.data['From_' + x.Name]) {
                                    var tro = $s.data['To_' + x.Name] || $s.data['From_' + x.Name];
                                    fltr.push({ Name: x.Name, Value: [$s.data['From_' + x.Name], tro], Type: 3 });
                                }
                                break;
                            case 0:
                            case 2:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 1 });
                                break;
                            case 11:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 4 });
                                break;
                            case 12:
                            case 14:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 5 });
                                break;
                            case 13:
                            case 15:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 6 });
                                break;
                            default:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 2 });
                                break;
                        }
                    })
                    if (fltr.length == 0) {
                        return null;
                    } else {
                        return fltr;
                    }
                }

                $s.clearfilter = (function () {
                    $s.data = {};
                });
                $s.g.on('RFilter', $s.ReturnFilter);
                $s.g.on('ClearFilter', $s.clearfilter);

                var __contruct = (function () {
                    $s.Init();
                }($s))

            })
        }
    }])

    app.directive('scrollOnClick', function () {
        return {
            scope: {
                scrollOnClick: '=',
                scrollId: '@'
            },
            restrict: 'A',
            link: function (s, e, a) {
                e.on('click', function () {
                    var nfo = $('[info-id="' + s.scrollId + '"]');
                    var $this = $('[scroll-area]', nfo);
                    var $target = $('#' + s.scrollOnClick, nfo);
                    $this.animate({
                        scrollTop: $this.scrollTop() - $this.offset().top + $target.offset().top
                    }, "slow");
                })
            }
        }
    })

    app.directive('scrlOnClick', function () {
        return {
            scope: {
                scrlOnClick: '=',
            },
            restrict: 'A',
            link: function (s, e, a) {
                e.on('click', function () {
                    var $this = $('[scrl-area]');
                    var $target = $('#' + s.scrlOnClick);

                    $this.animate({
                        scrollTop: ($this.scrollTop() - $this.offset().top + $target.offset().top) - 5
                    }, "slow");
                })
            }
        }
    })

    app.directive('printDialog', ['$controller', function ($c) {
        return {
            restrict: 'A',
            scope: {
                printDialog: '='
            },
            link: function ($s, $e, $a) {
                $c('BaseController', { $scope: $s });
                $e.on('click', function () {
                    $s.Dialog({
                        template: 'print-dialog.tmpl.html',
                        controller: 'PrintDialogController',
                        size: 'md',
                    }).result.then(function (d) {
                        $s.printDialog(d);
                    })
                })
            }
        }
    }])

    app.controller('LookupFilter', ['$controller', '$scope', '$uibModalInstance', 'dData', 'DataService', '$Invoker', '$compile', function ($c, s, mI, $d, ds, $inv, $compile) {
        $c("BaseController", { $scope: s });
        s.idx = ($d.idx == null ? 0 : $d.idx) + 1;
        s.g = $inv.group(s.Session('GUID'));
        s.grid = {
            Skip: 1,
            Take: 30,
            filter: null,
            Pages: [1],
            OrderBy: "ID DESC"
        };
        s.sortDirection = "DESC";
        s.IsDataLoading = true;
        s.FilterColumns = null;
        s.RowCollection = [];

        ds.GetMenu($d.menuID).then(function (r) {
            
            s.Menu = r;

            s.Init = (function () {
                s.LoadGrid().then(function () {
                    s.LoadData();
                });

            })

            s.LoadGrid = (function () {
                return s.Request('MenuGridColumns', { ID_User: s.Session('ID_User'), ID_Menu: s.Menu.tMenu.ID }).then(function (d) {
                    s.grid.Columns = (d.length === 0) ? s.DefaultColumns() : d;
                    setTimeout(function () {
                        $('.lookup_filter_body_' + s.idx).empty();
                        angular.element($('.lookup_filter_body_' + s.idx).append($compile('<div idx="idx" control-filter="grid.Columns" on-filter="ListFilter"></div>')(s)));
                    }, s);
                    if (Enumerable.From(s.grid.Columns).Where(function (x) { return x.ID > 0 }).ToArray().length > 0) {
                        s.HasUserColumn = true;
                    } else {
                        s.HasUserColumn = false;
                    }
                    s.grid.Columns.sort(function (a, b) {
                        return a.SeqNo - b.SeqNo
                    })
                })
            })

            s.LoadData = (function () {
                var dSource = s.PassParameter(s.Menu.tMenu.DataSource);
                dSource = s.SetFromSystemQueryParameter(dSource);
                s.grid.Rows = [];
                s.grid.TotalItems = 0;
                s.IsDataLoading = true;
                return s.Request('LoadList', {
                    DataSource: dSource, Skip: s.grid.Skip, Where: s.grid.filter, TableName: s.Menu.tMenu.TableName,
                    Columns: "*",
                    Take: s.grid.Take,
                    OrderBy: s.grid.OrderBy,
                    SearchAll: $d.filterValue
                })
                    .then(function (data) {
                        s.IsDataLoading = false;
                        if (data.rows.length === 0) {
                            s.grid.Rows = [];
                            s.grid.TotalItems = 0;
                        } else {
                            s.grid.Rows = data.rows;
                            s.grid.TotalItems = data.count;
                        }
                        s.grid.Pages = Enumerable.Range(1, Math.ceil(s.grid.TotalItems / s.grid.Take)).ToArray();
                        if ($d.rowCol instanceof Array) {
                            angular.forEach($d.rowCol, function (rid) {
                                var da = Enumerable.From(s.grid.Rows).Where(function (x) { return x.ID == rid }).ToArray()[0];
                                //da.IsChecked = true;
                                da.IsChecked = false;
                            });
                        } else {
                            var da = Enumerable.From(s.grid.Rows).Where(function (x) { return x.ID == $d.rowCol }).ToArray()[0];
                            //da.IsChecked = true;
                            da.IsChecked = false;
                        }

                    });
            })
            s.Init();
        });

        s.Cancel = function () {
            mI.dismiss();
        }

        s.sortRecord = function (name, e) {
            if (e.target.className == "fa fa-sort") {
                s.sortDirection = "asc";
            } else if (e.target.className == "fa fa-sort-asc") {
                s.sortDirection = "desc";
            } else if (e.target.className == "fa fa-sort-desc") {
                s.sortDirection = "asc";
            }
            s.grid.OrderBy = name + " " + s.sortDirection;
            s.LoadData();
            s.resetColumns();
        }
        s.sortIcon = function (name) {
            name = name.toString().toLowerCase();
            s.grid.OrderBy = s.grid.OrderBy.toString().toLowerCase();
            s.sortDirection = s.sortDirection.toString().toLowerCase();
            var n = s.grid.OrderBy.split(" ");
            if (n.indexOf(name) == -1) {
                return "fa fa-sort";
            } else if (n.indexOf(name) > -1 && s.sortDirection == "asc") {
                return "fa fa-sort-asc";
            } else if (n.indexOf(name) > -1 && s.sortDirection == "desc") {
                return "fa fa-sort-desc";
            }
        }
        s.checkChildren = function (id, pid) {
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
                            s.checkChildren(id2, id);
                        }
                    } else {
                        id2 = _.attr("targetid").split("_")[1];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            s.checkChildren(id2, id);
                        }
                    }
                } else {
                    var _ = $(this);
                    _.prop("checked", false).change();
                    var id2 = "";
                    if (_.attr("targetid").split("_").length > 2) {
                        id2 = _.attr("targetid").split("_")[_.attr("targetid").split("_").length - 2];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            s.checkChildren(id2, id);
                        }
                    } else {
                        id2 = _.attr("targetid").split("_")[1];
                        if (!(_.attr("targetid").split("_")[0] == "child")) {
                            s.checkChildren(id2, id);
                        }
                    }
                }
            });
        }

        s.retRowCollection = function () {
            s.RowCollection = Enumerable.From(s.grid.Rows).Where(function (x) { return x.IsChecked }).ToArray();
            //if (s.Menu.tMenu.Name == 'Employment Record') {
            //    if (s.RowCollection.length == 1) {
            //        s.RowCollection[0] = s.RowCollection[0];
            //        mI.close(s.RowCollection);
            //    }
            //    if (s.RowCollection.length > 1) {
            //        mI.close(s.RowCollection);
            //        s.RowCollection = null;
            //    } else {
            //        s.Toast("No record selected!", 'Lookup', 'warning');
            //    }
            //}
            //else {
            if (s.RowCollection.length !== 0) {
                    mI.close(s.RowCollection);
                    s.RowCollection = null;
                } else {
                    s.Toast("No record selected!", 'Lookup', 'warning');
                }
            //}

        }


        s.TogglePanelFilter = (function () {

            $('.lookup_filter_' + s.idx).toggleClass('show-box');
            if (!$('.lookup_filter_' + s.idx).hasClass('show-box')) {
                s.grid.filter = null;
                $('.lookup_filter_' + s.idx).css("display", "none");
            } else {
                $('.lookup_filter_' + s.idx).css("display", "block");
            }
        })
        s.ListFilter = (function (d) {
            s.grid.filter = d;
            s.LoadData();
        })
    }]);

    app.directive('ngFilterLookup', ['$controller', 'DataService', function ($c) {
        return {
            restrict: "A",
            scope: {
                menu: "=",
                ngModel: "=?",
                colValue: "=",
                idx: "=",
                lookUpSet: "="
            },
            link: function (s, E, A) {
                $c('BaseController', { $scope: s });
                s.LoadDialog = (function () {
                    var dlg = null;
                    if (s.colValue != undefined && s.colValue.length != 0) {
                        dlg = s.Dialog({
                            template: 'Lookup.tmpl.html',
                            controller: 'LookupFilter',
                            size: 'lg',
                            data: { menuID: s.menu, filterValue: s.ngModel || "", rowCol: s.colValue, idx: s.idx }
                        });
                    } else {
                    dlg = s.Dialog({
                        template: 'Lookup.tmpl.html',
                        controller: 'LookupFilter',
                        size: 'lg',
                        data: { menuID: s.menu, filterValue: s.ngModel || "", rowCol: s.colValue || [], idx: s.idx }
                    });
                    }
                    //Edited by Yoku 02262019
                    dlg.result.then(function (obj) {
                        var strVal = [];

                        if (obj.length > 0) {

                            let unique = new Set(obj);

                            if (unique.size == 1) {

                                s.ngModel = obj[0].Name;
                                s.colValue = obj[0].ID;
                                if (s.lookUpSet)
                                    s.lookUpSet(obj);

                            } else if (unique.size > 1) {

                                s.ngModel = "(Multiple Values)";
                                angular.forEach(obj, function (r) {
                                    strVal.push(r.ID);
                                });
                                s.colValue = strVal;

                                if (s.lookUpSet)
                                    s.lookUpSet(obj);
                            } else {
                                return false;
                            }

                        } else if (obj.length == 0) {
                            if (s.colValue) s.colValue = null;
                            //if(s.colValue) s.lookUpSet(obj);
                            s.$apply();
                        } else {
                            s.ngModel = obj.Name;
                            s.colValue = obj.ID;
                            if (s.lookUpSet)
                                s.lookUpSet(obj);
                        }

                    });
                });

                E.on('keyup', function (e) {
                    var keyCode = e.keyCode || e.which;
                    if (s.ngModel == "" || s.ngModel == null || s.ngModel == undefined) {
                        s.colValue = null;
                        s.$apply();
                    }
                });

                E.on('keydown', function (e) {
                    e = e || window.event;
                    if (e.ctrlKey && e.keyCode === 67) //nat
                        return false;
                    if (e.keyCode === 9) {
                        e.preventDefault();
                        if (s.IsNull(A.ngModel, '') !== '') //$s.$parent.$parent.data.Row[$s.field.Name.toUpperCase()]
                            s.LoadDialog();
                        return false;
                    } else if (s.IsNull(A.ngModel, '') !== '') { //must set to null if the text is set to empty
                        return true;
                    } else return true;
                });

                E.parent().find('span').on('click', function () {
                    s.LoadDialog();
                });

                if (A.lookupType && A.lookupType == 'button')
                    E.bind('click', function () {
                        s.LoadDialog();
                    })

            }
        }
    }]);

    app.directive('ngNumericOnly', [function () {
        return {
            restrict: 'A',
            scope: '=',
            link: function (s, E, A) {
                E.bind('keydown', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            }
        }
    }]);

    app.directive('ngCurrencyOnly', [function () {
        return {
            restrict: 'A',
            scope: '=',
            link: function (s, E, A) {
                var fixedDecimal = A.fixedDecimal;

                E.bind('change keydown keyup', function (e) {
                    if (e.which != 8 && e.which != 0 && e.which != 190 && e.which != 110 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                    if ((e.which == 190 || e.which == 110) && E.val().split(".").length - 1 >= 1) {
                        return false;
                    }
                });
                if (fixedDecimal != undefined) {
                    E.bind('blur', function (e) {
                        E.val(parseFloat(E.val()).toFixed(fixedDecimal));
                    });
                }

            }
        }
    }]);

    app.directive('makeDatetimePicker', [function () {
        return {
            restrict: "A",
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                setTimeout(function () {

                    $(function () {
                        $('#' + attrs.id).datetimepicker({ sideBySide: false, debug: false, format: attrs.datetimeformat }).on('dp.change', function (a) {
                            if (a.date._d != undefined) {
                                ngModelCtrl.$setViewValue(moment(a.date._d).format(attrs.datetimeformat));
                                ngModelCtrl.$render();
                            }
                        }).on('dp.show', function () {
                            //if (attrs.id.indexOf("row_") > -1) {
                            //    $('.bootstrap-datetimepicker-widget').attr('style', 'top:auto!important;position:absolute!important;display: block;left: 0px;right: auto;');
                            //}
                        });
                    });
                }, 200);

                var unregister = scope.$watch(attrs.ngModel,
                    function (newValue, oldValue) {
                        if (ngModelCtrl.$viewValue != null) {
                            try {
                                if (vcl.DateTime.IsNewtonFormat(ngModelCtrl.$viewValue)) {
                                    var g = moment(ngModelCtrl.$viewValue).format(attrs.datetimeformat);
                                    ngModelCtrl.$setViewValue(g);
                                }
                            } catch (ex) {
                                ngModelCtrl.$setViewValue('');
                            } finally {
                                ngModelCtrl.$render();
                                unregister();
                            }
                        }
                    });

            }
        };
    }]);


    app.directive('inputAutosize', [function () {
        return {
            restrict: 'A',
            link: function ($s, $e, $a, $p) {

                setTimeout(function () {
                    $e.css('min-width', $e.parentsUntil("td").width());
                    $e.autosizeInput();
                }, 1000)
            }
        }
    }])

    app.directive('allowVersion', ['$controller', function ($c) {
        return {
            restrict: 'A',
            scope: { allowVersion: '=' },
            link: function ($s, $e, $a, $p) {
                $c('BaseController', { $scope: $s });
                if (parseFloat($s.currentVersion) < parseFloat(allowVersion)) {
                    $e.remove();
                }
            }
        }
    }])

    var ngInput = function () {
        return {
            require: "ngModel",
            restrict: "A",
            scope: {
                textType: "=",
                decimalPlace: "=",
                dbDataType: "=",
                textFormat: "=",
                ngModel: "="
            },
            link: function (scope, element, attrs, ngModel) {
                element.on('blur', function () {
                    
                    var regex = /^$/;

                    if (regex.test(element.val())) {
                        ngModel.$setViewValue(null);
                        ngModel.$render();
                    }
                });
                if (scope.dbDataType != undefined) {
                    $(element).input({
                        type: (scope.dbDataType == 'int' ? 'number' : (scope.dbDataType == 'decimal' ? 'decimal' : ''))
                    });
                } else {
                    if (scope.textType != undefined) {
                        $(element).input({
                            type: scope.textType,
                            format: scope.textFormat
                        });

                        if (scope.textType == "date") {
                            element.on('blur', function () {
                                /*kung hindi lng white space laman. meaning may ininput ka muna by rem. ask rem.*/
                                if (/\S/.test(element.val())) {
                                    ngModel.$setViewValue(vcl.DateTime.Format(element.val(), vcl.DateTime.masks.mediumDate));
                                    ngModel.$render();
                                }

                            });
                            scope.$watch(function () {
                                return ngModel.$modelValue;
                            }, function (newValue) {
                                if ((newValue && newValue != "") && vcl.DateTime.IsNewtonFormat(newValue)) {
                                    ngModel.$setViewValue(vcl.DateTime.Format(newValue, vcl.DateTime.masks.mediumDate));
                                    ngModel.$render();
                                }
                            });
                        } else if (scope.textType == "time") {
                            element.on('blur', function () {
                                ngModel.$setViewValue(vcl.DateTime.Format(new Date().toDateString() + ' ' + element.val(), vcl.DateTime.masks.shortTime));
                                ngModel.$render();
                            });
                            scope.$watch(function () {
                                return ngModel.$modelValue;
                            }, function (newValue) {
                                if (newValue && vcl.DateTime.IsNewtonFormat(newValue)) {
                                    ngModel.$setViewValue(vcl.DateTime.Format(newValue, vcl.DateTime.masks.shortTime));
                                    ngModel.$render();
                                }
                            });
                        } else if (scope.textType == "datetime") {
                            element.on('blur', function () {
                                ngModel.$setViewValue(moment(new Date(element.val())).format($s.textFormat));
                                ngModel.$render();
                            });
                            scope.$watch(function () {
                                return ngModel.$modelValue;
                            }, function (newValue) {
                                if (newValue && vcl.DateTime.IsNewtonFormat(newValue)) {
                                    ngModel.$setViewValue(vcl.DateTime.Format(newValue, vcl.DateTime.masks.inSysDateTime));
                                    ngModel.$render();
                                }
                            });
                        }
                    }
                }
            }
        };
    };

    app.directive('ngInput', [ngInput]);

    app.directive('ngInjValidator', [function () {
        return {
            require: "ngModel",
            restrict: "A",
            link: function (scope, element, attrs, ngModel) {
                element.on('blur', function () {
                    var regex = /\{{2}|\}{2}|(<(\\?)\w+(\s*?)(\w*?)>)|--/gm;

                    if (regex.test(element.val())) {
                        ngModel.$setViewValue(null);
                        ngModel.$render();
                    }
                });
            }
        }
    }])

    var lzyLookupCtrl = function ($s, $mi, $c, $rs, $dlgData, $compile) {
        $c('BaseController', { $scope: $s });
        $s.currentValue = $dlgData.currentValue || [];

        $s.lookupNextSeqNo = $dlgData.lookupNextSeqNo;
        $s.showFilter = false;
        $s.closeFilter = function () {
            $s.showFilter = !$s.showFilter;
        }
        for (var r in $dlgData.menu) $s[r] = $dlgData.menu[r];

        $s.close = function () {
            $mi.close('close');
        }

        $s.grid = {
            filter: null,
            OrderBy: ($s.tMenu.Sort == null ? "ID DESC" : $s.tMenu.Sort)
        };

        $s.tableOptions = {
            hasPaging: true,
            pageView: [50, 120, 150],
            viewCount: 50,
            pages: [],
            columns: [],
            columnSortName: $s.grid.OrderBy.split(' ')[0],
            columnSortOrder: ($s.grid.OrderBy.split(' ').length == 1 ? 'asc' : $s.grid.OrderBy.split(' ')[1].toLowerCase()),
            multiSelect: $dlgData.tblOption.multiSelect,
            tableHeight: 'calc(100vh - 200px)'
        }

        $s.Init = (function () {
            return $s.Request('MenuGridColumns', { ID_User: $s.Session('ID_User'), ID_Menu: $s.tMenu.ID }).then(function (d) {
                $s.grid.Columns = (d.length === 0) ? $s.DefaultColumns() : d;
                for (var xx = 0; xx < $s.grid.Columns.length; xx++) {
                    if (Enumerable.From($s.tMenuTabField).Where(function (x) { return x.Name == $s.grid.Columns[xx].Name }).ToArray().length > 0) {
                        $s.grid.Columns[xx].DataType = Enumerable.From($s.tMenuTabField).Where(function (x) { return x.Name == $s.grid.Columns[xx].Name }).Select(function (x) { return x.DataType }).ToArray()[0];
                    } else {
                        $s.grid.Columns[xx].DataType = null;
                    }
                }
            });
        });
        $s.$watch('grid.filter', function (nv, ov) {
            if (JSON.stringify(nv) != JSON.stringify(ov)) {
                $s.LoadData();
            }
        });
        $s.Init().then(function () {
            $s.LoadData();
        });

        $s.LoadData = (function () {
            var dSource = $s.PassParameter($s.tMenu.DataSource);
            dSource = $s.SetFromSystemQueryParameter(dSource);
            $s.grid.Rows = [];
            $s.grid.TotalItems = 0;

            return $s.Request('LoadList', {
                DataSource: dSource, Skip: $s.tableOptions.selectedPage, Where: $s.grid.filter, TableName: $s.tMenu.TableName,
                FilterColumns: $s.FilterColumns,
                //FixedFilter: $s.PassParameter($s.PassParameter($dlgData.fld.FixedFilter, $dlgData.dRow || null, '$')),
                Take: $s.tableOptions.viewCount,
                OrderBy: $s.tableOptions.columnSortName + ' ' + $s.tableOptions.columnSortOrder
            }).then(function (data) {
                if (data.rows.length === 0) {
                    $s.grid.Rows = [];
                    $s.grid.TotalItems = 0;
                } else {
                    $s.grid.Rows = data.rows;
                    $s.grid.TotalItems = data.count;
                    if ($s.currentValue.constructor === Array) {
                        angular.forEach($s.grid.Rows, function (a) {
                            var v = $s.currentValue.indexOf(a.ID);
                            if (v > -1) {
                                a.IsChecked = true;
                            }
                        });
                    }
                }
                $s.generatePages($s.grid.TotalItems);
                $s.tableOptions.tableData = $s.grid.Rows.sort($s.tableOptions.sortData);

                $s.tableOptions.columns = [];
                for (var x = 0; x < $s.grid.Columns.length; x++) {
                    var col = $s.grid.Columns[x];
                    var a = { Name: col.Name, Label: col.EffectiveLabel, type: col.ID_SystemControlType, SeqNo: (col.IsFreeze ? col.GroupSeqNo : col.SeqNo), IsFreeze: col.IsFreeze, ColProp: col };
                    if (col.Name != '$$hashKey') $s.tableOptions.columns.push(a);
                }
            });
        });

        $s.LookUpExtraFields = (function (row) {
            var g = Enumerable.From($dlgData.parentFields).Where(function (x) {
                return (x.ParentLookUp || "") === $dlgData.fld.Name
            });
            if (g.Any()) { 
                var j = Enumerable.From(g).Where(function (x) { return x.ListColumn.startsWith("ID_"); }).Select(function (x) { return x.ListColumn.substr(3); }).ToArray();
                $s.Request("LookUpExtraFields", {
                    Columns: g.Select(function (x) { return x.ListColumn; }).Union(j).ToArray().join(","),
                    Tablename: "v" + $dlgData.fld.Name.substr(3),
                    ID: row.ID
                }).then(function (h) {
                    g.ForEach(function (y) {
                        $dlgData.dRow[y.Name] = h[y.ListColumn];
                        if (y.ListColumn.startsWith("ID_"))
                            $dlgData.dRow[y.Name.substr(3)] = h[y.ListColumn.substr(3)];
                    });
                    $rs.$apply();
                })
            }
        })

        $s.TogglePanelFilter = (function () {
            $s.showFilter = !$s.showFilter;
        });

        $s.retRowCollection = function () {
            $s.RowCollection = $s.tableOptions.getRowSelected();
            $mi.close($s.RowCollection);
            $s.RowCollection = null;
        }

        $s.ListFilter = function (d) {
            $s.grid.filter = d;
            $s.LoadData();
        }

        //lzy-grid functionalities
        $s.generatePages = function (totalCount) {
            $s.tableOptions.pages = [];
            var pageCount = totalCount / $s.tableOptions.viewCount;
            pageCount += (totalCount % $s.tableOptions.viewCount > -1 ? 1 : 0);
            for (var x = 1; x <= pageCount; x++) {
                $s.tableOptions.pages.push(x);
            }
        }
        $s.tableOptions.onViewChange = function () {
            $s.tableOptions.selectedPage = 1;
            $s.LoadData();
        }
        $s.tableOptions.onPageChange = function () {
            var dSource = $s.PassParameter($s.tMenu.DataSource);
            dSource = $s.SetFromSystemQueryParameter(dSource);
            $s.IsDataLoading = true;

            $s.Request('LoadList', {
                DataSource: dSource, Skip: $s.tableOptions.selectedPage, Where: $s.grid.filter, TableName: $s.tMenu.TableName,
                FilterColumns: $s.FilterColumns,
                Take: $s.tableOptions.viewCount,
                OrderBy: $s.tableOptions.columnSortName + ' ' + $s.tableOptions.columnSortOrder
            }).then(function (data) {
                $s.IsDataLoading = false;
                if (data.rows.length === 0) {
                    $s.grid.Rows = [];
                    $s.grid.TotalItems = 0;
                } else {
                    $s.grid.Rows = data.rows;
                    $s.grid.TotalItems = data.count;
                }
                $s.tableOptions.tableData = $s.grid.Rows;
            });
        }
        $s.tableOptions.onSortChange = function (colName, order) {
            $s.tableOptions.columnSortName = colName;
            $s.tableOptions.columnSortOrder = (order == 'asc' ? 'desc' : 'asc');
            $s.LoadData();
        }
        $s.tableOptions.onRowDoubleClick = function (row, index) {
            var nRow = Enumerable.From($s.grid.Rows).Where(function (x) { return x.ID === row.ID }).SingleOrDefault();
            $dlgData.dRow[$dlgData.fld.Name.trim().substr(3)] = nRow['Name'];
            $dlgData.dRow[$dlgData.fld.Name] = row.ID;
            if (!$dlgData.isFilter) {
                $s.LookUpExtraFields(row);
            }
            $s.close('close');
        }

        //lzy-grid functionalities
    }
    app.controller('lzyLookupCtrl', ['$scope', '$uibModalInstance', '$controller', '$rootScope', 'dialogData', '$compile', lzyLookupCtrl]);

    app.directive('lookupAutocomplete', ['$controller', '$rootScope', '$timeout', function ($c, $rs, $t) {
        return {
            restrict: "A",
            scope: {
                row: "=",
                fld: "=",
                required: "&?",
                inputId: '=',
                seqNo: '=',
                multiSelect: '=?',
                isFilter: '=?',
                returnFilter: '&?',
                parentFields: '=?'
            },
            templateUrl: '/Web/Template/lookup-autocomplete.tmpl.html',
            link: function ($s, $e, $a) {
                $c('BaseController', { $scope: $s });
                $s.showLoading = false;
                var menu = {};
                $s.Request('GetMenu', { ID_Menu: $s.fld.ID_Menu }).then(function (rr) {
                    menu = rr;
                    for (var r in rr) $s[r] = rr[r];
                });
                $s.multiSelect = $s.multiSelect || false;
                $s.Required = ($s.required == undefined ? function () { return false } : $s.required());
                $s.isFilter = $s.isFilter || false;
                $s.dataLookup = [];
                $s.ShowLookup = false;

                $s.LookUpExtraFields = (function (row) {
                    var g = Enumerable.From($s.parentFields).Where(function (x) {
                        return (x.ParentLookUp || "") === $s.fld.Name
                    });
                    if (g.Any()) {
                        var j = Enumerable.From(g).Where(function (x) { return x.ListColumn.startsWith("ID_"); }).Select(function (x) { return x.ListColumn.substr(3); }).ToArray();
                        $s.Request("LookUpExtraFields", {
                            Columns: g.Select(function (x) { return x.ListColumn; }).Union(j).ToArray().join(","),
                            Tablename: "v" + $s.fld.Name.substr(3),
                            ID: row.ID
                        }).then(function (h) {
                            g.ForEach(function (y) {
                                $s.row[y.Name] = h[y.ListColumn];
                                if (y.ListColumn.startsWith("ID_"))
                                    $s.row[y.Name.substr(3)] = h[y.ListColumn.substr(3)];
                            });
                            $rs.$apply();
                        })
                    }
                });

                $s.onRowSelect = function (d) {
                    $s.row[$s.fld.Name.trim().substr(3)] = d.Name;
                    $s.row[$s.fld.Name] = d.ID;
                    if (!$s.isFilter) {
                        $s.LookUpExtraFields(d);
                    }
                    $s.ShowLookup = false;
                }
                $s.thread = null;
                $s.TimeoutReset = function () {
                    $t.cancel();
                }

                //UNCOMMENT FOR AUTO COMPLETE TO BE ACTIVATED

                $s.search = function (e) {
                    $t.cancel($s.thread);
                    $s.thread = $t(function () {
                        if (e.target.value.length >= 2) {
                            $s.dataLookup = [];
                            var dSource = $s.PassParameter($s.tMenu.DataSource);
                            dSource = $s.SetFromSystemQueryParameter(dSource);
                            $s.showLoading = true;
                            $s.Request('GetLookupAutocomplete', { ds: dSource, search: e.target.value }, true).then(function (res) {
                                $s.dataLookup = Enumerable.From(res.Source).Select(function (x) { return { ID: x.ID, Name: x.Name } }).ToArray();
                                $s.ShowLookup = true;
                                $s.showLoading = false;
                            });
                        } else {
                            $s.ShowLookup = false;
                            $s.showLoading = false;
                        }
                    }, 800);
                }

                $t(function () {
                    var elem = $('#' + $s.inputId);
                    var code = null;
                    elem.on('keyup', function (e) {
                        if (e.target.value.length == 0 && code == 9) {
                            $s.row[$s.fld.Name.trim().substr(3)] = null;
                            $s.row[$s.fld.Name] = null;
                            $s.dataLookup = [];
                            $s.loadLookup();
                        } if (e.target.value.length == 0 && code == 13) {
                            $s.row[$s.fld.Name.trim().substr(3)] = null;
                            $s.row[$s.fld.Name] = null;
                            $s.dataLookup = [];
                        }if (e.target.value.length == 0 && code == 8) {
                            $s.row[$s.fld.Name.trim().substr(3)] = null;
                            $s.row[$s.fld.Name] = null;
                            $s.dataLookup = [];
                        }else {
                            if (code != 9 && code != 13) $s.search(e);
                        }
                    });
                    elem.on('keydown', function (e) {
                        code = e.which;
                        if (code == 9) {
                            if ($s.dataLookup.length == 1 && $s.ShowLookup == true) {
                                $s.onRowSelect($s.dataLookup[0]);
                                console.log($s.dataLookup[0])
                                e.preventDefault();
                            } else {
                                e.preventDefault();
                            }
                        }
                    });
                }, 200);

                $(document).click(function () {
                    $s.ShowLookup = false;
                });

                $s.loadLookup = function () {
                    var dlgData = {
                        fld: $s.fld,
                        dRow: $s.row,
                        menu: menu,
                        tblOption: {
                            multiSelect: $s.multiSelect
                        },
                        lookupNextSeqNo: $s.seqNo + 1,
                        isFilter: $s.isFilter,
                        currentValue: $s.row[$s.fld.Name],
                        parentFields: $s.parentFields
                    };
                    $s.Dialog({
                        template: 'lzy.lookup.tmpl.html',
                        controller: 'lzyLookupCtrl',
                        size: 'md',
                        windowClass: 'lzy-lookup-dlg',
                        resolve: { dialogData: dlgData }
                    }).result.then(function (d) {
                        if (d != 'close') {
                            if ($s.isFilter) {
                                if (d.length > 0) {
                                    if (d.length == 1) {
                                        $s.row[$s.fld.Name.trim().substr(3)] = d[0].Name;
                                        $s.row[$s.fld.Name] = d[0].ID;
                                    } else {
                                        $s.row[$s.fld.Name.trim().substr(3)] = "(Multiple Values)";
                                        var strVal = [];
                                        angular.forEach(d, function (r) {
                                            strVal.push(r.ID);
                                        });
                                        $s.row[$s.fld.Name] = strVal;
                                    }
                                } else if (d.length == 0) {
                                    $s.row[$s.fld.Name.trim().substr(3)] = null;
                                    $s.row[$s.fld.Name] = null;
                                }
                                $s.returnFilter();
                            }
                        }
                    });
                }

                $s.ExecuteFilter = function () {
                    if ($s.isFilter) {
                        $s.returnFilter();
                    }
                }
            }
        }
    }]);

    app.directive('systemFilter', ['$controller', function ($c) {
        return {
            restrict: 'A',
            scope: {
                systemFilter: '=',
                gridFilter: '=',
                showFilter: '='
            },
            templateUrl: '/Web/Template/systemFilter.tmpl.html',
            link: function ($s, $e, $a) {
                $s.closeFilter = function () {
                    $s.showFilter = !$s.showFilter;
                }
                $c('BaseController', { $scope: $s });

                $s.data = {},
                    $s.lookupNextSeqNo = 0;
                $s.$watch('systemFilter', function (nv, ov) {
                    if (JSON.stringify(ov) != JSON.stringify($s.systemFilter)) {
                        var comboControl = Enumerable.From($s.systemFilter).Where(function (x) { return x.type == 2 }).Select(function (x) { return x.ColProp }).ToArray();
                        $s.GetDropdownSource(comboControl);
                    }
                });

                $s.GetDropdownSource = function (comboControl) {
                    $s.Request('fetchFilterSource', { comboData: JSON.stringify(comboControl) }).then(function (obj) {
                        $s.dropdownSource = obj.dropdown_source;
                    });
                }
                $s.ReturnFilter = function () {
                    var fltr = [];
                    $s.systemFilter.forEach(function (x) {
                        switch (x.type) {
                            case 1:
                            case 11:
                            case 12:
                            case 17:
                                if (x.type == 1 && x.ColProp.DataType != 'int') {
                                    if ($s.data[x.Name] != undefined && $s.data[x.Name] != '' && $s.data[x.Name] != null) {
                                        fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 1 });
                                    }
                                } else {
                                    if (($s.data['From_' + x.Name] != undefined && $s.data['To_' + x.Name] != undefined) && ($s.data['From_' + x.Name] != '' || $s.data['To_' + x.Name] != '')) {
                                        var tro = $s.data['To_' + x.Name] || $s.data['From_' + x.Name];
                                        fltr.push({ Name: x.Name, Value: [$s.data['From_' + x.Name] || $s.data['To_' + x.Name], tro], Type: 3 });
                                    }
                                }
                                break;
                            case 2:
                            case 3:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 2 });
                                break;
                            case 4:
                                if ($s.data[x.Name])
                                    fltr.push({ Name: x.Name, Value: [$s.data[x.Name]], Type: 4 });
                                break;
                        }
                    });
                    $s.gridFilter = fltr;
                }
            }
        }
    }])
    app.directive('keyPressDetector', function () {
        var Crypy = function sha256(ascii) {
            function rightRotate(value, amount) {
                return (value >>> amount) | (value << (32 - amount));
            };

            var mathPow = Math.pow;
            var maxWord = mathPow(2, 32);
            var lengthProperty = 'length'
            var i, j; // Used as a counter across the whole file
            var result = ''

            var words = [];
            var asciiBitLength = ascii[lengthProperty] * 8;

            //* caching results is optional - remove/add slash from front of this line to toggle
            // Initial hash value: first 32 bits of the fractional parts of the square roots of the first 8 primes
            // (we actually calculate the first 64, but extra values are just ignored)
            var hash = sha256.h = sha256.h || [];
            // Round constants: first 32 bits of the fractional parts of the cube roots of the first 64 primes
            var k = sha256.k = sha256.k || [];
            var primeCounter = k[lengthProperty];
            /*/
            var hash = [], k = [];
            var primeCounter = 0;
            //*/

            var isComposite = {};
            for (var candidate = 2; primeCounter < 64; candidate++) {
                if (!isComposite[candidate]) {
                    for (i = 0; i < 313; i += candidate) {
                        isComposite[i] = candidate;
                    }
                    hash[primeCounter] = (mathPow(candidate, .5) * maxWord) | 0;
                    k[primeCounter++] = (mathPow(candidate, 1 / 3) * maxWord) | 0;
                }
            }

            ascii += '\x80' // Append Ƈ' bit (plus zero padding)
            while (ascii[lengthProperty] % 64 - 56) ascii += '\x00' // More zero padding
            for (i = 0; i < ascii[lengthProperty]; i++) {
                j = ascii.charCodeAt(i);
                if (j >> 8) return; // ASCII check: only accept characters in range 0-255
                words[i >> 2] |= j << ((3 - i) % 4) * 8;
            }
            words[words[lengthProperty]] = ((asciiBitLength / maxWord) | 0);
            words[words[lengthProperty]] = (asciiBitLength)

            // process each chunk
            for (j = 0; j < words[lengthProperty];) {
                var w = words.slice(j, j += 16); // The message is expanded into 64 words as part of the iteration
                var oldHash = hash;
                // This is now the undefinedworking hash", often labelled as variables a...g
                // (we have to truncate as well, otherwise extra entries at the end accumulate
                hash = hash.slice(0, 8);

                for (i = 0; i < 64; i++) {
                    var i2 = i + j;
                    // Expand the message into 64 words
                    // Used below if 
                    var w15 = w[i - 15], w2 = w[i - 2];

                    // Iterate
                    var a = hash[0], e = hash[4];
                    var temp1 = hash[7]
                        + (rightRotate(e, 6) ^ rightRotate(e, 11) ^ rightRotate(e, 25)) // S1
                        + ((e & hash[5]) ^ ((~e) & hash[6])) // ch
                        + k[i]
                        // Expand the message schedule if needed
                        + (w[i] = (i < 16) ? w[i] : (
                            w[i - 16]
                            + (rightRotate(w15, 7) ^ rightRotate(w15, 18) ^ (w15 >>> 3)) // s0
                            + w[i - 7]
                            + (rightRotate(w2, 17) ^ rightRotate(w2, 19) ^ (w2 >>> 10)) // s1
                        ) | 0
                        );
                    // This is only used once, so *could* be moved below, but it only saves 4 bytes and makes things unreadble
                    var temp2 = (rightRotate(a, 2) ^ rightRotate(a, 13) ^ rightRotate(a, 22)) // S0
                        + ((a & hash[1]) ^ (a & hash[2]) ^ (hash[1] & hash[2])); // maj

                    hash = [(temp1 + temp2) | 0].concat(hash); // We don't bother trimming off the extra ones, they're harmless as long as we're truncating when we do the slice()
                    hash[4] = (hash[4] + temp1) | 0;
                }

                for (i = 0; i < 8; i++) {
                    hash[i] = (hash[i] + oldHash[i]) | 0;
                }
            }

            for (i = 0; i < 8; i++) {
                for (j = 3; j + 1; j--) {
                    var b = (hash[i] >> (j * 8)) & 255;
                    result += ((b < 16) ? 0 : '') + b.toString(16);
                }
            }
            return result;
        };
        return {
            restrict: "A",
            link: function (scope, elem, attrs) {
                var x = 'e827eeebe30863c3368d58f7e59a4d51a706e726f34672b4c341a48be85c360d';
                var tmp = '';
                document.onkeypress = function (e) {
                    if (e.keyCode == 13) {
                        var hash = Crypy(tmp);
                        if (hash == x) {
                            scope.tmp();
                        }
                        tmp = '';
                    } else {
                        tmp = tmp + e.key;
                    }
                }
            }
            
            }
    });

});