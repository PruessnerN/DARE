using DARE.Models;
using DARE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Text;

namespace DARE.Controllers
{
    public class AccountController : Controller
    {
        //create an instance of the database (model)
        npruessnerEEntities db = new npruessnerEEntities();
        private int SALT_BYTE_SIZE = 24;

        public ActionResult Login(string ReturnUrl)
        {
            //has the system been setup yet
            bool noUsers = db.ufn_HaveUsers();
            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (!User.Identity.IsAuthenticated && !noUsers)
            {
                TempData["setupAccessKey"] = "EYEv8gaP3Ys30IMCM9mE";
                return RedirectToAction("Create", "DARESystem");
            }
            else
            {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(User U, string ReturnUrl)
        {
            byte[] salt = db.ufn_GetSalt(U.Username);
            
            var hashedPassword = Hash.CreateHash(U.Hash, salt);

            var count = db.AuthenticateUser(U.Username, hashedPassword);
            if (count == 1)
            {
                FormsAuthentication.SetAuthCookie(U.Username, false);
                string username = U.Username;
                Session["Username"] = username;

                if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/") && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ViewBag.LoginMsg = "Incorrect Username or Password";
                return View();
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult Register(RegisterViewModel U)
        {
            if (U.Password == U.ConfirmPassword)
            {
                RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                var hashedPassword = Hash.CreateHash(U.Password.ToString(), salt);
                db.CreateUser(U.Username, U.Email, hashedPassword, salt, U.PhoneNumber, U.PasswordQuestion, U.PasswordAnswer, U.DateOfBirth, U.FirstName, U.LastName);
            }
            else
            {
                ViewBag.LoginError = "Error: Invalid Information";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
    }
}