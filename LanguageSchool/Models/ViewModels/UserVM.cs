namespace LanguageSchool.Models.ViewModels
{
    public class UserVM
    {
        public int UserId { get; }
        public string Fullname { get; }

        public UserVM() { }

        public UserVM(User user)
        {
            UserId = user.Id;

            var userData = user.UserData;
            Fullname = userData.Name + " " + userData.Surname + " (" + user.Login + ")";
        }
    }
}