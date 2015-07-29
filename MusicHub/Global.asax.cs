using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using MusicHub.App_Start;

namespace MusicHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("AzureCon", "Users", "UserId", "UserName", true);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
