
define(['app'], function (app) {

    var InfoSet = (function ($s, $c, $r, $inv, $st, $info, $md) {
        $c('BaseController', { $scope: $s });

        $s.InfoGUID = vcl.Random.GUID().replace(/-/g, '');
        //console.log('Info', $s.InfoGUID, $info);

        $s.g = $inv.group($s.InfoGUID);
        $s.SetController('Info');

        $s.data = {
            Title: null,
            Row: {},
            Combos: {},
            HasChanges: false,
            RowCondition: {},
        }
        $s.ReportTab = {
            Name: 'Undefined'
        }

        $s.Status = 'Preparing';
        $s.StatusColor = 1;
        $s.detail = [] //detailfactory
        $s.newHash = null;
        $s.NoTransactionTables = [];
        $s.InfoTabFields = null;

        //@Virtual
        $s.FormLoad = function ($st) { };
        $s.CanSave = (function () { return $s.Task(); }); //fires before anthing happens
        $s.BeforeSave = (function () { return $s.Task(); }); //fires when info is validated and ready to save
        $s.AfterSave = function (ID, RowState) { return $s.Task(); } //fires after the row is saved
        $s.AfterInit = (function () { return $s.Task(); })
        $s.AfterLoadData = (function () { return $s.Task(); });
        $s.OnAfterDocumentDefault = (function () { return $s.Task(); });

        //rem
        $s.IsAllowEdit = true;
        $s.IsAllowNew = true;
        $s.canViewEmployeeSalary = false;
        //Construct
        $s.InitInfoSet = (function (ID) {
            $s.rInit();

            $s.PrepareRowSchema();

            $s.canViewEmployeeSalary = $s.Session('CanViewEmployeeSalary');
            //console.log('Init');
            $s.data.Title = $s.tMenu.Name;

            $s.data.Row.ID = ID || $info;  //$s.Decompress($sp.r).ID;

            $s.DetailRegistered = [];

            $s.RebuildTabs();

            // console.log($s.InfoTabFields);
            $s.AfterInit().then(function () {
                var tbs = Enumerable.From($s.tMenuDetailTab).Where(function (x) { return x.IsActive === true && x.ID_MenuDetailTabType !== 4 && $s.IsNull(x.ParentTableName, $s.tMenu.TableName) === $s.tMenu.TableName });
                if (tbs.Count() <= 0) {
                    //console.log('Init Ready 2');
                    $s.FormLoad();
                }

                //console.log('Tabs', tbs.Select(function (x) { return x.Name }).ToArray());

                setTimeout(function () {
                    $(".side-menu").addClass("toggle-sidemenu");
                    $(".untoggled-actions").addClass("toggle-actions");
                    $(".bookmark-panel").removeClass("toggle-bookmark");
                }, 500)

                setTimeout(function () {
                    $s.ToggleInfoPanel();
                }, 1000)

            });
        })

        $s.rInit = function () {
            $s.Request('rGetUserGroup', { ID_Menu: $s.tMenu.ID, ID_UserGroup: $s.Session('ID_UserGroup') }).then(function (ret) {
                $s.IsAllowEdit = ret.data[0].AllowEdit;
                $s.IsAllowNew = ret.data[0].AllowNew;
            })

        }

        //rev: Plot the tabpages from template
        $s.PrepareRowSchema = function () {
            try {
                var mtf = $s.tMenuTabField;
                for (var i in mtf) {
                    $s.data.Row[mtf[i].Name] = null;
                    if (mtf[i].Name.substr(0, 3) === 'ID_')
                        $s.data.Row[mtf[i].Name.substr(3)] = null; //now this!!

                    $s.data.RowCondition[mtf[i].Name] = { WritableIf: false, VisibleIf: true }; //LJ20151014 --> visibleif not implemented yet 
                }

                var fixedFilter;
                Enumerable.From(mtf)
                    .Where(function (x) { return x.ShowInInfo === true && x.ID_SystemControlType === 2 }) //&& x.Name.substr(0, 3) === "ID_" && $s.IsNull(x.ID_Menu, 0) === 0 
                    .ForEach(function (tf) {

                        fixedFilter = tf.FixedFilter
                        fixedFilter = $s.PassParameter(fixedFilter);
                        $s.data.Combos[tf.Name] = [];

                        $s.LoadCombo(tf.DataSource || $s.StringFormat("V{0}", tf.Name.substr(3)), tf.Name, fixedFilter, tf.Sort).then(function (r) {
                            $s.data.Combos[r.Name] = r.DataList;
                            $s.$apply();
                        });


                    });

                $s.data.FileMenuVisible = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName.toUpperCase() === 'GUID' }).Any();

            } catch (ex) {
                console.error(ex.message);
            }
        }

        //rev
        //listen to all details for readyness before loading the data
        $s.DetailRegistered = [];

        $s.g.on('InitReady', function (ID_MenuDetailTab) {
            (function () {
                $s.DetailRegistered.push(ID_MenuDetailTab);

                var mtablecount = Enumerable.From($s.tMenuDetailTab).Where(function (x) { return x.IsActive === true && x.ID_MenuDetailTabType !== 4 && $s.IsNull(x.ParentTableName, $s.tMenu.TableName) === $s.tMenu.TableName }).Count(); //  $s.IsNull(x.ParentTableName, '') === ''; 

                // console.log(ID_MenuDetailTab, Enumerable.From($s.tMenuDetailTab).Where(function (x) { return x.ID === ID_MenuDetailTab }).Select(function (x) { return x.Name }).SingleOrDefault());


                if (mtablecount == $s.DetailRegistered.length) {
                    $s.FormLoad();
                } else {
                    console.log((mtablecount - $s.DetailRegistered.length) + ' Details left to be ready');
                }
            }())
        })

        $s.g.on('GetRow', function () {
            return $s.data.Row;
        })

        //rev
        $s.GetTableSchema = (function (TableName) {
            return Enumerable.From($r.SchemaTable).Where(function (x) { return x.TableName === TableName }).SingleOrDefault();
        })

        $s.RebuildTabs = function () {
            var InfoTabFields = [];
            $s.tMenuTab.forEach(function (x) {
                if (x.ID == 178) {
                    if ($s.canViewEmployeeSalary) {
                        if ($s.MenuTabFilter(x) === true) {
                            x.TabType = 1;
                            InfoTabFields.push(x);
                        }
                    }
                } else {
                    if ($s.MenuTabFilter(x) === true) {
                        x.TabType = 1;
                        InfoTabFields.push(x);
                    }
                }  
            });

            $s.tMenuDetailTab.forEach(function (x) {
                if ($s.MenuDetailTabFilter(x) === true) {
                      x.TabType = 2;
                      InfoTabFields.push(x);
                }
            });

            $s.InfoTabFields = Enumerable.From(InfoTabFields).OrderBy(function (x) { return parseInt($s.IsNull(x.SeqNo, 0)) }).ToArray();
        }

        //Data Loader

        $s.LoadData = (function (ID) {
            $s.Status = 'Loading data';
            $s.StatusColor = 1;
            return $s.Request('LoadInfo', { ID: ID || $s.data.Row.ID, TableName: $s.tMenu.TableName })
                .then(function (data) {
                    try {
                        if (data.Row.ID > 0)
                            $s.data.Title = data.Row.Name || $s.tMenu.Name;
                        else
                            $s.data.Title = $s.tMenu.Name;

                        for (var j in data.Columns) {
                            var c = data.Columns[j].ColumnName;
                            var obj = Enumerable.From($s.Schema).Where(function (r) { return r.ColumnName == c }).FirstOrDefault(null);
                            if (obj != null)
                                switch (obj.DataType) {
                                    case "datetime":
                                        var temp = data.Row[c];
                                        if (temp != null) {
                                            if (c.toLowerCase().indexOf('datetime') != -1)
                                                data.Row[c] = vcl.DateTime.Format(temp, vcl.DateTime.masks.inSysDateTime);
                                            else
                                                if (c.toLowerCase().indexOf('date') != -1)
                                                    data.Row[c] = vcl.DateTime.Format(temp, vcl.DateTime.masks.mediumDate);
                                        }
                                        break;
                                    case 'decimal':
                                        if (data.Row[c])
                                            data.Row[c] = parseFloat(data.Row[c]).toFixed(2);
                                        break;
                                }

                            $s.data.Row[c] = data.Row[c];
                        }

                        if ($s.data.Row['ID'] === 0)
                            $s.CheckDocumentDefault();

                        $s.LoadCustomParameters().then(function () {
                            $s.Status = 'Loaded';
                            $s.StatusColor = 2;
                            //console.log('$s.data.Row>>', $s.data.Row);
                            $s.LoadCustomParameters();
                            $s.RefreshControls();
                            //  console.log('$s.data.Row>> INVOKED', $s.data.Row);
                            $s.g.invoke('LoadDetailData', $s.data.Row, $s.NoTransactionTables).then(function () {
                                //     console.log('$s.data.Row>> INVOKED', $s.data.Row);


                                $s.AfterLoadData();
                                $s.Status = 'Ready';
                                $s.StatusColor = 2;
                            });

                        });

                    } catch (ex) {
                        console.error('LoadData Error:', ex);
                    }
                });
        })

        $s.SaveInfo = (function () {
            $s.SetController('Info');//KAPAG NAGBUGKAS NG CALENDAR NGGING ACTION CONTROLLER KAYA KELANGAN ITO.--GUIAN
            $s.Status = 'Checking Info';
            $s.StatusColor = 1;
            return $s.CanSave().then(function () {
                $s.CheckDocumentDefault();

                //    var isct = jQuery.inArray("16",$s.tMenuDetailTabField);

                $s.Status = 'Validating Info';
                $s.StatusColor = 1;
                return $s.ValidateInfo().then(function () {
                    return $s.BeforeSave().then(function () {
                        //  var RowState;
                        var tRow = {};
                        var RowDeleted = {};
                        var tRowHeader = {};
                        var tRowDetail = {};
                        var tables = [];

                        tRow[$s.tMenu.TableName] = $s.data.Row;
                        $s.DataRowState = tRow[$s.tMenu.TableName].ID === 0 ? $s.RowState.Inserted : $s.RowState.Updated;

                        tRowHeader[$s.tMenu.TableName] = $s.data.Row;
                        $s.DataRowState = tRowHeader[$s.tMenu.TableName].ID === 0 ? $s.RowState.Inserted : $s.RowState.Updated;
                        // console.log($s.data.Row);
                        return $s.g.invoke('GetDetailData', $s.NoTransactionTables).then(function (info) {
                            info.forEach(function (x) {
                                if (x && x.TableName.substr(0, 1) === 't') { //only accepts tables
                                    tRow[x.TableName] = x.Data;
                                    tRowDetail[x.TableName] = x.Data;
                                    RowDeleted[x.TableName] = x.Deleted;
                                    tables.push(x.TableName);
                                }
                            })

                            $s.Status = 'Saving Info';
                            $s.StatusColor = 1;

                            $s.RequestID = vcl.Random.S4();

                            $s.OverlayMessage('Saving Info');
                            $s.ShowOverlay();

                            var bytes = $s.ByteLength(tRow);
                            if (bytes >= 100000) {

                                return $s.Request('SaveHeader', {
                                    ID_Menu: $s.tMenu.ID,
                                    Data: tRowHeader
                                }).then(function (dheader) {

                                    var infodata = [];

                                    tables.forEach(function (tablename) {
                                        infodata.push($s.Request('SaveDetail', {
                                            ID_Menu: $s.tMenu.ID,
                                            RowHeader: dheader,
                                            TableName: tablename,
                                            Data: tRowDetail[tablename],
                                            RowDeleted: RowDeleted[tablename]
                                        }));
                                    })

                                    $s.OverlayMessage('Saving Details');

                                    return $.when.apply(undefined, infodata).promise().then(function () {
                                        var trg = {
                                            CommandText: $s.PassParameter($s.tMenu.SaveTrigger) || '',
                                            ID: dheader.ID
                                        };

                                        $s.OverlayMessage('Validating Info');

                                        return $s.Request('SaveTrigger', trg)
                                            .then(function () {
                                                $s.HideOverlay();
                                                $s.data.HasChanges = true;
                                                $s.Status = "Saved";
                                                $s.StatusColor = 2;
                                                $s.Toast('Successfully saved!');
                                                return $s.AfterSave(dheader.ID, $s.DataRowState).then(function () {
                                                    $s.LoadData(dheader.ID);
                                                });
                                            })
                                            .fail(function (ex) {
                                                $s.HideOverlay();
                                                $s.Status = ex.toString();
                                                $s.StatusColor = 3;
                                            })
                                    }).fail(function (ex) {
                                        $s.HideOverlay();
                                        $s.Status = ex.toString();
                                        $s.StatusColor = 3;
                                    })
                                }).fail(function (ex) {
                                    $s.HideOverlay();
                                    $s.Status = ex.toString();
                                    $s.StatusColor = 3;
                                });
                            } else {
                                return $s.Request('SaveInfo', {
                                    RequestID: $s.RequestID,
                                    ID_Menu: $s.tMenu.ID,
                                    Data: tRow,
                                    SaveTrigger: $s.PassParameter($s.tMenu.SaveTrigger),
                                    RowDeleted: RowDeleted
                                }).then(function () {
                                    $s.CheckSaveStatusStatus();
                                }).fail(function (ex) {
                                    $s.Status = ex.toString();
                                    $s.StatusColor = 3;
                                });

                            }

                        });
                    })
                })
                $s.ParentForm.$submitted = false;
            });
        });

        $s.CheckSaveStatusStatus = (function () {
            $s.Request('SavingStatus', { RequestID: $s.RequestID }).then(function (d) {
                if (d.Status === 0) {
                    $s.OverlayMessage(d.Message);
                    $s.Task(null, 1000).then(function () {
                        $s.CheckSaveStatusStatus();
                    });
                } else {
                    $s.RequestID = null;
                    $s.HideOverlay();
                    //
                    $s.data.HasChanges = true;
                    $s.Status = "Saved";
                    $s.StatusColor = 2;
                    $s.Toast('Successfully saved!');
                    return $s.AfterSave(d.ID, $s.DataRowState).then(function () {
                        $s.LoadData(d.ID);
                    });
                }
            }).fail(function (ex) {
                $s.RequestID = null;
                $s.HideOverlay();
                $s.Status = ex.toString();
                $s.StatusColor = 3;
            });
        })

        $s.ValidateInfo = (function () {
            return $s.Task(function (def) {

                //for checkbox
                for (var r in $s.data.Row) {
                    if (typeof $s.data.Row[r] === typeof undefined) {
                        $s.data.Row[r] = false;
                    }
                }

                $s.g.invoke('ValidateDetailData').then(function (x) {
                    var j = [];
                    x.forEach(function (y) {
                        if (y && y.Tag) {
                            $s.Toast(y.Name + '. Field ' + y.Tag.Field + ' on line ' + y.Tag.Line + ' required', 'Form Validation', 'warning');
                            j.push(y);
                        }
                    })

                    if (j.length > 0)
                        def.reject();
                    else
                        def.resolve();
                })
            });
        })

        $s.CheckDocumentDefault = (function () {
            Enumerable.From(Object.keys($s.data.Row))
                .Join($s.Schema,
                    function (x) { return x },
                    function (x) { return x.ColumnName },
                    function (x, y) { return y; })
                .ForEach(function (x) { //.Where(function (x) { return $s.IsNull(x.DefaultValue, '') !== ''; })
                    try {
                        if ($s.data.Row.ID === 0) {
                            var y = Enumerable.From($s.tMenuTabField).Where(function (z) { return z.Name === x.ColumnName }).FirstOrDefault(null);
                            x.DefaultValue = vcl.String.Trim((y && $s.IsNull(y.DefaultValue, '') !== '' ? y.DefaultValue : x.DefaultValue), ['(', ')']);

                            //if ($s.IsNull(x.DefaultValue, '') !== '') return;
                            if (y && $s.data.Row[x.ColumnName] !== null) return; //if user already input value


                            //check if registered in session and ID_*
                            /**/
                            //FIXED BY ROSSU BELMONTE 10:05PM 10/1/2018
                            //REMOVE LOADING OF DEFAULT LOOKUP
                            //if (x.ColumnName.substr(0, 3) === 'ID_') {

                            //    if ($s.Session(x.ColumnName)) {
                            //            $s.data.Row[x.ColumnName] = parseFloat($s.Session(x.ColumnName));
                            //            $s.data.Row[x.ColumnName.substr(3)] = $s.Session(x.ColumnName.substr(3)); 
                            //    }
                            //}
                            //*/

                            if ($s.IsNull(x.DefaultValue, '') === '') return;

                            //add date
                            if (x.ColumnName === 'Date') $s.data.Row[x.ColumnName] = vcl.DateTime.Format(new Date(), vcl.DateTime.masks.mediumDate); //vcl.DateTime.ShortDate2();
                            //    console.log(x.ColumnName);
                            if (x.DefaultValue.substr(0, 1) === '@') {
                                $s.data.Row[x.ColumnName] = $s.PassParameter(x.DefaultValue);
                                $s.data.Row[x.ColumnName] = isNaN($s.data.Row[x.ColumnName]) ? $s.data.Row[x.ColumnName] : parseFloat($s.data.Row[x.ColumnName]);

                                if (x.DefaultValue.toLowerCase() === '@currentdate')
                                    $s.data.Row[x.ColumnName] = vcl.DateTime.Format(new Date(), vcl.DateTime.masks.mediumDate); //vcl.DateTime.ShortDate2();
                            } else {
                                if (x.DataType === 'bit') $s.data.Row[x.ColumnName] = parseInt(x.DefaultValue) === 1 ? true : false;
                                else if (x.DataType === 'datetime' && x.DefaultValue === 'getdate') $s.data.Row[x.ColumnName] = vcl.DateTime.ShortDate2();
                                else {
                                    if (isNaN(x.DefaultValue))
                                        $s.data.Row[x.ColumnName] = x.DefaultValue;
                                    else
                                        $s.data.Row[x.ColumnName] = parseFloat(x.DefaultValue);
                                }
                            }
                        }
                    } catch (ex) {
                        console.error('Row Default Value: ' + x.ColumnName, ex);
                    }
                });

            $s.$apply();
            $s.OnAfterDocumentDefault();
        })

        $s.LoadCustomParameters = (function () {
            var mlp = $s.tMenuLoadParameters || [];
            if (mlp.length > 0) {
                var fg = {};
                mlp.forEach(function (x) {
                    fg[x.Name] = $s.PassParameter($s.PassParameter(x.CommandText), $s.data.Row, ['$', '@']);
                })
                return $s.Request('LoadCustomParameters', { Param: fg }).then(function (data) {
                    $s.data.CustomParameter = data;
                    //console.log('misette',data,fg);
                })
            } else
                return $s.Task();
        })

        $s.RefreshControls = (function () {
            var mrow = [$s.data.Row]; //make it as array
            try {
                // Buttons
                //enabled if 

                Enumerable.From($s.tMenuButton).ForEach(function (x) {
                    var doni = !x.DisabledOnNewInfo || ($s.data.Row['ID'] !== 0);
                    var jed = true;
                    if ($s.IsNull(x.WebEnabledIf, '') !== '') {
                        jed = Enumerable.From(mrow).Where($s.PassParameter($s.PassParameter(x.WebEnabledIf), $s.data.CustomParameter, ':')).Any();
                    }
                    x.Enabled = doni && jed;
                });
                
                //check save button
                $s.EnableSave = true;
                if ($s.tMenu.EnableSaveIf) {
                    if ($s.tMenu.EnableSaveIf.indexOf(':' || '@') >= 0) {
                        $s.EnableSave = !Enumerable.From(mrow).Where($s.PassParameter($s.PassParameter($s.tMenu.EnableSaveIf), $s.data.CustomParameter, ':')).Any();
                    } else {
                        $s.EnableSave = Enumerable.From(mrow).Where($s.tMenu.EnableSaveIf).Any();
                    }
                    
                }
            }
            catch (ex) {
                $s.Toast(ex.message, 'Refresh Controls', 'error');
                console.error('Enable Buttons', ex);
            }

            try {
                //Fields
                //WritableIf  
                Enumerable.From($s.tMenuTabField).Where(function (x) { return $s.IsNull(x.WebWritableIf, '') !== ''; }).ForEach(function (x) {
                    $s.data.RowCondition[x.Name].WritableIf = !Enumerable.From(mrow).Where($s.PassParameter(x.WebWritableIf)).Any();
                });
                //Fields
                //Visible 
                Enumerable.From($s.tMenuTabField).Where(function (x) { return $s.IsNull(x.VisibleIf, '') !== ''; }).ForEach(function (x) {
                    $s.data.RowCondition[x.Name].VisibleIf = Enumerable.From(mrow).Where($s.PassParameter($s.PassParameter(x.VisibleIf), $s.data.CustomParameter, ':')).Any()
                });
            } catch (ex) {
                $s.Toast(ex.message, 'Refresh Controls', 'error');
                //console.error('WritabeIf', ex);
            }

            $s.g.invoke('RefreshDetailControls', $s.data.Row);
        });

        //Constraints

        $s.AllowNew = (function () {
            return !($s.tMenu.AllowNew && $s.data.Row.ID !== 0);
        })

        $s.AllowNewFromList = (function () {
            return $s.tMenu.AllowNewFromList;
        })

        $s.AllowEdit = (function () {
            return !$s.tMenu.AllowOpen;
        })

        $s.Cancel = (function () {
            //clear handlers
            $s.tMenuDetailTab.forEach(function (x) {
                $s.g.clear(x.ID);
            })
            var kk = LZString.decompressFromEncodedURIComponent($st.params.r)
            if (kk.split('-').length > 2 && kk.indexOf(":") == -1) {
                var pName = LZString.decompressFromEncodedURIComponent($st.params.r).split('-')[2].replace('%20', '-');
                var pID = LZString.decompressFromEncodedURIComponent($st.params.r).split('-')[3];
                $st.go('List', {
                    Name: pName,
                    r: LZString.compressToEncodedURIComponent(pID.toString())
                });
            } else {
                var eid = kk.split('-')[2];
                if (eid != undefined) { //Weng Para pagnagcancel ng Filing sa Direct Report ang balik
                    $st.go('DirectReport', {
                        Name: "DirectReport",
                        r: LZString.compressToEncodedURIComponent("3060")
                    });
                } else {
                    $st.go('List', {
                        Name: $s.tMenu.Name.replace(/ /g, '-'),
                        r: LZString.compressToEncodedURIComponent($s.tMenu.ID.toString())
                    });
                }
            }
        })

        //Filters

        $s.MenuTabFilter = (function (mtab) {
            return mtab.Name.toLowerCase() !== 'comment' && mtab.HasTable === true;
        })

        $s.MenuTabPanel = (function (mtab) {
            var lk = Enumerable.From($s.tMenuTabField).Where(function (x) { return x.ID_MenuTab === mtab.ID }).Max(function (x) { return x.Panel });
            return Enumerable.Range(1, lk).ToArray();
        })

        $s.MenuTabFieldFilter = (function (mtab, panel) {
            return function (tabField) {
                return tabField.ID_MenuTab === mtab.ID && tabField.Panel === panel && tabField.ShowInInfo === true;
            }
        })

        $s.ButtonFilter = (function (btn) {
            return $s.IsNull(btn.ID_MenuDetailTab, 0) === 0;
        })

        // filter - detail tab
        $s.MenuDetailTabFilter = (function (mdTab) {
            return ($s.IsNull(mdTab.ParentTableName, '') === '' || $s.IsNull(mdTab.ParentTableName, '') === $s.tMenu.TableName) && mdTab.ID_MenuDetailTabType !== 4; //exclude Reports
        })

        $s.MenuDetailTabReportOnlyFilter = (function (mdTab) {
            return mdTab.ID_MenuDetailTabType === 4;
        })

        $s.MDTFieldFilter = (function (mdTab) {
            try {
                return Enumerable.From($s.tMenuDetailTabField).Where(function (x) { return x.ID_MenuDetailTab === mdTab.ID }).ToArray();
            } catch (ex) {
                console.error(ex);
            }
        })

        //Actions

        $s.NewInfo = (function () {
            //console.log($s.tMenu.Name.replace(/ /g, '-'), LZString.compressToEncodedURIComponent($s.tMenu.ID.toString() + '-' + '0'))
            if (LZString.decompressFromEncodedURIComponent($st.params.r).split('-').length > 2) {
                var pName = LZString.decompressFromEncodedURIComponent($st.params.r).split('-')[2].replace('%20', '-');
                var pID = LZString.decompressFromEncodedURIComponent($st.params.r).split('-')[3];
                $st.go('Info', {
                    Name: $s.tMenu.Name.replace(/ /g, '-'),
                    r: LZString.compressToEncodedURIComponent($s.tMenu.ID.toString() + '-' + '0' +
                        (LZString.decompressFromEncodedURIComponent($st.params.r).split('-').length > 2 ? '-' + LZString.decompressFromEncodedURIComponent($st.params.r).split('-')[2] + '-' + pID.toString() : ''))
                }, { reload: true })
            } else {
                $st.go('Info', {
                    Name: $s.tMenu.Name.replace(/ /g, '-'),
                    r: LZString.compressToEncodedURIComponent($s.tMenu.ID.toString() + '-' + '0')
                }, { reload: true })
            }

        })



        $s.btnSaveInfo = (function () {

            var c = $s.ValidateParent();
            if (c) {
                    $s.SaveInfo();
            }
            // console.log($s.tMenuDetailTabField);
            // console.log($s.tMenu.TableName);
            // console.log($s.tMenuDetailTabField);

            //Created by Yoku 03052019

            //var xy = $("#checkif").val();

            //var idemp = $s.data.Row.ID_Employee;

            //var m = $s.tMenu.Name;

            //var cid2 = $s.data.Row.ID;

            //if (m === "Missed Log" || m === "Leave") {

            //    $s.Request("CountIDAttachment", { Table: m, ID: cid2 }).then(function (rData) {

            //        var cidn = rData.CountID[0].Column1;

            //        $s.tMenuDetailTabField.forEach(function (x) {
            //            var ch = $s.RowData;
            //            switch (x.ID_SystemControlType) {
            //                case 16:
            //                    if (xy >= 0 && m === "Missed Log" && $s.data.Row.IsPosted === false && $s.tMenu.ID_ApplicationType === 2 && cidn === 0) {

            //                        $s.UploadAttachment();

            //                    } else if (m === "Missed Log" && $s.tMenu.ID_ApplicationType === 1 && cidn === 0) {

            //                        $s.UploadAttachment();
            //                    }
            //                    else if (xy >= 0 && m === "Leave" && $s.data.Row.ID_LeavePayrollItem === 27 && $s.data.Row.IsPosted === false && $s.tMenu.ID_ApplicationType === 2 && cidn === 0) {

            //                        $s.UploadAttachment();

            //                    } else if (m === "Leave" && $s.data.Row.ID_LeavePayrollItem === 27 && $s.tMenu.ID_ApplicationType === 1 && cidn === 0) {

            //                        $s.UploadAttachment();

            //                    } else {

            //                        var c = $s.ValidateParent();
            //                        if (c) {
            //                            if ($s.tMenu.ID_ApplicationType === 1 || $s.data.Row.ApproverComment !== null) {
            //                                $s.SaveInfo();
            //                            } else {
            //                                $s.SaveInfo();
            //                            }
            //                        }

            //                    }

            //                    break;
            //                default:

            //                    break;
            //            }
            //        })
            //    })
            //    //Created by Yoku 03052019
            //} else {

            //    var c = $s.ValidateParent();
            //    if (c) {
            //        if ($s.tMenu.ID_ApplicationType === 1 || $s.data.Row.ApproverComment !== null) {
            //            $s.SaveInfo();
            //        } else {
            //            if ($s.data.Row.IsPosted === false && $s.tMenu.ID_ApplicationType === 2) {
            //                $s.SaveInfo();
            //            } else {
            //                $s.Toast('Cannot save already posted', 'SaveInfo', 'warning');
            //            }
            //        }
            //    }


            //}
        })

        $s.RefreshInfo = (function () {
            $s.LoadData();
        })

        $s.PrintData = (function (doc) {
            $s.CheckDocumentDefault();
            $s.Status = 'Validating Info';
            $s.StatusColor = 1;
            return $s.ValidateInfo().then(function () {
                var RowState;
                var tRow = {};
                var RowDeleted = {};

                tRow[$s.tMenu.TableName] = $s.data.Row;
                RowState = tRow[$s.tMenu.TableName].ID === 0 ? $s.RowState.Inserted : $s.RowState.Updated;

                return $s.g.invoke('GetDetailData').then(function (info) {
                    info.forEach(function (x) {
                        if (x && x.TableName.substr(0, 1) === 't') { //only accepts tables
                            tRow[x.TableName] = x.Data;
                            RowDeleted[x.TableName] = x.Deleted;
                        }
                    })

                    $s.Status = 'Printing Info';
                    $s.StatusColor = 1;
                    switch (doc.Type) {
                        case 1: //Download
                            $s.Download('PrintInfo', {
                                ID_Menu: $s.tMenu.ID,
                                Data: tRow,
                                opt: doc.DocType
                            }).then(function () {
                                $s.Status = 'Ready';
                                $s.StatusColor = 2;
                            });
                            break;
                        case 3: //Email
                            $s.Dialog({
                                template: 'email.tmpl.html',
                                controller: 'EmailDialogController',
                                size: 'md',
                            }).result.then(function (d) {

                            })

                            //$s.Request('PrintInfo', {
                            //    ID_Menu: $s.tMenu.ID,
                            //    Data: tRow,
                            //    opt: doc.DocType
                            //}).then(function () {

                            //})
                            break;
                    }
                })
            })
        })

        $s.ButtonClick = (function (d) {
            try {
                if (d.Click) { //if the extended info manually handled the click event
                    d.Click(d);
                    return;
                }
                var ValidateCommand = (function () {
                    return $s.Request('ButtonValidateCommand', {
                        ValidateCommandText: $s.PassParameter($s.PassParameter(d.ValidateCommandText, $s.data.Row, ['$', '@']))
                    });
                })

                var ExecCommand = (function () {
                    if (d.ID_MenuButtonType === 3)
                        ExecBackgroundCommand();
                    else {
                        if ($s.IsNull(d.CommandText, '') !== '') {
                            if (d.IsGeneratedTextFile === true) {
                                $s.Download('GenerateText', {
                                    DefaultFileName: $s.PassParameter($s.PassParameter($s.data.Row.FileName || d.DefaultFileName || d.Name), $s.data.Row, '@')
                                    , CommandText: $s.PassParameter($s.PassParameter(d.CommandText), $s.data.Row, '@')
                                    , ID: $s.data.Row.ID
                                });
                            }
                            else {
                                $s.Request('ButtonCommand',
                                    {
                                        CommandText: $s.PassParameter($s.PassParameter(d.CommandText), $s.data.Row, '$', ['@.ID'])
                                    })
                                    .then(function (data) {
                                        if ($s.IsNull(d.SuccessInfoText, '') !== '')
                                            $s.Toast(d.SuccessInfoText);
                                        else
                                            $s.Toast('Done');
                                        $s.LoadData();
                                    });
                            }
                        }
                    }
                });

                var ExecBackgroundCommand = function () {

                    var args = {
                        Name: d.Name,
                        ID_Menu: $s.tMenu.ID,
                        ID_Record: $s.data.Row.ID,
                        ID_User: $s.Session('ID_User'),
                        CommandText: $s.PassParameter($s.PassParameter(d.CommandText), $s.data.Row, '$', ['@.ID']),
                        MenuName: $s.tMenu.Name
                    }

                    $s.Request('Job_CheckQueue', args).then(function (da) {
                        if (da) {
                            $s.Toast('The process request has been queue, the system will tell you when done.');
                        } else {
                            $s.Request('Job_Enqueue', args).then(function () {
                                $s.Toast('job has been queue.');
                            });
                        }
                    })
                }

                if (d.Enabled)
                    if ($s.IsNull(d.ConfirmationText, '') !== '') {
                        $s.Confirm(d.ConfirmationText).then(function (btn) {
                            if (d.ValidateCommandText)
                                ValidateCommand().then(function () { ExecCommand() });
                            else
                                ExecCommand();
                        });
                    } else if (d.ValidateCommandText)
                        ValidateCommand().then(function () { ExecCommand() });
                    else
                        ExecCommand();
            } catch (ex) {
                console.error(ex.message);
            }
        })

        //Enum
        $s.RowState = {
            Inserted: 0,
            Updated: 1
        };

        //Fields

        $s.ReadOnly = (function (field) {
            try {
                var mschema = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName === field.Name.trim() }).SingleOrDefault();
                var h = false;
                if (field.ReadOnly === true) h = true;
                else if (mschema && (mschema.Identity || mschema.Computed)) h = true;
                return h || $s.data.RowCondition[field.Name].WritableIf;
            } catch (ex) {
                console.error('ReadOnly: ' + field.Name, ex);
                return true;
            }
        })

        $s.Required = (function (field) {
            try {
                var mschema = Enumerable.From($s.Schema).Where(function (x) { return x.ColumnName === field.Name.trim() }).SingleOrDefault();

                if (mschema && mschema.Identity) return false;

                var h = false;
                if (field.IsRequired) h = true;
                else if (mschema && mschema.AllowDBNull === 0) h = true;
                return h;
            } catch (ex) {
                console.error('Required: ' + field.Name, ex);
                return false;
            }
        })

        $s.VisibleIf = (function (field) {
            var h = true;
            if (field.Name === "ID") h = true;
            if (h && $s.data.RowCondition[field.Name].VisibleIf) return h;
            else h = h && $s.data.RowCondition[field.Name].VisibleIf;
            return h;
        })
        //Detail Tabs

        $s.GetTabButton = (function (mdt) {
            return Enumerable.From($s.tMenuButton).Where(function (x) { return x.ID_MenuDetailTab === mdt.ID }).ToArray(); //&& x.ID_MenuButtonModuleType === 1 
        })

        $s.GetDetailTableField = (function (mdt) {
            return Enumerable.From($s.tMenuDetailTabField).Where(function (x) { return x.ID_MenuDetailTab === mdt.ID }).ToArray();

        });

        //$s.DetailTabActivate = (function (mdTab) {
        //     $s.g.invoke('DetailActivate', mdTab);
        //})

        $s.UploadImage = (function (ColumnName) {
            $s.UploadFile('UploadImage', null, false, 'image/*').then(function (d) {
                $s.data.Row[ColumnName] = d.GUID;
                $s.Toast('Image Uploaded');
            }).fail(function (msg) {
                $s.Toast(msg, document.title, 'warning');
            })
        })

        $s.ToggleInfoPanel = (function () {
            var g = $(".info-panel");
            g.toggleClass("info-toggled");

            if (g.hasClass("info-toggled")) {
                $('[scroll-area]').width($('[scroll-area]').width() - 200);
                $('#pdfViewer').width($('.info-body-container .panel-body').width() + 30);
            } else {
                $('[scroll-area]').width($('[scroll-area]').width() + 200);
                $('#pdfViewer').width($('.info-body-container .panel-body').width() + 30);
            }
        })

        $s.ShowReportTab = (function (md) {
            $s.ReportTab = md;
            $s.PDFViewerApplication.close();
            $s.PDFViewerApplication.SetTitle(md.Name);
            $('.report-body').addClass('report-body-show');
        })

        $s.HideReportTab = (function () {
            $('.report-body').removeClass('report-body-show');
        })

        $s.SwitchActiveTab = (function (mTab) {
            $s.HideReportTab();

            //console.log('switch tab',mTab.ID);

            $('.tab-container').removeClass('tab-container-show');

            if (mTab.TabType == 1) {
                $('.tab-container[tab-type="1"]').addClass('tab-container-show');
            } else if (mTab.TabType == 2) {
                $('.tab-container[tab-uid="tbh_' + mTab.ID + '"]').addClass('tab-container-show');
            }

        })

        $s.InitialStateofTabContainer = (function () {
            console.log('intial state loaded');
            $('.tab-container[tab-type="1"]').addClass('tab-container-show');
        })

        $s.ClearGUID = (function () {
            console.log('Cleared', $s.InfoGUID);
            $inv.clear($s.InfoGUID);
        })

        $s.g.on('ParentCurrentRow', function (TableName) {
            if ($s.tMenu.TableName === $s.IsNull(TableName, $s.tMenu.TableName)) {
                return $s.data.Row;
            }
        })

        //#report

        $s.pdfSource = $s.TrustSrc('/Scripts/Pack/PDFJS/web/viewer.html');

        $s.PDFViewerApplication = null;

        window.pdfViewerReady = (function (pdf, btn) {
            btn.LoadButton.click($s.LoadReportClick);
            btn.DownloadExcel.click($s.DownloadExcelClick);
            //btn.FilterButton.hide();

            //console.log('viewer ready');

            $s.PDFViewerApplication = pdf;

        })

        $s.LoadReportClick = (function () {
            console.log('InfoSet na LoadReport');
            $s.LoadReport();
        });

        $s.LoadReport = (function () {
            console.log('Infoset na LoadReport d2');
            $s.ReportTab.ReportParams = null;
            $s.Request('LoadReportParameters', { ReportName: $s.ReportTab.ReportFile }).then(function (rData) {
                if (rData.length > 0) {
                    $s.ReportTab.ReportParams = rData;
                    $('.report-param-dialog').addClass('report-param-show');
                } else
                    $s.ShowReport();
            });
        });

        $s.DownloadExcelClick = (function () {
            $s.DownloadExcel();
        });

        $s.DownloadExcel = (function () {
            $s.ReportTab.ReportParams = null;
            $s.Request('LoadReportParameters', { ReportName: $s.ReportTab.ReportFile }).then(function (rData) {
                if (rData.length > 0) {
                    $s.ReportTab.ReportParams = rData;
                    $('.report-param-dialog').addClass('report-param-show');
                } else
                    var param = {
                        ReportName: $s.ReportTab.ReportFile,
                        DataSource: $s.PassParameter($s.PassParameter($s.ReportTab.DataSource, $s.data.Row)),
                        Author: $s.Session('Name'),
                        ReportParameter: $s.ReportTab.ReportParams
                    }

                $s.Download('LoadExcelReport', param);
            });
        });

        $s.ReportParamDialogSubmit = (function () {
            $('.report-param-dialog').removeClass('report-param-show');
            $s.ShowReport();
        });

        $s.CloseReportParamDialog = (function () {
            $('.report-param-dialog').removeClass('report-param-show');
        });

        $s.ShowReport = (function () {
            var param = {
                ReportName: $s.ReportTab.ReportFile,
                DataSource: $s.PassParameter($s.PassParameter($s.ReportTab.DataSource, $s.data.Row)),
                Author: $s.Session('Name'),
                ReportParameter: $s.ReportTab.ReportParams
            }

            $s.Request('LoadReport', param).then(function (data) {
                var pdfArray = $s.convertDataURIToBinary(data.FileString);
                $s.PDFViewerApplication.open(pdfArray);
            });
        });

        //#end report

        //#Detail Menu

        $s.g.on('DetailOpen', function (ID_Menu, ID, ID_DetailMenu) {
            $s.DetailOpen(ID_Menu, ID, ID_DetailMenu);
        })

        $s.DetailOpen = (function (ID_Menu, ID, ID_DetailMenu) {
            $md.Load($s, ID_Menu, ID);
        })

        //#Extended Infos

        $s.NewTable = (function (TableName) {
            return $s.Request('NewTableSet', { ParentTableName: $s.tMenu.TableName, TableName: TableName }).then(function (d) {
                d.MenuDetailTab.ID_Menu = $s.tMenu.ID;
                return d;
            })
        })

        $s.AddTable = (function (NewTableData) {
            NewTableData.MenuDetailTabField.forEach(function (x) {
                $s.tMenuDetailTabField.push(x);
            });
            $r.SchemaTable.push(NewTableData.Schema);
            $s.tMenuDetailTab.push(NewTableData.MenuDetailTab);
            $s.RebuildTabs();
        })

        $s.GetTable = (function (TableName) {
            return $s.g.invoke('GetTable', TableName).then(function (d) {
                for (var i in d) {
                    if (d[i] && d[i] !== null)
                        return d[i];
                }
            })
        })

        $s.GetButton = (function (Name) {
            return Enumerable.From($s.tMenuButton).Where(function (x) { return x.Name === Name }).SingleOrDefault(null);
        })

        $s.AddButton = (function (Name, Action) {
            $s.tMenuButton.push({ Click: Action, Name: Name, Enabled: true });
        })

        $s.StatusBar = (function () {
            var st = null;
            if ($s.StatusColor == 2) {
                st = "ready";
            } else if ($s.StatusColor == 3) {
                st = "error";
            }

            return st;
        })

        $s.StatusFa = (function () {
            var st = null;
            if ($s.StatusColor == 2) {
                st = "fa-check-circle";
            } else if ($s.StatusColor == 3) {
                st = "fa-exclamation-circle";
            } else {
                st = "fa-circle-o-notch fa-spin";
            }

            return st;
        })

        $s.View = (function (tble) {
            return 'v' + tble.substr(1);
        })

        $s.ClearThenFill = (function (grd, tbl) {
            return $.when(grd, tbl).then(function (al, rd) {
                al.RowData = rd[0];
                al.UpdateView();
            })
        })

        $s.UploadFileTab = function (data, col) {
            $s.UploadFile('UploadFileTab', null, false, 'application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/pdf,image/jpg,image/jpeg,image/png').then(function (ret) {
                if (ret.error != undefined) {
                    $s.Toast(ret.error, 'danger');
                } else {
                    data[col] = ret.OrigFileName;
                    data[col + '_GUID'] = ret.GUID;
                    $s.$apply();
                }
            })
        }

        var offsetTop = $(".main-content")[0].offsetTop;
        var clientHeight = $(".main-content")[0].clientHeight;
        $(".info-body-container .panel-body").css("maxHeight", (clientHeight - 66) - offsetTop);
        $(".info-body-container .panel-body").css("minHeight", (clientHeight - 66) - offsetTop);

        //#end Extended Infos

        var __Construct = (function () {
            //console.log('loaded')
            //Menu Schema
            $s.Schema = $s.GetTableSchema($r.Menu.tMenu.TableName).Schema

            for (var r in $r.Menu) $s[r] = $r.Menu[r];
            for (var k in $s.tMenuButton) $s.tMenuButton[k].Enabled = true;

        }())


        $s.ValidateParent = function () {
            var ret = false;
            if (!$s.ParentForm.$valid) {


                if ($s.ParentForm.$error.required)
                    vcl.Array.Remove($s.ParentForm.$error.required, function (x) {
                        if (x.$$element[0].type == "checkbox") {
                            return true;
                        } else {

                            return false;
                        }
                    });


                if ($s.ParentForm.$error.required) {

                    if ($s.ParentForm.$error.required.length > 0) {

                        var gg = Enumerable.From($s.ParentForm.$error.required).Select(function (x) { return x.$$attr.name }).Distinct().ToArray();
                        gg.toString().trim().substr(3);

                        $s.Toast('Affected Columns: > ' + gg.join(", "), 'Fill all required fields!', 'warning');


                        return false;
                    } else {
                        return true;


                    }
                }
                if ($s.ParentForm.$error.pattern) {
                    if ($s.ParentForm.$error.pattern.length > 0) {
                        $s.Toast('Invalid values on other fields!', 'SaveInfo', 'warning');
                        return false;
                    } else {
                        return true;
                    }
                }
                if ($s.ParentForm.$error.maxlength) {

                    //console.log($s.ParentForm.$error);

                    if ($s.ParentForm.$error.maxlength.length > 0) {

                        //var gg = Enumerable.From($s.ParentForm.$error.maxlength).Select(function (x) { return x.$$attr.name }).Distinct().ToArray();

                        //$s.Toast('Affected Columns: ' + gg.join(", "), 'You have exceeded the max length!', 'warning');
                        //return false;

                        var gg = Enumerable.From($s.ParentForm.$error.maxlength).Where(function (x) { return x.$$attr.name !== 'ID' }).Select(function (x) { return x.$$attr.name }).Distinct().ToArray();
                        if (gg.length > 0) {
                            $s.Toast('Affected Columns: ' + gg.join(", "), 'You have exceeded the max length!', 'warning');
                            return false;
                        }

                        return true;
                    } else {
                        //  return true;

                    }
                }
                if ($s.ParentForm.$error.minlength) {
                    if ($s.ParentForm.$error.minlength.length > 0) {
                        $s.Toast('Minimum length not reach!', 'SaveInfo', 'warning');
                        return false;
                    } else {
                        return true;
                    }
                }
                $s.ParentForm.$submitted = true;
                $s.ParentForm.$setPristine();
                return true;
            } else {
                $s.ParentForm.$submitted = true;
                $s.ParentForm.$setPristine();
                return true;
            }
        }

    })

    app.controller('InfoSet', ['$scope', '$controller', 'resources', '$Invoker', '$state', '$info', 'MenuDialog', InfoSet]);
})