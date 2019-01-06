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
    public class GroupController : LanguageSchoolController
    {
        //// GET: Group/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(group);
        //}

        [HttpGet]
        [Route("Group/Create/{id}")]
        public ActionResult Create(int id)
        {
            return View(new GroupViewModel(unitOfWork.CourseRepository.GetById(id)));
        }

        [HttpGet]
        public ActionResult PickHours(GroupViewModel groupViewModel)
        {
            return View(groupViewModel);
        }

        [HttpPost]
        [Route("Group/Create/{id}")]
        public ActionResult Create(GroupViewModel groupViewModel)
        {
            var selectedTeacher = unitOfWork.UserRepository.GetById(groupViewModel.UserId);

            if (groupViewModel.TeacherTimetable == null)
            {
                var startDate = unitOfWork.CourseRepository.GetById(groupViewModel.CourseId).StartDate;

                groupViewModel.FillTimetable(selectedTeacher, startDate);

                return View("PickHours", groupViewModel);
            }
            else
            {
                var now = DateTime.Now;

                var group = new Group {
                    CourseId = groupViewModel.CourseId,
                    Name = groupViewModel.Name,
                    CreationDate = now,
                    IsActive = true
                };

                group.UsersGroups.Add(new UsersGroup
                {
                    CreationDate = now,
                    UserId = groupViewModel.SelectedUser.Id
                });

                var groupTimes = new List<GroupTime>();

                GroupTime groupTime = null;

                for (int i = 0; i < 7; i++) // days
                {
                    if (groupTime != null)
                    {
                        groupTimes.Add(groupTime);
                    }

                    groupTime = null;

                    for (int j = 0; j < 12; j++) // hours
                    {
                        var isHourChecked = groupViewModel.TeacherTimetable.ElementAt(j).ElementAt(i);

                        if(isHourChecked == true)
                        {
                            if(groupTime == null)
                            {
                                groupTime = new GroupTime
                                {
                                    IsActive = true,
                                    CreationDate = now,
                                    DayOfWeekId = (i + 1),
                                    StartTime = j + 8,
                                    EndTime = j + 8,
                                };
                            }
                            else
                            {
                                if (groupTime.EndTime == (j + 8) - 1)
                                    groupTime.EndTime++;
                                else
                                {
                                    groupTimes.Add(groupTime);

                                    groupTime = null;
                                }
                            }
                        }
                    }
                }

                if (groupTime != null)
                {
                    groupTimes.Add(groupTime);
                }

                group.GroupTimes = groupTimes;

                unitOfWork.GroupRepository.Insert(group);

                unitOfWork.Save();

                return RedirectToAction("Index", "Home");
            }
        }

        //// POST: Group/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,CourseId,Name,IsActive")] Group group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Groups.Add(group);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", group.CourseId);
        //    return View(group);
        //}

        //// GET: Group/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", group.CourseId);
        //    return View(group);
        //}

        //// POST: Group/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,CourseId,Name,IsActive")] Group group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(group).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", group.CourseId);
        //    return View(group);
        //}

        //// GET: Group/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(group);
        //}

        //// POST: Group/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Group group = db.Groups.Find(id);
        //    db.Groups.Remove(group);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
