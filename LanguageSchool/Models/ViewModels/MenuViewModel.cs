using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models.ViewModels
{
    public class MenuViewModel
    {
        public string ShownName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int RoleId { get; set; }
    }
}