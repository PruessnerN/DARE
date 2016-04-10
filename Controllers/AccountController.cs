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
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace DARE.Controllers
{
    [RequireHttps]
    public class AccountController : Controller
    {
        //create an instance of the database (model)
        npruessnerEEntities1 db = new npruessnerEEntities1();
        private int SALT_BYTE_SIZE = 24;

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
        public int ValidateUser(string username, string password)
        {
            int count = CheckUser(username, password);

            if (count == 1)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                string url = FormsAuthentication.GetRedirectUrl(username, false);
                User user = db.Users.Where(m => m.Username == username).Single();
                db.LastLoginUpdate(username);
                Session["Name"] = user.FirstName + " " + user.LastName;
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //
        // GET: Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var valid = db.ResetPassword(model.Username, model.Email).FirstOrDefault();
                if(valid.ReturnCode == 1)
                {
                    User user = db.Users.Where(e => e.Email == model.Email && e.Username == model.Username).FirstOrDefault();
                    // MailMessage class is present is System.Net.Mail namespace
                    MailMessage mailMessage = new MailMessage("donotreply@DARE.net", user.Email);

                    // StringBuilder class is present in System.Text namespace
                    StringBuilder sbEmailBody = new StringBuilder();
                    sbEmailBody.Append("Dear " + user.FirstName + ",<br/><br/>");
                    sbEmailBody.Append("Please click on the following link to reset your password.");
                    sbEmailBody.Append("<br/>"); sbEmailBody.Append("https://dare.local.net/Account/ChangePasswordUnauthenticated?uid=" + valid.UniqueId);
                    sbEmailBody.Append("<br/><br/>");
                    sbEmailBody.Append("<b>DARE System</b>");

                    mailMessage.IsBodyHtml = true;

                    mailMessage.Body = sbEmailBody.ToString();
                    mailMessage.Subject = "Reset Your Password";
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "nathanielipruessner@gmail.com",
                        Password = "80dbknF9"
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    return View("ForgotPasswordConfirmation");
                }
            }
            else
            {
                return View("ForgotPasswordConfirmation");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePasswordUnauthenticated(Guid uid)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel{
                Code = uid
            };
            var valid = db.ResetPasswordRequests.Where(u => u.Guid == uid).FirstOrDefault();

            if (valid.Guid == uid)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePasswordUnauthenticated(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var goodUid = db.ResetPasswordRequests.Where(u => u.Guid == model.Code).FirstOrDefault();

            if (goodUid.Guid != model.Code)
            {
                return View(model);
            }

            var valid = db.ValidateUser(model.Username, model.Email).FirstOrDefault();
            if ((model.Password == model.ConfirmPassword) && (valid.Valid != 0))
            {
                RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                var hashedPassword = Hash.CreateHash(model.Password, salt);
                db.ChangePassword(model.Username, hashedPassword, salt);

                return RedirectToAction("Login");
            }
            else {
                ViewBag.Error = "Incorrect Information!";
                return View();
            }
        }

        public int CheckUser(string username, string password)
        {
            if (username != null)
            {
                byte[] salt = db.ufn_GetSalt(username);
                if (salt != null)
                {
                    var hashedPassword = Hash.CreateHash(password, salt);
                    int authenticated = db.AuthenticateUser(username, hashedPassword);
                    return authenticated;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
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

        // GET: /Manage/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var username = User.Identity.Name;            
            if ((CheckUser(username, model.OldPassword) == 1) && (model.NewPassword == model.ConfirmPassword) && (username != null))
            {
                RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                var hashedPassword = Hash.CreateHash(model.NewPassword, salt);
                db.ChangePassword(username,hashedPassword, salt);

                return RedirectToAction("Logout");
            } else {
                ViewBag.Error = "Incorrect Information!";
                return View();
            }
        }

        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
    }
}