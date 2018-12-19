using System.Collections.Generic;

using LanguageSchool.Models.ViewModels;

namespace LanguageSchool
{
    public class Consts
    {
        // Alerts
        public static readonly string Success = "green";

        public static readonly string Error = "red";

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
                new MenuViewModel { ShownName = "Kursy", ControllerName = "Course", ActionName = "FullList", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Testy", ControllerName = "Test", ActionName = "FullList", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Materiały", ControllerName = "Test", ActionName = "FullList", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Dane kontaktowe", ControllerName = "Users", ActionName = "FullList", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "Message", ActionName = "FullList", RoleName = "Administrator" },
                new MenuViewModel { ShownName = "Zapisy", ControllerName = "UserData", ActionName = "FullList", RoleName = "Administrator" },

                //Sekretariat
                new MenuViewModel { ShownName = "Kursy", ControllerName = "Course", RoleName = "Secretary",
                    SubMenu = new List<MenuViewModel>()
                    {
                        new MenuViewModel { ShownName = "Lista", ControllerName = "Course", ActionName = "List" },
                        new MenuViewModel { ShownName = "Utwórz", ControllerName = "Course", ActionName = "Create" }
                    }
                },
                new MenuViewModel { ShownName = "Dane użytkowników", ControllerName = "Users", ActionName = "List", RoleName = "Secretary" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "Message", ActionName = "List", RoleName = "Secretary" },
                new MenuViewModel { ShownName = "Zapisy", ControllerName = "UserData", ActionName = "List", RoleName = "Secretary" },

                //Nauczyciel
                new MenuViewModel { ShownName = "Kursy", ControllerName = "Course", ActionName = "List", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Materiały", ControllerName = "Test", ActionName = "List", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Testy", ControllerName = "Test", ActionName = "List", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "Message", ActionName = "List", RoleName = "Teacher" },
                new MenuViewModel { ShownName = "Uczniowie", ControllerName = "Users", ActionName = "List", RoleName = "Teacher" },

                //Uczeń
                new MenuViewModel { ShownName = "Kursy", ControllerName = "Course", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Materiały", ControllerName = "Test", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Testy", ControllerName = "Test", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Komunikaty", ControllerName = "Message", ActionName = "Index", RoleName = "Student" },
                new MenuViewModel { ShownName = "Prowadzący", ControllerName = "Users", ActionName = "Index", RoleName = "Student" },
            }
        );
    }
}