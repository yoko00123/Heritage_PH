
define(['app'], function (app) {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('EmployeeAttendanceLogFileInfo');

        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.AfterInit = (function () {
            return $s.Task(function () {
                $s.GetButton('Import File').Click = $s.ImportFile;
                $s.GetButton('Generate Template').Click = $s.GenerateTemplate;
            })
        });

        $s.ImportFile = (function () {
            $s.UploadFile('ImportLogFile').then(function (d) {

                $s.data.Row['Name'] = d.FileName;
                $s.data.Row['Code'] = d.OriginalFileName;
   
                $s.GetTable('tEmployeeAttendanceImportLogs').then(function (dtl) { 
                    try {
                        d.Data.forEach(function (x) {
                            var d = dtl.NewRowSchema();
                            d.AccessNo = x.AccessNo.replace("'",'');
                            d.Source = x.Source;
                            d.DateTime = x.DateTime;
                            d.ID_AttendanceLogType = x.ID_AttendanceLogType; 
                            d.ID_EmployeeAttendanceLogCreditDate = 1; //default
                            d.DateTimeCreated = $s.Session('CurrentDate');
                            dtl.AddRow(d);
                        })
                        dtl.UpdateView();
                    } catch (Ex) {
                        console.error(Ex);
                    }
                });

            });
        })

        $s.GenerateTemplate = (function (d) {
            $s.Download('GenerateEmployeeTemplate',
                 { 
                     ID_Header: $s.Session('ID_Session')
                 });
        })


    })
})