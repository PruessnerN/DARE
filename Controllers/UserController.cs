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

namespace DARE.Controllers
{
    [Authorize(Roles = "3")]
    public class UserController : Controller
    {
        private npruessnerEEntities db = new npruessnerEEntities();
        private PrivilegeProvider pp = new PrivilegeProvider();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

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

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,EMail,Password,CreationDate,ApprovalDate,LastLoginDate,IsLocked,PasswordQuestion,PasswordAnswer,ActivationToken,EmailConfirmed,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] User user, int roleID)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Permissions(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewmodel = new UserPrivilegeViewModel
            {
                User = db.Users.Include(i => i.Privileges).First(i => i.UserID == id),
                UserPrivilegeArray = pp.GetUserPrivileges(id)
            };

            var allPermissionsList = db.Privileges.ToList();
            viewmodel.AllPrivileges = allPermissionsList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.PrivilegeID.ToString()
            });
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Permissions([Bind(Include = "User, UserPermissionsArray")] UserPrivilegeViewModel userPriv)
        {
            try
            {
                UserPrivilege[] oldPermissions = pp.GetUserPrivileges(userPriv.User.UserID);

                for (int i = 0; i < userPriv.UserPrivilegeArray.Length; i++)
                {
                    if (userPriv.UserPrivilegeArray[i].Name.ToString() == oldPermissions[i].Name.ToString() && userPriv.UserPrivilegeArray[i].IsSet.ToString() != oldPermissions[i].IsSet.ToString())
                    {
                        if (userPriv.UserPrivilegeArray[i].IsSet == true)
                        {
                            pp.AddPrivilegeToUser(userPriv.User.UserID, userPriv.UserPrivilegeArray[i].Name);
                        }
                        else {
                            pp.RemovePrivilegeFromUser(userPriv.User.UserID, userPriv.UserPrivilegeArray[i].Name);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(userPriv);
            }

        }

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
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,EMail,Password,CreationDate,ApprovalDate,LastLoginDate,IsLocked,PasswordQuestion,PasswordAnswer,ActivationToken,EmailConfirmed,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
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
