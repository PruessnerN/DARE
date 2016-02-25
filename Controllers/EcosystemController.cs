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
    
    public class EcosystemController : Controller
    {
        private int SALT_BYTE_SIZE = 24;

        private npruessnerEEntities1 db = new npruessnerEEntities1();

        // GET: Ecosystem
        public ActionResult Index()
        {
            return View(db.Ecosystems.ToList());
        }

        // GET: Ecosystem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecosystem dARESystem = db.Ecosystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // GET: Ecosystem/Create
        public ActionResult Create()
        {
            if(TempData["setupAccessKey"] != null)
            {
                if(TempData["setupAccessKey"].ToString() == "EYEv8gaP3Ys30IMCM9mE")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Ecosystem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,FamilyName,HomeAddress,City,State,ZIP,Username,Email,Password,ConfirmPassword,PasswordQuestion,PasswordAnswer,PhoneNumber,DateOfBirth,FirstName,LastName")] SetupSystemViewModel setupInfo)
        {

            if (setupInfo.Password == setupInfo.ConfirmPassword)
            {
                RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);

                var hashedPassword = Hash.CreateHash(setupInfo.Password.ToString(), salt);
                if (ModelState.IsValid)
                {
                    db.SetupSystem(setupInfo.Name, setupInfo.HomeAddress, setupInfo.City, setupInfo.State, setupInfo.ZIP, setupInfo.Description, setupInfo.FamilyName, setupInfo.Username, setupInfo.Email, hashedPassword, salt, setupInfo.PhoneNumber, setupInfo.PasswordQuestion, setupInfo.PasswordAnswer, setupInfo.DateOfBirth, setupInfo.FirstName, setupInfo.LastName);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.LoginError = "Error: Invalid Information";
                    return View();
                }
            }
            else
            {
                ViewBag.LoginError = "Error: Invalid Information";
                return View(setupInfo);
            }
        }

        // GET: Ecosystem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecosystem dARESystem = db.Ecosystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // POST: Ecosystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,DateInitiated,HomeAddress,City,State,ZIP,Description,FamilyName")] Ecosystem dARESystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dARESystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dARESystem);
        }

        // GET: Ecosystem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecosystem dARESystem = db.Ecosystems.Find(id);
            if (dARESystem == null)
            {
                return HttpNotFound();
            }
            return View(dARESystem);
        }

        // POST: Ecosystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ecosystem dARESystem = db.Ecosystems.Find(id);
            db.Ecosystems.Remove(dARESystem);
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
