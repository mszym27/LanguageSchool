using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.GroupViewModels
{
    public class SecretaryGroupDetailsVM
    {
        public int GroupId { get; }
        public string Name { get; }
        public string StartDate { get; }
        public string EndDate { get; }

        public UserDataVM Teacher { get; }

        public List<StudentVM> Students { get; }
        public List<GroupTime> Hours { get; }

        public SecretaryGroupDetailsVM(Group group)
        {
            GroupId = group.Id;
            Name = group.Name;
            StartDate = group.StartDate.ToString("yyyy-MM-dd");
            EndDate = group.EndDate.ToString("yyyy-MM-dd");

            Teacher = group.UsersGroups
                .Where(ug => !ug.IsDeleted)
                .Where(ug => ug.User.RoleId == (int)Consts.Roles.Teacher)
                .Select(ug => new UserDataVM(ug.User))
                .First();
            
            Students = group.UsersGroups
                .Where(ug => !ug.IsDeleted)
                .Where(ug => ug.User.RoleId == (int)Consts.Roles.Student)
                .Select(ug => new StudentVM(ug))
                .ToList();

            Hours = group.GroupTimes
                .Where(gt => !gt.IsDeleted)
                .ToList();
        }
    }
}