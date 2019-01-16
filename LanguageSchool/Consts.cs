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
            Admin = 1,
            Secretary = 2,
            Teacher = 3,
            Student = 4
        }

        public enum MessageTypes
        {
            ToUser = 1,
            ToGroup = 2,
            ToCourse = 3,
            ToRole = 4,
            ToAll = 5,
            StudentWelcome = 6
        }

        public static List<int> pages = new List<int>()
        {
            20,
            50,
            100
        };

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
                new MenuViewModel { ShownName = "Dane użytkowników", RoleName = "Secretary",
                    SubMenu = new List<MenuViewModel>()
                    {
                        new MenuViewModel { ShownName = "Lista", ControllerName = "UserData", ActionName = "List" },
                        new MenuViewModel { ShownName = "Utwórz użytkownika", ControllerName = "UserData", ActionName = "Create" }
                    }
                },
                new MenuViewModel { ShownName = "Komunikaty", RoleName = "Secretary",
                    SubMenu = new List<MenuViewModel>()
                    {
                        new MenuViewModel { ShownName = "Moje komunikaty", ControllerName = "Message", ActionName = "Index" },
                        new MenuViewModel { ShownName = "Wyślij komunikat", ControllerName = "Message", ActionName = "Create" }
                    }
                },
                new MenuViewModel { ShownName = "Administracja", RoleName = "Secretary" }
            }
        );
    }
}