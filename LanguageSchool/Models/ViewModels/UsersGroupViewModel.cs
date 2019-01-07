using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class UsersGroupViewModel
    {
        public int GroupId { get; set; }

        public List<UserViewModel> usersAvaible { get; set; }
        public List<UserViewModel> usersNonavaible { get; set; }

        public UsersGroupViewModel() { }

        public UsersGroupViewModel(Group group)
        {
            GroupId = group.Id;

            usersAvaible = new List<UserViewModel>();
            usersNonavaible = new List<UserViewModel>();
        }
    }
}