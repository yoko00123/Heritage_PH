
define(['app'], function (app) {

    var Helper = (function ($s, $c, $st) {
        $c('BaseController', { $scope: $s });
        $("#fHelper").attr("width", $(".main-content").width())
        $("#fHelper").attr("height", $(".main-content").height() - 40)
       
        $('#fHelper').each(function () {
            var $iframe = $(this);
            function injectCSS() {
                $iframe.contents().find('head').append(
                    $('<link/>', { rel: 'stylesheet', href: 'css/faq.css', type: 'text/css' })
                );
            }
            $iframe.on('load', injectCSS);
            injectCSS();
        });
    })


    app.register.controller('Helper', ['$scope', '$controller', '$state', Helper]);
})