using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DARE.Models;
using DARE.ViewModels;
using System.Security.Cryptography;

namespace DARE.Controllers
{
    public class UserController : Controller
    {
        private npruessnerEEntities1 db = new npruessnerEEntities1();
        private AccessProvider pp = new AccessProvider();
        private int SALT_BYTE_SIZE = 24;

        // GET: Users
        [Authorize(Roles = "3")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        [Authorize]
        public ActionResult UserProfile()
        {
            var id = db.ufn_GetUserID(User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            RegisterViewModel pertinentUser = new RegisterViewModel()
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                PasswordQuestion = user.PasswordQuestion,
                PasswordAnswer = user.PasswordAnswer,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = (DateTime)user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleID = user.Roles.Single().RoleID
            };
            return View(pertinentUser);
        }
        [Authorize(Roles = "3")]
        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "3")]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "3")]
        [HttpPost]
        public ActionResult Create(RegisterViewModel U)
        {
            if (U.Password == U.ConfirmPassword)
            {
                RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                var hashedPassword = Hash.CreateHash(U.Password.ToString(), salt);
                db.CreateUser(U.Username, U.Email, hashedPassword, salt, U.PhoneNumber, U.PasswordQuestion, U.PasswordAnswer, U.DateOfBirth, U.FirstName, U.LastName, U.RoleID);
            }
            else
            {
                ViewBag.LoginError = "Error: Invalid Information";
                return View();
            }
            return RedirectToAction("Index", "User");
        }
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult UserAccess(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccess[] userAccessArray = pp.GetUserAccess(id);
            var viewmodel = new UserAccessViewModel
            {
                UserAccessArray = userAccessArray,
                User = db.Users.Find(id)
            };

            var allThingsList = db.Things.ToList();
            viewmodel.AllThings = allThingsList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.ThingID.ToString()
            });
            return View(viewmodel);
        }
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAccess([Bind(Include = "User, UserAccessArray")] UserAccessViewModel userAccess)
        {
            try
            {
                UserAccess[] oldAccess = pp.GetUserAccess(userAccess.User.UserID);
                for (int i = 0; i < userAccess.UserAccessArray.Length; i++)
                {
                    if ((userAccess.UserAccessArray[i].Name.ToString() == oldAccess[i].Name.ToString()) && (userAccess.UserAccessArray[i].IsSet.ToString() != oldAccess[i].IsSet.ToString()))
                    {
                        if (userAccess.UserAccessArray[i].IsSet == true)
                        {
                            pp.AddAccessToUser(userAccess.User.UserID, userAccess.UserAccessArray[i].ThingID);
                        }
                        else
                        {
                            pp.RemoveAccessFromUser(userAccess.User.UserID, userAccess.UserAccessArray[i].ThingID);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return RedirectToAction("UserAccess");
            }
        }
        
        [Authorize]
        // GET: Users/Edit/5
        public ActionResult UserProfileEdit()
        {
            var id = db.ufn_GetUserID(User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            RegisterViewModel pertinentUser = new RegisterViewModel()
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                PasswordQuestion = user.PasswordQuestion,
                PasswordAnswer = user.PasswordAnswer,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = (DateTime)user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleID = user.Roles.Single().RoleID
            };
            return View(pertinentUser);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfileEdit([Bind(Include = "UserID,Username,Email,PasswordQuestion,PasswordAnswer,DateOfBirth,PhoneNumber,FirstName,LastName,RoleID")] RegisterViewModel user)
        {
            if (user != null)
            {
                db.UpdateUser(user.UserID, user.Username, user.Email, user.PasswordQuestion, user.PasswordAnswer, user.DateOfBirth, user.PhoneNumber, user.FirstName, user.LastName, user.RoleID);
                return RedirectToAction("UserProfile");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            RegisterViewModel pertinentUser = new RegisterViewModel()
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                PasswordQuestion = user.PasswordQuestion,
                PasswordAnswer = user.PasswordAnswer,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = (DateTime)user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleID = user.Roles.Single().RoleID
            };
            return View(pertinentUser);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Email,PasswordQuestion,PasswordAnswer,DateOfBirth,PhoneNumber,FirstName,LastName,RoleID")] RegisterViewModel user)
        {
            if (user != null )
            {
                db.UpdateUser(user.UserID, user.Username, user.Email, user.PasswordQuestion, user.PasswordAnswer, user.DateOfBirth, user.PhoneNumber, user.FirstName, user.LastName, user.RoleID);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
        // GET: Users/Delete/5
        [Authorize(Roles = "3")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "3")]
        public ActionResult DeleteConfirmed(long? id)
        {
                var deleted = db.DeleteUser(id);
                return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
