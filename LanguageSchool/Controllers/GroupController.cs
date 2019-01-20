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
    [Authorize]
    public class GroupController : LanguageSchoolController
    {
        [Route("Group/View/{id}")]
        public ActionResult View(int id)
        {
            var loggedUser = GetLoggedUser();

            switch (loggedUser.Role.Id)
            {
                case ((int)Consts.Roles.Teacher): return RedirectToAction("Details", "Group", id);
                case ((int)Consts.Roles.Student): return RedirectToAction("LessonSubjects", "LessonSubject", id);
                default: return View();
            }
        }

        [Route("Group/Details/{id}")]
        public ActionResult Details(int id)
        {
            var group = UnitOfWork.GroupRepository.GetById(id);

            var groupViewModel = new GroupViewModel(group);

            return View(groupViewModel);
        }

        [Route("Group/FullDetails/{id}")]
        public ActionResult FullDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = UnitOfWork.GroupRepository.GetById(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            var groupViewModel = new GroupViewModel(group);

            return View(groupViewModel);
        }

        [HttpGet]
        [Route("Group/AddUsers/{id}")]
        public ActionResult AddUsers(int id)
        {
            var group = UnitOfWork.GroupRepository.GetById(id);

            var usersGroupViewModel = new UsersGroupViewModel(group);

            var students = UnitOfWork.UserRepository.Get(u => !u.IsDeleted && u.RoleId == (int)Consts.Roles.Student);

            foreach(var student in students)
            {
                var studentPotentiallyConflictingGroups = student.UsersGroups.Where(ug => !ug.IsDeleted && (
                    (ug.Group.StartDate <= group.StartDate && group.StartDate <= ug.Group.EndDate) ||
                    (ug.Group.StartDate <= group.EndDate && group.EndDate <= ug.Group.EndDate) ||
                    (group.StartDate <= ug.Group.StartDate && ug.Group.StartDate <= group.EndDate) ||
                    (group.StartDate <= ug.Group.EndDate && ug.Group.EndDate <= group.EndDate)
                ));

                if (studentPotentiallyConflictingGroups == null)
                    usersGroupViewModel.usersAvaible.Add(new UserViewModel(student));
                else
                {
                    var isAvaible = true;

                    foreach (var groupTime in group.GroupTimes.Where(gt => gt.IsActive && !gt.IsDeleted))
                    {
                        if(studentPotentiallyConflictingGroups.Where(ug => ug.Group.GroupTimes.Where(
                            gt => gt.DayOfWeekId == groupTime.DayOfWeekId && (
                                (gt.StartTime <= groupTime.StartTime && groupTime.StartTime <= gt.EndTime) ||
                                (gt.StartTime <= groupTime.EndTime && groupTime.EndTime <= gt.EndTime) ||
                                (groupTime.StartTime <= gt.StartTime && gt.StartTime <= groupTime.EndTime) ||
                                (groupTime.StartTime <= gt.EndTime && gt.EndTime <= groupTime.EndTime)
                            )).Any()
                        ).Any())
                        {
                            isAvaible = false;
                            break;
                        }
                    }

                    if(isAvaible)
                        usersGroupViewModel.usersAvaible.Add(new UserViewModel(student));
                    else
                        usersGroupViewModel.usersNonavaible.Add(new UserViewModel(student)); // todo?
                }
            }

            return View(usersGroupViewModel);
        }

        [HttpPost]
        [Route("Group/AddUsers/{id}")]
        public ActionResult AddUsers(UsersGroupViewModel usersGroupViewModel)
        {
            var group = UnitOfWork.GroupRepository.GetById(usersGroupViewModel.GroupId);

            foreach (var userViewModel in usersGroupViewModel.usersAvaible)
            {
                if (userViewModel.IsMarked)
                {
                    var user = UnitOfWork.UserRepository.GetById(userViewModel.Id);

                    group.UsersGroups.Add(new UsersGroup()
                    {
                        CreationDate = DateTime.Now,
                        User = user
                    });
                }
            }

            UnitOfWork.Save();

            return RedirectToAction("FullDetails", "Group", new { id = usersGroupViewModel.GroupId });
        }

        [HttpGet]
        [Route("Group/Create/{id}")]
        public ActionResult Create(int id)
        {
            return View(new GroupViewModel(UnitOfWork.CourseRepository.GetById(id)));
        }

        [HttpGet]
        [Route("Group/PickHours/{id}")]
        public ActionResult PickHours(GroupViewModel groupViewModel)
        {
            return View(groupViewModel);
        }

        [HttpPost]
        [Route("Group/PickHours/{id}")]
        [Route("Group/Create/{id}")]
        public ActionResult Create(GroupViewModel groupViewModel)
        {
            var selectedTeacher = UnitOfWork.UserRepository.GetById(groupViewModel.UserId);

            if (groupViewModel.TeacherTimetable == null)
            {
                groupViewModel.FillTimetable(selectedTeacher);

                return View("PickHours", groupViewModel);
            }
            else
            {
                var now = DateTime.Now;

                var group = new Group {
                    CourseId = groupViewModel.CourseId,
                    Name = groupViewModel.Name,
                    StartDate = groupViewModel.StartDate,
                    EndDate = groupViewModel.EndDate,
                    CreationDate = now,
                    IsActive = true
                };

                group.UsersGroups.Add(new UsersGroup
                {
                    CreationDate = now,
                    UserId = groupViewModel.Teacher.Id
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
                        else if (groupTime != null)
                        {
                            groupTimes.Add(groupTime);

                            groupTime = null;
                        }
                    }
                }

                if (groupTime != null)
                {
                    groupTimes.Add(groupTime);
                }

                group.GroupTimes = groupTimes;

                UnitOfWork.GroupRepository.Insert(group);

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Grupa została utworzona pomyślnie", "proszę zapisać do niej studentów");

                return RedirectToAction("FullDetails", "Group", new { id = group.Id });
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
