
var env = $('environment');

var IsDev = env.attr('prod') === 'false';  

require.config({
    baseUrl: '/Scripts/System/' + (IsDev ? '' : 'Out'),
});
require(
    [
        'App' + (IsDev ? '' : '.min'),
        'Config' + (IsDev ? '' : '.min'),
        'Controllers' + (IsDev ? '' : '.min'),
        'Dataservices' + (IsDev ? '' : '.min'),
        'Directives' + (IsDev ? '' : '.min'),
        'MainComponent2' + (IsDev ? '' : '.min'),
        'InfoSet' + (IsDev ? '' : '.min'),
        'ActiveModule' + (IsDev ? '' : '.min'),
        'ng-lzy-table' + (IsDev ? '' : '.min')
    ],
    function (app) {
        window.PreLoad(function () {
             
            angular.bootstrap(document, ['app'])
            setTimeout(function () {
                $('#window-screen').fadeOut();
            }, 1000);

        })
    }
);
 
var cssRule =
    "color: rgb(249, 162, 34);" +
    "font-size: 30px;" +
    "font-weight: bold;" +
    "text-shadow: 1px 1px 5px rgb(249, 162, 34);" +
    "filter: dropshadow(color=rgb(249, 162, 34), offx=1, offy=1);";
setTimeout(console.log.bind(console, '%cIntellismart Technology Inc. 2018', cssRule), 0);

	


