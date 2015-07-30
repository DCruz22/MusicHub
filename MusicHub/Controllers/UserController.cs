using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.Data.Reps.Reps;
using MusicHub.Data.StrategyAlgorithms;

namespace MusicHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private UserRepository _usrrep = new UserRepository();

        [Authorize]
        public ActionResult Index(string user)
        {
            return View();
        }

        [ActionName("Profile")]
        public async Task<ActionResult> User_Profile(string user)
        {
            User usr = await _usrrep.FindAsync(x => x.UserName == user);
            return View(usr);
        }

        [Authorize]
        public ActionResult Suggestions(string user)
        {
            return View();
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult Follow()
        {
            return View();
        }
    }
}