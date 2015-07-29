using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private ProjectsRepository _projrep = new ProjectsRepository();
        private UserRepository _usrrep = new UserRepository();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Results(string search, string type)
        {
            var projects = await _projrep.FilterAsync(x => x.ProjectName.Contains(search));

            ViewBag.Result = search;
            return View();
        }
    }
}