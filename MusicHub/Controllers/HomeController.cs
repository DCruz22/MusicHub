using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;
using MusicHub.Data.StrategyAlgorithms;

namespace MusicHub.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private ProjectsRepository _projrep = new ProjectsRepository();
        private UserRepository _usrrep = new UserRepository();

        [SeguridadAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Results(string search)
        {
            SearchProjectsAlgorithm searchProj = new SearchProjectsAlgorithm(search);
            ViewBag.Result = search;
            return View(await searchProj.SearchProjectsAsync());
        }

        [Authorize]
        public async Task<ActionResult> User_Results(string search)
        {
            SearchUserAlgorithm searchuser = new SearchUserAlgorithm(search);
            return View(await searchuser.SearchUserAsync());
        }
    }
}