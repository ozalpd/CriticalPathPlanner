using System.Web;
using System.Web.Optimization;

namespace CriticalPath.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/themes").Include(
                        "~/Content/themes/base/core.css",
                        //"~/Content/themes/base/resizable.css",
                        //"~/Content/themes/base/selectable.css",
                        //"~/Content/themes/base/accordion.css",
                        //"~/Content/themes/base/autocomplete.css",
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

            /* */
            bundles.Add(new StyleBundle("~/Content/Slate").Include( //Dark & Cool
                      "~/Content/Slate/bootstrap.min.css",
                      "~/Content/Slate/Light.css",
                        "~/Content/themes/dark-hive/jquery-ui.css",
                        "~/Content/themes/dark-hive/theme.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Superhero").Include(
                    "~/Content/Superhero/bootstrap.min.css",
                      "~/Content/NavbarColors-Red.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Cosmo").Include(
                    "~/Content/Cosmo/bootstrap.min.css",
                      "~/Content/NavbarColors-Blue.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Flatly").Include(
                      "~/Content/Flatly/bootstrap.min.css",
                      "~/Content/NavbarColors-Red.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Darkly").Include(
                      "~/Content/Darkly/bootstrap.min.css",
                      "~/Content/Site.css"));
                     
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Default/bootstrap.min.css",
                      "~/Content/Default/darker.css",
                      "~/Content/NavbarColors-Blue.css",
                      //"~/Content/NavbarColors-Grey.css",
                      //"~/Content/NavbarColors-Red.css",
                      "~/Content/Site.css"));
        }
    }
}
