using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class QuestionController : LanguageSchoolController
    {
        [Route("Questions/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Index(int lessonSubjectId)
        {
            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            var subjectsQuestionsViewModel = new SubjectsQuestionsViewModel(lessonSubject);

            return View(subjectsQuestionsViewModel);
        }

        [HttpGet]
        [Route("Questions/CreateOpen/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateOpen(int lessonSubjectId)
        {
            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            var openQuestion = new OpenQuestionViewModel(lessonSubject);

            return View(openQuestion);
        }

        [HttpPost]
        [Route("Questions/CreateOpen/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateOpen(OpenQuestionViewModel openQuestionViewModel)
        {
            try
            {
                var openQuestion = new OpenQuestion();

                openQuestion.LessonSubjectId = openQuestionViewModel.LessonSubjectId;
                openQuestion.Points = openQuestionViewModel.Points;
                openQuestion.Contents = openQuestionViewModel.Contents;

                UnitOfWork.OpenQuestionRepository.Insert(openQuestion);
                UnitOfWork.Save();

                return RedirectToAction("Index", openQuestionViewModel.LessonSubjectId);
            }
            catch
            {
                return View(openQuestionViewModel);
            }
        }

        [HttpGet]
        [Route("Questions/CreateClosed/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateClosed(int lessonSubjectId)
        {
            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            var closedQuestion = new ClosedQuestionViewModel(lessonSubject);

            return View(closedQuestion);
        }

        [Route("Questions/DeleteClosed/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteClosed(int id)
        {
            try
            {
                var now = DateTime.Now;
                var closedQuestion = UnitOfWork.ClosedQuestionRepository.GetById(id);

                closedQuestion.IsDeleted = true;
                closedQuestion.DeletionDate = now;

                foreach(var testQuestion in closedQuestion.TestClosedQuestions)
                {
                    testQuestion.IsDeleted = true;
                    testQuestion.DeletionDate = now;

                    var test = testQuestion.Test;

                    var questionSubject = closedQuestion.LessonSubject;

                    var testSubject = test.TestsLessonSubjects.Where(ls => ls.LessonSubjectId == questionSubject.Id).First();

                    if (!test.TestClosedQuestions.Where(q => !q.IsDeleted && q.ClosedQuestion.LessonSubjectId == questionSubject.Id).Any())
                    {
                        testSubject.IsDeleted = true;
                        testSubject.DeletionDate = now;
                    }

                    test.Points = test.Points - closedQuestion.Points;

                    var correctAnswerIds = test.TestClosedQuestions
                        .Where(q => q.QuestionId == closedQuestion.Id)
                        .First()
                        .TestAnswers.Where(a => a.Answer.IsCorrect)
                        .OrderBy(a => a.AnswerId)
                        .Select(a => a.AnswerId);


                    test.NumberOfClosedQuestions--;

                    if (test.NumberOfClosedQuestions == 0)
                    {
                        test.IsDeleted = true;
                        test.DeletionDate = now;

                        foreach (var userTest in test.UsersTests)
                        {
                            userTest.IsDeleted = true;
                            userTest.DeletionDate = now;
                        }
                    }
                    else
                    {
                        foreach (var userTest in test.UsersTests)
                        {
                            var userAnswers = userTest.UserClosedAnswers
                                .Where(a => a.TestClosedQuestion.QuestionId == closedQuestion.Id)
                                .OrderBy(a => a.AnswerId)
                                .Select(a => a.AnswerId);

                            if (correctAnswerIds.SequenceEqual(userAnswers))
                            {
                                userTest.Points = userTest.Points - closedQuestion.Points;
                            }

                            double percentageGoten = GradeTest((int)userTest.Points, test.Points);

                            userTest.MarkId = Consts.GetGrade(percentageGoten);
                        }
                    }
                }

                return RedirectToAction("Index", new { id = closedQuestion.LessonSubjectId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [Route("Questions/DeleteOpen/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteOpen(int id)
        {
            try
            {
                var now = DateTime.Now;
                var openQuestion = UnitOfWork.OpenQuestionRepository.GetById(id);

                openQuestion.IsDeleted = true;
                openQuestion.DeletionDate = now;

                foreach (var testQuestion in openQuestion.TestOpenQuestions)
                {
                    testQuestion.IsDeleted = true;
                    testQuestion.DeletionDate = now;

                    var test = testQuestion.Test;

                    var questionSubject = openQuestion.LessonSubject;

                    var testSubject = test.TestsLessonSubjects.Where(ls => ls.LessonSubjectId == questionSubject.Id).First();

                    if (!test.TestOpenQuestions.Where(q => !q.IsDeleted && q.OpenQuestion.LessonSubjectId == questionSubject.Id).Any())
                    {
                        testSubject.IsDeleted = true;
                        testSubject.DeletionDate = now;
                    }

                    test.Points = test.Points - openQuestion.Points;

                    test.NumberOfClosedQuestions--;

                    if (test.NumberOfClosedQuestions == 0)
                    {
                        test.IsDeleted = true;
                        test.DeletionDate = now;

                        foreach (var userTest in test.UsersTests)
                        {
                            userTest.IsDeleted = true;
                            userTest.DeletionDate = now;
                        }
                    }
                    else
                    {
                        foreach (var userTest in test.UsersTests)
                        {
                            var userAnswer = userTest.UserOpenAnswers
                                .Where(a => a.OpenQuestionId == openQuestion.Id)
                                .FirstOrDefault();

                            if (userAnswer != null)
                            {
                                if (userAnswer.IsMarked)
                                {
                                    userTest.Points = userTest.Points - openQuestion.Points;
                                }

                                if (!userTest.IsMarked && !userTest.UserOpenAnswers.Where(a => !a.IsMarked && a.OpenQuestionId != openQuestion.Id).Any())
                                {
                                    userTest.Points = userTest.Points - openQuestion.Points;
                                }
                            }

                            double percentageGoten = GradeTest((int)userTest.Points, test.Points);

                            userTest.MarkId = Consts.GetGrade(percentageGoten);
                        }
                    }
                }

                return RedirectToAction("Index", new { id = openQuestion.LessonSubjectId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Route("Questions/AddAnswers/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddAnswers(ClosedQuestionViewModel closedQuestion)
        {
            return View(closedQuestion);
        }

        [HttpPost]
        [Route("Questions/CreateClosed/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateClosed(ClosedQuestionViewModel closedQuestionViewModel, string submit)
        {
            try
            {
                if (submit == "Utwórz pytanie")
                {
                    var closedQuestion = new ClosedQuestion();

                    closedQuestion.LessonSubjectId = closedQuestionViewModel.LessonSubjectId;
                    closedQuestion.Contents = closedQuestionViewModel.Contents;
                    closedQuestion.NumberOfPossibleAnswers = closedQuestionViewModel.NumberOfPossibleAnswers;
                    closedQuestion.Points = closedQuestionViewModel.Points;
                    closedQuestion.IsMultichoice = closedQuestionViewModel.IsMultichoice;

                    closedQuestion.Answers = new List<Answer>();

                    foreach(var answerViewModel in closedQuestionViewModel.Answers)
                    {
                        var answer = new Answer();

                        answer.AnswerContent = answerViewModel.Answer;
                        answer.IsCorrect = answerViewModel.IsCorrect;

                        closedQuestion.Answers.Add(answer);
                    }

                    UnitOfWork.ClosedQuestionRepository.Insert(closedQuestion);
                    UnitOfWork.Save();

                    return RedirectToAction("Index", closedQuestionViewModel.LessonSubjectId);
                }
                else
                {
                    if (closedQuestionViewModel.Answers == null)
                        closedQuestionViewModel.Answers = new List<AnswerViewModel>();

                    closedQuestionViewModel.Answers.Add(new AnswerViewModel());

                    return View("AddAnswers", closedQuestionViewModel);
                }
            }
            catch(Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View("AddAnswers", closedQuestionViewModel);
            }
        }

        private double GradeTest(int obtainedPoints, int maxPoints)
        {
            return 100 * ((double)obtainedPoints / maxPoints);
        }
    }
}
