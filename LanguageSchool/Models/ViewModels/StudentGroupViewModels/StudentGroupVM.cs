using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Models.ViewModels.StudentGroupViewModels
{
    public class StudentGroupVM
    {
        public int StudentId { get; }
        public string StudentName { get; }
        public string EnrollmentDate { get; }
        public string EmailAdress { get; }
        public string PhoneNumber { get; }

        public List<UserTestViewModel> TakenTests { get; }
        public List<StudentAwaitingTestVM> AwaitingMark { get; }

        public StudentGroupVM(UserGroup userGroup)
        {
            var group = userGroup.Group;
            var student = userGroup.User;

            EnrollmentDate = userGroup.CreationDate.ToString("yyyy/MM/dd");

            var studentData = userGroup.User.UserData.First();

            StudentId = student.Id;
            StudentName = studentData.Name + " " + studentData.Surname;
            EmailAdress = studentData.EmailAdress;
            PhoneNumber = studentData.PublicPhoneNumber;

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