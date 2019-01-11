using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class EntryTestController : Controller // toDO
    {
        private LanguageSchoolEntities db = new LanguageSchoolEntities();
        private Random random = new Random();

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

        [HttpGet]
        public ActionResult TakeTest(int id)
        {
            Test t = db.Tests.Find(id);

            TestViewModel tvm = new TestViewModel(t);
            
            var ChosenQuestions = new List<ClosedQuestionViewModel>();

            //int LessonSubjectsCount = t.TestsLessonSubjects.Count;

            int LessonSubjectsCount = t.NumberOfQuestions; // TODO

            var QuestionPartitioned = RandomList(t.Points, LessonSubjectsCount);

            for (int i = 1; i <= LessonSubjectsCount; i++)
            {
                int LessonSubjectId = t.TestsLessonSubjects.ElementAt(i - 1).LessonSubjectId;

                var cqquery = from cq in db.ClosedQuestions
                        where cq.LessonSubjectId == LessonSubjectId
                            && cq.IsDeleted == false
                        select cq;

                var LessonSubjectQuestions = cqquery.ToList();

                int HowMany = QuestionPartitioned[i - 1];

                var ChosenForTheSubject = LessonSubjectQuestions.OrderBy(x => random.Next()).Take(HowMany);

                foreach(var chosen in ChosenForTheSubject)
                {
                    var paquery = from a in db.Answers
                                 where a.ClosedQuestionId == chosen.Id
                                     && a.IsDeleted == false
                                     && a.IsCorrect == true
                                  select a;

                    var ProperAnswer = paquery.First();

                    var waquery = from a in db.Answers
                                  where a.ClosedQuestionId == chosen.Id
                                      && a.IsDeleted == false
                                      && a.IsCorrect == false
                                  select a;

                    var WrongAnswers = waquery.ToList().OrderBy(x => random.Next()).Take(chosen.NumberOfPossibleAnswers - 1);

                    var chosenvm = new ClosedQuestionViewModel(chosen);

                    foreach (var wronganswer in WrongAnswers)
                    {
                        var WrongAnswerVM = new AnswerViewModel(wronganswer);

                        chosenvm.Answers.Add(WrongAnswerVM);
                    }

                    var ProperAnswerVM = new AnswerViewModel(ProperAnswer);

                    chosenvm.Answers.Add(ProperAnswerVM);

                    chosenvm.Answers = chosenvm.Answers.OrderBy(x => random.Next()).ToList();

                    ChosenQuestions.Add(chosenvm);
                }

                //NumberOfQuestions
            }

            tvm.Questions = ChosenQuestions;

            return View(tvm);
        }

        [HttpPost]
        public ActionResult TakeTest(TestViewModel tvm)
        {
            //try
            //{
            //    ContactRequest cr = new ContactRequest();

            //    cr.PhoneNumber = crvm.PhoneNumber;
            //    cr.EmailAdress = crvm.EmailAdress;
            //    cr.Comment = crvm.Comment;
            //    cr.CourseId = crvm.CourseId;
            //    cr.EntryTestId = crvm.EntryTestId;
            //    cr.Points = crvm.Points;

            //    cr.IsAwaiting = true;
            //    cr.CreationDate = DateTime.Now;

            //    unitOfWork.ContactRequestRepository.Insert(cr);
            //    unitOfWork.Save();

            //userAnswers = userQuestions.ToList();
            //var carToFind = cars.FirstOrDefault(car => car.Parts.Any(part => part.Id == idToFind));
            //tvm.Questions

            var userCorrectAnswers = new List<AnswerViewModel>();
            int userPoints = 0;

            foreach (ClosedQuestionViewModel cqvm in tvm.Questions)
            {
                if(cqvm.Answers.Where(a => (a.IsCorrect == true) && (a.IsMarked == true)).Any())
                    userPoints += cqvm.Points;
            }

            TempData["Alert"] = new AlertViewModel()
            {
                Title = "Ukończono test",
                Message = "wynik który otrzymałeś to " + userPoints + " na " + tvm.Points + " możliwych",
                AlertType = Consts.Success
            };

            return RedirectToAction("Create", "ContactRequest", new { id = tvm.CourseId });
            //}
            //catch
            //{
            //    return View();
            //}
        }

        private List<int> RandomList(int Points, int LessonSubjectsCount)
        {
            List<int> list = new List<int>(LessonSubjectsCount);

            int quotient = Points / LessonSubjectsCount;
            int remainder = Points % LessonSubjectsCount;

            for (int i = 1; i <= LessonSubjectsCount; i++) list.Add(quotient);

            if (remainder != 0)
            {
                int rPlace = random.Next(1, LessonSubjectsCount);

                list[rPlace] = remainder;
            }

            return list;
        }
    }
}