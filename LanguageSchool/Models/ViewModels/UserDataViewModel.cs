using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class UserDataViewModel
    {
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string HomeNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PublicPhoneNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PrivatePhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        // na potrzeby tworzenia

        public SelectList Roles { get; set; }
        public int RoleId { get; set; }

        public UserDataViewModel()
        {

        }

        public UserDataViewModel(UserData userData)
        {
            //Id = userMessage.Id;
            //SentDate = userMessage.Message.CreationDate.ToString("yyyy-MM-dd");
            //ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            //Topic = userMessage.Message.Header;
            //Contents = userMessage.Message.Contents;
            //HasBeenReceived = userMessage.HasBeenReceived;
            //IsSystem = userMessage.Message.IsSystem;

            //ShortenedContents = (Contents.Length > 100) ? Contents.Substring(0, 100) + "..." : Contents;
        }
    }
}