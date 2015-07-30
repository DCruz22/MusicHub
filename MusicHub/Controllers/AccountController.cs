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

        private UserRepository _usr = new UserRepository();
        private CountriesRepository _country = new CountriesRepository();
        private GendersRepository _gender = new GendersRepository();

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
            ViewBag.GenderId = new SelectList(_gender.All(), "GenderId", "GenderName");

            ViewBag.CountryId = new SelectList(_country.All(), "CountryId", "CountryName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            User username = await _usr.FindAsync(x => x.UserName == model.UserName);
            User usermail = await _usr.FindAsync(x => x.Email == model.Email);

            if(username != null || usermail != null)
            {
                string error = username != null ? "Username is already in use." : "Email is already in use.";

                ViewBag.GenderId = new SelectList(_gender.All(), "GenderId", "GenderName");
                ViewBag.CountryId = new SelectList(_country.All(), "CountryId", "CountryName");

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

            ViewBag.GenderId = new SelectList(_gender.All(), "GenderId", "GenderName");

            ViewBag.CountryId = new SelectList(_country.All(), "CountryId", "CountryName");
            return View(model);
        }

        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return Redirect("~/Home/Index");
        }

    }
}