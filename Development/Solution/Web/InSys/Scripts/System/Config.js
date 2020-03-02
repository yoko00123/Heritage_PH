'use strict';
define(['app'], function (app) {
    app.config(['$stateProvider', '$stickyStateProvider', '$urlRouterProvider', '$controllerProvider', '$compileProvider', '$provide', '$httpProvider', '$locationProvider', 'DataServiceProvider', 'cfpLoadingBarProvider', 'toastrConfig',
        function ($stateProvider, $stickyStateProvider, $urlRouterProvider, $controllerProvider, $compileProvider, $provide, $httpProvider, $locationProvider, DataServiceProvider, cfpLoadingBarProvider, toastrConfig) {

            var c = "/Web/Components/",
                v = "/Web/Views/",
                ApplicationType;
            
            cfpLoadingBarProvider.includeSpinner = false;
            DataServiceProvider.SetDefaultController('Action');
            DataServiceProvider.SetAppName(document.title);
            
            angular.extend(toastrConfig, {
                positionClass: 'toast-bottom-right',
                target: 'body'
            });

     

            function rD(q, R, dependencies) {
                var d = q.defer();
                require(dependencies, function () {
                    d.resolve();
                    R.$apply();
                });
                return d.promise;
            };

            app.register = {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                factory: $provide.factory,
                service: $provide.service
            };

            var Session = function (name) {
                var bh = vcl.Encryption.Encrypt(window.localStorage.getItem(name), window.localStorage.getItem('SessionID'), false);
                if (bh === null || bh === 'null') return null;
                return bh === 'true' || bh === 'false' ? (bh === 'true' ? true : false) : bh;
            }

            if (window.sessionStorage.ActiveApplicationType != null && window.sessionStorage.ActiveApplicationType != typeof (undefined)) {
                ApplicationType = window.sessionStorage.ActiveApplicationType;
            } else {
                if (Session('ID_ApplicationType') == null) {
                    ApplicationType = 1;
                } else {
                    ApplicationType = Session('ID_ApplicationType');
                }
            }

            var S = [];

            //S.push({
            //    name: 'Dashboard',
            //    url: '/Dashboard',
            //    controller: 'Dashboard',
            //    templateUrl: v + 'Dashboard.html',
            //    resolve: {
            //        load: ['$q', '$rootScope', function (q, R) {
            //            return rD(q, R, [c + 'Dashboard.js']);
            //        }]
            //    }
            //})

            S.push({
                name: 'List',
                url: '/List/{Name}/{r}',
                controller: 'List',
                templateUrl: v + 'List.html',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'List.js']);
                    }],
                    resources: ['DataService', '$stateParams', '$state', function (d, S, state) {
                        var o = parseFloat(LZString.decompressFromEncodedURIComponent(S.r));
                        return d.Post('GetMenu', { ID_Menu: o })
                            .fail(function (ex) {
                                window.location = '#!/404';
                            });
                    }]
                }
            })

            S.push({
                name: 'Report',
                url: '/Report/{Name}/{r}',
                controller: 'Report',
                templateUrl: v + 'Report.html',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'Report.js']);
                    }],
                    resources: ['DataService', '$stateParams', function (d, S) {
                        var o = parseFloat(LZString.decompressFromEncodedURIComponent(S.r));
                        d.SetController('Info');
                        return d.Post('GetInfoSchema', { ID_Menu: o }).fail(function (ex) {
                            window.location = '#!/404';
                        });

                    }]
                }
            })

            S.push({
                name: 'Info',
                url: '/Info/{Name}/{r}',
                controller: 'ZInfo',
                templateUrl: v + 'Info.html',
                resolve: {
                    load: ['$stateParams', 'ActiveModule', function ($sp, am) {
                        var j = LZString.decompressFromEncodedURIComponent($sp.r).split('-');
                        return am.Load(parseFloat(j[0]));
                    }],
                    resources: ['$stateParams', 'DataService', function (S, res) {
                        var o = LZString.decompressFromEncodedURIComponent(S.r).split('-');
                        res.SetController("Info");
                        return res.Post('GetInfoSchema', { ID_Menu: o[0] });
                    }],
                    $info: ['$stateParams', function ($sp) {
                        return LZString.decompressFromEncodedURIComponent($sp.r).split('-')[1];
                    }]
                }
            })

            // DO NOT REMOVE 
            // 404
            S.push({
                name: 'Unauthorized',
                url: '/404?r',
                templateUrl: v + 'Unauthorized.html',
                controller: ['$scope', '$state', '$rootScope', '$stateParams', function (s, S, R, sp) {

                    s.ErrorMsg = sp.r ? LZString.decompressFromEncodedURIComponent(sp.r) : '<i class="fa fa-fw fa-warning fa-lg text-warning"></i> Page <u>Not</u> Found';

                    s.GoBack = function () {
                        if (R.prevState == undefined) {
                            window.location = '/Home';
                        } else {
                            S.go(R.prevState.name, R.prevParams, { reload: true, inherit: false, notify: true });
                        }

                    }

                }]
            });

            S.push({
                name: 'MyProfile',
                url: '/MyProfile',
                templateUrl: v + 'MyProfile.html',
                controller: 'MyProfile',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'MyProfile.js']);
                    }],
                    resources: ['DataService', '$stateParams', '$state', function (d, S, state) {
                        return d.GetMenu(16);
                    }]
                }
            })

            S.push({
                name: 'About',
                url: '/About',
                templateUrl: v + 'About.html',
                controller: 'About',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'About.js']);
                    }],
                }
            })

            S.push({
                name: 'Contents',
                url: '/Contents',
                templateUrl: v + 'Contents.html',
                controller: 'Contents',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'Contents.js']);
                    }]
                }
            })

            S.push({
                name: 'Themes',
                url: '/Themes',
                controller: 'Themes',
                templateUrl: v + 'Themes.html',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'Themes.js']);
                    }]
                }
            })

            //S.push({
            //    name: 'Dashboard2',
            //    url: '/Dashboardv2',
            //    controller: 'Dashboard2',
            //    templateUrl: v + 'Dashboard2.html',
            //    resolve: {
            //        load: ['$q', '$rootScope', function (q, R) {
            //            return rD(q, R, [c + 'Dashboard2.js']);
            //        }]
            //    }
            //})
            if (ApplicationType == 1) {
                S.push({
                    name: 'Dashboard',
                    url: '/Dashboardv3',
                    controller: 'Dashboard3',
                    templateUrl: v + 'Dashboard3.html',
                    resolve: {
                        load: ['$q', '$rootScope', function (q, R) {
                            return rD(q, R, [c + 'Dashboard3.js']);
                        }]
                    }
                });
            }

            S.push({
                name: 'IONS',
                url: '/IONSDashboard',
                controller: 'IONSDashboard',
                templateUrl: v + 'IONSDashboard.html',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'IONSDashboard.js']);
                    }]
                }
            })

            S.push({
                name: 'Helper',
                url: '/Helper',
                controller: 'Helper',
                templateUrl: v + 'Helper.html',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'Helper.js']);
                    }]
                }
            })

            S.push({
                name: 'MyApprovals',
                url: '/MyApprovals',
                templateUrl: v + 'MyApprovals.html',
                controller: 'MyApprovals',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'MyApprovals.js']);
                    }],
                    resources: ['DataService', '$stateParams', '$state', function (d, S, state) {
                        return d.GetMenu(3059);
                    }]
                }
            })

            S.push({
                name: 'MyCancellations',
                url: '/MyCancellations',
                templateUrl: v + 'MyCancellations.html',
                controller: 'MyCancellations',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'MyCancellations.js']);
                    }],
                    resources: ['DataService', '$stateParams', '$state', function (d, S, state) {
                        return d.GetMenu(4088);
                    }]
                }
            })

            S.push({
                name: 'DirectReport',
                url: '/DirectReport',
                templateUrl: v + 'DirectReport.html',
                controller: 'DirectReport',
                resolve: {
                    load: ['$q', '$rootScope', function (q, R) {
                        return rD(q, R, [c + 'DirectReport.js']);
                    }],
                    resources: ['DataService', '$stateParams', '$state', function (d, S, state) {
                        return d.GetMenu(3060);
                    }]
                }
            })

            angular.forEach(S, function (state) {
                try {
                    $stateProvider.state(state);
                } catch (e) {
                    console.log(e);
                }

            });

            if (window.location.pathname.indexOf('Account') != -1) {
                $urlRouterProvider.otherwise("/");
            }
            else {
                ApplicationType = ApplicationType == 1 ? "/Dashboardv3" : "/IONSDashboard";
                if (window.location.hash.indexOf("IONSDashboard") >= 0 && ApplicationType == "/Dashboardv3")
                    window.location = '#!' + ApplicationType;
                $urlRouterProvider.otherwise(ApplicationType);
            }

        }]);

    app.run(['$rootScope', '$http', '$cookies', 'DataService', 'Session', '$timeout', '$document', function ($rs, $http, $cookies, $ds, $ss, $timeout, $document) {

        if ($cookies)
            $http.defaults.headers.post['X-CSRF-Token'] = $cookies.get('XSRF-TOKEN');

        var insys = document.querySelector("insys");
        var auth = insys.innerHTML.trim();

        $ds.SetEncryption(auth);
        insys.parentNode.removeChild(insys);

        var timeOutElem = document.querySelector('sessionTimeOut');

        if (timeOutElem !== null) {
            var timeOut = parseInt(timeOutElem.innerHTML.trim());
            timeOutElem.parentNode.removeChild(timeOutElem);
            var TimeOutTimerValue = timeOut * 1000 * 60;

            //console.log(TimeOutTimerValue, 'to');

            // Start a timeout
            var TimeOut_Thread = $timeout(function () { LogoutByTimer() }, TimeOutTimerValue);
            var bodyElement = angular.element($document);

            /// Keyboard Events
            bodyElement.bind('keydown', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('keyup', function (e) { TimeOut_Resetter(e) });

            /// Mouse Events	
            bodyElement.bind('click', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('mousemove', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('DOMMouseScroll', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('mousewheel', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('mousedown', function (e) { TimeOut_Resetter(e) });

            /// Touch Events
            bodyElement.bind('touchstart', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('touchmove', function (e) { TimeOut_Resetter(e) });

            /// Common Events
            bodyElement.bind('scroll', function (e) { TimeOut_Resetter(e) });
            bodyElement.bind('focus', function (e) { TimeOut_Resetter(e) });

            function LogoutByTimer() {
                var action = "LogOut",
                    controller = "Account";

                var url;
                if (controller.toLowerCase().indexOf("controller") >= 0) {
                    url = '/' + controller.replace('Controller', '') + '/' + action;
                } else {
                    url = '/' + controller + '/' + action;
                }
                if (vcl.String.StartsWith(url, '/http'))
                    return url.substr(1);

               // window.location = url;
            }

            function TimeOut_Resetter(e) {

                /// Stop the pending timeout
                $timeout.cancel(TimeOut_Thread);

                /// Reset the timeout
                TimeOut_Thread = $timeout(function () { LogoutByTimer() }, TimeOutTimerValue);
            }
        }
    }])
});