define(['app'], function (app) {
    var DirectReport = (function ($s, $c, r, $st, d) {
        $c('BaseController', { $scope: $s });
        $s.Controller = "Action";
        $s.Menu = r;
        $s.Menu.tMenu.DataSource = $s.PassParameter($s.Menu.tMenu.DataSource);
        $s.Menu.Columns = Enumerable.From($s.Menu.tMenuTabField).Where(function (x) { return x.ID_SystemControlType !== 15 }).ToArray();
        $s.AData = [];
        $s.Employee = [];
        $s.ELC = [];
        $s.FilingModule = [];

        $s.FormLoad = (function () {
            $s.LoadData();
        });


        $s.Init = (function () {
            $s.Request("GetData", { ds: $s.Menu.tMenu, lp: "" }).then(function (data) {
                $s.AData = data;
                if (data.Source.length == 1)
                    $s.Employee = data.Source[0];
            });
            $s.Request("ActiveFilingType").then(function (d) { 
                $s.FilingModule = Enumerable.From(d).Where(function (x) {return x.ID !== 7 }).ToArray();
            });
        });
        $s.SearchEmployee = (function (e) {
            return (function (d) {
                if ($s.AData.Source.length > 0) {
                    if (d.Name.toLowerCase().indexOf(e) >= 0 || e == undefined || d.Department.toLowerCase().indexOf(e) >= 0) {
                        return d;
                    }
                }
            });
        })

        $s.SelectEmployee = (function (d) {
            $s.Employee = d;
            var m = { CommandText: $s.PassParameter($s.Menu.tMenuLoadParameters[0].CommandText, d, "$") };
            $s.Request("GetData", { ds: m, lp: "ds" }).then(function (data) {
                $s.ELC = data.Source;
            });
            
        })

        $s.DateFormatter = (function (data) {
            return vcl.DateTime.Format(data, "mmm d yyyy");
        })

        $s.OpenInfo = (function (ft, eid) {
            d.GetMenu(ft).then(function (x) {
                var param = x.tMenu;
                var kk = param.ID + '-0-' + eid + ':' + $s.Employee.Name;
                var lz = LZString.compressToEncodedURIComponent(kk);
                $st.go('Info', {
                    Name: param.Name.replace(/ /g, '-'),
                    r: lz
                });
            });
        });

        $s.IsNull = (function (d) {
            if (d == undefined)
                return "-"
            else
                return d
        })
    })
    app.register.controller('DirectReport', ['$scope', '$controller', 'resources', '$state', 'DataService', DirectReport]);

})