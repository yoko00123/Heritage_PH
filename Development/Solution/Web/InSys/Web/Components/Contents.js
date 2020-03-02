define(['app'], function (app) {

    var Contents = (function ($s, $c) {
        $c('BaseController', { $scope: $s });

        $s.ItemList = null; 
        $s.rptFilter = null;
        $s.CheckItems = [];
        $s.CurrentFolder = 'Reports'

        $s.InitContents = (function () {
            $s.ViewType = 1;
            $s.LoadItems($s.CurrentFolder);
            $s.SetHeight();

            setTimeout(function () {
                $(".side-menu").addClass("toggle-sidemenu");
                $(".untoggled-actions").addClass("toggle-actions");
                $(".bookmark-panel").removeClass("toggle-bookmark");
            }, 500)
        })

        $s.LoadItems = (function (Name) {
            $s.CurrentFolder = Name;
            $s.ItemList = null;
            $s.rptFilter = null;
            $s.CheckItems = [];
            $s.Request('ListContents', { Container: Name }).then(function (d) { 
                $s.ItemList = d;
            })
        })

        $s.InitView = (function () {
            $s.SetHeight();
        })
         
        $s.AddReport = (function () {
            $s.UploadFileSlim($s.CurrentFolder, { UseOriginalName: true }).then(function (d) {
                $s.LoadItems($s.CurrentFolder);
            })
        })

        $s.$watch('ItemList', function () {
            $s.ItemCount();
        })

        $s.ItemCount = (function () {
            if ($s.rptFilter == '' || $s.rptFilter == null || $s.rptFilter == undefined) {
                $s.rcnt = $s.ItemList.length;
            } else {
                $s.rcnt = Enumerable.From($s.ItemList).Where(function (x) { return x.Name.indexOf($s.rptFilter) != -1 }).ToArray().length;
            }
        })

        $s.ToList = (function () {
            $s.ViewType = 2;
            $s.ItemCount();
        })

        $s.ToThumb = (function () {
            $s.ViewType = 1;
            $s.ItemCount();
        })

        $s.Selected = false;
        $s.SelectAll = function () {
            $s.Selected = !$s.Selected;
            $s.CheckItems = [];

            if ($s.Selected) {
                for (var gg = 0; gg < $s.ItemList.length; gg++) {
                    if ($s.ReportFilter($s.ItemList[gg])) {
                        $s.CheckItems.push($s.ItemList[gg].Name);
                    }
                }
            }
        }

        $s.SelectContent = (function (e) {
            if ($s.CheckItems.indexOf(e.Name) !== -1)
                vcl.Array.Remove($s.CheckItems, function (x) { return x === e.Name });
            else
                $s.CheckItems.push(e.Name);
        })

        $s.FindClear = function () {
            $("#find").val("");

            var e = jQuery.Event("keydown");
            e.which = 8;
            $("#find").trigger(e);
            console.log(e);

            if ($("#find").val("")) {
                $s.rptFilter = null;
            }
        }

        $s.ReportFilter = (function (str) {
            $s.ItemCount();
            if ($s.rptFilter == null || $s.rptFilter === '') return true;
            return str.Name.indexOf($s.rptFilter) != -1;
        })

        $s.DeleteItem = (function () {
            if ($s.CheckItems.length > 0) {
                $s.Confirm('Delete Selected Items').then(function () {
                    $s.Request('DeleteContents', { FileNames: $s.CheckItems, Container: $s.CurrentFolder }).then(function () {
                        $s.CheckItems = [];
                        $s.LoadItems($s.CurrentFolder);
                    })
                })
            }
        });

        $s.DownloadItem = (function () {
            if ($s.CheckItems.length > 0) {
                $s.CheckItems.forEach(function (x) {
                    $s.DownloadSlim(x, $s.CurrentFolder).then(function () {
                        vcl.Array.Remove($s.CheckItems, function (xy) { return xy === x });
                    });
                })
            }
        })

        $s.SetHeight = (function () {
            setTimeout(function () {
                var offsetTop = $(".main-content")[0].offsetTop;
                var clientHeight = $(".main-content")[0].clientHeight;
                $(".itm-content").css("maxHeight", (clientHeight - 97) - offsetTop);
                $(".itm-content").css("minHeight", (clientHeight - 97) - offsetTop);
            }, 1000)
        })
    })

    app.register.controller('Contents', ['$scope', '$controller', Contents]);

})