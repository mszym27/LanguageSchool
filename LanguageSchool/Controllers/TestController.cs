﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using LanguageSchool.Models.ViewModels.TakenTestViewModels;

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

            test.TestsLessonSubjects = new List<TestsLessonSubject>();
            test.TestClosedQuestions = new List<TestClosedQuestion>();
            test.TestOpenQuestions = new List<TestOpenQuestion>();

            foreach (var chosenSubject in testViewModel.LessonSubjects.Where(ls => ls.IsMarked == true))
            {
                var closedQuestionsNumber = chosenSubject.NumberOfClosedQuestions;
                var openQuestionsNumber = chosenSubject.NumberOfOpenQuestions;

                var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(chosenSubject.Id);

                var testLessonSubject = new TestsLessonSubject() {
                    LessonSubjectId = lessonSubject.Id
                };

                test.TestsLessonSubjects.Add(testLessonSubject);

                var closedQuestions = lessonSubject.ClosedQuestions
                    .Where(q => !q.IsDeleted)
                    .OrderBy(x => Rand.Next())
                    .Take(closedQuestionsNumber);

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

                    test.Points += question.Points;
                }

                var openQuestions = lessonSubject.OpenQuestions
                    .Where(q => !q.IsDeleted)
                    .OrderBy(x => Rand.Next())
                    .Take(openQuestionsNumber);

                foreach (var question in openQuestions)
                {
                    test.TestOpenQuestions.Add(
                        new TestOpenQuestion
                        {
                            QuestionId = question.Id
                        }
                    );

                    test.Points += question.Points;
                }

                test.NumberOfClosedQuestions += closedQuestionsNumber;

                test.NumberOfOpenQuestions += openQuestionsNumber;
            }

            UnitOfWork.TestRepository.Insert(test);
            UnitOfWork.Save();

            return RedirectToAction("Details", "Group", new { id = testViewModel.GroupId });
        }

        [Route("Test/Taken/{userTestId}")]
        public ActionResult Taken(int userTestId)
        {
            var userTest = UnitOfWork.UserTestRepository.GetById(userTestId);

            var testViewModel = new TakenTestVM(userTest);

            return View(testViewModel);
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
            //try
            //{
                var student = GetLoggedUser();
                var takenTest = UnitOfWork.TestRepository.GetById(testViewModel.Id);

                var userTest = new UserTest();

                var now = DateTime.Now;

                userTest.CreationDate = now;
                userTest.TestId = testViewModel.Id;
                userTest.UserId = student.Id;
                userTest.Points = 0;

                foreach (var question in testViewModel.ClosedQuestions)
                {
                    if(question.ChosenAnswerId != null || question.Answers.Where(a => a.IsMarked).Any())
                    {
                        var testQuestion = takenTest.TestClosedQuestions.Where(q => q.QuestionId == question.Id).First();

                        var correctAnswerIds = takenTest.TestClosedQuestions
                            .Where(q => q.QuestionId == question.Id)
                            .First()
                            .TestAnswers.Where(a => a.Answer.IsCorrect)
                            .OrderBy(a => a.AnswerId)
                            .Select(a => a.AnswerId);

                        if (question.IsMultichoice)
                        {
                            var chosenAnswerIds = question.Answers
                                .Where(a => a.IsMarked)
                                .OrderBy(a => a.AnswerId)
                                .Select(a => a.AnswerId);

                            foreach(var id in chosenAnswerIds)
                            {
                                student.UserClosedAnswers.Add(
                                    new UserClosedAnswer()
                                    {
                                        TestId = testViewModel.Id,
                                        TestClosedQuestionId = testQuestion.Id,
                                        AnswerId = id
                                    }
                                );
                            }

                            if (correctAnswerIds.SequenceEqual(chosenAnswerIds))
                            { 
                                userTest.Points += question.Points;
                            }
                        }
                        else
                        {
                            student.UserClosedAnswers.Add(
                                new UserClosedAnswer()
                                {
                                    TestId = testViewModel.Id,
                                    TestClosedQuestionId = testQuestion.Id,
                                    AnswerId = (int)question.ChosenAnswerId
                                }
                            );

                            if (correctAnswerIds.Contains((int)question.ChosenAnswerId))
                            {
                                userTest.Points += question.Points;
                            }
                        }
                    }
                }

                double percentageGoten = GradeTest((int)userTest.Points, testViewModel.Points);

                var mark = UnitOfWork.MarkRepository.GetById(Consts.GetGrade(percentageGoten));

                userTest.Mark = mark;

                string userAlertContents = "uzyskana przez Ciebie ocena to " + mark.PLName + ". ";

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

                    userAlertContents += "Czekaj aż prowadzący sprawdzi część otwartą testu - ocena może wtedy ulec zmianie.";
                }
                else
                {
                    userTest.IsMarked = true;

                    if(percentageGoten <= Consts.FailingPercentage)
                        userAlertType = Consts.Failure;
                    else
                        userAlertType = Consts.Success;
                }

                student.UsersTests.Add(userTest);

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(userAlertType, "Test został zakończony", userAlertContents);

                return RedirectToAction("LessonSubjects", "LessonSubject", new { id = testViewModel.GroupId });
            //}
            //catch (Exception ex)
            //{
            //    var errorLogGuid = LogException(ex);

            //    TempData["Alert"] = new AlertViewModel(errorLogGuid);

            //    return View(testViewModel);
            //}
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
            return 100 * ((double) obtainedPoints / maxPoints);
        }
    }
}