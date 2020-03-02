
define([], function () {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('EmployeeDailyScheduleInfo');

        $s.mtEmployeeDailySchedule_Detail = 'tEmployeeDailySchedule_Detail';
        $s.mtEmployeeAttendanceLog = 'tEmployeeAttendanceLog';
        $s.mtAttendance = 'tAttendance';
        $s.mtOvertime = 'tOvertime';
        $s.mtLeaveDetail = 'tLeave_Detail';

        $s.mtOB_Detail = 'tOB_Detail';
        $s.mtCOSDetail = 'tEmployeeChangeOfSchedule_Detail';


        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.AfterInit = (function () {

            var gdEmployeeDailySchedule_Detail = $s.NewTable($s.mtEmployeeDailySchedule_Detail).then(function (d) {
                d.MenuDetailTab.Name = 'Detail';
                d.MenuDetailTab.ParentColumn = 'ID';
                d.MenuDetailTab.ChildColumn = 'ID_EmployeeDailySchedule';

                d.MenuDetailTabField.forEach(function (df) {
                    //df.ReadOnly = true;
                    if (df.Name.substr(0, 3) === 'ID_')
                        df.EffectiveLabel = df.Name.substr(3)
                })

                return d;
            })

            var gdEmployeeAttendanceLog = $s.NewTable($s.mtEmployeeAttendanceLog).then(function (d) {
                d.MenuDetailTab.Name = 'Attendance Log';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                    if (df.Name.substr(0, 3) === 'ID_')
                        df.EffectiveLabel = df.Name.substr(3)
                })

                vcl.Array.Remove(d.MenuDetailTabField, function (x) { return x.Name == 'ID_Employee' });

                return d;
            })

            var gdAttendance = $s.NewTable($s.mtAttendance).then(function (d) {
                d.MenuDetailTab.Name = 'Attendance';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                    if (df.Name.substr(0, 3) === 'ID_')
                        df.EffectiveLabel = df.Name.substr(3)
                })

                vcl.Array.Remove(d.MenuDetailTabField, function (x) { return ['ID_Employee', 'Date', 'DailySchedule'].indexOf(x.Name) !== -1 });

                return d;
            })

            var gdOvertime = $s.NewTable($s.mtOvertime).then(function (d) {
                d.MenuDetailTab.Name = 'Overtime';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                })

                return d;
            })

            var gdLeaveDetail = $s.NewTable($s.mtLeaveDetail).then(function (d) {
                d.MenuDetailTab.Name = 'Leave';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                })
                return d;
            })

            var gdOB_Detail = $s.NewTable($s.mtOB_Detail).then(function (d) {
                d.MenuDetailTab.Name = 'Official Business';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField = Enumerable.From(d.MenuDetailTabField).Where(function (x) { return ['ID', 'Date', 'StartTime', 'EndTime'].indexOf(x.Name) !== -1 }).ToArray();

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                })

                return d;
            })

            var gdCOSDetail = $s.NewTable($s.mtCOSDetail).then(function (d) {
                d.MenuDetailTab.Name = 'Change of Schedule';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;

                d.MenuDetailTabField = Enumerable.From(d.MenuDetailTabField).Where(function (x) { return ['ID', 'SchedDate', 'OldSched', 'NewSched', 'ForRDSD'].indexOf(x.Name) !== -1 }).ToArray();
                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                    if (df.Name == 'SchedDate')
                        df.EffectiveLabel = 'Date';
                })
                return d;
            })

            $s.NoTransactionTables.push($s.mtEmployeeAttendanceLog);
            $s.NoTransactionTables.push($s.mtAttendance);
            $s.NoTransactionTables.push($s.mtOvertime);
            $s.NoTransactionTables.push($s.mtLeaveDetail);
            $s.NoTransactionTables.push($s.mtOB_Detail);
            $s.NoTransactionTables.push($s.mtCOSDetail);

            return $.when(gdEmployeeDailySchedule_Detail, gdEmployeeAttendanceLog, gdAttendance, gdOvertime, gdLeaveDetail, gdOB_Detail, gdCOSDetail).then(function (a, b, c, d, e, f, g) {
                $s.AddTable(a);
                $s.AddTable(b);
                $s.AddTable(c);
                $s.AddTable(d);
                $s.AddTable(e);
                $s.AddTable(f);
                $s.AddTable(g);
            });
        })

        //Override
        $s.AfterLoadData = (function () {
            try {

                // Employee Attendance Log 
                var grdEmployeeAttendanceLog = $s.GetTable($s.mtEmployeeAttendanceLog);
                var tblEmployeeAttendanceLog = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtEmployeeAttendanceLog),
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                        { Name: 'WorkDate', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });

                $s.ClearThenFill(grdEmployeeAttendanceLog, tblEmployeeAttendanceLog);

                // Attendance
                var grdAttendanceLog = $s.GetTable($s.mtAttendance);
                var tblAttendanceLog = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtAttendance),
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                        { Name: 'WorkDate', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });

                $s.ClearThenFill(grdAttendanceLog, tblAttendanceLog);

                // Leave
                var grdLeave = $s.GetTable($s.mtLeaveDetail);
                var tblLeave = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtLeaveDetail),
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                        { Name: 'Date', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });
                $s.ClearThenFill(grdLeave, tblLeave);

                //Overtime
                var grdOvertime = $s.GetTable($s.mtOvertime);
                var tblOvertime = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtOvertime),
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                        { Name: 'Date', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });
                $s.ClearThenFill(grdOvertime, tblOvertime);

                //OB
                var grdOB_Detail = $s.GetTable($s.mtOB_Detail);
                var tblOB_Detail = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtOB_Detail),
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                         { Name: 'Date', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });
                $s.ClearThenFill(grdOB_Detail, tblOB_Detail);

                //COS
                var grdCOSDetail = $s.GetTable($s.mtCOSDetail);
                var tblCOSDetail = $s.Request('LoadTable', {
                    DataSource: $s.View($s.mtCOSDetail),
                    Columns: "ID,SchedDate AS Date,OldSched,NewSched,CASE WHEN ID_ForRDSD = 1 THEN 'Rest Day'  WHEN ID_ForRDSD = 2 THEN 'Straight Duty' ELSE NULL END ForRDSD",
                    Where: [
                        { Name: 'ID_Employee', Value: [$s.data.Row['ID_Employee']], Type: 2 },
                         { Name: 'SchedDate', Value: [$s.data.Row['Date']], Type: 2 }
                    ]
                });
                $s.ClearThenFill(grdCOSDetail, tblCOSDetail);
            } catch (ex) {
                $s.Toast(ex.message);
                console.error(ex);
            }
        })
         
    })
})
