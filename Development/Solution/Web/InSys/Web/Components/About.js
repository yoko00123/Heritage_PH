
define(['app'], function (app) {

    var About = (function ($s, $c) {
        $c('BaseController', { $scope: $s });


        $s.InitAbout = (function () {
            $s.setHeight();
        })

        $s.setHeight = (function () {
            var offsetTop = $(".main-content")[0].offsetTop;
            var clientHeight = $(".main-content")[0].clientHeight;
            $(".about-container").css("maxHeight", (clientHeight - 35) - offsetTop);
            $(".about-container").css("minHeight", (clientHeight - 35) - offsetTop);
        })
    })

    app.register.controller('About', ['$scope', '$controller', About]);

})