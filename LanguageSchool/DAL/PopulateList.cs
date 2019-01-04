using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.DAL
{
    public static class PopulateList
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static SelectList GetAllTeachers()
        {
            var teachers = unitOfWork.UserRepository.Get(u => !u.IsDeleted && u.RoleId == (int)Consts.Roles.Teacher);

            var teachersViewModels = new List<UserViewModel>();

            foreach (var teacher in teachers)
                teachersViewModels.Add(new UserViewModel(teacher));

            return new SelectList(teachersViewModels, "Id", "Fullname");
        }
    }
}