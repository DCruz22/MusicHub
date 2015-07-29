using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Profile")]
        public ActionResult User_Profile()
        {
            return View();
        }

        public ActionResult Suggestions()
        {
            return View();
        }
    }
}