using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;

namespace MusicHub
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Logged",
                url: "{user}",
                defaults: new { controller = "User", action = "Profile" }
            );

            routes.MapRoute(
                name: "Suggestions",
                url: "Suggestions/{user}",
                defaults: new { controller = "User", action = "Suggestions" }
            );

            /*routes.MapRoute(
                name: "Picture",
                url: "Settings/ChangePicture/{user}",
                defaults: new { controller = "User", action = "ChangeProfilePicture" }
            );*/

            routes.MapRoute(
                name: "Settings",
                url: "Settings/BasicInfo/{user}",
            defaults: new { controller = "User", action = "Settings" }
            );

            routes.MapRoute(
                name: "Preferences",
                url: "Settings/Preferences/{user}",
                defaults: new { controller = "User", action = "Preferences" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
