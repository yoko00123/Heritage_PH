/**
 * @LJ20170622
 * TK Processing
 * 
 */
define(['app'], function (app) {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('TimeAndAttendanceProcessingInfo');

        $s.mtDailySchedule = 'tEmployeeDailySchedule';
        $s.vDailySchedule = 'v' + $s.mtDailySchedule.substr(1)


        $s.AfterInit = (function () {
            return $s.NewTable($s.mtDailySchedule).then(function (d) {

                d.MenuDetailTab.Name = 'Daily Schedule';
                d.MenuDetailTab.AllowNewRow = false;
                d.MenuDetailTab.AllowDeleteRow = false;
                d.MenuDetailTab.ID_DetailMenu = 188; //load eds  //to show the open detail button

                d.MenuDetailTabField.forEach(function (df) {
                    df.ReadOnly = true;
                    if (df.Name === 'Date')
                        df.IsFrozen = true;
                })

                $s.NoTransactionTables.push($s.mtDailySchedule);
                $s.AddTable(d);


                //Add Button Handlers
                $s.GetButton('Check All').Click = $s.CheckAll;
                $s.GetButton('UnCheck All').Click = $s.UnCheckAll
                $s.GetButton('Compute Hours').Click = $s.ComputeHours;

            })
        })

        //@Override
        $s.FormLoad = (function () {
            $s.LoadData(); //.then(function () { $s.LoadDetail(); });
        });

        //@Override
        $s.AfterLoadData = (function () {
            $s.LoadDetail();
        })

        $s.LoadDetail = (function () {
            console.log('load detail schedule');
            var grdDailySchedule = $s.GetTable($s.mtDailySchedule); 
            var grdTable = $s.Request('LoadScheduleDetail', { ID: $s.data.Row.ID });

            $.when(grdDailySchedule, grdTable).then(function (detail, data) {
                //detail.Clear();
                detail.RowData = data[0];
                detail.UpdateView();
            });

        });
         
        //Buttons
        $s.CheckAll = (function () {
            $s.Request('EmployeeDailySchedule_CheckUnCheck', { ID: $s.data.Row.ID, Check: 1, ID_User: $s.Session('ID_User') }).then(function () {
                $s.Toast('Done');
                $s.LoadDetail();
            });
        })

        $s.UnCheckAll = (function () {
            $s.Request('EmployeeDailySchedule_CheckUnCheck', { ID: $s.data.Row.ID, Check: 0, ID_User: $s.Session('ID_User') }).then(function () {
                $s.Toast('Done');
                $s.LoadDetail();
            });
        })

        $s.ComputeHours = (function () {
            $s.Confirm('Compute Hours?', 'Time Keeping Processing').then(function () {
                var RequestID = vcl.Random.S4();
                $s.OverlayMessage('Computing Hours');
                $s.Request('ComputeHours', { ID: $s.data.Row.ID, RequestID: RequestID }).then(function () {
                    $s.ShowOverlay();
                    $s.CheckComputeStatus(RequestID)
                });
            })
        })

        $s.CheckComputeStatus = (function (RequestID) {
            $s.Request('ComputeHoursStatus', { RequestID: RequestID }).then(function (d) {
                if (d.Status === 0) {
                    $s.OverlayMessage(d.Message);
                    $s.Task(null, 1000).then(function () {
                        $s.CheckComputeStatus(RequestID);
                    });
                } else {
                    $s.HideOverlay();
                    $s.LoadDetail();
                }
            });
        })

    })
})