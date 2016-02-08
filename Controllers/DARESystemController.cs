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
    public class DARESystemController : Controller
    {
        private npruessnerEEntities db = new npruessnerEEntities();

        // GET: DARESystem
        public ActionResult Index()
        {
            return View(db.DARESystems.ToList());
        }

        // GET: DARESystem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DARESystem dARESystem = db.DARESystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // GET: DARESystem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DARESystem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SystemID,Name,DateInitiated,HomeAddress,City,State,ZIP,Description,FamilyName")] DARESystem dARESystem)
        {
            if (ModelState.IsValid)
            {
                db.DARESystems.Add(dARESystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dARESystem);
        }

        // GET: DARESystem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DARESystem dARESystem = db.DARESystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // POST: DARESystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SystemID,Name,DateInitiated,HomeAddress,City,State,ZIP,Description,FamilyName")] DARESystem dARESystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dARESystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dARESystem);
        }

        // GET: DARESystem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DARESystem dARESystem = db.DARESystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // POST: DARESystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DARESystem dARESystem = db.DARESystems.Find(id);
            db.DARESystems.Remove(dARESystem);
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
