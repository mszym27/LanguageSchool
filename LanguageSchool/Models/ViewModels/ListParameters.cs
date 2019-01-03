using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class ListParameters
    {
        public string sortColumn = "startDate";
        public string sortDirection = "asc";
        public int pageIndex = 1;
    }
}
