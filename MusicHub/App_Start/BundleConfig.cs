using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MusicHub.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-migrate-{version}.js",
                       "~/Scripts/jquery.pageslide.*",
                       "~/Scripts/jquery.snippet.*",
                       "~/Scripts/pwstrength.*",
                       "~/Scripts/jquery.icheck.*",
                       "~/Scripts/jquery.age.js",
                       "~/Scripts/dropzone.js",
                       "~/Scripts/jquery.requestAnimationFrame.js",
                       "~/Scripts/jquery.mousewheel.js",
                       "~/Scripts/ilightbox.packed.js",
                       "~/Scripts/jquery.pnotify.*",
                       "~/Scripts/jquery.autosize.*",
                       "~/Scripts/jquery.backstretch.*",
                       "~/Scripts/tabIndent.*"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                "~/Scripts/jquery.signalR-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryAjax").Include(
                        "~/Scripts/MicrosoftAjax.*",
                        "~/Scripts/MicrosoftMVCAjax.*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/tag-it.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                        "~/Scripts/materialize.*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css-style").Include(
                      //"~/Content/style/bootstrap.*",
                      "~/Content/style/materialize.*"
                      //"~/Content/style/Layout.css",
                      //"~/Content/style/style.css",
                      //"~/Content/style/jquery.snippet.*",
                      //"~/Content/style/jquery.pageslide.*",
                      //"~/Content/style/font-awesome.*",
                      //"~/Content/style/tagit.ui-zendesk.css",
                      //"~/Content/style/jquery.tagit.css",
                      //"~/Content/style/dropzone.css",
                      //"~/Content/style/ilightbox.css",
                      //"~/Content/style/jquery-ui.*",
                      //"~/Content/style/jquery.pnotify.default.css",
                      /*"~/Content/style/jquery.pnotify.default.icons.css"*/));

            bundles.Add(new StyleBundle("~/Content/css-account").Include(
                      "~/Content/style/style-account.css",
                      "~/Content/style/forms.css",
                      "~/Content/style/animate.*",
                      "~/Content/style/font-awesome.*"
                      ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}