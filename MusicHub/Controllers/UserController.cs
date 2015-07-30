using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;
using MusicHub.Data.StrategyAlgorithms;

namespace MusicHub.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private UserRepository _usrrep = new UserRepository();
        private ProjectsRepository _projrep = new ProjectsRepository();
        private User_MusicalStylesRepository _musicrep = new User_MusicalStylesRepository();
        private User_InstrumentsRepository _instrep = new User_InstrumentsRepository();

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

        public async Task<ActionResult> MyProjects(string user)
        {
            List<Project> projects = (await _projrep.FilterAsync(x => x.User.UserName == user))
                                     .OrderByDescending(x => x.CreationDate)
                                     .ToList();
            return View(projects);
        }


        [Authorize]
        public async Task<ActionResult> Settings(string username)
        {
            User user = (await _usrrep.FilterAsync(x => x.UserName == username)).ToList().FirstOrDefault();

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Profile", "User", new { user = WebSecurity.CurrentUserName });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Settings(User user)
        {
            if (ModelState.IsValid)
            {
                await _usrrep.UpdateAsync(user);
            }

            return View(user);
        }

        [Authorize]
        public async Task<ActionResult> Preferences(string user)
        {
            PreferencesViewModel preferences = new PreferencesViewModel();

            IEnumerable<MusicalStyle> styles = (await _musicrep.FilterAsync(x => x.User.UserName == user))
                                        .Select(x => x.MusicalStyle)
                                        .ToList();

            IEnumerable<Instrument> instruments = (await _instrep.FilterAsync(x => x.User.UserName == user))
                                            .Select(x => x.Instrument)
                                            .ToList();
            if(styles != null)
            {
                foreach(MusicalStyle style in styles)
                {
                    preferences.Styles.Add(style);
                }
            }

            if(instruments != null)
            {
                foreach(Instrument instrument in instruments)
                {
                    preferences.Instruments.Add(instrument);
                }
            }

            return View(preferences);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Preferences(PreferencesViewModel preferences)
        {
            List<User_Instrument> user_instruments = new List<User_Instrument>();
            List<User_MusicalStyle> user_styles = new List<User_MusicalStyle>();

            User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));

            if(ModelState.IsValid)
            {
                await _instrep.DeleteAsync(x => x.User.UserName == WebSecurity.CurrentUserName);
                await _musicrep.DeleteAsync(x => x.User.UserName == WebSecurity.CurrentUserName);

                foreach(MusicalStyle style in preferences.Styles)
                {
                    user_styles.Add(new User_MusicalStyle()
                    {
                        MusicalStyle = style,
                        User = user
                    });
                }

                foreach (Instrument instrument in preferences.Instruments)
                {
                    user_instruments.Add(new User_Instrument()
                    {
                        Instrument = instrument,
                        User = user
                    });
                }

                await _instrep.CreateAsync(user_instruments);
                await _musicrep.CreateAsync(user_styles);

                return View();
            }
            return View(preferences);
        }

        [Authorize]
        public async Task<ActionResult> ChangeProfilePicture(string user)
        {
            User usr = (await _usrrep.FindAsync(x => x.UserName == user));

            return View(usr);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangeProfilePicture()
        {
            return View();
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