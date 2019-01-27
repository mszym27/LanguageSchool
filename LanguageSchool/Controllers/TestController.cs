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
        private Random Rand = new Random();

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
                var chosenSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubject.Id);

                var testLessonSubject = new TestsLessonSubject() {
                    LessonSubjectId = chosenSubject.Id
                };

                test.TestsLessonSubjects.Add(testLessonSubject);

                var closedQuestions = chosenSubject.ClosedQuestions
                    .Where(q => !q.IsDeleted)
                    .OrderBy(x => Rand.Next())
                    .Take(test.NumberOfClosedQuestions);

                test.TestClosedQuestions = new List<TestClosedQuestion>();

                foreach (var question in closedQuestions)
                {
                    var possibleAnswers = question.NumberOfPossibleAnswers;

                    var properAnswers = question.IsMultichoice? Rand.Next(1, possibleAnswers) : 1;

                    var answers = question.Answers
                        .Where(a => !a.IsDeleted && !a.IsCorrect)
                        .OrderBy(x => Rand.Next())
                        .Take(possibleAnswers - properAnswers)
                        .ToList();

                    var proper = question.Answers
                        .Where(a => !a.IsDeleted && a.IsCorrect)
                        .OrderByDescending(a => a.IsCorrect)
                        .ThenBy(x => Rand.Next())
                        .Take(properAnswers)
                        .ToList();

                    answers.AddRange(proper);

                    TestClosedQuestion testQuestion = new TestClosedQuestion
                    {
                        QuestionId = question.Id
                    };

                    testQuestion.TestAnswers = new List<TestAnswer>();

                    foreach (var answer in answers)
                    {
                        testQuestion.TestAnswers.Add(
                            new TestAnswer
                            {
                                AnswerId = answer.Id
                            }
                        );
                    }

                    test.TestClosedQuestions.Add(testQuestion);
                }

                var openQuestions = chosenSubject.OpenQuestions
                    .Where(q => !q.IsDeleted)
                    .OrderBy(x => Rand.Next())
                    .Take(test.NumberOfOpenQuestions);

                test.TestOpenQuestions = new List<TestOpenQuestion>();

                foreach (var question in openQuestions)
                {
                    test.TestOpenQuestions.Add(
                        new TestOpenQuestion
                        {
                            QuestionId = question.Id
                        }
                    );
                }
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

            foreach (var question in testViewModel.ClosedQuestions)
            {
                Shuffle(question.Answers);
            }

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
                    if(question.ChosenAnswerId != null)
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
                            if(correctAnswerIds.Contains((int)question.ChosenAnswerId))
                                userTest.Points += question.Points;
                        }
                    }
                }

                double percentageGoten = GradeTest((int)userTest.Points, testViewModel.Points);

                string userAlertContents = "uzyskana przez Ciebie ocena to " + Consts.GetGrade(percentageGoten) + ". ";

                string userAlertType = Consts.Info;

                if (testViewModel.OpenQuestions != null
                    && testViewModel.OpenQuestions.Where(q => q.Answer != null).Any())
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
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(testViewModel);
            }
        }

        private void Shuffle(List<AnswerViewModel> answers)
        {
            int n = answers.Count;
            while (n > 1)
            {
                n--;
                int k = Rand.Next(n + 1);
                var value = answers[k];
                answers[k] = answers[n];
                answers[n] = value;
            }
        }

        private double GradeTest(int obtainedPoints, int maxPoints)
        {
            return ((double)maxPoints / 100) * obtainedPoints;
        }
    }
}