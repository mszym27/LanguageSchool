using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using System.Net;

namespace LanguageSchool.Controllers
{
    public class CourseController : Controller
    {
        private LanguageSchoolEntities db = new LanguageSchoolEntities();

        // GET: Course
        public ActionResult Index()
        {
            var query = from c in db.Courses
                      orderby c.CreationDate
                      where (c.IsActive == true && c.IsDeleted == false)
                      select c;

            List<Course> Courses = query.ToList();

            if(Courses != null)
            {
                return View(Courses);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Route("Course/{id}")]
        public ActionResult Details(int id)
        {
            Course c = db.Courses.Find(id);

            if (c == null)
            {
                return HttpNotFound();
            }

            return View(c);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
