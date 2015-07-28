using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebMatrix.WebData;
using System.Web.Mvc;
using MusicHub.Models;
using MusicHub.ViewModels;

namespace MusicHub.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid) 
            {
                WebSecurity.Login(model.UserName, model.Password);
                return View();
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    BirthName = model.BirthDate,
                    GenderId = model.GenderId,
                    UserName = model.UserName,
                    CountryId = model.CountryId,
                    JoinDate = DateTime.Now,
                    PhotoUrl = model.PhotoUrl == null ? "" : model.PhotoUrl
                };

                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, user);
                WebSecurity.Login(model.UserName, model.Password);
                return Redirect("User/Profile");
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return Redirect("Home/Index");
        }

    }
}