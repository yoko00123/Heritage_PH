
define([], function () {
    return (function ($s, $c, $r, $info, $cpl) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('TKProcessingInfo');

        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.AfterInit = (function () {
            $s.GetButton('Download Daily Time Record').Click = $s.DailyTimeRecord;

            return $s.Task(); 
        })

        $s.DailyTimeRecord = (function () {
            $s.ShowOverlay();
            $s.Request('DailyTimeRecord', { ID: $s.data.Row.ID, ID_Session: $s.Session('ID_Session') }).then(function (fs) {
                $s.HideOverlay();
                $s.DownloadSlim(fs, 'Files');
            }).catch(function (x) {
                $s.HideOverlay();
                return x;
            });
        })

        var DetailOpenBase = $s.DetailOpen;
        $s.DetailOpen = (function (ID_Menu, ID, ID_DetailMenu) {
            $s.ShowOverlay();
            $s.Request('DailyTimeRecord', { ID: $s.data.Row.ID, ID_Session: $s.Session('ID_Session'), ID_Employee: ID }).then(function (fs) {
                $s.HideOverlay();
                $s.DownloadSlim(fs, 'Files');
            }).catch(function (x) {
                $s.HideOverlay();
                return x;
            });
        })
    })
})
