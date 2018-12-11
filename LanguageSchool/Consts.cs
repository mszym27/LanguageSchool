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
                // Administrator
                new MenuViewModel { ShownName = "Lista kursów", ControllerName = "Course", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Testy", ControllerName = "Test", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Materiały", ControllerName = "Test", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Dane kontaktowe", ControllerName = "Users", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "UserData", ActionName = "Index", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Zapisy", ControllerName = "UserData", ActionName = "Index", RoleName = "Secretary" },

                //Sekretariat
                new MenuViewModel { ShownName = "Lista kursów", ControllerName = "Course", ActionName = "Index", RoleName = "Secretary" },
                new MenuViewModel { ShownName = "Dane użytkowników", ControllerName = "Users", ActionName = "Index", RoleName = "Secretary" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "Test", ActionName = "Index", RoleName = "Secretary" },
                new MenuViewModel { ShownName = "Zapisy", ControllerName = "UserData", ActionName = "Index", RoleName = "Secretary" },

                //Nauczyciel
                new MenuViewModel { ShownName = "Moje kursy", ControllerName = "Course", ActionName = "Index", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Moje materiały", ControllerName = "Test", ActionName = "Index", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Moje testy", ControllerName = "Test", ActionName = "Index", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Moje komunikaty", ControllerName = "Test", ActionName = "Index", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Uczniowie", ControllerName = "Users", ActionName = "Index", RoleName = "Teacher" },

                //Uczeń
                new MenuViewModel { ShownName = "Moje kursy", ControllerName = "Course", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Moje materiały", ControllerName = "Test", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Moje testy", ControllerName = "Test", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Moje komunikaty", ControllerName = "Test", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Prowadzący", ControllerName = "Users", ActionName = "Index", RoleName = "Student" },
            }
        );
    }
}