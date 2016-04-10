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
using System.Data.Entity.Core.Objects;
using System.Globalization;

namespace DARE.Controllers
{
    public class SensorDatasController : Controller
    {
        private npruessnerEEntities1 db = new npruessnerEEntities1();

        // GET: SensorDatas
        public ActionResult Index()
        {
            var sensorDatas = db.SensorDatas.Include(s => s.Thing);
            return View(sensorDatas.ToList());
        }

        [HttpPost]
        public string getChartData(int thingID)
        {
            Array data = (from t in db.SensorDatas
                          where t.ThingID == thingID
                          orderby t.Date
                          select t.Temperature).ToArray();
            Array labels = db.SensorDatas.OrderBy(d => d.Date).Select(d => d.Date)
                .AsEnumerable().Select(date => date.Value.ToString("ddd - h:mm tt")).ToArray();
            GraphDataViewModel gd = new GraphDataViewModel
            {
                Data = data,
                Labels = labels
            };
            var json = JsonConvert.SerializeObject(gd);
            return json;
        }

        // GET: SensorDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorData sensorData = db.SensorDatas.Find(id);
            if (sensorData == null)
            {
                return HttpNotFound();
            }
            return View(sensorData);
        }

        // GET: SensorDatas/Create
        public ActionResult Create()
        {
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name");
            return View();
        }

        // POST: SensorDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DataID,ThingID,Temperature,Date")] SensorData sensorData)
        {
            if (ModelState.IsValid)
            {
                db.SensorDatas.Add(sensorData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", sensorData.ThingID);
            return View(sensorData);
        }

        // GET: SensorDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorData sensorData = db.SensorDatas.Find(id);
            if (sensorData == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", sensorData.ThingID);
            return View(sensorData);
        }

        // POST: SensorDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DataID,ThingID,Temperature,Date")] SensorData sensorData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", sensorData.ThingID);
            return View(sensorData);
        }

        // GET: SensorDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorData sensorData = db.SensorDatas.Find(id);
            if (sensorData == null)
            {
                return HttpNotFound();
            }
            return View(sensorData);
        }

        // POST: SensorDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorData sensorData = db.SensorDatas.Find(id);
            db.SensorDatas.Remove(sensorData);
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
