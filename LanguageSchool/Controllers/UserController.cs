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
        public ActionResult TimeTable()
        {
            var loggedUser = GetLoggedUser();

            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)System.DayOfWeek.Monday);
            var endOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)System.DayOfWeek.Saturday);

            var thisWeekUserGroups = loggedUser.UsersGroups.Where(ug => !ug.IsDeleted && !(ug.Group.EndDate < startOfWeek) && !(ug.Group.StartDate > endOfWeek ));

            var model = new List<GroupTime>();

            foreach (var userGroup in thisWeekUserGroups)
                model.AddRange(userGroup.Group.GroupTimes.Where(gt => gt.IsActive && !gt.IsDeleted));

            return View(model);
        }

        //private void FillTimetable(User user)
        //{
        //    Teacher = new UserViewModel(user);

        //    TeacherTimetable = new List<List<bool?>>();
        //    TeacherExistingTimetable = new List<List<GroupTimeViewModel>>();

        //    var userGroups = user.UsersGroups.Where(ug => !ug.IsDeleted && (
        //        (ug.Group.StartDate <= StartDate && StartDate <= ug.Group.EndDate) ||
        //        (ug.Group.StartDate <= EndDate && EndDate <= ug.Group.EndDate) ||
        //        (StartDate <= ug.Group.StartDate && ug.Group.StartDate <= EndDate) ||
        //        (StartDate <= ug.Group.EndDate && ug.Group.EndDate <= EndDate)
        //    )).ToList();

        //    List<GroupTime> groupTimes = new List<GroupTime>();

        //    foreach (var userGroup in userGroups)
        //    {
        //        foreach (var groupTime in userGroup.Group.GroupTimes)
        //        {
        //            if (!groupTime.IsDeleted && groupTime.IsActive)
        //                groupTimes.Add(groupTime);
        //        }
        //    }

        //    for (int i = 0; i < 12; i++) // hours
        //    {
        //        TeacherTimetable.Add(new List<bool?>(7));
        //        TeacherExistingTimetable.Add(new List<GroupTimeViewModel>(7));

        //        for (int j = 0; j < 7; j++) // days
        //        {
        //            var existingTime = groupTimes.Where(gt => gt.DayOfWeekId == (j + 1) && (gt.EndTime >= (i + 8) || gt.StartTime <= (i + 8))).FirstOrDefault();

        //            if (existingTime != null)
        //            {
        //                TeacherTimetable[i].Add(null);
        //                var group = existingTime.Group;
        //                TeacherExistingTimetable[i].Add(new GroupTimeViewModel(group));
        //            }
        //            else
        //            {
        //                TeacherTimetable[i].Add(false);
        //                TeacherExistingTimetable[i].Add(null);
        //            }
        //        }
        //    }
        //}

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = unitOfWork.UserRepository.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
