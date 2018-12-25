using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    public class UserDataController : Controller
    {
        private LanguageSchoolEntities db = new LanguageSchoolEntities();

        // GET: UserData
        public ActionResult Index()
        {
            var userDatas = db.UserDatas.Include(u => u.User);
            return View(userDatas.ToList());
        }

        // GET: UserData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: UserData/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login");
            return View();
        }

        // POST: UserData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,UserId,Name,Surname,City,Street,HouseNumber,HomeNumber,PublicPhoneNumber,PrivatePhoneNumber,EmailAdress,Comment")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.UserDatas.Add(userData);
                db.SaveChanges();

                return RedirectToAction("Details", "User", new { id = userData.UserId });
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", userData.UserId);
            return View(userData);
        }

        // GET: UserData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", userData.UserId);
            return View(userData);
        }

        // POST: UserData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,UserId,Name,Surname,City,Street,HouseNumber,HomeNumber,PublicPhoneNumber,PrivatePhoneNumber,EmailAdress,Comment")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", userData.UserId);
            return View(userData);
        }

        // GET: UserData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: UserData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserData userData = db.UserDatas.Find(id);
            db.UserDatas.Remove(userData);
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
