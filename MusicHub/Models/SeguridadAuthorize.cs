using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MusicHub.Models
{
    public class SeguridadAuthorize : ActionFilterAttribute
    {
        public string Area { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (WebSecurity.IsAuthenticated) filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Profile", user = WebSecurity.CurrentUserName}));
        }
    }
}