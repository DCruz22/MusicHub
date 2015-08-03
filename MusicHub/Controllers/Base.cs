using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MusicHub.Helpers;

namespace MusicHub.Controllers
{
    public abstract class Base : Controller
    {
        protected bool IsOwner(int UserId) {
            return WebSecurity.IsAuthenticated && WebSecurity.CurrentUserId == UserId;
        }

        protected MusicHub.Helpers.FilesHelper.Photo_types GetImageType()
        {
            return MusicHub.Helpers.FilesHelper.Photo_types.PROFILE_PICTURE;
        }

        [HttpPost]
        public ActionResult SaveImage(HttpPostedFileBase file)
        {
            string fileName = "";

            if (file != null)
            {

                fileName = FilesHelper.SaveFile(file, GetImageType());
            }

            return Content(fileName);
        }
    }
}