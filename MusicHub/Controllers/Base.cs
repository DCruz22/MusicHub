using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MusicHub.Helpers;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Controllers
{
    public abstract class Base : Controller
    {
        protected GendersRepository _genderep = new GendersRepository();
        protected CountriesRepository _countryrep = new CountriesRepository();
        protected UserRepository _usrrep = new UserRepository();
        protected ProjectsRepository _projrep = new ProjectsRepository();

        protected bool IsOwner(int UserId) {
            return WebSecurity.IsAuthenticated && WebSecurity.CurrentUserId == UserId;
        }

        protected virtual MusicHub.Helpers.FilesHelper.Photo_types GetImageType()
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