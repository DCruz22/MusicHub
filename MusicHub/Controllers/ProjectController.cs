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
using MusicHub.Helpers;

namespace MusicHub.Controllers
{
    public class ProjectController : Base
    {
        // GET: Project
        private ProjectsRepository _projrep = new ProjectsRepository();
        private Project_CommentsRepository _procomrep = new Project_CommentsRepository();
        private Project_ContentsRepository _procontrep = new Project_ContentsRepository();
        private MusicalStylesRepository _stylerep = new MusicalStylesRepository();

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MusicalStyleId = new SelectList(_stylerep.All(), "MusicalStyleId", "MusicalStyleName");
            return View(new Project());
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(Project project)
        {
            var proj = await _projrep.FindAsync(a => a.ProjectName == project.ProjectName);

            if (proj != null)
            {
                ViewBag.MusicalStyleId = new SelectList(await _stylerep.AllAsync(), "MusicalStyleId", "MusicalStyleName");
                ModelState.AddModelError("", @"Project name is already in use.");
                return View(project);
            }

            var user = (await _usrrep.FilterAsync(x => x.UserName == WebSecurity.CurrentUserName)).ToList().FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Login", "Account");
            }

            project.User = user;
            project.CreationDate = DateTime.Now;

            if(ModelState.IsValid)
            {
                _projrep.Create(project);
                return RedirectToRoute(new { controller = "Project", action = "Detail", projectname = project.ProjectName});
            }

            ViewBag.MusicalStyleId = new SelectList(await _stylerep.AllAsync(), "MusicalStyleId", "MusicalStyleName");
            ModelState.AddModelError("", @"Unable to create project/n See previous errors.");
            return View(project);
        }

        public async Task<ActionResult> Details(string projectname)
        {
            Project proj = await _projrep.FindAsync(x => x.ProjectName == projectname);

            if (proj == null)
            {
                return HttpNotFound();
            }

            return View(proj);
        }

        public async Task<ActionResult> Comments(string projectname)
        {
            List<Project_Comment> comments = (await _procomrep.FilterAsync(x => x.Project.ProjectName == projectname))
                                                .OrderByDescending(x => x.Date)
                                                .ToList();
            ProjectCommentViewModel project_comment = new ProjectCommentViewModel();

            project_comment.PastComments = comments;

            ViewBag.ProjectName = projectname;
            ViewBag.UserName = comments[0].Project.User.UserName;
            return View(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Comment(Project_Comment comment, string projectname)
        {
            User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
            comment.User = user;
            comment.Date = DateTime.Now;
            comment.Project = (await _projrep.FindAsync(x => x.ProjectName == projectname));

            if(ModelState.IsValid)
            {
                await _procomrep.CreateAsync(comment);

                return View();
            }
            return View(comment);
        }

        public async Task<ActionResult> Contents(string projectname) 
        {
            List<Project_Content> contents = (await _procontrep.FilterAsync(x => x.Project.ProjectName == projectname)).ToList();
            ProjectContentViewModel project_content = new ProjectContentViewModel();

            project_content.Past_Contents = contents;

            var user = contents[0].Project.User;

            ViewBag.IsOwner = IsOwner(user.UserId);
            ViewBag.ProjectName = projectname;
            ViewBag.UserName = user.UserName;
            return View(project_content);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Contents(Project_Content content, string photoTempUrl, string projectname)
        {
            if(!String.IsNullOrEmpty(photoTempUrl))
            {
                string filePath = FilesHelper.movePostFile(photoTempUrl, FilesHelper.Photo_types.CONTENT_PICTURE);
                User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
                Project proj = (await _projrep.FindAsync(x => x.ProjectName == projectname));
                content = new Project_Content(){
                    User = user,
                    Content = filePath,
                    Project = proj,
                    Date = DateTime.Now
                };
                await _procontrep.CreateAsync(content);
                return RedirectToAction("Contents", new { projectname = projectname });
            }
            ViewBag.Error = "There isn't an image to select.";
            return View(content);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Follow()
        {
            return View();
        }

        protected override MusicHub.Helpers.FilesHelper.Photo_types GetImageType()
        {
            return MusicHub.Helpers.FilesHelper.Photo_types.CONTENT_PICTURE;
        }
    }
}