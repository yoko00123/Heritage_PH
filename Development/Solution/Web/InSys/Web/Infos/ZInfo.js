/**
 * @LJ20170609
 * Default template for infoset
 * 
 */
define([], function () {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });
    })
})
 