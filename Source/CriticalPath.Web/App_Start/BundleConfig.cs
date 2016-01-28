using System.Web;
using System.Web.Optimization;

namespace CriticalPath.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/ag-grid").Include(
                        "~/libs/ag-grid/ag-grid.css",
                        "~/libs/ag-grid/theme-fresh.css",
                        "~/libs/ag-grid/theme-dark.css",
                        "~/libs/ag-grid/theme-blue.css",
                        "~/libs/ag-grid/ag-dark-blue.css",
                        "~/libs/ag-grid/ag-light-blue.css",
                        "~/libs/ag-grid/ag-grid-addt.css"));

            bundles.Add(new ScriptBundle("~/js/ag-grid").Include(
                        "~/libs/ag-grid/ag-grid.js",
                        "~/libs/ag-grid/agCellRenderers.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/utilities.jq.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.min.js"));

            bundles.Add(new StyleBundle("~/Content/themes").Include(
                        "~/Content/themes/base/core.css",
                        //"~/Content/themes/base/resizable.css",
                        //"~/Content/themes/base/selectable.css",
                        //"~/Content/themes/base/accordion.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        //"~/Content/themes/base/button.css",
                        //"~/Content/themes/base/dialog.css",
                        //"~/Content/themes/base/slider.css",
                        //"~/Content/themes/base/tabs.css",
                        "~/Content/themes/base/datepicker.css",
                        //"~/Content/themes/base/progressbar.css",
                        "~/Content/themes/base/theme.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            /* Dark & Cool */
            bundles.Add(new StyleBundle("~/Content/Slate").Include(
                      "~/Content/Slate/bootstrap.min.css",
                      "~/Content/themes/dark-hive/jquery-ui.css",
                      "~/Content/themes/dark-hive/theme.css",
                      "~/Content/tools.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Slate-Ligth").Include(
                      "~/Content/Slate/bootstrap.min.css",
                      "~/Content/Slate/Light.css",
                      "~/Content/themes/dark-hive/jquery-ui.css",
                      "~/Content/themes/dark-hive/theme.css",
                      "~/Content/tools.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Superhero").Include(
                        "~/Content/Superhero/bootstrap.min.css",
                        "~/Content/NavbarColors-Red.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Cosmo").Include(
                        "~/Content/Cosmo/bootstrap.min.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/NavbarColors-Blue.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Flatly").Include(
                        "~/Content/Flatly/bootstrap.min.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/NavbarColors-Red.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Darkly").Include(
                        "~/Content/Darkly/bootstrap.min.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Default").Include(
                        "~/Content/Default/bootstrap.min.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Default-Darker").Include(
                        "~/Content/Default/bootstrap.min.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/Default/darker.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Default-Blue").Include(
                        "~/Content/Default/bootstrap.min.css",
                        "~/Content/Default/darker.css",
                        "~/Content/NavbarColors-Blue.css",
                        //"~/Content/NavbarColors-Grey.css",
                        //"~/Content/NavbarColors-Red.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Default-Red").Include(
                        "~/Content/Default/bootstrap.min.css",
                        "~/Content/Default/darker.css",
                        //"~/Content/NavbarColors-Blue.css",
                        //"~/Content/NavbarColors-Grey.css",
                        "~/Content/NavbarColors-Red.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/tools.css",
                        "~/Content/Site.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //            "~/Content/Default/bootstrap.min.css",
            //            "~/Content/Site.css"));
        }
    }
}
