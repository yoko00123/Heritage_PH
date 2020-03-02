/// <reference path="AccountComponent.js" />

'use strict';
define(['app'], function (app) {
    var defaultCtrl = function ($s, $c, $ss, $rs) {
        $c("BaseController", { $scope: $s });

        $s.Controller = "Login";

        $s.data = {
            Username: null,
            Password: null,
            SelectCompanyAfterValidation: false
        }

        $s.Companies = [];
        $s.UserData = null;

        $s.InitCompanies = (function () {
            //, { GUID: vcl.Cookie('X-UID') }
            try {
                $s.Request('Companies').then(function (d) {
                    $s.Companies = d.tCompany;
                    $s.UserData = d;
                });
            } catch (ex) {
                $s.LogOut();
            }

        })

        $s.SelectCompany = (function (c) {
            $s.Request('StartSession', {
                ID: $s.UserData.ID,
                ID_Employee: $s.UserData.ID_User,
                ID_Company: c.ID === 0 ? null : c.ID,
                GUID: $s.UserData.GUID
            }).then(function (d) {
                $ss.UserRow(d);
                window.sessionStorage.setItem("ActiveApplicationType", (d.ID_ApplicationType == null ? 1 : d.ID_ApplicationType));
                vcl.Cookie('X-UID', null, -1);
                setTimeout(function () {
                    $s.ActionUrl('Index', 'Home');
                }, 1000);
            })
        })

        $s.Validating = false;
        $s.ValidationTrials = 0;
        $s.CaptchaValue = null;
        $s.CaptchaInput = null;

        $s.SignIn = (function () {
             
            if ($s.Validating) return;

            if ($s.CaptchaValue == null) {
                if ($s.ValidationTrials >= 2) {
                    vcl.Cookie('Captcha', 1);
                    $s.GenerateCaptcha();
                    return;
                } else
                    vcl.Cookie('Captcha', null, -1);
            } else {
                if ($s.CaptchaInput != $s.CaptchaValue) {
                    $s.GenerateCaptcha();
                    return;
                } else {
                    $s.CaptchaInput = $s.CaptchaValue = null;
                    $s.ValidationTrials = 0;
                }
            }

            $s.Validating = true;

            $s.Request('AuthUser', $s.data).then(function (d, s) {
                $s.Validating = false;
                $s.ValidationTrials = 0;
                switch (s) {
                    case 4: //Select Company
                        $s.ActionUrl('Companies', 'Account');
                        break;
                    case 3: //Expired Password
                        $s.Dialog({
                            template: 'FirstLog.tmpl.html',
                            size: 'md',
                            controller: 'changePassword',
                            data: { IsFirstLog: false, d: d }
                        }).result.then(function (d) {
                            //$s.printDialog(d);
                        })
                        break;
                    case 2: //First time
                        $s.Dialog({
                            template: 'FirstLog.tmpl.html',
                            size: 'md',
                            controller: 'changePassword',
                            data: { IsFirstLog: true, d: d }
                        }).result.then(function (d) {
                            //$s.printDialog(d);
                        })
                        break;
                    default:
                        $s.UserData = d;
                        $s.SelectCompany({ ID: d.ID_Company });
                        break;
                }
                
            }).catch(function () {
                $s.Validating = false;
                $s.ValidationTrials++;
            })
        })

        $s.GenerateCaptcha = function () {

            $s.CaptchaInput = null;

            $s.Request('Captcha').then(function (x) {
                $s.CaptchaValue = x.Value;

                $('captcha').css({
                    'background-image': "url(" + x.Image +")",
                    'background-repeat': 'no-repeat',
                    'background-position': 'center', 
                })

            });
        }

        $s.LogOut = (function () {
            $ss.Clear();
            setTimeout(function () {
                $s.ActionUrl('LogOut', 'Account');
            }, 1000)
        })

        $s.SetIcon = (function (str) {
            var abbr = "";
            str = str.split(" ");
            for (var i = 0; i < str.length; i++) {
                abbr += str[i].substr(0, 1);
            }

            if (abbr.length > 2) {
                abbr = abbr.substr(0, 2);
            }

            return abbr.toLowerCase();
        })

        $s.SetSlide = (function (Id) {
            var $this = $("#ml_" + Id);
            var span = $("span", $this);
            var pwidth = $($this).width();
            var swidth = $("span", $this).width();

            if (pwidth < swidth) {
                $this.addClass("text-slide");
                span.css("marginLeft", (swidth + 5) * (-1));
                $(span).bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
                    $s.DeSetSlide();
                });
            }
        })

        $s.DeSetSlide = (function () {
            $(".company-ul li div p").removeClass("text-slide");
            $(".company-ul li div p span").css("marginLeft", 0);
        })

    }

    app.controller('AccountComponent', ['$scope', '$controller', 'Session', '$rootScope', defaultCtrl]);

    var changePassword = function ($sc, $cnt, $modal, $data) {
        $cnt("BaseController", { $scope: $sc });
        $sc.Master = {};
        $sc.Master.ID_SecurityQuestion = null;
        $sc.Master.SecurityAnswer = null;
        $sc.cList = [];
        $sc.IsFirstLog = $data.IsFirstLog;
        $sc.PasswordExpiration = null;
        var d = $data.d;
        $sc.Request('FetchQuestions').then(function (question) {
            $sc.cList = question.data;
        })
        $sc.Request('GetPasswordExpiration').then(function (ret) {
            $sc.PasswordExpiration = 'Your password meets ' + ret + ' days expiration, Please Change Your Password to log in';
        })

        $sc.typechange = 'password';
        $sc.typechange1 = 'password';
        $sc.keyupchange = function () {
            $sc.typechange = 'password';
        }
        $sc.keydownchange = function () {
            $sc.typechange = 'text';
        }
        $sc.keyupchange1 = function () {
            $sc.typechange1 = 'password';
        }
        $sc.keydownchange1 = function () {
            $sc.typechange1 = 'text';
        }

        $sc.Cancel = (function () {
            $modal.dismiss();
        })

        $sc.OK = (function () {


            if ($sc.Master.password !== $sc.Master.password1) {
                $sc.Toast('Must input the same password.', 'Set Password', 'warning');
            } else if (($sc.Master.password == undefined || $sc.Master.password1 == undefined) || ($sc.Master.password == null || $sc.Master.password1 == null)) {
                $sc.Toast('Must input password', 'Set Password', 'warning');
            } else if ($sc.IsFirstLog === true && (($sc.Master.ID_SecurityQuestion == undefined || $sc.Master.SecurityAnswer == undefined) || ($sc.Master.ID_SecurityQuestion == null || $sc.Master.SecurityAnswer == null) || ($sc.Master.ID_SecurityQuestion == "" || $sc.Master.SecurityAnswer == ""))) {
                console.log($sc.IsFirstLog)
                $sc.Toast('Must input Security Question and Answer', 'Set Security Question', 'warning');
            } else {
                if (parseInt(d) !== 1) {
                    $sc.Request('SetPassword', { password: $sc.Master.password, gUser: d, ID_User: d, IsFirstLog: $sc.IsFirstLog, question: $sc.Master.ID_SecurityQuestion, answer: $sc.Master.SecurityAnswer })
                        .then(function (d, s) {
                            if (s == 9) {
                                $sc.Toast(d);
                                $sc.Cancel();
                            } else {
                                $sc.Toast(d, 'Set Password', 'warning')
                            }
                        })
                } else
                    $sc.Toast('System Account cannot change password: Contact your system provider to resolve this issue');
            }

        });
    }

    app.controller('changePassword', ['$scope', '$controller', '$uibModalInstance', 'dData', changePassword]);
});