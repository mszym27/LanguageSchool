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
            var group = UnitOfWork.GroupRepository.GetById(GroupId);

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

            UnitOfWork.TestRepository.Insert(test);
            UnitOfWork.Save();

            return RedirectToAction("Details", "Group", new { id = testViewModel.GroupId });
        }

        [HttpGet]
        [Route("Test/Take/{testId}")]
        public ActionResult Take(int testId)
        {
            var test = UnitOfWork.TestRepository.GetById(testId);

            var testViewModel = new TestViewModel(test);

            return View(testViewModel);
        }

        [HttpPost]
        [Route("Test/Take/{testId}")]
        public ActionResult Take(TestViewModel testViewModel)
        {
            try
            {
                var student = GetLoggedUser();

                var userTest = new UsersTests();

                var now = DateTime.Now;

                userTest.CreationDate = now;
                userTest.TestId = testViewModel.Id;
                userTest.UserId = student.Id;
                userTest.Points = 0;

                foreach (var question in testViewModel.ClosedQuestions) // userTest.Points
                {
                    var correctAnswerIds = UnitOfWork.AnswerRepository.Get(a => !a.IsDeleted 
                        && a.ClosedQuestion.Id == question.Id && a.IsCorrect).Select(a => a.Id);

                    if (question.IsMultichoice)
                    {
                        //if(question.NumberOfPossibleAnswers) // TODO - czy user wybral WSZYSTKIE poprawne ktore mu sie wyswietlily

                        userTest.Points += question.Points;
                    }
                    else
                    {
                        if(correctAnswerIds.Contains(question.ChosenAnswerId))
                            userTest.Points += question.Points;
                    }
                }

                double percentageGoten = GradeTest((int)userTest.Points, testViewModel.Points);

                string userAlertContents = "uzyskana przez Ciebie ocena to " + Consts.GetGrade(percentageGoten) + ". ";

                string userAlertType = Consts.Info;

                if (testViewModel.OpenQuestions.Any()
                    & testViewModel.OpenQuestions.Where(q => q.Answer != null).Any())
                {
                    foreach (var question in testViewModel.OpenQuestions.Where(q => q.Answer != null))
                    {
                        var answer = new UserOpenAnswer();

                        answer.CreationDate = now;
                        answer.TestId = testViewModel.Id;
                        answer.OpenQuestionId = question.Id;
                        answer.Content = question.Answer;

                        student.UserOpenAnswers.Add(answer);
                    }

                    userAlertContents += "Czekaj aż prowadzący oceni część otwartą testu.";
                }
                else
                {
                    userTest.IsMarked = true;

                    if(percentageGoten < Consts.FailingPercentage)
                        userAlertType = Consts.Failure;
                    else
                        userAlertType = Consts.Success;
                }

                student.UsersTests.Add(userTest);

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(userAlertType, "Test został zakończony", userAlertContents);

                return RedirectToAction("LessonSubjects", "LessonSubject", new { id = testViewModel.GroupId });
            }
            catch (Exception ex)
            {
                TempData["Alert"] = new AlertViewModel(Consts.Error, "Operacja nie powiodła się", "jeśli problem się powtórzy skontaktuj się z prowadzącym.");

                return View(testViewModel);
            }
        }

        private double GradeTest(int obtainedPoints, int maxPoints)
        {
            return ((double)maxPoints / 100) * obtainedPoints;
        }
    }
}