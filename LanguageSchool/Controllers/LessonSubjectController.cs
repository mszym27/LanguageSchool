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
        public ActionResult LessonSubjects(int id)
        {
            var group = UnitOfWork.GroupRepository.GetById(id);

            var student = GetLoggedUser();

            var groupViewModel = new GroupStudentVM(group, student);

            return View(groupViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int id)
        {
            var lessonSubjectViewModel = new LessonSubjectViewModel() { GroupId = id };

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
                    CreationDate = DateTime.Now
                };

                UnitOfWork.LessonSubjectRepository.Insert(lessonSubject);

                UnitOfWork.Save();

                return RedirectToAction("Details", "Group", new { id = lessonSubjectViewModel.GroupId });
            }
            catch
            {
                return View(lessonSubjectViewModel);
            }
        }

        [Route("LessonSubject/CreateFromExisting/{groupId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateFromExisting(int groupId)
        {
            var group = UnitOfWork.GroupRepository.GetById(groupId);

            var lessonSubjects = new ExistingLessonSubjectsVM(group.Course, groupId);

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
                    IsActive = false,
                    CreationDate = DateTime.Now
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
                        CreationDate = DateTime.Now
                    };

                    foreach (var existingAnswer in existingQuestion.Answers.Where(a => !a.IsDeleted))
                    {
                        var copiedAnswer = new Answer()
                        {
                            AnswerContent = existingAnswer.AnswerContent,
                            IsCorrect = existingAnswer.IsCorrect,
                            CreationDate = DateTime.Now
                        };

                        copiedQuestion.Answers.Add(copiedAnswer);
                    }

                    lessonSubject.ClosedQuestions.Add(copiedQuestion);
                }

                lessonSubject.OpenQuestions = new List<OpenQuestion>();

                foreach (var existingQuestion in existingSubject.OpenQuestions)
                {
                    var copiedQuestion = new OpenQuestion()
                    {
                        Contents = existingQuestion.Contents,
                        Points = existingQuestion.Points,
                        CreationDate = DateTime.Now
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
                        File = existingMaterial.File,
                        CreationDate = DateTime.Now
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
        public ActionResult DeActivate(int id)
        {
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
            catch
            {
                TempData["Alert"] = new AlertViewModel(Consts.Error, "Nastąpił nieoczekiwany problem", "operacja nie powiodła się.");

                return RedirectToAction("Details", new { id = id });
            }
        }

        [Route("LessonSubject/Delete/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int id)
        {
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
                }

                foreach (var question in lessonSubject.OpenQuestions)
                {
                    question.IsDeleted = true;
                    question.DeletionDate = now;
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
