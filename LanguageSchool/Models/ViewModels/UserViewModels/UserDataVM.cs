using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels
{
    public class UserDataVM
    {
        public int UserId { get; }
        public string FullName { get; }

        public UserDataVM(User user)
        {
            UserId = user.Id;

            FullName = user.UserData.Name + " " + user.UserData.Surname + " (" + user.Login + ")";
        }
    }
}