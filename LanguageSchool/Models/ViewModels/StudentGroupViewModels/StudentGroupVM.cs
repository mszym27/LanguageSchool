using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Models.ViewModels.StudentGroupViewModels
{
    public class StudentGroupVM
    {
        public string StudentName { get; }

        public List<UserTestViewModel> TakenTests { get; }
        public List<StudentAwaitingTestVM> AwaitingMark { get; }

        public StudentGroupVM(UsersGroup userGroup)
        {
            var group = userGroup.Group;
            var student = userGroup.User;

            var studentData = userGroup.User.UserData.First();

            StudentName = studentData.Name + " " + studentData.Surname;

            var studentTests = student.UsersTests.Where(t => t.GroupId == group.Id).ToList();

            TakenTests = new List<UserTestViewModel>();

            foreach (var test in studentTests.Where(t => t.IsMarked).OrderByDescending(t => t.CreationDate))
            {
                TakenTests.Add(new UserTestViewModel(test));
            }

            AwaitingMark = new List<StudentAwaitingTestVM>();

            foreach (var test in studentTests.Where(t => !t.IsMarked).OrderByDescending(t => t.CreationDate))
            {
                AwaitingMark.Add(new StudentAwaitingTestVM(test));
            }
        }
    }
}