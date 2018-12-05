using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class EntryTestController : Controller
    {
        private LanguageSchoolEntities db = new LanguageSchoolEntities();

        [Route("EntryTest/{id}")]
        public ActionResult Index(int id)
        {
            var query = from t in db.Tests
                        join et in db.EntryTests
                            on t.Id equals et.TestId
                        orderby et.CreationDate
                        where et.CourseId == id
                            && (et.IsActive == true && et.IsDeleted == false)
                        select t;

            List<Test> Tests = query.ToList();

            var TestViewModels = new List<TestViewModel>();

            foreach (Test c in Tests)
            {
                TestViewModels.Add(new TestViewModel(c));
            }

            return View(TestViewModels);
        }

        [Route("TakeTest/{id}")]
        public ActionResult TakeTest(int id)
        {
            Test t = db.Tests.Find(id);

            TestViewModel tvm = new TestViewModel(t);
            
            var ChosenQuestions = new List<ClosedQuestionViewModel>();

            int LessonSubjectsCount = t.TestsLessonSubjects.Count;

            var QuestionPartitioned = RandomList(t.Points, LessonSubjectsCount);

            for (int i = 1; i <= LessonSubjectsCount; i++)
            {
                int LessonSubjectId = t.TestsLessonSubjects.ElementAt(i).LessonSubjectId;

                var cqquery = from cq in db.ClosedQuestions
                        where cq.LessonSubjectId == LessonSubjectId
                            && cq.IsDeleted == false
                        select cq;

                var LessonSubjectQuestions = cqquery.ToList();

                //NumberOfQuestions
            }

            tvm.Questions = ChosenQuestions;

            return View(tvm);
        }

        private List<int> RandomList(int Points, int LessonSubjectsCount)
        {
            List<int> list = new List<int>(LessonSubjectsCount);

            int quotient = Points / LessonSubjectsCount;
            int remainder = Points % LessonSubjectsCount;

            for (int i = 1; i <= LessonSubjectsCount; i++) list.Add(quotient);

            if (remainder != 0)
            {
                Random random = new Random();
                int rPlace = random.Next(1, LessonSubjectsCount);

                list[rPlace] = remainder;
            }

            return list;
        }
    }
}