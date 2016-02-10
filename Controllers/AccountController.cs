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

        public ActionResult Login()
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
                return View();
            }

        }

        public string ValidateUser(string username, string password)
        {
            int count;
            if (username != null)
            {
                byte[] salt = db.ufn_GetSalt(username);
                if (salt != null)
                {
                    var hashedPassword = Hash.CreateHash(password, salt);
                    count = db.AuthenticateUser(username, hashedPassword);
                }
                else
                {
                    count = 0;
                }
            }
            else
            {
                count = 0;
            }
            

            
            if (count == 1)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Session["Username"] = username;
                return "1";
            }
            else
            {
                return "0";
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