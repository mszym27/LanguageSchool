using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        public List<bool> Monday { get; set; }
        public List<bool> Tuesday { get; set; }
        public List<bool> Wednesday { get; set; }
        public List<bool> Thursday { get; set; }
        public List<bool> Friday { get; set; }
        public List<bool> Saturday { get; set; }
        public List<bool> Sunday { get; set; }

        public bool testcheck { get; set; }

        public UserDataViewModel()
        {
            Monday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Monday.Add(false);
            Tuesday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Tuesday.Add(false);
            Wednesday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Wednesday.Add(false);
            Thursday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Thursday.Add(false);
            Friday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Friday.Add(false);
            Saturday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Saturday.Add(false);
            Sunday = new List<bool>(12);
            for (int i = 0; i < 12; i++) Sunday.Add(false);

            //Monday[0] = true;
            //Tuesday[1] = true;
            //Wednesday[2] = true;
            //Thursday[3] = true;
            //Friday[4] = true;
            //Saturday[5] = true;
            //Sunday[6] = true;
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