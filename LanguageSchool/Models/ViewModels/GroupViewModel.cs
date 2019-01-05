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
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SelectList Users { get; set; }
        public int UserId { get; set; }

        public UserViewModel SelectedUser { get; set; }

        public List<List<bool?>> TeacherTimetable { get; set; }

        public GroupViewModel()
        {
        }

        public GroupViewModel(int CourseId)
        {
            this.CourseId = CourseId;

            Users = PopulateList.GetAllTeachers();
        }

        public void FillTimetable(User user)
        {
            SelectedUser = new UserViewModel(user);

            TeacherTimetable = new List<List<bool?>>();

            var userGroups = user.UsersGroups.Where(ug => !ug.IsDeleted).ToList();

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

                for (int j = 0; j < 7; j++) // days
                {
                    if (
                        groupTimes.Where(gt => gt.DayOfWeekId == (j + 1) && gt.EndTime >= (i + 8) && gt.StartTime <= (i + 8)).Any()
                    )
                        TeacherTimetable[i].Add(null);
                    else
                        TeacherTimetable[i].Add(false);
                }
            }
        }
    }
}