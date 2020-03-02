
define([], function () {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('EmployeeTemplateFileInfo');

        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.AfterInit = (function () {
            return $s.Task(function () {
                $s.AddButton('Generate Template', $s.GenerateTemplate);
                $s.AddButton('Import File', $s.ImportTemplate);
                $s.AddButton('Apply File', $s.ApplyFile);
            })
        });

        $s.GenerateTemplate = (function () {
            $s.Download('GenerateEmployeeTemplate', { ExcelName: 'Employee Template File' });
        })

        $s.ApplyFile = function () {
            $s.Confirm('Apply Employees?', $s.tMenu.Name).then(function () {
                $s.Request('ApplyFile', { ID: $s.data.Row.ID, ID_User: $s.Session('ID_User') }).then(function (q, p) {
                    if (p == 8) {
                        $s.Toast('Done');
                    } else {
                        angular.forEach(q.Errors, function (obj) {
                            $s.Toast(obj.Message, 'danger');
                        });
                    }
                });
            })
        }

        $s.ImportTemplate = function () {

            $s.Dialog({
                size: 'md',
                template: 'EmployeTemplateFile-Excel-Dialog.tmpl.html',
                controller: ['$scope', '$uibModalInstance', function ($ds, $dmi) {


                    $ds.lookupName = 'Excel Template Sheet Selection Dialog';
                    $ds.Width = '600px';
                    $ds.Sheets = [
                        { Sheet: 'PersonalInfo', Table: 'tEmployeeTemplateFileDetail_PersonalInfo', Data: null, IsChecked: true },
                        { Sheet: 'EducationalBackground', Table: 'tEmployeeTemplateFileDetail_EducationalBackground', Data: null, IsChecked: true },
                        { Sheet: 'PersonaDependent', Table: 'tEmployeeTemplateFileDetail_Dependent', Data: null, IsChecked: true },
                        { Sheet: 'EmploymentHistory', Table: 'tEmployeeTemplateFileDetail_EmploymentHistory', Data: null, IsChecked: true },
                        { Sheet: 'CompanyInfo', Table: 'tEmployeeTemplateFileDetail_CompanyInfo', Data: null, IsChecked: true },
                    ]
                    $ds.UploadOption = "2";
                    $ds.Code = null;
                    $ds.Name = null;

                    //$ds.Options = [{ ID: 1, Name: 'Append Only' }, { ID: 2, Name: 'Delete and Overwrite' }, ]

                    $ds.Init = function () {

                    }

                    $ds.Upload = (function () {

                        $s.UploadFile('ImportEmployeeTemplate', { sheets: Enumerable.From($ds.Sheets).Where(function (x) { return x.IsChecked == true }).Select(function (x) { return x.Sheet }).ToArray() }).then(function (d) {
                            if (d.HasError) {
                                var p = { FileName: d.HasErrorFile, OrigName: d.FileName };
                                $s.Toast('The file uploaded has errors. Please check the file generated.', 'danger');
                                $s.Download('DownloadExcelWithError', p)
                            } else {
                                try {
                                    Enumerable.From($ds.Sheets).ForEach(function (x) {
                               
                                        if (Enumerable.From(d.Data).Where(function (y) { return y.TableName === x.Sheet }).ToArray().length == 0) {
                                            x.Data = null;
                                        } else {
                                            x.Data = Enumerable.From(d.Data).Where(function (y) { return y.TableName === x.Sheet }).Single().Rows;
                                        }
                                    });

                                    $ds.Code = d.Code;
                                    $ds.Name = d.Name;
                                } catch (ex) {
                                    console.error(ex);
                                    $s.Toast(ex.message, 'danger');
                                }
                            }
                        })
                    })

                    $ds.Submit = (function () {
                      //  console.log('SHUUUUUUUUU', Enumerable.From($ds.Sheets).Any(function (x) { return x.Data != null && x.IsChecked == true }));
                        if (!Enumerable.From($ds.Sheets).Any(function (x) { return x.Data != null && x.IsChecked == true })) {
                            $s.Toast('You must upload a template file.', 'Template file upload', 'warning');
                            return;
                        }

                        $dmi.close({ Sheets: $ds.Sheets, Option: $ds.UploadOption, Code: $ds.Code, Name: $ds.Name });
                    })

                    $ds.CloseLookUp = function () {
                        $dmi.close(null);
                    }

                }]
            }).result.then(function (data) {
                if (data != null) {
                    try {

                        $s.data.Row['Code'] = data.Code;
                        $s.data.Row['Name'] = data.Name;

                        data.Sheets.forEach(function (x) {
                            $s.Task(function () {
                                $s.GetTable(x.Table).then(function (dtl) {
                                    var j = Enumerable.From(data.Sheets).Where(function (y) { return y.Table === x.Table}).Single().Data;
                                     
                                    console.log(x.Table, j);

                                    if (x.Table === 'tEmployeeTemplateFileDetail_CompanyInfo') {
                                        for (var i in j) {
                                            j[i]['IsRequiredToLog'] = ($s.IsNull(j[i]['IsRequiredToLog'], '') !== '' ? (j[i]['IsRequiredToLog'].toUpperCase() === 'YES' ? true : false) : false);
                                        }
                                    }
                                    
                                    if (parseInt(data.Option) == 2) {
                                        if (x.IsChecked == true) {
                                            dtl.RowData = j;
                                        } else {
                                            dtl.RowData = [];
                                        }
                                    }
                                    else if (parseInt(data.Option) == 1) {
                                        if (x.IsChecked == true) {
                                            j.forEach(function (row) {
                                                var g = dtl.NewRowSchema();
                                                Enumerable.From(Object.keys(row)).Where(function (xs) { return xs !== 'ID' }).ForEach(function (c) {
                                                    g[c] = row[c];
                                                });
                                                dtl.AddRow(g);
                                            });
                                        }
                                    }
                                    dtl.UpdateView();
                                });
                            });
                        });
                    } catch (ex) {
                        console.error(ex);
                        $s.Toast(ex.message, 'danger');
                    }
                }
            })
        }

        //$s.PostExcelFile = (function () {
        //    $s.UploadFile('ImportEmployeeTemplate').then(function (d) {
        //        try {
        //            var tbls = [
        //                { Sheet: 'PersonalInfo', Table: 'tEmployeeTemplateFileDetail_PersonalInfo' },
        //                { Sheet: 'EducationalBackground', Table: 'tEmployeeTemplateFileDetail_EducationalBackground' },
        //                { Sheet: 'PersonaDependent', Table: 'tEmployeeTemplateFileDetail_Dependent' },
        //                { Sheet: 'EmploymentHistory', Table: 'tEmployeeTemplateFileDetail_EmploymentHistory' },
        //                { Sheet: 'CompanyInfo', Table: 'tEmployeeTemplateFileDetail_CompanyInfo' },
        //            ];

        //            $s.data.Row['Code'] = d.Code;
        //            $s.data.Row['Name'] = d.Name;

        //            tbls.forEach(function (x) {
        //                $s.Task(function () {
        //                    $s.GetTable(x.Table).then(function (dtl) {
        //                        var j = Enumerable.From(d.Data).Where(function (y) { return y.TableName === x.Sheet }).Single().Rows;

        //                        if (x.Table === 'tEmployeeTemplateFileDetail_CompanyInfo') {

        //                            for (var i in j) {

        //                                j[i]['IsRequiredToLog'] = ($s.IsNull(j[i]['IsRequiredToLog'], '') !== '' ? (j[i]['IsRequiredToLog'].toUpperCase() === 'YES' ? true : false) : false);

        //                            }

        //                        }

        //                        dtl.RowData = j;
        //                        dtl.UpdateView();
        //                    });
        //                });
        //            });
        //        } catch (ex) {
        //            console.error(ex);
        //            $s.Toast(ex.message, 'danger');
        //        }
        //    })
        //})

    })
})
