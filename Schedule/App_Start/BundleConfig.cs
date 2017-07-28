using System.Web;
using System.Web.Optimization;

namespace Schedule
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/confirmCss").Include(
                "~/Content/jquery-confirm.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryconf").Include(
                        "~/Scripts/jquery-confirm.min.js"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                "~/Content/dataTables/dataTables.bootstrap.min.css",
                "~/Content/dataTables/select.bootstrap.min.css",
                "~/Content/dataTables/buttons.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/dataTables").Include(
                "~/Scripts/dataTables/jquery.dataTables.min.js",
                "~/Scripts/dataTables/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables/dataTables.buttons.min.js",
                "~/Scripts/dataTables/buttons.bootstrap.min.js",
                "~/Scripts/dataTables/dataTables.select.min.js"));

            bundles.Add(new StyleBundle("~/Content/panelCss").Include(
                "~/Content/jquery-confirm.min.css",
                "~/Content/dataTables/dataTables.bootstrap.min.css",
                "~/Content/dataTables/select.bootstrap.min.css",
                "~/Content/dataTables/buttons.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/panelScripts").Include(
                "~/Scripts/jquery-confirm.min.js",
                "~/Scripts/dataTables/jquery.dataTables.min.js",
                "~/Scripts/dataTables/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables/dataTables.buttons.min.js",
                "~/Scripts/dataTables/buttons.bootstrap.min.js",
                "~/Scripts/dataTables/dataTables.select.min.js",
                "~/Scripts/jquery.redirect.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryUI").Include(
                "~/Content/UI/jquery-ui.structure.min.css",
                "~/Content/UI/jquery-ui.theme.min.css",
                "~/Content/UI/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryUI").Include(
                "~/Scripts/UI/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryWC").Include(
                "~/Scripts/WeekCalendar/jquery.weekcalendar.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryWC").Include(
                "~/Scripts/WeekCalendar/date.js",
                "~/Scripts/WeekCalendar/jquery.weekcalendar.js"));


            //WC Tests
            bundles.Add(new StyleBundle("~/Content/WC").Include(
                "~/Content/WCTests/smoothness/jquery-ui-1.8.11.custom.css",
                "~/Content/WCTests/jquery.weekcalendar.css"));

            bundles.Add(new ScriptBundle("~/Scripts/WC").Include(
                "~/Scripts/WCTests/jquery-1.4.4.min.js",
                "~/Scripts/WCTests/jquery-ui-1.8.11.custom.min.js",
                "~/Scripts/WCTests/date.js",
                "~/Scripts/WCTests/jquery.weekcalendar.js"));

        }
    }
}
