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
    [Authorize(Roles = "Teacher")]
    public class UserOpenAnswerController : LanguageSchoolController
    {
        [Route("UserOpenAnswers/{userId}")]
        public ActionResult Index(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UnitOfWork.UserRepository.GetById(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user.UserOpenAnswers.Where(a => !a.IsMarked));
        }

        [HttpGet]
        [Route("Grade/{id}")]
        public ActionResult Grade(int id)
        {
            var answer = UnitOfWork.UserOpenAnswerRepository.GetById(id);

            if (answer == null)
            {
                return HttpNotFound();
            }

            return View(new UserOpenAnswerViewModel(answer));
        }

        [HttpPost]
        [Route("Grade/{id}")]
        public ActionResult Grade(UserOpenAnswerViewModel answerVM)
        {
            try
            {
                var user = UnitOfWork.UserRepository.GetById(answerVM.UserId);

                var userAnswer = user.UserOpenAnswers.Where(t => t.OpenQuestionId == answerVM.QuestionId).First();

                userAnswer.Points = answerVM.PointsAwarded;
                userAnswer.Comment = answerVM.Comment;

                var test = user.UsersTests.Where(t => t.TestId == answerVM.TestId).First();

                test.Points += answerVM.PointsAwarded;

                if(!user.UserOpenAnswers.Where(a => a.TestId == test.Id && !a.IsMarked).Any())
                {
                    test.IsMarked = true;
                }

                UnitOfWork.Save();

                return RedirectToAction("Taken", "Test", new { id = answerVM.TestId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(answerVM);
            }
        }

        //// GET: User/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
        //    return View();
        //}

        //// POST: User/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,Login,Password,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// GET: User/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// POST: User/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,Login,Password,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// GET: User/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
