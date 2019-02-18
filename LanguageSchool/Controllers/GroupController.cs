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
using LanguageSchool.Models.ViewModels.GroupViewModels;
using LanguageSchool.Models.ViewModels.StudentGroupViewModels;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class GroupController : LanguageSchoolController
    {
        [Route("Group/View/{id}")]
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loggedUser = GetLoggedUser();

            switch (loggedUser.Role.Id)
            {
                case ((int)Consts.Roles.Teacher): return RedirectToAction("Details", "Group", id);
                case ((int)Consts.Roles.Student): return RedirectToAction("LessonSubjects", "LessonSubject", id);
                default: return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Route("Group/Details/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Details(int? id)
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

            var groupVM = new TeacherGroupDetailsVM(group);

            return View(groupVM);
        }

        [Route("Group/FullDetails/{id}")]
        [Authorize(Roles = "Secretary")]
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

            var groupViewModel = new SecretaryGroupDetailsVM(group);

            return View(groupViewModel);
        }

        [HttpGet]
        [Route("Group/AddUsers/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult AddUsers(int? id)
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

                    foreach (var groupTime in group.GroupTimes.Where(gt => !gt.IsDeleted))
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
        [Authorize(Roles = "Secretary")]
        public ActionResult AddUsers(UsersGroupViewModel usersGroupViewModel)
        {
            try
            {
                var group = UnitOfWork.GroupRepository.GetById(usersGroupViewModel.GroupId);

                foreach (var userViewModel in usersGroupViewModel.usersAvaible)
                {
                    if (userViewModel.IsMarked)
                    {
                        var user = UnitOfWork.UserRepository.GetById(userViewModel.Id);

                        group.UsersGroups.Add(new UserGroup()
                        {
                            User = user
                        });
                    }
                }

                UnitOfWork.GroupRepository.Update(group);
                UnitOfWork.Save();

                return RedirectToAction("FullDetails", "Group", new { id = usersGroupViewModel.GroupId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("FullDetails", "Group", new { id = usersGroupViewModel.GroupId });
            }
        }

        [HttpGet]
        [Route("Group/Create/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            var groupVM = new GroupViewModel(course);

            PopulateInputLists(ref groupVM);

            TempData["Alert"] = new AlertViewModel(Consts.Info, "Wprowadź dane nowej grupy", "wybierz prowadzącego oraz daty w których odbywać się będą zajęcia");

            return View(groupVM);
        }

        [HttpGet]
        [Route("Group/PickHours/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult PickHours(GroupViewModel groupViewModel)
        {
            return View(groupViewModel);
        }

        [HttpPost]
        [Route("Group/PickHours/{id}")]
        [Route("Group/Create/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create(GroupViewModel groupViewModel)
        {
            PopulateInputLists(ref groupViewModel);

            if (!ModelState.IsValid)
            {
                return View(groupViewModel);
            }

            var selectedTeacher = UnitOfWork.UserRepository.GetById(groupViewModel.TeacherId);

            if (groupViewModel.TeacherTimetable == null)
            {
                groupViewModel.FillTimetable(selectedTeacher);

                TempData["Alert"] = new AlertViewModel(Consts.Info, "Wybierz godziny zajęć", "są one ograniczone do dostępnych dla wybranego przez Ciebie prowadzącego w tym przedziale dat");

                return View("PickHours", groupViewModel);
            }
            else
            {
                try
                {
                    var group = new Group
                    {
                        CourseId = groupViewModel.CourseId,
                        Name = groupViewModel.Name,
                        StartDate = groupViewModel.StartDate,
                        EndDate = groupViewModel.EndDate,
                        IsActive = true
                    };

                    group.UsersGroups.Add(new UserGroup
                    {
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

                            if (isHourChecked == true)
                            {
                                if (groupTime == null)
                                {
                                    groupTime = new GroupTime
                                    {
                                        DayOfWeekId = 5000 + (i + 1),
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
                catch (Exception ex)
                {
                    var errorLogGuid = LogException(ex);

                    TempData["Alert"] = new AlertViewModel(errorLogGuid);

                    return View("PickHours", groupViewModel);
                }
            }
        }

        [HttpGet]
        [Route("Group/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(int? id)
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

            var groupVM = new GroupViewModel(group);

            PopulateInputLists(ref groupVM);

            return View(groupVM);
        }

        [HttpPost]
        [Route("Group/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(GroupViewModel groupVM)
        {
            PopulateInputLists(ref groupVM);

            if (!ModelState.IsValid)
            {
                return View(groupVM);
            }

            try
            {
                var group = UnitOfWork.GroupRepository.GetById(groupVM.GroupId);

                group.Name = groupVM.Name;
                group.StartDate = groupVM.StartDate;
                group.EndDate = groupVM.EndDate;

                UnitOfWork.GroupRepository.Update(group);

                UnitOfWork.Save();

                return RedirectToAction("FullDetails", new { id = group.Id });
            }
            catch(Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(groupVM);
            }
        }

        [Route("Group/StudentDetails/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult StudentDetails(int? userGroupId)
        {
            if (userGroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userGroup = UnitOfWork.UserGroupRepository.GetById(userGroupId);

            if (userGroup == null)
            {
                return HttpNotFound();
            }

            var groupViewModel = new StudentGroupVM(userGroup);

            return View(groupViewModel);
        }

        [Route("Group/Delete/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var now = DateTime.Now;

                var group = UnitOfWork.GroupRepository.GetById(id);

                group.IsDeleted = true;
                group.DeletionDate = now;

                foreach (var student in group.UsersGroups)
                {
                    student.IsDeleted = true;
                    student.DeletionDate = now;
                }

                foreach (var subject in group.LessonSubjects)
                {
                    subject.IsDeleted = true;
                    subject.DeletionDate = now;

                    foreach (var material in subject.Materials)
                    {
                        material.IsDeleted = true;
                        material.DeletionDate = now;
                    }

                    foreach (var question in subject.ClosedQuestions)
                    {
                        question.IsDeleted = true;
                        question.DeletionDate = now;
                    }

                    foreach (var question in subject.OpenQuestions)
                    {
                        question.IsDeleted = true;
                        question.DeletionDate = now;
                    }
                }

                UnitOfWork.GroupRepository.Update(group);
                UnitOfWork.Save();

                return RedirectToAction("FullDetails", "Course", new { id = group.CourseId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        private void PopulateInputLists(ref GroupViewModel groupVM)
        {
            var teachers = UnitOfWork.UserRepository
                .Get(u => !u.IsDeleted && u.RoleId == (int)Consts.Roles.Teacher)
                .Select(u => new UserDataVM(u))
                .ToList();

            groupVM.Teachers = new SelectList(teachers, "UserId", "Fullname");
        }

        [Route("Group/DeleteStudent/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var now = DateTime.Now;

                var userGroup = UnitOfWork.UserGroupRepository.GetById(id);

                userGroup.IsDeleted = true;
                userGroup.DeletionDate = now;

                UnitOfWork.Save();

                return RedirectToAction("FullDetails", new { id = userGroup.GroupId });
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
