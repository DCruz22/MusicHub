using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;
using MusicHub.Data.Reps;
using System.Threading.Tasks;
using MusicHub.Helpers;

namespace MusicHub.Controllers
{
    public class ProjectController : Base
    {
        // GET: Project
        private Project_CommentsRepository _procomrep = new Project_CommentsRepository();
        private Project_ContentsRepository _procontrep = new Project_ContentsRepository();
        private MusicalStylesRepository _stylerep = new MusicalStylesRepository();
        private FeedsRepository _feedrep = new FeedsRepository();

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MusicalStyleId = new SelectList(_stylerep.All(), "MusicalStyleId", "MusicalStyleName");
            return View(new Project());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            project.UserId = user.UserId;
            project.CreationDate = DateTime.Now;
            
            try
            {
                if (ModelState.IsValid)
                {
                    Feed feed = new Feed()
                    {
                        UserId = user.UserId,
                        Action = user.UserName + " Has created a new project",
                        Url = project.ProjectName
                    };

                    await _projrep.CreateAsync(project);
                    await _feedrep.CreateAsync(feed);
                    return RedirectToRoute(new { controller = "Project", action = "Details", projectname = project.ProjectName });
                }
            }
            catch(Exception ex)
            {
                
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
            Project proj = (await _projrep.FindAsync(x => x.ProjectName == projectname));

            if (proj == null)
            {
                return HttpNotFound();
            }

            List<Project_Comment> comments = (await _procomrep.FilterAsync(x => x.Project.ProjectName == projectname))
                                                .OrderBy(x => x.Date)
                                                .ToList();
            ProjectCommentViewModel project_comment = new ProjectCommentViewModel();

            project_comment.PastComments = comments;

            ViewBag.ProjectName = projectname;
            ViewBag.UserName = proj.User.UserName;
            return View(project_comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Comments(string comment, string projectname)
        {
            User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
            Project proj = (await _projrep.FindAsync(x => x.ProjectName == projectname));
            if (proj == null)
            {
                return HttpNotFound();
            }

            if (String.IsNullOrEmpty(comment))
            {
                ViewBag.Error = "You must write a comment.";
                return RedirectToAction("Comments", new { projectname = projectname });
            }

            Project_Comment proj_comment = new Project_Comment(){
                UserId = user.UserId,
                Comment = comment,
                Date = DateTime.Now,
                ProjectId  = proj.ProjectId
            };

            try
            {
                if (ModelState.IsValid)
                {
                    await _procomrep.CreateAsync(proj_comment);

                    return RedirectToAction("Comments", new { projectname = projectname });
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = "There was an error while adding the content. Try again later.";
                return RedirectToAction("Comments", new { projectname = projectname });
            }
            ViewBag.Error = "You must write a comment.";
            return RedirectToAction("Comments", new { projectname = projectname });
        }

        public async Task<ActionResult> Contents(string projectname) 
        {
            Project proj = (await _projrep.FindAsync(x => x.ProjectName == projectname));
            List<Project_Content> contents = (await _procontrep.FilterAsync(x => x.Project.ProjectName == projectname)).ToList();
            ProjectContentViewModel project_content = new ProjectContentViewModel();

            project_content.Past_Contents = contents;

            var user = proj.User;

            ViewBag.IsOwner = IsOwner(user.UserId);
            ViewBag.ProjectName = projectname;
            ViewBag.UserName = user.UserName;
            return View(project_content);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Contents(string photoTempUrl, string projectname, string Name)
        {
            try
            {
                if (!String.IsNullOrEmpty(photoTempUrl) && !String.IsNullOrEmpty(Name))
                {
                    string filePath = FilesHelper.movePostFile(photoTempUrl, FilesHelper.Photo_types.CONTENT_PICTURE);
                    User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
                    Project proj = (await _projrep.FindAsync(x => x.ProjectName == projectname));
                    Project_Content content = new Project_Content()
                    {
                        UserId = user.UserId,
                        Content = filePath,
                        ProjectId = proj.ProjectId,
                        Date = DateTime.Now,
                        Name = Name
                    };
                    await _procontrep.CreateAsync(content);
                    return RedirectToAction("Contents", new { projectname = projectname });
                }
            }
            catch(Exception ex){
                ViewBag.Error = "There was an error while adding the content. Try again later.";
                return RedirectToAction("Contents", new { projectname = projectname });
            }
            ViewBag.Error = "There isn't an image to select.";
            return RedirectToAction("Contents", new { projectname = projectname });
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