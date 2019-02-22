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
    [Authorize]
    public class LessonSubjectController : LanguageSchoolController
    {
        [Route("LessonSubjects/{id}")]
        [Authorize(Roles = "Student")]
        public ActionResult LessonSubjects(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var student = GetLoggedUser();

                var group = student.UsersGroups
                    .Where(ug => !ug.IsDeleted)
                    .Where(ug => ug.GroupId == id)
                    .Select(ug => ug.Group)
                    .FirstOrDefault();

                if (group == null)
                {
                    return HttpNotFound();
                }

                var groupViewModel = new GroupStudentVM(group, student);

                return View(groupViewModel);
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lessonSubjectViewModel = new LessonSubjectViewModel() { GroupId = (int)id };

            return View(lessonSubjectViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(LessonSubjectViewModel lessonSubjectViewModel)
        {
            try
            {
                var lessonSubject = new LessonSubject()
                {
                    GroupId = lessonSubjectViewModel.GroupId,
                    Name = lessonSubjectViewModel.Name,
                    Description = lessonSubjectViewModel.Description,
                    IsActive = lessonSubjectViewModel.IsActive,
                };

                UnitOfWork.LessonSubjectRepository.Insert(lessonSubject);
                UnitOfWork.Save();

                return RedirectToAction("Details", "Group", new { id = lessonSubjectViewModel.GroupId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(lessonSubjectViewModel);
            }
        }

        [HttpGet]
        [Route("LessonSubject/Edit/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(id);

            if (lessonSubject == null)
            {
                return HttpNotFound();
            }

            var lessonSubjectViewModel = new LessonSubjectViewModel(lessonSubject);

            return View(lessonSubjectViewModel);
        }

        [HttpPost]
        [Route("LessonSubject/Edit/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(LessonSubjectViewModel lessonSubjectViewModel)
        {
            try
            {
                var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectViewModel.Id);

                lessonSubject.Name = lessonSubjectViewModel.Name;
                lessonSubject.Description = lessonSubjectViewModel.Description;
                lessonSubject.IsActive = lessonSubjectViewModel.IsActive;

                UnitOfWork.LessonSubjectRepository.Update(lessonSubject);
                UnitOfWork.Save();

                return RedirectToAction("Details", new { id = lessonSubjectViewModel.Id });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(lessonSubjectViewModel);
            }
        }

        [Route("LessonSubject/CreateFromExisting/{groupId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateFromExisting(int? groupId)
        {
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = UnitOfWork.GroupRepository.GetById(groupId);

            if (group == null)
            {
                return HttpNotFound();
            }

            var lessonSubjects = new ExistingLessonSubjectsVM(group.Course, (int)groupId);

            return View(lessonSubjects);
        }

        [Route("LessonSubject/Copy/{lessonSubjectId}/{groupId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Copy(int lessonSubjectId, int groupId)
        {
            try
            {
                var existingSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

                var lessonSubject = new LessonSubject()
                {
                    GroupId = groupId,
                    Name = existingSubject.Name,
                    Description = existingSubject.Description,
                    IsActive = false
                };

                lessonSubject.ClosedQuestions = new List<ClosedQuestion>();

                foreach (var existingQuestion in existingSubject.ClosedQuestions.Where(q => !q.IsDeleted))
                {
                    var copiedQuestion = new ClosedQuestion()
                    {
                        Contents = existingQuestion.Contents,
                        NumberOfPossibleAnswers = existingQuestion.NumberOfPossibleAnswers,
                        IsMultichoice = existingQuestion.IsMultichoice,
                        Points = existingQuestion.Points,
                    };

                    foreach (var existingAnswer in existingQuestion.Answers.Where(a => !a.IsDeleted))
                    {
                        var copiedAnswer = new Answer()
                        {
                            AnswerContent = existingAnswer.AnswerContent,
                            IsCorrect = existingAnswer.IsCorrect
                        };

                        copiedQuestion.Answers.Add(copiedAnswer);
                    }

                    lessonSubject.ClosedQuestions.Add(copiedQuestion);
                }

                lessonSubject.OpenQuestions = new List<OpenQuestion>();

                foreach (var existingQuestion in existingSubject.OpenQuestions.Where(q => !q.IsDeleted))
                {
                    var copiedQuestion = new OpenQuestion()
                    {
                        Contents = existingQuestion.Contents,
                        Points = existingQuestion.Points
                    };

                    lessonSubject.OpenQuestions.Add(copiedQuestion);
                }

                lessonSubject.Materials = new List<Material>();

                foreach (var existingMaterial in existingSubject.Materials.Where(m => !m.IsDeleted))
                {
                    var copiedMaterial = new Material()
                    {
                        Name = existingMaterial.Name,
                        Comment = existingMaterial.Comment,
                        File = existingMaterial.File
                    };

                    lessonSubject.Materials.Add(copiedMaterial);
                }

                UnitOfWork.LessonSubjectRepository.Insert(lessonSubject);
                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Utworzono nowy temat", "możesz przystąpić do dostosowywania go do potrzeb prowadzonej przez Ciebie grupy");

                return RedirectToAction("Details", new { id = lessonSubject.Id });
            }
            catch(Exception ex)
            {
                var logGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(logGuid);

                return RedirectToAction("Details", "Group", new { id = groupId });
            }
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(id);

            if (lessonSubject == null)
            {
                return HttpNotFound();
            }

            return View(lessonSubject);
        }

        [Route("LessonSubject/DeActivate/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeActivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(id);

                if (lessonSubject == null)
                {
                    return HttpNotFound();
                }

                lessonSubject.IsActive = !lessonSubject.IsActive;

                UnitOfWork.LessonSubjectRepository.Update(lessonSubject);
                UnitOfWork.Save();

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [Route("LessonSubject/Delete/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var now = DateTime.Now;

                var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(id);

                lessonSubject.IsDeleted = true;
                lessonSubject.DeletionDate = now;

                foreach (var material in lessonSubject.Materials)
                {
                    material.IsDeleted = true;
                    material.DeletionDate = now;
                }

                foreach (var question in lessonSubject.ClosedQuestions)
                {
                    question.IsDeleted = true;
                    question.DeletionDate = now;

                    foreach (var testQuestion in question.TestClosedQuestions)
                    {
                        testQuestion.IsDeleted = true;
                        testQuestion.DeletionDate = now;

                        var test = testQuestion.Test;

                        var questionSubject = question.LessonSubject;

                        var testSubject = test.TestsLessonSubjects.Where(ls => ls.LessonSubjectId == questionSubject.Id).First();

                        if (!test.TestClosedQuestions.Where(q => !q.IsDeleted && q.ClosedQuestion.LessonSubjectId == questionSubject.Id).Any())
                        {
                            testSubject.IsDeleted = true;
                            testSubject.DeletionDate = now;
                        }

                        test.Points = test.Points - question.Points;

                        var correctAnswerIds = test.TestClosedQuestions
                            .Where(q => q.QuestionId == question.Id)
                            .First()
                            .TestAnswers.Where(a => a.Answer.IsCorrect)
                            .OrderBy(a => a.AnswerId)
                            .Select(a => a.AnswerId);

                        test.NumberOfClosedQuestions--;
                        test.NumberOfQuestions--;

                        if (test.NumberOfQuestions == 0)
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
                                    .Where(a => a.TestClosedQuestion.QuestionId == question.Id)
                                    .OrderBy(a => a.AnswerId)
                                    .Select(a => a.AnswerId);

                                if (correctAnswerIds.SequenceEqual(userAnswers))
                                {
                                    userTest.Points = userTest.Points - question.Points;
                                }

                                double percentageGoten = GradeTest((int)userTest.Points, test.Points);

                                userTest.MarkId = Consts.GetGrade(percentageGoten);
                            }
                        }
                    }
                }

                foreach (var question in lessonSubject.OpenQuestions)
                {
                    question.IsDeleted = true;
                    question.DeletionDate = now;

                    foreach (var answer in question.UserOpenAnswers)
                    {
                        answer.IsDeleted = true;
                        answer.DeletionDate = now;
                    }

                    foreach (var testQuestion in question.TestOpenQuestions)
                    {
                        testQuestion.IsDeleted = true;
                        testQuestion.DeletionDate = now;

                        var test = testQuestion.Test;

                        var questionSubject = question.LessonSubject;

                        var testSubject = test.TestsLessonSubjects
                            .Where(ls => ls.LessonSubjectId == questionSubject.Id)
                            .First();

                        if (!test.TestOpenQuestions.Where(q => !q.IsDeleted && q.OpenQuestion.LessonSubjectId == questionSubject.Id).Any())
                        {
                            testSubject.IsDeleted = true;
                            testSubject.DeletionDate = now;
                        }

                        test.Points = test.Points - question.Points;

                        test.NumberOfOpenQuestions--;
                        test.NumberOfQuestions--;

                        if (test.NumberOfQuestions == 0)
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
                                    .Where(a => a.OpenQuestionId == question.Id)
                                    .FirstOrDefault();

                                if (userAnswer != null)
                                {
                                    if (userAnswer.IsMarked)
                                    {
                                        userTest.Points = userTest.Points - question.Points;
                                    }

                                    if (!userTest.IsMarked && !userTest.UserOpenAnswers.Where(a => !a.IsMarked && a.OpenQuestionId != question.Id).Any())
                                    {
                                        userTest.Points = userTest.Points - question.Points;
                                    }
                                }

                                double percentageGoten = GradeTest((int)userTest.Points, test.Points);

                                userTest.MarkId = Consts.GetGrade(percentageGoten);
                            }
                        }
                    }
                }

                UnitOfWork.Save();

                return RedirectToAction("Details", "Group", new { id = lessonSubject.GroupId } );
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
