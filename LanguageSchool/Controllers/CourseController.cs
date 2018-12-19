using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class CourseController : LanguageSchoolController
    {
        public ActionResult Index(string sortColumn = "startDate", string sortDirection = "asc", int page = 1)
        {
            var courses = unitOfWork.CourseRepository.Get(c => (c.IsActive && !c.IsDeleted));

            if(page == 1)
            {
                sortDirection = (sortDirection == "desc") ? "asc" : "desc";
            }

            courses = this.Sort(courses, sortColumn, sortDirection);

            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;

            List<Course> Courses = courses.ToList();

            var CoursesViewModels = new List<CourseViewModel>();

            foreach (Course c in Courses)
            {
                CoursesViewModels.Add(new CourseViewModel(c));
            }

            return View(CoursesViewModels.ToPagedList(page, 3));
        }

        [Route("Course/List/")]
        [Authorize(Roles = "Secretary")]
        public ActionResult List(string sortColumn = "startDate", string sortDirection = "asc", int page = 1)
        {
            var courses = unitOfWork.CourseRepository.Get(c => !c.IsDeleted);

            if (page == 1)
            {
                sortDirection = (sortDirection == "desc") ? "asc" : "desc";
            }

            courses = this.Sort(courses, sortColumn, sortDirection);

            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;

            // toDO
            //List<Course> Courses = courses.ToList();

            //var CoursesViewModels = new List<CourseViewModel>();

            //foreach (Course c in Courses)
            //{
            //    CoursesViewModels.Add(new CourseViewModel(c));
            //}

            return View(courses.ToPagedList(page, 20));
        }

        [Route("Course/{id}")]
        public ActionResult Details(int id)
        {
            Course course = unitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [Route("Course/Create/")]
        [Authorize(Roles = "Secretary")]
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

        [Route("Course/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(int id)
        {
            var course = unitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
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

        private IEnumerable<Course> Sort(IEnumerable<Course> courses, string sortColumn, string sortDirection)
        {
            switch (sortColumn)
            {
                case "isActive":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.IsActive);
                    else
                        courses = courses.OrderByDescending(c => c.IsActive);
                    break;
                case "creationDate":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.CreationDate);
                    else
                        courses = courses.OrderByDescending(c => c.CreationDate);
                    break;
                case "startDate":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.StartDate);
                    else
                        courses = courses.OrderByDescending(c => c.StartDate);
                    break;
                case "endDate":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.EndDate);
                    else
                        courses = courses.OrderByDescending(c => c.EndDate);
                    break;
                case "name":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.Name);
                    else
                        courses = courses.OrderByDescending(c => c.Name);
                    break;
                case "proficencyLevel":
                    if (sortDirection == "asc")
                        courses = courses.OrderBy(c => c.LanguageProficency.Name);
                    else
                        courses = courses.OrderByDescending(c => c.LanguageProficency.Name);
                    break;
            }

            return courses;
        }

    }
}
