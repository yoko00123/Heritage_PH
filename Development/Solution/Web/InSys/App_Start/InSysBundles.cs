using System;
using System.Web.Optimization;
using z.Controller;
using z.Data;

namespace InSys.App_Start
{
    public static class InSysBundles
    {

        public static void Register(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/core-scripts")
                .Include("~/Scripts/References/compatibility.js")
                .Include("~/Scripts/References/jquery-3.2.1.min.js")
                .Include("~/Scripts/References/angular.min.js")
                .Include("~/Scripts/References/angular-animate.min.js")
                .Include("~/Scripts/References/angular-aria.min.js")
                .Include("~/Scripts/References/angular-cookies.min.js")
                .Include("~/Scripts/References/angular-loader.min.js")
                .Include("~/Scripts/References/angular-sanitize.min.js")
                .Include("~/Scripts/References/angular-toastr.tpls.js")
                .Include("~/Scripts/References/angular-ui-router.min.js")
                .Include("~/Scripts/References/ct-ui-router-extras.min.js")
                .Include("~/Scripts/References/tether.min.js")
                .Include("~/Scripts/References/bootstrap.min.js")
                .Include("~/Scripts/References/bootstrap-toggle.min.js")
                .Include("~/Scripts/References/bootstrap.min.js")
                .Include("~/Scripts/References/ui-bootstrap-tpls-2.5.0.min.js")
                .Include("~/Scripts/References/loading-bar.min.js")
                .Include("~/Scripts/References/linq.min.js")
                .Include("~/Scripts/References/lz-string.js")
                .Include("~/Scripts/References/moment.min.js")
                .Include("~/Scripts/References/vcl.js")
                .Include("~/Scripts/References/handsontable.full.min.js")
                .Include("~/Scripts/References/HandsonTableExtensions.js")
                .Include("~/Scripts/System/rosDirective.js")
                .Include("~/Scripts/References/paging.min.js")
                //.Include("~/Scripts/References/chart.min.js")
                //.Include("~/Scripts/References/angular-chart.min.js")
                .Include("~/Scripts/Pack/dhtmlx/dhtmlx.js")
                .Include("~/Scripts/References/angular-drag-and-drop-lists.min.js")
                .Include("~/Scripts/References/spectrum.js")
                .Include("~/Scripts/References/timeline.js")
                .Include("~/Scripts/References/jquery.tokeninput.js")
                .Include("~/Scripts/References/contextMenu.js")
                .Include("~/Scripts/References/angucomplete.js")
                .Include("~/Scripts/References/bootstrap-clockpicker.min.js")
                .Include("~/Scripts/References/vue.min.js")
                .Include("~/Scripts/References/vue-grid-layout.min.js")
                .Include("~/Scripts/References/bootstrap-datetimepicker.js")
                //.Include("~/Scripts/References/vue-chartjs.full.min.js")
                .Include("~/Scripts/References/jquery.canvasjs.min.js")
                .Include("~/Scripts/References/input-module.js")
                .Include("~/Scripts/References/fullcalendar.3.9.0.min.js")
                .Include("~/Scripts/References/nickcrypt.js")
                .Include("~/Scripts/References/base64js.min.js")
                .Include("~/Scripts/References/textAngular.min.js")
                .Include("~/Scripts/References/textAngular-rangy.min.js")
                .Include("~/Scripts/References/textAngular-sanitize.min.js")
            );

            bundles.Add(new StyleBundle("~/bundles/core-styles")
                .Include("~/Styles/References/bootstrap.min.css")
                .Include("~/Styles/References/bootstrap-toggle.min.css")
                .Include("~/Styles/References/font-awesome.min.css")
                .Include("~/Styles/References/angular-toastr.css")
                .Include("~/Styles/References/loading-bar.min.css")
                .Include("~/Styles/References/tether.min.css")
                .Include("~/Styles/References/handsontable.full.min.css")
                //.Include("~/Styles/References/angular-chart.css")
                .Include("~/Scripts/Pack/dhtmlx/dhtmlx.css")
                .Include("~/Styles/References/spectrum.css")
                .Include("~/Styles/References/timeline.css")
                .Include("~/Styles/References/jquery.tokeninput.css")
                .Include("~/Styles/References/angucomplete.css")
                .Include("~/Styles/References/bootstrap-clockpicker.min.css")
                .Include("~/Styles/References/bootstrap-datetimepicker.css")
                .Include("~/Styles/References/fullcalendar.3.9.0.min.css")
            );

            try
            {
                BundleTable.EnableOptimizations = Config.Get("OptimizedBundles").ToBool();
            }
            catch (Exception)
            {
                BundleTable.EnableOptimizations = true;
            }
        }

    }
}