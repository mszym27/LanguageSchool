using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LanguageSchool.DAL;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class CourseController : LanguageSchoolController
    {
        // GET: Course
        public ActionResult Index()
        {
            var query = unitOfWork.CourseRepository.Get().Where(c => c.IsActive).Where(c => !c.IsDeleted).OrderBy(c => c.CreationDate);

            List<Course> Courses = query.ToList();

            var CoursesViewModels = new List<CourseViewModel>();

            foreach (Course c in Courses)
            {
                CoursesViewModels.Add(new CourseViewModel(c));
            }

            return View(CoursesViewModels);
        }

        [Route("Course/{id}")]
        public ActionResult Details(int id)
        {
            Course c = unitOfWork.CourseRepository.GetById(id);

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
