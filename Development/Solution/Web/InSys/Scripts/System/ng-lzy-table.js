'use-strict';
angular.module('lzyTable', [])
    .controller('gCtrl', ['$scope', '$element', '$attrs', '$controller', function ($s, $e, $a, $c) {
        $c('lzyGridController', { $scope: $s, $element: $e, $attrs: $a });
    }])
    .directive('lzyTable', ['$timeout', '$controller', function ($t, $c) {
        return {
            templateUrl: '../Web/Template/lzy-grid.tmpl.html',
            restrict: 'E',
            replace: true,
            scope: {
                tableOptions: '=?'
            },
            controller: 'gCtrl'
        }
    }]);