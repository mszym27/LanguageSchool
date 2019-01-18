using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class TestController : LanguageSchoolController
    {
        private Random random = new Random();

        //[Route("Test/{id}")]
        //public ActionResult Index(int id)
        //{
        //    var query = from t in db.Tests
        //                join et in db.EntryTests
        //                    on t.Id equals et.TestId
        //                orderby et.CreationDate
        //                where et.CourseId == id
        //                    && (et.IsActive == true && et.IsDeleted == false)
        //                select t;

        //    List<Test> Tests = query.ToList();

        //    var TestViewModels = new List<TestViewModel>();

        //    foreach (Test c in Tests)
        //    {
        //        TestViewModels.Add(new TestViewModel(c));
        //    }

        //    return View(TestViewModels);
        //}

        [HttpGet]
        [Route("Test/Create/{GroupId}")]
        public ActionResult Create(int GroupId)
        {
            var group = unitOfWork.GroupRepository.GetById(GroupId);

            var testViewModel = new TestViewModel(group);

            return View(testViewModel);
        }

        [HttpPost]
        [Route("Test/Create/{GroupId}")]
        public ActionResult Create(TestViewModel testViewModel)
        {
            var test = new Test();

            test.CreationDate = DateTime.Now;

            test.Name = testViewModel.Name;
            test.Comment = testViewModel.Comment;
            test.IsActive = testViewModel.IsActive;
            test.GroupId = testViewModel.GroupId;
            test.NumberOfOpenQuestions = testViewModel.NumberOfOpenQuestions;
            test.NumberOfClosedQuestions = testViewModel.NumberOfClosedQuestions;

            test.TestsLessonSubjects = new List<TestsLessonSubject>();

            foreach (var lessonSubject in testViewModel.LessonSubjects.Where(ls => ls.IsMarked == true))
            {
                var testLessonSubject = new TestsLessonSubject();

                testLessonSubject.LessonSubjectId = lessonSubject.Id;

                test.TestsLessonSubjects.Add(testLessonSubject);
            }

            unitOfWork.TestRepository.Insert(test);
            unitOfWork.Save();

            return RedirectToAction("Details", "Group", new { id = testViewModel.GroupId });
        }

        [HttpGet]
        [Route("Test/Take/{testId}")]
        public ActionResult Take(int testId)
        {
            var test = unitOfWork.TestRepository.GetById(testId);

            var testViewModel = new TestViewModel(test);

            return View(testViewModel);
        }
    }
}