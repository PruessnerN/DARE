using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DARE.Models;
using Newtonsoft.Json;
using DARE.ViewModels;

namespace DARE.Controllers
{
    public class SchedulesController : Controller
    {
        private npruessnerEEntities1 db = new npruessnerEEntities1();

        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Thing).Include(s => s.Action);
            
            return View(schedules.ToList());
        }

        [HttpPost]
        public string getActions(int thingid)
        {
            List<ActionViewModel> actions = null;
            actions = (from a in db.Actions
                       where a.ThingID == thingid
                       select new ActionViewModel
                       {
                           ActionID = a.ActionID,
                           ThingID = a.ThingID,
                           Name = a.Name,
                           Description = a.Description
                       }).ToList();
            var json = JsonConvert.SerializeObject(actions);
            return json;
        }
            

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleID,ThingID,ActionID,Name,CronExpression,Description")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", schedule.ThingID);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", schedule.ThingID);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleID,ThingID,ActionID,Name,CronExpression,Description")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", schedule.ThingID);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
