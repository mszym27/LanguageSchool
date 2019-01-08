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
        public int UserId { get; set; }

        public string Fullname { get; set; }

        public bool IsAvaible { get; set; }
        public bool IsMarked { get; set; }

        public List<UserViewModel> usersAvaible { get; set; }
        public List<UserViewModel> usersNonavaible { get; set; }
        public List<UsersGroupViewModel> GroupsAvaible { get; set; }
        public List<UsersGroupViewModel> GroupsNonavaible { get; set; }

        public List<GroupTimeViewModel> Hours { get; set; }

        public UsersGroupViewModel() { }

        public UsersGroupViewModel(User user)
        {
            UserId = user.Id;

            GroupsAvaible = new List<UsersGroupViewModel>();
            GroupsNonavaible = new List<UsersGroupViewModel>();
        }

        public UsersGroupViewModel(Group group)
        {
            GroupId = group.Id;

            usersAvaible = new List<UserViewModel>();
            usersNonavaible = new List<UserViewModel>();
            Hours = new List<ViewModels.GroupTimeViewModel>();

            Fullname = group.Name + " (" + group.Course.Name + ")";
        }
    }
}