'use strict';
define([], function () {
    var A = angular.module('app', [
        'ui.bootstrap',
        'ct.ui.router.extras',
        'ngAnimate',
        'ngSanitize',
        'angular-loading-bar',
        'toastr',
        'ngCookies',
        'rosWidget',
        'dndLists',
        'ui.bootstrap.contextMenu',
        'angucomplete',
        'lzyTable',
        'textAngular'
    ]);
    return A;
});