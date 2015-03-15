using Microsoft.AspNet.Identity;
using PracticeMvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PracticeMvc2.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && model.Username == "nitin" && model.Password == "reddy")
            {
                System.Web.HttpContext.Current.GetOwinContext().Authentication.SignIn(new ClaimsIdentity[] { new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Name, model.Username) }, DefaultAuthenticationTypes.ApplicationCookie ) });
                string returnUrl = System.Web.HttpContext.Current.Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToRoute(new { Controller="Home" });
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return RedirectToRoute(new { Controller = "Login", Action = "Index" });
        }
    }
}