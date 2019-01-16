﻿using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class GroupViewModel
    {
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public string CourseNumberOfHours { get; set; }
        public string CourseName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SelectList Users { get; set; }
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public UserViewModel Teacher { get; set; }

        public List<List<bool?>> TeacherTimetable { get; set; }
        public List<List<GroupTimeViewModel>> TeacherExistingTimetable { get; set; }

        public List<UserViewModel> Students { get; set; }
        public List<LessonSubject> LessonSubjects { get; set; }
        public List<GroupTime> Hours { get; set; }

        public List<Test> Tests { get; set; }

        public GroupViewModel()
        {
        }

        public GroupViewModel(Group group)
        {
            GroupId = group.Id;
            Name = group.Name;
            StartDate = group.StartDate;
            EndDate = group.EndDate;

            var assignedUsers = group.UsersGroups.Where(ug => !ug.IsDeleted);

            Teacher = new UserViewModel(assignedUsers.Where(u => u.User.RoleId == (int)Consts.Roles.Teacher).FirstOrDefault().User);

            Students = new List<UserViewModel>();

            foreach (var userGroup in assignedUsers.Where(u => u.User.RoleId == (int)Consts.Roles.Student))
                Students.Add(new UserViewModel(userGroup.User));

            Hours = new List<GroupTime>();

            foreach (var GroupTime in group.GroupTimes)
                Hours.Add(GroupTime);

            if(group.LessonSubjects != null)
                LessonSubjects = group.LessonSubjects.Where(ls => !ls.IsDeleted).OrderByDescending(ls => ls.CreationDate).ToList();

            Tests = group.Tests.Where(t => !t.IsDeleted).ToList();
        }

        public GroupViewModel(Course course)
        {
            CourseId = course.Id;
            CourseName = course.Name;
            CourseNumberOfHours = course.NumberOfHours;
            StartDate = course.StartDate;
            EndDate = course.EndDate;

            Users = PopulateList.AllUsersInRole((int)Consts.Roles.Teacher);
        }

        public void FillTimetable(User user)
        {
            Teacher = new UserViewModel(user);

            TeacherTimetable = new List<List<bool?>>();
            TeacherExistingTimetable = new List<List<GroupTimeViewModel>>();

            var userGroups = user.UsersGroups.Where(ug => !ug.IsDeleted && (
                (ug.Group.StartDate <= StartDate && StartDate <= ug.Group.EndDate) ||
                (ug.Group.StartDate <= EndDate && EndDate <= ug.Group.EndDate) ||
                (StartDate <= ug.Group.StartDate && ug.Group.StartDate <= EndDate) ||
                (StartDate <= ug.Group.EndDate && ug.Group.EndDate <= EndDate)
            )).ToList();

            List<GroupTime> groupTimes = new List<GroupTime>();

            foreach (var userGroup in userGroups)
            {
                foreach (var groupTime in userGroup.Group.GroupTimes)
                {
                    if (!groupTime.IsDeleted && groupTime.IsActive)
                        groupTimes.Add(groupTime);
                }
            }

            for (int i = 0; i < 12; i++) // hours
            {
                TeacherTimetable.Add(new List<bool?>(7));
                TeacherExistingTimetable.Add(new List<GroupTimeViewModel>(7));

                for (int j = 0; j < 7; j++) // days
                {
                    var existingTime = groupTimes.Where(gt => gt.DayOfWeekId == (j + 1) && gt.EndTime >= (i + 8) && gt.StartTime <= (i + 8)).FirstOrDefault();

                    if (existingTime != null)
                    {
                        TeacherTimetable[i].Add(null);
                        var group = existingTime.Group;
                        TeacherExistingTimetable[i].Add(new GroupTimeViewModel(group));
                    }
                    else
                    {
                        TeacherTimetable[i].Add(false);
                        TeacherExistingTimetable[i].Add(null);
                    }
                }
            }
        }
    }
}