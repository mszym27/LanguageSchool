using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class TimeTableViewModel
    {
        public DateTime StartOfWeek { get; set; }
        public DateTime EndOfWeek { get; set; }
        public bool IsTeacher { get; set; }
  
        public List<GroupTime> Times { get; set; }

        public TimeTableViewModel(User user)
        {
            IsTeacher = (user.RoleId == (int)Consts.Roles.Teacher);

            StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)System.DayOfWeek.Monday);
            EndOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)System.DayOfWeek.Saturday);

            var thisWeekUserGroups = user.UsersGroups.Where(ug => !ug.IsDeleted && !(ug.Group.EndDate < StartOfWeek) && !(ug.Group.StartDate > EndOfWeek));

            Times = new List<GroupTime>();

            foreach (var userGroup in thisWeekUserGroups)
                Times.AddRange(userGroup.Group.GroupTimes.Where(gt => !gt.IsDeleted));
        }
    }
}