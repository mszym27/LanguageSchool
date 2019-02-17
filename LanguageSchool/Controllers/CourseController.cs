using System;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using PagedList;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using LanguageSchool.Models.ViewModels.CourseViewModels;

namespace LanguageSchool.Controllers
{
    public class CourseController : LanguageSchoolController
    {
        [Route("Course")]
        public ActionResult Index(string sortColumn = "startDate", string sortDirection = "asc", int page = 1)
        {
            var courses = UnitOfWork.CourseRepository.Get(c => (c.IsActive && !c.IsDeleted));

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

        [Route("Course/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null || course.IsDeleted || !course.IsActive)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [Route("Course/List/")]
        [Authorize(Roles = "Secretary")]
        public ActionResult List(
            string searchString, bool showActivated = true, bool showDeactivated = false,
            string sortColumn = "startDate", string sortDirection = "asc", int page = 1
        )
        {
            var courses = UnitOfWork.CourseRepository.Get(c => (
                    !c.IsDeleted && (
                        (showActivated && c.IsActive) || (showDeactivated && !c.IsActive)
                    ) && (
                        searchString == null ||
                        (
                            c.Name.Contains(searchString) ||
                            c.Description.Contains(searchString) ||
                            c.NumberOfHours.Contains(searchString) ||
                            c.LanguageProficency.PLName.Contains(searchString)
                        )
                    )
                )
            );

            if (page == 1)
            {
                sortDirection = (sortDirection == "desc") ? "asc" : "desc";
            }

            courses = this.Sort(courses, sortColumn, sortDirection);

            var pageSize = 20;

            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;
            ViewBag.searchString = searchString;
            ViewBag.showActivated = showActivated;
            ViewBag.showDeactivated = showDeactivated;

            return View(courses.ToPagedList(page, pageSize));
        }
        
        [Route("Course/FullDetails/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult FullDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            var courseVM = new CourseDetailsVM(course);

            return View(courseVM);
        }

        [HttpGet]
        [Route("Course/Create/")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create()
        {
            var courseVM = new CourseInputVM();

            PopulateInputLists(ref courseVM);

            return View(courseVM);
        }

        [HttpPost]
        [Route("Course/Create/")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create(CourseInputVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }

            try
            {
                Course course = new Course();

                course.Name = courseVM.Name;
                course.Description = courseVM.Description;
                course.LanguageProficencyId = courseVM.LanguageProficencyId;
                course.IsActive = courseVM.IsActive;
                course.StartDate = courseVM.StartDate;
                course.EndDate = courseVM.EndDate;
                course.NumberOfHours = courseVM.NumberOfHours;

                UnitOfWork.CourseRepository.Insert(course);

                UnitOfWork.Save();

                return RedirectToAction("FullDetails", new { id = course.Id });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                PopulateInputLists(ref courseVM);

                return View(courseVM);
            }
        }

        [Route("Course/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            var courseVM = new CourseInputVM(course);

            PopulateInputLists(ref courseVM);

            return View(courseVM);
        }

        [Route("Course/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        [HttpPost]
        public ActionResult Edit(CourseInputVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }

            try
            {
                var course = UnitOfWork.CourseRepository.GetById(courseVM.CourseId);

                course.Name = courseVM.Name;
                course.Description = courseVM.Description;
                course.LanguageProficencyId = courseVM.LanguageProficencyId;
                course.IsActive = courseVM.IsActive;
                course.StartDate = courseVM.StartDate;
                course.EndDate = courseVM.EndDate;
                course.NumberOfHours = courseVM.NumberOfHours;

                UnitOfWork.CourseRepository.Update(course);

                UnitOfWork.Save();

                return RedirectToAction("FullDetails", new { id = course.Id });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                PopulateInputLists(ref courseVM);

                return View(courseVM);
            }
        }

        [Route("Course/DeActivate/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult DeActivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            try
            {
                course.IsActive = !course.IsActive;

                UnitOfWork.CourseRepository.Update(course);

                UnitOfWork.Save();

                return RedirectToAction("FullDetails", new { id = id });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("FullDetails", new { id = id });
            }
        }

        [Route("Course/Delete/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Delete(int id)
        {
            try
            {
                var now = DateTime.Now;

                var course = UnitOfWork.CourseRepository.GetById(id);

                course.IsDeleted = true;

                foreach(var group in course.Groups)
                {
                    group.IsDeleted = true;
                    group.DeletionDate = now;

                    foreach (var student in group.UsersGroups)
                    {
                        student.IsDeleted = true;
                        student.DeletionDate = now;
                    }

                    foreach (var subject in group.LessonSubjects)
                    {
                        subject.IsDeleted = true;
                        subject.DeletionDate = now;

                        foreach (var material in subject.Materials)
                        {
                            material.IsDeleted = true;
                            material.DeletionDate = now;
                        }

                        foreach (var question in subject.ClosedQuestions)
                        {
                            question.IsDeleted = true;
                            question.DeletionDate = now;
                        }

                        foreach (var question in subject.OpenQuestions)
                        {
                            question.IsDeleted = true;
                            question.DeletionDate = now;
                        }
                    }
                }

                UnitOfWork.Save();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        private void PopulateInputLists(ref CourseInputVM courseInputVM)
        {
            courseInputVM.LanguageProficenciens = new SelectList(Consts.LanguageProficencyList, "Key", "Value");
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
                        courses = courses.OrderBy(c => c.LanguageProficency.PLName);
                    else
                        courses = courses.OrderByDescending(c => c.LanguageProficency.PLName);
                    break;
            }

            return courses;
        }

    }
}
