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

        public static Dictionary<int, string> RoleList = new Dictionary<int, string>
        {
            {1001, "Sekretariat"},
            {1002, "Nauczyciel"},
            {1003, "Student"},
        };

        public enum MessageTypes
        {
            ToUser = 2001,
            ToGroup = 2002,
            ToCourse = 2003,
            ToRole = 2004,
            ToAll = 2005,
            StudentWelcome = 2006
        }

        public static Dictionary<int, string> MessageTypeList = new Dictionary<int, string>
        {
            {2002, "Do członków grupy"},
            {2003, "Do uczestników kursu"},
            {2004, "Do wszystkich użytkowników o roli"},
            {2005, "Do wszystkich"},
        };

        public static Dictionary<int, string> LanguageProficencyList = new Dictionary<int, string>
        {
            {4001, "A1"},
            {4002, "A2"},
            {4003, "B1"},
            {4004, "B2"},
            {4005, "C1"},
            {4006, "C2"},
        };

        public static Dictionary<double, int> Grades = new Dictionary<double, int>() {
            { 40, 3001 }, // niedostateczny
            { 55, 3002 }, // dopuszczający
            { 70, 3003 }, // dostateczny
            { 85, 3004 }, // dobry
            { 95, 3005 }, // bardzo dobry
            { 100, 3006 } //celujący
        };

        public enum DaysOfWeek
        {
            Monday = 5001,
            Tuesday = 5005,
            Wednesday = 5003,
            Thursday = 5004,
            Friday = 5005,
            Saturday = 5006,
            Sunday = 5007
        }

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