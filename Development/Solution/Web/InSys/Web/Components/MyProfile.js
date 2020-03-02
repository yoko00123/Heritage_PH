
define(['app'], function (app) {

    var MyProfile = (function ($s, $c, r) {
        $c('BaseController', { $scope: $s });
        $s.Controller = "Action";

        $s.Menu = r;
        $s.Menu.tMenu.DataSource = $s.PassParameter($s.Menu.tMenu.DataSource);
        $s.Parent = {};
        $s.IsNull();
        $s.ElseVal = 'n/a';
        $s.ParentColumns = [];
        $s.Detail = {}; 
        $s.Request("GetProfile", $s.Menu).then(function (res) {
            console.log('anu tong $s.Menu ang alam ko eh eto info home kaso wala laman p to. gwa sa direct',$s.Menu);
            $s.Parent = res.data.ParentData.Rows[0];
            $s.ParentColumns = res.data.ParentData.Columns;
            angular.forEach($s.Menu.tMenuDetailTab, function (obj, idx) {
                $s.Detail[obj.ID] = {};
                $s.Detail[obj.ID].Rows = res.data.tabFieldData[obj.ID];
                var cols = Enumerable.From($s.Menu.tMenuDetailTabField).Where(function (x) { return x.ID_MenuDetailTab == obj.ID }).ToArray();
                cols = Enumerable.From(cols).Where(function (x) { return x.Name.toLowerCase() != 'id' }).ToArray();
                $s.Detail[obj.ID].Columns = cols;
                $s.Detail[obj.ID].Menu = obj;
            });
        });

        $s.removeSpace = (function (id) {
            var id_name = id.replace(/\s/g, '');
            return id_name;
        })

        $s.FormatBD = (function (r) {
            if (vcl.DateTime.IsNewtonFormat(r)) {
                r = vcl.DateTime.Format(r, 'mmmm dd, yyyy');
            }
            return r;
        });

        $s.InitProfile = (function () {
            $s.SetHeight();

            setTimeout(function () {
                $(".side-menu").addClass("toggle-sidemenu");
                $(".untoggled-actions").addClass("toggle-actions");
                $(".bookmark-panel").removeClass("toggle-bookmark");
            }, 500)

            setTimeout(function () {
                $s.ToggleProfilePanel();
            }, 1000)
        })

        $s.ToggleUpload = (function () {
            $(".camera").addClass("camera-toggle");
        })

        $s.ToggleUploadLeave = (function () {
            $(".camera").removeClass("camera-toggle")
        })

        $s.ToggleProfilePanel = (function () {
            $(".profile-panel").addClass("profile-toggled");
        })

        $s.RemovePanel = (function () {
            $(".profile-panel").removeClass("profile-toggled");
        })

        $s.SetHeight = (function () {
            setTimeout(function () {
                var offsetTop = $(".main-content")[0].offsetTop;
                var clientHeight = $(".main-content")[0].clientHeight;
                $(".profile-container").css("maxHeight", (clientHeight - 97) - offsetTop);
                $(".profile-container").css("minHeight", (clientHeight - 97) - offsetTop);
            }, 1000)
        })
    })

    app.register.controller('MyProfile', ['$scope', '$controller', 'resources', MyProfile]);

})