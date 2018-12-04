using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class ContactRequestViewModel
    {
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string Comment { get; set; }
        public int CourseId { get; set; }
        public Nullable<int> EntryTestId { get; set; }
        public Nullable<int> Points { get; set; }
    }
}
