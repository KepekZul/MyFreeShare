﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using MyFreeShare.App_Start;

namespace MyFreeShare
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MyFreeShare.App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
