using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class GroupStudentVM
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Name { get; set; }

        public UserViewModel Teacher { get; set; }

        public List<LessonSubject> LessonSubjects { get; set; }

        public List<Test> Tests { get; set; }
        public List<UserTestViewModel> TakenTests { get; set; }

        public GroupStudentVM(Group group, User student)
        {
            CourseId = group.CourseId;
            Name = group.Name;

            var assignedUsers = group.UsersGroups.Where(ug => !ug.IsDeleted);

            Teacher = new UserViewModel(assignedUsers.Where(u => u.User.RoleId == (int)Consts.Roles.Teacher).FirstOrDefault().User);

            if(group.LessonSubjects != null)
            {
                LessonSubjects = group.LessonSubjects
                    .Where(ls => !ls.IsDeleted && ls.IsActive)
                    .OrderByDescending(ls => ls.CreationDate)
                    .ToList();
            }

            TakenTests = student.UsersTests
                .Where(ut => !ut.IsDeleted)
                .Where(ut => ut.Test.GroupId == group.Id)
                .OrderByDescending(ut => ut.CreationDate)
                .Select(ut => new UserTestViewModel(ut))
                .ToList();

            Tests = group.Tests
                .Where(t => !t.IsDeleted)
                .Where(t => t.IsActive)
                .Where(t => !student.UsersTests.Where(ut => ut.TestId == t.Id).Any())
                .OrderByDescending(t => t.CreationDate)
                .ToList();
        }
    }
}