using System.Linq;
using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public List<UsersGroup> UserGroups { get; set; }
        public string Fullname { get; set; }
        public string EmailAdress { get; set; }
        public string PublicPhoneNumber { get; set; }
        public bool IsMarked { get; set; }
        public List<UsersTests> UserTests { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            CreationDate = user.CreationDate.ToString("yyyy/MM/dd");
            UserGroups = user.UsersGroups.ToList();
            UserTests = user.UsersTests.ToList();

            var userData = user.UserData.OfType<UserData>().FirstOrDefault();
            Fullname = userData.Name + " " + userData.Surname + " (" + user.Login + ")";
            EmailAdress = userData.EmailAdress;
            PublicPhoneNumber = userData.PublicPhoneNumber;
        }

        //public UserViewModel(User user)
        //{
        //    Id = user.Id;
        //    CreationDate = user.CreationDate.ToString("yyyy/MM/dd");

        //    var userData = user.UserData.OfType<UserData>().FirstOrDefault();
        //    Fullname = userData.Name + " " + userData.Surname + " (" + user.Login + ")";
        //}
    }
}