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
using System.Web.UI.WebControls;

namespace DARE.Controllers
{
    [RequireHttps]
    public class AccountController : Controller
    {
        //create an instance of the database (model)
        npruessnerEEntities db = new npruessnerEEntities();
        [AllowAnonymous]
        [HttpGet]
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

       

        [HttpPost]
        public JsonResult ValidateUser(string username, string password)
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
                string url = FormsAuthentication.GetRedirectUrl(username, false);
                User user = db.Users.Where(m => m.Username == username).Single();
                db.LastLoginUpdate(username);
                Session["Name"] = user.FirstName + " " + user.LastName;
                var status = new jsonObject { message = "1", redirecturl = url };
                return Json(status);
            }
            else
            {
                var status = new jsonObject { message = "0", redirecturl = "Account/Login" };
                return Json(status);
            }
        }

        public class jsonObject
        {
            public string message { get; set; }
            public string redirecturl { get; set; }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
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