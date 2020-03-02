
define(['app'], function (app) {

    var Themes = (function ($s, $c, $ds, $state) {
        $c('BaseController', { $scope: $s });

        $s.Request("FetchThemes", {}).then(function (skn) {
            $s.skins = [];
            $s.gridRow = [];

            Enumerable.From(skn.data).ForEach(function (x) {
                $s.skins.push(x);
            });

            for (var x = 0; x < (skn.data.length / 3) ; x++) {
                $s.gridRow.push({ start: (x * 3), end: ((x + 1) * 3) });
            }

            if ((skn.data.length % 3) > 0) {
                $s.gridRow.push({ start: ($s.gridRow.length * 3), end: (($s.gridRow.length + 1) * 3) });
            }
        });

        $s.NewTheme = function () {
            $s.Dialog({
                template: 'ThemesDialog.tmpl.html',
                controller: 'ThemesDialogController',
                size: 'lg',
                data: {
                    skin: {
                        Header: null,
                        HeaderSearch: null,
                        ID: 0,
                        ID_Company: null,
                        IsActive: false,
                        LabelColorRB: null,
                        Name: null,
                        PrimaryColor: null,
                        RightBox: null,
                        RightBoxHeader: null,
                        RightBoxInput: null,
                        SearchTextColor: null,
                        SecondaryColor: null,
                        SideMenu: null,
                        TextColor: null,
                        TextColorMenu: null,
                        TextColorRB: null
                    }
                }
            }).result.then(function (d) {
                //$s.printDialog(d);
            })
        }

        $s.OpenTheme = function (skn) {
            $s.Dialog({
                template: 'ThemesDialog.tmpl.html',
                controller: 'ThemesDialogController',
                size: 'lg',
                data: { skin: skn }
            }).result.then(function (d) {
                //$s.printDialog(d);
            })
        }

        $s.$watch("skins", function () {
            $s.selectedTheme = Enumerable.From($s.skins).Where(function (x) { return x.IsActive }).ToArray()[0];
        });

        $s.toggleTheme = function (skn) {
            $s.selectedTheme = skn;
        }

        $s.ApplyTheme = function () {
            $s.Request("ApplyTheme", { ID_Skin: $s.selectedTheme.ID }).then(function (ob) {
                if (ob.error != undefined) {
                    $s.Toast(ob.error, 'Apply Theme', 'warning');
                } else {
                    $s.Toast("Theme Applied Successfully!", 'Apply Theme', 'success');
                    $state.reload();
                }
            });
        }

        var offsetTop = $(".main-content")[0].offsetTop;
        var clientHeight = $(".main-content")[0].clientHeight;
        $(".web-themes .table-container").css("maxHeight", (clientHeight - 30) - offsetTop);
        $(".web-themes .table-container").css("minHeight", (clientHeight - 30) - offsetTop);
    })

    app.register.controller('Themes', ['$scope', '$controller', 'DataService', '$state', Themes]);

    var ThemesDialogController = function ($s, $data, $modal, $c, $ds, $state) {
        $c("BaseController", { $scope: $s });

        $s.getPhoto = function (image, path, targetid) {
            $ds.Post('LoadImage', { ImgFile: image, Container:"Photos", Path: null, Size: null }).then(function (d) {
                //e.css({ opacity: 0 });
                var a = setTimeout(function () {
                    clearTimeout(a);
                    if (d) {
                        $("#img_" + targetid).attr("style", "margin-top:-18px;background-image: url(" + d + ");background-size: contain;width: 25px;height:25px;");
                        var b = setTimeout(function () {
                            clearTimeout(b);
                        }, 1000)
                    } else {
                        $("#img_" + targetid).attr("style", "margin-top:-18px;background-image: url('/CompanyLogo5.png');background-size: contain;width: 25px;height:25px;");
                    }
                });
            }).fail(function () {
                $("#img_" + targetid).attr("style", "margin-top:-18px;background-image: url('/CompanyLogo5.png');background-size: initial;width: 25px;height:25px;");
            });
        }

        $s.Company = [];

        $s.skin = angular.copy($data.skin);
        
        $s.Request("FetchSkinCompany", { ID_Skin: $s.skin.ID }).then(function (obj) {
            $s.Company = obj.data;

            var preData = [];
            var companyIds = ($s.skin.ID_Company == null || $s.skin.ID_Company == '' ? [] : $s.skin.ID_Company.split(","));
            for (var x = 0; x < companyIds.length; x++) {
                var comp = Enumerable.From($s.Company).Where(function (b) { return b.ID == parseInt(companyIds[x]) }).ToArray()[0];
                preData.push(comp);
            }

            setTimeout(function () {
                $("#auto_Company").tokenInput($s.Company, {
                    excludeCurrent: true,
                    theme: "facebook",
                    propertyToSearch: "Name",
                    resultsFormatter: function (item) { return "<li>" + "<img id='img_" + item.ID + "' style='" + $s.getPhoto(item.ImageFile, 'Photos', item.ID) + "' />" + "<div style='display: inline-block; padding-left: 10px;'><div class='full_name'>" + item.Name + "</div><div class='email'>" + item.Address + "</div></div></li>" },
                    prePopulate: preData
                });
            }, 500);
        })

        $s.Cancel = function () {
            $modal.dismiss();
        }

        $s.saveThemes = function () {
            if ($s.skin.Name == null || $s.skin.Name == "") {
                $s.Toast('Skin Name is Required.', 'Save Theme', 'warning');
            } else {
                $s.Request("SaveTheme", { data: $s.skin }).then(function (ob) {
                    if (ob.error != undefined) {
                        $s.Toast(ob.error, 'Save Theme', 'warning');
                    } else {
                        $s.Toast("Theme Created Successfully!", 'Save Theme', 'success');
                        $s.skin.ID = ob.ID;
                        $state.reload();
                    }
                });
            }
        }

        $s.OpenThemeHelp = function () {
            $s.Dialog({
                template: 'ThemesHelp.tmpl.html',
                controller: 'ThemesHelpController',
                size: 'lg',
            }).result.then(function (d) {
            })
        }
    }

    app.register.controller('ThemesDialogController', ['$scope', 'dData', '$uibModalInstance', '$controller', 'DataService', '$state', ThemesDialogController]);

    var ThemesHelpController = function ($s, $data, $modal, $c, $ds, $state) {
        $c("BaseController", { $scope: $s });

        $s.Cancel = function () {
            $modal.dismiss();
        }

    }

    app.register.controller('ThemesHelpController', ['$scope', 'dData', '$uibModalInstance', '$controller', 'DataService', '$state', ThemesHelpController]);
})