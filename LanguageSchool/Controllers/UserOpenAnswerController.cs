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
        [Route("Mark/{id}")]
        public ActionResult Mark(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = UnitOfWork.UserOpenAnswerRepository.GetById(id);

            if (answer == null)
            {
                return HttpNotFound();
            }

            return View(new UserOpenAnswerViewModel(answer));
        }

        [HttpPost]
        [Route("Mark/{id}")]
        public ActionResult Mark(UserOpenAnswerViewModel answerVM)
        {
            try
            {
                var user = UnitOfWork.UserRepository.GetById(answerVM.UserId);

                var userAnswer = user.UserOpenAnswers.Where(t => t.OpenQuestionId == answerVM.QuestionId).First();

                userAnswer.Points = answerVM.PointsAwarded;
                userAnswer.Comment = answerVM.Comment;
                userAnswer.IsMarked = true;

                var userTest = user.UsersTests.Where(t => t.Id == answerVM.UserTestId).First();

                userTest.Points += answerVM.PointsAwarded;

                double percentageGoten = GradeTest(userTest.Points, userTest.Test.Points);

                userTest.MarkId = Consts.GetGrade(percentageGoten);

                if (!user.UserOpenAnswers.Where(a => a.UserTestId == userTest.Id && !a.IsMarked).Any())
                {
                    userTest.IsMarked = true;
                }

                UnitOfWork.Save();

                return RedirectToAction("Taken", "Test", new { id = answerVM.UserTestId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(answerVM);
            }
        }

        private double GradeTest(int obtainedPoints, int maxPoints)
        {
            return 100 * ((double)obtainedPoints / maxPoints);
        }
    }
}
