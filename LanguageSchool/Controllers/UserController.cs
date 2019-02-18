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
    public class UserController : LanguageSchoolController
    {
        [Authorize(Roles = "Teacher,Student")]
        public ActionResult TimeTable()
        {
            try
            {
                var loggedUser = GetLoggedUser();

                var model = new TimeTableViewModel(loggedUser);

                return View(model);
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Secretary")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = UnitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Secretary")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = UnitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            try
            {
                user.IsDeleted = true;

                var userData = user.UserData;

                userData.IsDeleted = true;

                foreach(var userGroup in user.UsersGroups.Where(ug => !ug.IsDeleted))
                {
                    userGroup.IsDeleted = true;
                }

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Operacja przebiegła pomyślnie", "użytkownik został usunięty. Nie będzie miał on już dostępu do systemu.");

                return RedirectToAction("Index", "Home");
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
