using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebMatrix.WebData;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.Data.Reps.Reps;
using MusicHub.ViewModels;

namespace MusicHub.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        private UserRepository _usrep = new UserRepository();
        private CountriesRepository _countryrep = new CountriesRepository();
        private GendersRepository _genderep = new GendersRepository();

        [SeguridadAuthorize()]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password))
            {
                return RedirectToAction("Profile", "User", new { user = model.UserName});
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [SeguridadAuthorize()]
        public ActionResult Register()
        {
            ViewBag.GenderId = new SelectList(_genderep.All(), "GenderId", "GenderName");

            ViewBag.CountryId = new SelectList(_countryrep.All(), "CountryId", "CountryName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            User username = await _usrep.FindAsync(x => x.UserName == model.UserName);
            User usermail = await _usrep.FindAsync(x => x.Email == model.Email);

            if(username != null || usermail != null)
            {
                string error = username != null ? "Username is already in use." : "Email is already in use.";

                ViewBag.GenderId = new SelectList(_genderep.All(), "GenderId", "GenderName");
                ViewBag.CountryId = new SelectList(_countryrep.All(), "CountryId", "CountryName");

                ModelState.AddModelError("", error);
                return View(model);
            }

            if(ModelState.IsValid)
            {
                var user = new
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    GenderId = model.GenderId,
                    UserName = model.UserName,
                    CountryId = model.CountryId,
                    AboutMe = model.AboutMe,
                    JoinDate = DateTime.Now,
                    PhotoUrl = model.PhotoUrl == null ? @"~/Content/images/profile_pictures/generic_picture.png" : model.PhotoUrl
                };

                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, user);
                WebSecurity.Login(model.UserName, model.Password);
                return RedirectToAction("Profile", "User", new { user = model.UserName});
            }

            ViewBag.GenderId = new SelectList(_genderep.All(), "GenderId", "GenderName");

            ViewBag.CountryId = new SelectList(_countryrep.All(), "CountryId", "CountryName");
            return View(model);
        }

        [Authorize]
        public ActionResult PasswordReset(string Token)
        {
            if (!WebSecurity.IsAuthenticated || string.IsNullOrEmpty(Token))
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
                User user = await _usrep.FindAsync(a => a.UserId == userId);

                WebSecurity.ResetPassword(Token, PasswordReset.ConfirmPassword);
                WebSecurity.Login(user.UserName, PasswordReset.ConfirmPassword, persistCookie: false);

                return RedirectToAction("Index", "User", new { user = WebSecurity.CurrentUserName});
            }

            return View();
        }
 

        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return Redirect("~/Home/Index");
        }

    }
}