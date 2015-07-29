using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;
using System.Threading.Tasks;

namespace MusicHub.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private ProjectsRepository _projrep = new ProjectsRepository();
        private Project_CommentsRepository _procomrep = new Project_CommentsRepository();
        private Project_ContentsRepository _procontrep = new Project_ContentsRepository();
        private UserRepository _usrrep = new UserRepository();

        [Authorize]
        public ActionResult Create()
        {
            return View(new Project());
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(Project project)
        {
            var proj = await _projrep.FindAsync(a => a.ProjectName == project.ProjectName);

            if (proj != null)
            {
                ModelState.AddModelError("", @"Project name is already in use.");
                return View(project);
            }

            if(ModelState.IsValid)
            {
                _projrep.Create(project);
                return RedirectToRoute(new { controller = "Project", action = "Detail", projectname = project.ProjectName});
            }

            ModelState.AddModelError("", @"Unable to create project/n See previous errors.");
            return View(project);
        }

        public async Task<ActionResult> Details(string projectname)
        {
            Project proj = await _projrep.FindAsync(x => x.ProjectName == projectname);
            return View(proj);
        }

        public async Task<ActionResult> Comment(string projectname)
        {
            List<Project_Comment> comments = (await _procomrep.FilterAsync(x => x.Project.ProjectName == projectname))
                                                .OrderByDescending(x => x.Date)
                                                .ToList();
                                             
            return View(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Comment(Project_Comment coment)
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