using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class LessonSubjectController : LanguageSchoolController
    {
        // GET: LessonSubject

        [Route("LessonSubjects/{id}")]
        public ActionResult Index(int id)
        {
            var group = unitOfWork.GroupRepository.GetById(id);

            var groupViewModel = new GroupViewModel(group);

            return View(groupViewModel);
        }

        //// GET: LessonSubject/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LessonSubject lessonSubject = db.LessonSubjects.Find(id);
        //    if (lessonSubject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lessonSubject);
        //}

        //// GET: LessonSubject/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
        //    return View();
        //}

        //// POST: LessonSubject/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,CourseId,Name,Description,IsActive")] LessonSubject lessonSubject)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.LessonSubjects.Add(lessonSubject);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", lessonSubject.CourseId);
        //    return View(lessonSubject);
        //}

        //// GET: LessonSubject/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LessonSubject lessonSubject = db.LessonSubjects.Find(id);
        //    if (lessonSubject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", lessonSubject.CourseId);
        //    return View(lessonSubject);
        //}

        //// POST: LessonSubject/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,CourseId,Name,Description,IsActive")] LessonSubject lessonSubject)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lessonSubject).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", lessonSubject.CourseId);
        //    return View(lessonSubject);
        //}

        //// GET: LessonSubject/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LessonSubject lessonSubject = db.LessonSubjects.Find(id);
        //    if (lessonSubject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lessonSubject);
        //}

        //// POST: LessonSubject/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LessonSubject lessonSubject = db.LessonSubjects.Find(id);
        //    db.LessonSubjects.Remove(lessonSubject);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
