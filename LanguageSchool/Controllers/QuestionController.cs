﻿using System;
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
        public ActionResult Index(int lessonSubjectId)
        {
            var lessonSubject = unitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            var subjectsQuestionsViewModel = new SubjectsQuestionsViewModel(lessonSubject);

            return View(subjectsQuestionsViewModel);
        }

        [HttpGet]
        [Route("Questions/CreateOpen/{lessonSubjectId}")]
        public ActionResult CreateOpen(int lessonSubjectId)
        {
            var lessonSubject = unitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            var openQuestion = new OpenQuestionViewModel(lessonSubject);

            return View(openQuestion);
        }

        [HttpPost]
        [Route("Questions/CreateOpen/{lessonSubjectId}")]
        public ActionResult CreateOpen(OpenQuestionViewModel openQuestionViewModel)
        {
            try
            {
                var openQuestion = new OpenQuestion();

                openQuestion.LessonSubjectId = openQuestionViewModel.LessonSubjectId;
                openQuestion.Points = openQuestionViewModel.Points;
                openQuestion.Contents = openQuestionViewModel.Contents;
                openQuestion.CreationDate = DateTime.Now;

                unitOfWork.OpenQuestionRepository.Insert(openQuestion);
                unitOfWork.Save();

                return RedirectToAction("Index", openQuestionViewModel.LessonSubjectId);
            }
            catch
            {
                return View(openQuestionViewModel);
            }
        }

        //// POST: Question/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,LessonSubjectId,Contents,NumberOfPossibleAnswers,IsMultichoice,Points")] ClosedQuestion closedQuestion)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ClosedQuestions.Add(closedQuestion);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.LessonSubjectId = new SelectList(db.LessonSubjects, "Id", "Name", closedQuestion.LessonSubjectId);
        //    return View(closedQuestion);
        //}

        //// GET: Question/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClosedQuestion closedQuestion = db.ClosedQuestions.Find(id);
        //    if (closedQuestion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.LessonSubjectId = new SelectList(db.LessonSubjects, "Id", "Name", closedQuestion.LessonSubjectId);
        //    return View(closedQuestion);
        //}

        //// POST: Question/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,LessonSubjectId,Contents,NumberOfPossibleAnswers,IsMultichoice,Points")] ClosedQuestion closedQuestion)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(closedQuestion).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LessonSubjectId = new SelectList(db.LessonSubjects, "Id", "Name", closedQuestion.LessonSubjectId);
        //    return View(closedQuestion);
        //}

        //// GET: Question/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClosedQuestion closedQuestion = db.ClosedQuestions.Find(id);
        //    if (closedQuestion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(closedQuestion);
        //}

        //// POST: Question/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ClosedQuestion closedQuestion = db.ClosedQuestions.Find(id);
        //    db.ClosedQuestions.Remove(closedQuestion);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
