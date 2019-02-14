namespace LanguageSchool.Models.ViewModels
{
    public class UserVM
    {
        public int UserId { get; protected set; }
        public string Fullname { get; protected set; }

        public UserVM() { }

        public UserVM(User user)
        {
            UserId = user.Id;

            var userData = user.UserData;
            Fullname = userData.Name + " " + userData.Surname + " (" + user.Login + ")";
        }
    }
}