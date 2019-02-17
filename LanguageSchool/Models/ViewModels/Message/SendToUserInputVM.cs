using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.MessageViewModels
{
    public class SendToUserInputVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Uzupełnij temat wiadomości")]
        [StringLength(50, ErrorMessage = "Długość pola jest ograniczona do 50 znaków")]
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Wprowadź treść wiadomości")]
        [StringLength(4000, ErrorMessage = "Długość pola jest ograniczona do 4000 znaków")]
        public string Contents { get; set; }
        public bool IsSystem { get; set; }

        public SendToUserInputVM() { }

        public SendToUserInputVM(User user)
        {
            UserId = user.Id;

            var userData = user.UserData;

            UserName = userData.Name + " " + userData.Surname;
        }
    }
}