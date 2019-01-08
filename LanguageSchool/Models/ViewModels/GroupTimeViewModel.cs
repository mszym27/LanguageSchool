using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class GroupTimeViewModel
    {
        public int GroupId { get; set; }
        public string CourseName { get; set; }
        public string GroupName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string ConflictingDateFullName { get; set; }
        public bool IsBlocked { get; set; }
        public int DayOfWeekId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public GroupTimeViewModel(GroupTime groupTime)
        {
            DayOfWeekId = groupTime.DayOfWeekId;
            StartTime = groupTime.StartTime;
            EndTime = groupTime.EndTime;
        }

        public GroupTimeViewModel(Group group)
        {
            GroupId = group.Id;

            GroupName = group.Name;
            CourseName = group.Course.Name;
            StartDate = group.StartDate.ToString("yyyy/MM/dd");
            EndDate = group.EndDate.ToString("yyyy/MM/dd");
        }
    }
}