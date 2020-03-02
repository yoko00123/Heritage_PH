define(['app'], function (app) {

    app.factory('ActiveModule', ['$rootScope', 'DataService', '$q', function ($rootScope, $d, q) {
        var _ = this;
        _.Info = null;
        _.Template = 'InfoSet.html';

        var Menu = {
            /* for extended infos please add name here */
            Administrative_UserGroup: 14,
            Administrative_User: 15,
            TimeAndAttendanceProcessing: 96,
            Employee_Attendance_Log_File: 106,
            Administrative_EmployeeTemplate: 352,
            TimekeepingItems_EmployeeDailySchedule: 188, 
            Official_Business: 1003,
            Missed_Log: 1002,
            Leave: 1001,
            Overtime: 1000,
            Change_Of_Schedule: 999,
            Offset: 1042,
            TK_Processing: 1020
        }

        return {
            Load: (function (ID_Menu, ControllerName) {
                try {
                    switch (parseFloat(ID_Menu)) {
                        // case Menu.Administrative_UserGroup: _.Info = 'UserGroupInfo'; break;
                        case Menu.TimeAndAttendanceProcessing: _.Info = 'TimeAndAttendanceProcessingInfo'; break;
                        case Menu.Employee_Attendance_Log_File: _.Info = "EmployeeAttendanceLogFileInfo"; break;
                        case Menu.Administrative_User: _.Info = 'UserInfo'; break;
                        case Menu.Administrative_EmployeeTemplate: _.Info = 'EmployeeTemplateFileInfo'; break;
                        case Menu.TimekeepingItems_EmployeeDailySchedule: _.Info = 'EmployeeDailyScheduleInfo'; break;
                        case Menu.Administrative_UserGroup: _.Info = 'UserGroupInfo'; break;
                        case Menu.Official_Business: _.Info = 'TKFilingInfo'; break;
                        case Menu.Missed_Log: _.Info = 'TKFilingInfo'; break;
                        case Menu.Leave: _.Info = 'TKFilingInfo'; break;
                        case Menu.Overtime: _.Info = 'TKFilingInfo'; break;
                        case Menu.Change_Of_Schedule: _.Info = 'TKFilingInfo'; break;
                        case Menu.Offset: _.Info = 'TKFilingInfo'; break;
                        case Menu.TK_Processing: _.Info = 'TKProcessingInfo'; break;
                        default:
                            _.Info = 'ZInfo';
                            break;
                    }

                    var d = q.defer();
                    require(['/Web/Infos/' + _.Info + '.js'], function (nfo) {
                        app.register.controller(ControllerName || 'ZInfo', ['$scope', '$controller', 'resources', '$info', '$stateParams', nfo]);
                        d.resolve();
                        $rootScope.$apply();
                    });
                    return d.promise;
                }
                catch (ex) {
                    console.error(ex);
                }
            }),
            Info: _.Info,
            Template: _.Template
        }
    }])

    app.factory('MenuDialog', ['$controller', function ($c) {
        return {
            Load: function ($s, ID_Menu, ID) {
                var TempName = vcl.Random.GUID().replace(/-/g, '') //enure not to duplicate the info registered
               
                $s.SetController($s.CurrentController || 'Info');

                $s.Dialog({
                    controller: ['$scope', '$uibModalInstance', 'resources', '$info', function ($eds, $uib, $edsr, $einfo) {
                        $c(TempName, {
                            $scope: $eds,
                            resources: $edsr,
                            $info: $einfo
                        });

                        $eds.IsDialog = true;

                        $eds.Cancel = (function () {
                            $eds.ClearGUID();
                            $uib.dismiss();
                        })

                        $eds.ToggleInfoPanel = (function (infoID) {
                            var nfo = $('[info-id="' + infoID + '"]');
                            var g = $(".info-panel", nfo);
                            g.toggleClass("info-toggled");

                            if (g.hasClass("info-toggled")) {
                                $('[scroll-area]', nfo).width($('[scroll-area]', nfo).width() - 200 + 5);
                                $('#pdfViewer', nfo).width($('.info-body-container .panel-body', nfo).width() + 30);
                            } else {
                                $('[scroll-area]', nfo).width($('[scroll-area]', nfo).width() + 200 - 5);
                                $('#pdfViewer', nfo).width($('.info-body-container .panel-body', nfo).width() + 30);
                            }
                        })
                    }],
                    templateUrl: '/Web/Views/Info.html',
                    windowClass: 'custom-lg-dialog',
                    resolve: {
                        load: ['ActiveModule', function (am) {
                            return am.Load(ID_Menu, TempName);
                        }],
                        resources: $s.Request('GetInfoSchema', { ID_Menu: ID_Menu }),
                        $info: ID
                    }
                }).closed.then(function () {
                    if ($s.CurrentController)
                        $s.SetController($s.CurrentController);
                })
            }
        }
    }])

})
