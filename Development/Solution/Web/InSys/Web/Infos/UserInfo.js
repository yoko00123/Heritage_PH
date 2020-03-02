/**
 * @LJ20170622
 * TK Processing
 * 
 */
define(['app'], function (app) {
    return (function ($s, $c, $r, $info) {
        $c('InfoSet', { $scope: $s, resources: $r, $info: $info });
        $s.SetController('UserInfo');

        //@Override
        $s.AfterInit = (function () {
            return $s.Task(function (d) {

                $s.AddButton('Reset Password', $s.ResetPassword);
                $s.AddButton('Unlock User', $s.UnlockUser);
                $s.AddButton('Change Password', $s.ChangePassword);
                if (parseInt($s.Session('ID_User')) === 1) {
                    $s.AddButton('Show Password', $s.ShowPassword);
                }
                d.resolve();
            });
        })

        //@Override
        $s.FormLoad = (function () {
            $s.LoadData();
        });

        $s.ResetPassword = (function () {
            if ($s.data.Row.ID_Employee == null && ($s.data.Row.ID_ApplicationType == null || $s.data.Row.ID_ApplicationType == 2)) {
                $s.Toast('Cannot Reset Password, User\'s UserGroup is not applicable for ResetPassword', 'danger');
            } else if ($s.data.Row.ID !== 1) {
                $s.Confirm('Do you want to reset user\'s password?').then(function () {
                    $s.Request('ResetPassword', { ID_User: $s.data.Row.ID }).then(function (d) {
                        $s.data.Row.Password = d.Password
                        $s.data.Row.IsFirstLog = d.IsFirstLog
                        $s.SaveInfo().then(function () {
                            $s.Toast('Password has been Reset.')
                        })
                    })
                });
            } else {
                $s.Toast('Cannot reset System Password.', 'danger')
            }

        })

        $s.UnlockUser = (function () {
            $s.Confirm('Do you want to unblock user?').then(function () {
                $s.Request('UnlockUser', { ID_User: $s.data.Row.ID }).then(function (u) {
                    $s.Toast('User is Unblocked.')
                });
            });
        })

        $s.ChangePassword = (function () {
            $s.Dialog({
                controller: ['$scope', 'dData', '$uibModalInstance', '$controller', function ($s, $data, $d, $c) {
                    $c('BaseController', { $scope: $s });

                    //Show Password Weng 20160812
                    $s.Master = {};
                    $s.typechange = 'password';
                    $s.typechange1 = 'password';
                    $s.keyupchange = function () {
                        $s.typechange = 'password';
                    }
                    $s.keydownchange = function () {
                        $s.typechange = 'text';
                    }
                    $s.keyupchange1 = function () {
                        $s.typechange1 = 'password';
                    }
                    $s.keydownchange1 = function () {
                        $s.typechange1 = 'text';
                    }

                    $s.Cancel = (function () {
                        $d.dismiss();
                    })

                    $s.OK = (function () {
                        if ($s.Master.password !== $s.Master.password1) {
                            $s.Toast('Must input the same password.', 'danger');
                        } else if ($s.Master.password == undefined && $s.Master.password1 == undefined || $s.Master.password == null && $s.Master.password1 == null) {
                            $s.Toast('Must input password', 'danger');
                        } else {
                            if (parseInt($data.ID_User) !== 1) {
                                $s.Request('SetPassword', { password: $s.Master.password, gUser: $s.Session('ID_User'), ID_User: $data.ID_User })
                                    .then(function (d, s) {
                                        console.log(d, s);
                                        if (s == 9) {
                                            console.log('asdasd');
                                            $d.close(0);
                                            $s.Toast(d);

                                        } else {
                                            $s.Toast(d, 'error')
                                        }
                                    })
                            }
                        }

                    })
                }],
                template: 'ChangePassword.tmpl.html',
                size: 'sm',
                data: { ID_User: $s.data.Row.ID }
            })
        })

        $s.ShowPassword = (function () {
            $s.Request('ShowPassword', { ID_User: $s.data.Row.ID }).then(function (d) {
                if (d != null) {
                    $s.Confirm(d.Spassword.substr(0, d.Spassword.length - 7), 'Your Password');
                }
            })
        })

    });
});