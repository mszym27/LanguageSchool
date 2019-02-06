using System.Collections.Generic;
using System.Linq;

using LanguageSchool.Models.ViewModels;

namespace LanguageSchool
{
    public class Consts
    {
        // Alerts
        public static readonly string Success = "forestgreen";
        public static readonly string Failure = "darkred";

        public static readonly string Info = "dodgerblue";

        public static readonly string Error = "red";

        public enum Roles
        {
            Secretary = 1001,
            Teacher = 1002,
            Student = 1003
        }

        public enum MessageTypes
        {
            ToUser = 2001,
            ToGroup = 2002,
            ToCourse = 2003,
            ToRole = 2004,
            ToAll = 2005,
            StudentWelcome = 2006
        }

        public static Dictionary<double, int> Grades = new Dictionary<double, int>() {
            { 40, 1 },
            { 55, 2 },
            { 70, 3 },
            { 85, 4 },
            { 95, 5 },
            { 100, 6 },
        };

        public static readonly int FailingPercentage = 40;

        public static int GetGrade(double percentage)
        {
            return Grades.FirstOrDefault(g => g.Key >= percentage).Value;
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
                new MenuViewModel { ShownName = "Wyślij komunikat masowy", RoleName = "Secretary", ControllerName = "Message", ActionName = "Send" }
            }
        );
    }
}