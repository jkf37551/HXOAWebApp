using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HXWebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/ckform.js",
                      "~/Scripts/common.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                      "~/assets/js/jquery-1.8.1.min.js",
                      "~/assets/js/bui-min.js",
                      "~/assets/js/common/main-min.js",
                      "~/assets/js/config-min.js"));

            bundles.Add(new StyleBundle("~/Content/assets").Include(
                      "~/assets/css/dpl-min.css",
                      "~/assets/css/bui-min.css",
                      "~/assets/css/main-min.css"));
        }
    }
}