using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DARE.Models;

namespace DARE.Controllers
{
    public class PrivilegeController : Controller
    {
        private npruessnerEEntities db = new npruessnerEEntities();

        // GET: Privileges
        public ActionResult Index()
        {
            return View(db.Privileges.ToList());
        }

        // GET: Privileges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Privilege Privilege = db.Privileges.Find(id);
            if (Privilege == null)
            {
                return HttpNotFound();
            }
            return View(Privilege);
        }

        // GET: Privileges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Privileges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermId,PermName")] Privilege Privilege)
        {
            if (ModelState.IsValid)
            {
                db.Privileges.Add(Privilege);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Privilege);
        }

        // GET: Privileges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Privilege Privilege = db.Privileges.Find(id);
            if (Privilege == null)
            {
                return HttpNotFound();
            }
            return View(Privilege);
        }

        // POST: Privileges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermId,PermName")] Privilege Privilege)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Privilege).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Privilege);
        }

        // GET: Privileges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Privilege Privilege = db.Privileges.Find(id);
            if (Privilege == null)
            {
                return HttpNotFound();
            }
            return View(Privilege);
        }

        // POST: Privileges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Privilege Privilege = db.Privileges.Find(id);
            db.Privileges.Remove(Privilege);
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
