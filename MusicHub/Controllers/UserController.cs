using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MusicHub.Models;
using MusicHub.Helpers;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;
using MusicHub.Data.StrategyAlgorithms;

namespace MusicHub.Controllers
{
    public class UserController : Base
    {
        // GET: User
        private User_MusicalStylesRepository _musicrep = new User_MusicalStylesRepository();
        private User_InstrumentsRepository _instrep = new User_InstrumentsRepository();
        private FriendshipsRepository _friendrep = new FriendshipsRepository();

        [Authorize]
        public ActionResult Index(string user)
        {
            return View();
        }

        [Authorize]
        [ActionName("Profile")]
        public async Task<ActionResult> User_Profile(string user)
        {
            User usr = await _usrrep.FindAsync(x => x.UserName == user);
            ViewBag.IsOwner = IsOwner(usr.UserId);

            if (!IsOwner(usr.UserId))
            {
                Friendship friendship = (await _friendrep.FilterAsync(x => x.UserId_follower == WebSecurity.CurrentUserId && x.UserId_followed == usr.UserId)).FirstOrDefault();

                if (friendship != null)
                {
                    ViewBag.IsFollowing = true;
                }
            }

            if(usr != null)
            {
                return View(usr);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> MyProjects(string user)
        {
            List<Project> projects = (await _projrep.FilterAsync(x => x.User.UserName == user))
                                     .OrderByDescending(x => x.CreationDate)
                                     .ToList();
            return View(projects);
        }

        public async Task<ActionResult> MyPreferences(string user)
        {
            PreferencesViewModel preferences = new PreferencesViewModel();

            preferences.Instruments = (await _instrep.FilterAsync(x => x.User.UserName == user))
                                      .Select(x => x.Instrument)
                                      .ToList();

            preferences.MusicalStyles = (await _musicrep.FilterAsync(x => x.User.UserName == user))
                                 .Select(x => x.MusicalStyle)
                                 .ToList();

            return View(preferences);
        }

        public async Task<ActionResult> Badges()
        {

            return View();
        }

        [Authorize]
        public async Task<ActionResult> Settings(string user)
        {
            if (!WebSecurity.CurrentUserName.Equals(user))
            {
                return RedirectToAction("Profile", "User", new { user = WebSecurity.CurrentUserName });
            }
            User usr = (await _usrrep.FilterAsync(x => x.UserName == user)).ToList().FirstOrDefault();

            if (usr != null)
            {
                ViewBag.CountryId = new SelectList(_countryrep.All(), "CountryId", "CountryName");
                return View(usr);
            }
            return RedirectToAction("Profile", "User", new { user = WebSecurity.CurrentUserName });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Settings(User usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.CountryId = new SelectList(_countryrep.All().Where(x => x.CountryId == usr.CountryId), "CountryId", "CountryName");
                    await _usrrep.UpdateAsync(usr);
                    return RedirectToAction("Profile", new { user = WebSecurity.CurrentUserName });
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "There was an error while updating the data.\n Please try again later.");
                return View(usr);
            }

            ViewBag.CountryId = new SelectList(_countryrep.All(), "CountryId", "CountryName");
            return View(usr);
        }

        [Authorize]
        public ActionResult PasswordReset(string user)
        {
            if (!WebSecurity.IsAuthenticated || string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PasswordReset(PasswordReset PasswordReset, string Token)
        {
            if (ModelState.IsValid)
            {
                int userId = WebSecurity.GetUserIdFromPasswordResetToken(Token);
                User user = await _usrrep.FindAsync(a => a.UserId == userId);

                WebSecurity.ResetPassword(Token, PasswordReset.ConfirmPassword);
                WebSecurity.Login(user.UserName, PasswordReset.ConfirmPassword, persistCookie: false);

                return RedirectToAction("Profile", "User", new { user = WebSecurity.CurrentUserName });
            }

            return View();
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
                    preferences.MusicalStyles.Add(style);
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

            try
            {
                if (ModelState.IsValid)
                {
                    await _instrep.DeleteAsync(x => x.User.UserName == WebSecurity.CurrentUserName);
                    await _musicrep.DeleteAsync(x => x.User.UserName == WebSecurity.CurrentUserName);

                    foreach (MusicalStyle style in preferences.MusicalStyles)
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
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "There was an error while updating the data.\n Please try again later.");
                return View(preferences);
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
        public async Task<ActionResult> _ChangeProfilePicture(string photoTempUrl)
        {
            if (!string.IsNullOrEmpty(photoTempUrl))
            {
                string filePath = FilesHelper.movePostFile(photoTempUrl, FilesHelper.Photo_types.PROFILE_PICTURE);
                User user = (await _usrrep.FindAsync(x => x.UserName == WebSecurity.CurrentUserName));
                user.PhotoUrl = filePath;
                await _usrrep.UpdateAsync(user);
                return RedirectToAction("Profile", new { user = WebSecurity.CurrentUserName });
            }
            ViewBag.Error = "There isn't an image to select.";
            return View("ChangeProfilePicture");
        }

        [Authorize]
        public ActionResult Suggestions(string user)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Follow(int userId)
        {
            Friendship friendshipFromDb = _friendrep.Find(a => a.UserId_follower == WebSecurity.CurrentUserId &&
                                                        a.UserId_followed == userId);

            //Follow
            if (friendshipFromDb == null)
            {
                Friendship friendship = new Friendship()
                {
                    UserId_follower = WebSecurity.CurrentUserId,
                    UserId_followed = userId,
                    Date = (DateTime.Now).ToUniversalTime().AddMinutes(double.Parse((string)Session["UTCOffset"] ?? "-300.00"))
                };

                _friendrep.Create(friendship);

                return Json(new
                {
                    result = "<span id=\"btnfriendship\">Unfollow</span>",
                    userFollowedId = userId,
                    userFollowerId = WebSecurity.CurrentUserId

                });
            }
            else
            {
                //Unfollow 

                _friendrep.Delete(friendshipFromDb);

                return Json(new
                {
                    result = "<span class=\"glyphicon glyphicon-ok-sign\"></span> <span id=\"btnfriendship\">Follow</span>",
                    userFollowedId = userId,
                    userFollowerId = WebSecurity.CurrentUserId

                });
            }
        }
    }
}