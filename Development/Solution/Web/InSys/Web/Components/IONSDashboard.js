
define(['app'], function (app) {

    var IONSDashboard = (function ($s, $c, $ds, $state, d) {
        $c('BaseController', { $scope: $s });
        $s.Employee = [];
        $s.LeaveCredits = [];
        $s.Announcements = [];
        $s.FilingData = [];
        $s.ApprovalData = [];
        $s.CancellationData = [];
        $s.ShowOverlay();
        $s.ApprovalCount = 0;
        $s.Request('Session').then(function (dta) {
            // console.log('session', dta);
            $s.UserRow(dta);

            $s.Init();
        });
        $s.Init = (function () { 
            $s.Request("IONSDashboard", { ID_User: $s.Session("ID_User") }).then(function (data) { 
                $s.Employee = data.Employee[0]; 
                //console.log($s.Employee)
                $s.LeaveCredits = data.LeaveCredits;
                $s.FilingData = Enumerable.From(data.FilingData).Where(function (x) { return x.Filing == 1 }).Select(function (x) { return { Name: x.Name, ID_Menu: x.ID_MenuFiling, Count: x.Count, Icon: x.IconFontAwesome } }).ToArray();
                $s.ApprovalData = Enumerable.From(data.FilingData).Where(function (x) { return x.Filing == 0 }).Select(function (x) { return { Name: x.Name, ID_Menu: x.ID_MenuApproval, Count: x.Count, Icon: x.IconFontAwesome } }).ToArray();
                $s.CancellationData = Enumerable.From(data.FilingData).Where(function (x) { return x.Filing == 3 }).Select(function (x) { return { Name: x.Name, ID_Menu: x.ID_MenuCancellationApproval, Count: x.Count, Icon: x.IconFontAwesome } }).ToArray();
            });
            $s.Request("getAnnouncements", { ID_ApplicationType: 2, ID_Company: parseInt($s.Session("ID_Company")) }).then(function (obj) {
                $s.Announcements = Enumerable.From(obj).Where(function (x) { return x.ID_AnnouncementType != 2 }).ToArray();
            });
            $s.GetMenu(3059).then(function (m) {
                m.tMenu.DataSource = $s.PassParameter(m.tMenu.DataSource)
                $s.Request("GetData", { ds: m.tMenu, lp: "" }).then(function (ap) {
                    $s.ApprovalCount = ap.Source.length;
                });
            })
            $s.HideOverlay();
        });
        $s.Active = (function (ind) {
            if (ind == 0)
                return true
            else
                return false
        })

        $s.OpenHR = (function () {
            d.GetMenu(2047).then(function (m) {
                $state.go('List', {
                    Name: m.tMenu.Name.replace(/ /g, '-'),
                    r: LZString.compressToEncodedURIComponent(m.tMenu.ID.toString())
                });
            });
        });

        $s.OpenTK = (function (mid) { 
            d.GetMenu(mid).then(function (m) { 
                $state.go('List', {
                    Name: m.tMenu.Name.replace(/ /g, '-'),
                    r: LZString.compressToEncodedURIComponent(m.tMenu.ID.toString())
                });
            });
        });
        $s.col = 'Attachment';
        $s.DownloadAttachment = function (data, col) {
            var p = { FileName: data[col + '_GUID'], OrigName: data[col] };
            $s.Download('DownloadFileTab', p);
        }
    });
    app.register.controller('IONSDashboard', ['$scope', '$controller', 'DataService', '$state', 'DataService', IONSDashboard]);
})