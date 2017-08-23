using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace MyFreeShare.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                "~/Content/*.css",
                "~/Content/css/*.css"
                ));
            bundles.Add(new StyleBundle("~/Adminlte/Style").Include(
                "~/admin-lte/css/*.css",
                "~/admin-lte/css/skins/*.css"
                ));
            bundles.Add(new ScriptBundle("~/Script").Include(
                "~/Scripts/*.js"));
            bundles.Add(new ScriptBundle("~/Adminlte/Script").Include(
                "~/admin-lte/js/*.js"
                ));
        }
    }
}