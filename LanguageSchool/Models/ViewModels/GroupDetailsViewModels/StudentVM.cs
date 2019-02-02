using System.Linq;
using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels.GroupDetailsViewModels
{
    public class StudentVM
    {
        public int UserId { get; }
        public int UserGroupId { get; }
        public string EnrollmentDate { get; }
        public string Fullname { get; }
        public string EmailAdress { get; }
        public string PublicPhoneNumber { get; }

        public StudentVM(UserGroup userGroup)
        {
            UserGroupId = userGroup.Id;
            UserId = userGroup.UserId;
            EnrollmentDate = userGroup.CreationDate.ToString("yyyy/MM/dd");

            var student = userGroup.User;
            var studentData = student.UserData.First();

            Fullname = studentData.Name + " " + studentData.Surname + " (" + student.Login + ")";
            EmailAdress = studentData.EmailAdress;
            PublicPhoneNumber = studentData.PublicPhoneNumber;
        }
    }
}