define(['app'], function (app) {
    var MyApprovals = (function ($s, $c, r, $st, d) {
        $c('BaseController', { $scope: $s });
        $s.Controller = "Action";
        $s.Menu = r;
        $s.Menu.tMenu.DataSource = $s.PassParameter($s.Menu.tMenu.DataSource);
        $s.Menu.Columns = Enumerable.From($s.Menu.tMenuTabField).Where(function (x) { return x.ID_SystemControlType !== 15 }).ToArray();
        $s.MyApprovals = []
        $s.ListData = []
        $s.AData = []
        $s.CB = false;
        $s.Icon = 'Select All';


        $s.FilingFilter = (function (ID_Name) {
            return function (ft) {
                return ft.ID_FilingType === ID_Name;
            };
        })

        $s.Init = (function () {
            $s.Request("GetData", { ds: $s.Menu.tMenu, lp: "" }).then(function (data) {
                $s.AData = data;
                $s.MyApprovals = Enumerable.From(data.Source).Select(function (x) { return { ID: x.ID_FilingType, Name: x.Name, ID_Menu: x.ID_Menu } }).Distinct(function (x) { return x.Name }).ToArray();
            });
        });

        $s.DataFormatter = (function (data, col) {
            if (col.ID_SystemControlType == 11)
                return vcl.DateTime.Format(data[col.Name], "mmm d yyyy")
            else if (col.ID_SystemControlType == 3)
                return null;
            else if (col.Name.indexOf("ID_") > -1)
                return data[col.Label.replace(/\s/g, '')]
            else
                return data[col.Name];
        })

        $s.OpenInfo = (function (ID, ft) {
            d.GetMenu(ft.ID_Menu).then(function (x) {
                var param = x.tMenu;
                $st.go('Info', {
                    Name: param.Name.replace(/ /g, '-'),
                    r: LZString.compressToEncodedURIComponent(param.ID.toString() + '-' + ID.toString() +
                        (param.ID_MenuType == 6 ? '-' + param.Name.replace(/ /g, '%20') + '-' + param.ID.toString() : ''))
                })
            });
        })

        $s.CBAction = (function (ID, ft, cb) {
            if (cb) {
                var cnt = Enumerable.From($s.ListData).Where(function (x) { return x.ID == ID && x.ID_FilingType == ft }).Count();
                if (cnt == 0)
                    $s.ListData.push({ ID: ID, ID_FilingType: ft });
            } else {
                for (var i = 0; i < $s.ListData.length; i++)
                    if ($s.ListData[i].ID === ID && $s.ListData[i].ID_FilingType === ft) {
                        $s.ListData.splice(i, 1);
                        break;
                    };
            }
        });

        $s.SelectAll = (function (ft, sa) {
            var cnt = $("[targetid$=row_" + ft + "]")
            cnt.each(function () {
                var _ = $(this);
                _.prop("checked", sa).change();
            })
            if (sa) {
                Enumerable.From($s.AData.Source).Where(function (x) { return x.ID_FilingType === ft }).ForEach(function (x) {
                    $s.CBAction(x.ID, ft, true);
                });

            } else {
                Enumerable.From($s.AData.Source).Where(function (x) { return x.ID_FilingType === ft }).ForEach(function (x) {
                    $s.CBAction(x.ID, ft, false);
                });
            }
        });
        $s.BatchApprovals = (function (m) {
            if ($s.ListData.length != 0) {
                $s.Request("BatchApprovals", { Data: $s.ListData, ID_User: $s.Session("ID_User"), Mode: m }).then(function (r) {
                    $s.Toast(r);
                    setTimeout(function () {
                        $s.LoadData();
                    },500)
                })
            }
        });

        $s.CheckAll = (function () {
            var c = $s.Icon == 'Select All' ? true : false;
            var par = $("[targetcb$=filingtype]")
            par.each(function (x) {
                var _ = $(this);
                _.prop("checked", c).change();
            })
            if (c) {
                $s.Icon = 'Deselect All';
                Enumerable.From($s.MyApprovals).ForEach(function (x) {
                    $s.SelectAll(x.ID, true);
                });
            } else {
                $s.Icon = 'Select All';
                Enumerable.From($s.MyApprovals).ForEach(function (x) {
                    $s.SelectAll(x.ID, false);
                });
            }
        });

        $s.LoadData = (function () {
            $s.Request("GetData", { ds: $s.Menu.tMenu, lp: "" }).then(function (data) {
                $s.AData = data;
                $s.MyApprovals = Enumerable.From(data.Source).Select(function (x) { return { ID: x.ID_FilingType, Name: x.Name, ID_Menu: x.ID_Menu } }).Distinct(function (x) { return x.Name }).ToArray();
            });
        })

    })
    app.register.controller('MyApprovals', ['$scope', '$controller', 'resources', '$state', 'DataService', MyApprovals]);

})