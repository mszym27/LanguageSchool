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
                new MenuViewModel { ShownName = "Lista kursów", ControllerName = "Course", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Testy", ControllerName = "Test", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Dane użytkowników", ControllerName = "Users", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "UserData", ActionName = "Index", RoleName = "Administrator" }
            }
        );
    }
}