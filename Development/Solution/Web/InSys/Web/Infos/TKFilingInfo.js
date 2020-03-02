
define([], function () {
    return (function ($s, $c, $r, $info, $sp) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });

        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.OnAfterDocumentDefault = (function () {
            var ee = LZString.decompressFromEncodedURIComponent($sp.r).split('-')[2];

            if (ee == undefined) {
                $s.data.Row.ID_Employee = $s.Session('ID_Employee');
                $s.data.Row.Employee = $s.Session('Employee');
            }
            else {
                var edr = ee.split(':');
                $s.data.Row.ID_Employee = parseInt(edr[0]);
                $s.data.Row.Employee = edr[1];
            }
            
        });

    })
})
