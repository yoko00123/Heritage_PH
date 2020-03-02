
define(['app'], function (app) {

    var Dashboard = (function ($s, $c, $ds) {
        $c('BaseController', { $scope: $s });

        $s.Init = (function () {
            $(".dash-board").addClass("toggled-dashboard");
            $s.toggled = $(".dash-board").hasClass("toggled-dashboard");
            //setTimeout(function () {
            //    $(".dash-board").removeClass("toggled-dashboard");
            //}, 2000)
        })
         
        var widget = $ds.GetWebWidget('tWebWidgets');
        if (parseInt($s.Session("ID_User")) > 2) {
            var ids = Enumerable.From(JSON.parse(LZString.decompressFromUTF16($s.Session('UserWebWidgetIDList')))).Select(function (x) { return { ID: x.Item1 }; }).ToArray();
            widget.then(function (d) {
                $s.WebWidget = Enumerable.From(d)
                    .Join(ids, '$.ID', '$.ID', function (a, b) {
                        return a;
                    })
                    .Where(function (r) {
                        return r.ID_ApplicationType == $s.ActiveApplicationType;
                    })
                    .ToArray();
            });
        } else {
            widget.then(function (d) {
                $s.WebWidget = Enumerable.From(d).Where(function (r) { return r.ID_ApplicationType == $s.ActiveApplicationType }).ToArray();
            });
        }

        $s.ToggledDashBoard = (function () {
            $(".dash-board").toggleClass("toggled-dashboard");
            $s.toggled = $(".dash-board").hasClass("toggled-dashboard");
        })

    })

    app.register.controller('Dashboard', ['$scope', '$controller', 'DataService', Dashboard]);
})