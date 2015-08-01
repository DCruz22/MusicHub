using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace MusicHub.Helpers
{
    public static class HelperExtensions
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper,
                                                  string linkText,
                                                  string actionName,
                                                  string controllerName,
                                                  object routeValues,
                                                  AjaxOptions ajaxOptions,
                                                  object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString SubString(this HtmlHelper helper, string text, int characters)
        {

            return text != null ? (text.Length >= characters ? MvcHtmlString.Create(text.Substring(0, characters - 1).TrimEnd(new char[] { '.' }) + "...") : MvcHtmlString.Create(text)) : MvcHtmlString.Create("");
        }
    }
}