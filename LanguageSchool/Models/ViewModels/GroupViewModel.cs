using System;
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
        public string CourseNumberOfHours { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SelectList Users { get; set; }
        public int UserId { get; set; }

        public UserViewModel Teacher { get; set; }

        public List<List<bool?>> TeacherTimetable { get; set; }
        public List<List<GroupTimeViewModel>> TeacherExistingTimetable { get; set; }

        public List<UserViewModel> Students { get; set; }

        public List<GroupTime> Hours { get; set; }

        public GroupViewModel()
        {
        }

        public GroupViewModel(Group group)
        {
            Name = group.Name;

            var assignedUsers = group.UsersGroups.Where(ug => !ug.IsDeleted);

            Teacher = new UserViewModel(assignedUsers.Where(u => u.User.RoleId == (int)Consts.Roles.Teacher).FirstOrDefault().User);

            Students = new List<UserViewModel>();

            foreach (var userGroup in assignedUsers.Where(u => u.User.RoleId == (int)Consts.Roles.Student))
                Students.Add(new UserViewModel(userGroup.User));

            Hours = new List<GroupTime>();

            foreach (var GroupTime in group.GroupTimes)
                Hours.Add(GroupTime);
        }

        public GroupViewModel(Course course)
        {
            CourseId = course.Id;
            CourseNumberOfHours = course.NumberOfHours;

            Users = PopulateList.GetAllTeachers();
        }

        public void FillTimetable(User user, DateTime startingDate)
        {
            Teacher = new UserViewModel(user);

            TeacherTimetable = new List<List<bool?>>();
            TeacherExistingTimetable = new List<List<GroupTimeViewModel>>();

            var userGroups = user.UsersGroups.Where(ug => !ug.IsDeleted && ug.Group.Course.EndDate > startingDate).ToList();

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
                        var GroupTimeViewModel = existingTime.Group;
                        TeacherExistingTimetable[i].Add(new GroupTimeViewModel(GroupTimeViewModel));
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