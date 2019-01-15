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

            return View(testViewModel);
        }
    }
}