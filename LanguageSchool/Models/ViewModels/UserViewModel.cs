using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public string Fullname { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            CreationDate = user.CreationDate.ToString("yyyy/MM/dd");

            var userData = user.UserData.OfType<UserData>().FirstOrDefault();
            Fullname = userData.Name + " " + userData.Surname + " (" + user.Login + ")";
        }
    }
}