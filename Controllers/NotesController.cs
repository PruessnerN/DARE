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
    public class NotesController : Controller
    {
        private npruessnerEEntities1 db = new npruessnerEEntities1();

        // GET: Notes
        public ActionResult Sent()
        {
            var userID = db.ufn_GetUserID(HttpContext.User.Identity.Name);
            var notes = db.Notes.Where(n => n.SenderID == userID).Include(n => n.User).Include(n => n.User1).OrderBy(n => n.CreationDate);
            return View(notes.ToList());
        }

        public ActionResult Received()
        {
            var userID = db.ufn_GetUserID(HttpContext.User.Identity.Name);
            var notes = db.Notes.Where(n => n.ReceiverID == userID).Include(n => n.User).Include(n => n.User1).OrderBy(n => n.CreationDate);
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Username");
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SenderID,ReceiverID,Subject,Message,Alert,PushNotification,AlertDate")] Note note)
        {
            db.CreateNote(note.SenderID, note.ReceiverID, note.Subject, note.Message, note.Alert, note.PushNotification, note.AlertDate);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Username", note.SenderID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Username", note.ReceiverID);
            return RedirectToAction("Index");
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Username", note.SenderID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Username", note.ReceiverID);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoteID,SenderID,ReceiverID,Subject,Message,isNew,Alert,PushNotification,CreationDate,AlertDate")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Username", note.SenderID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Username", note.ReceiverID);
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
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
