using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
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

            var user = (await _usrrep.FilterAsync(x => x.UserName == WebSecurity.CurrentUserName)).ToList().FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Login", "Account");
            }

            project.User = user;

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

        public async Task<ActionResult> Comments(string projectname)
        {
            List<Project_Comment> comments = (await _procomrep.FilterAsync(x => x.Project.ProjectName == projectname))
                                                .OrderByDescending(x => x.Date)
                                                .ToList();
                                             
            return View(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Comment(Project_Comment comment)
        {
            User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
            comment.User = user;

            if(ModelState.IsValid)
            {
                await _procomrep.CreateAsync(comment);

                return View();
            }
            return View(comment);
        }

        public async Task<ActionResult> Contents(string projectname) 
        {
            IEnumerable<Project_Content> contents = (await _procontrep.FilterAsync(x => x.Project.ProjectName == projectname)).ToList();
            return View(contents);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Content(Project_Content content, HttpPostedFile file)
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