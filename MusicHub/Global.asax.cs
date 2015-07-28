using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("AzureCon", "User", "UserId", "UserName", true);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
