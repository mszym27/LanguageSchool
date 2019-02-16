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
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
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