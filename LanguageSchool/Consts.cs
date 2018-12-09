using System.Collections.Generic;

using LanguageSchool.Models.ViewModels;

namespace LanguageSchool
{
    public class Consts
    {
        public enum Roles
        {
            Administrator = 1,
            Secretary = 2,
            Teacher = 3,
            Student = 4
        }

        public static List<MenuViewModel> menus = (
            new List<MenuViewModel>()
            {
                new MenuViewModel { ShownName = "Działa", ControllerName = "Course", ActionName = "Index", RoleId = (int)Consts.Roles.Administrator }
            }
        );
    }
}