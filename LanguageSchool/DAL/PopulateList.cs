using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.DAL
{
    public static class PopulateList
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static SelectList AllUsers(User selectedUser = null)
        {
            var users = unitOfWork.UserRepository.Get(u => !u.IsDeleted);

            var usersViewModels = new List<UserViewModel>();

            foreach (var user in users)
                usersViewModels.Add(new UserViewModel(user));

            return new SelectList(usersViewModels, "Id", "Fullname", selectedUser);
        }

        public static SelectList AllUsersInRole(int RoleId)
        {
            var users = unitOfWork.UserRepository.Get(u => !u.IsDeleted && u.RoleId == RoleId);

            var usersViewModels = new List<UserViewModel>();

            foreach (var user in users)
                usersViewModels.Add(new UserViewModel(user));

            return new SelectList(usersViewModels, "Id", "Fullname");
        }

        public static SelectList GetPageSizes()
        {
            List<SelectListItem> pages = Consts.pages.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString()
                };
            });

            return new SelectList(pages);
        }
    }
}