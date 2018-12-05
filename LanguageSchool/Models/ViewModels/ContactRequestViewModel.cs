using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class ContactRequestViewModel
    {
        //[DataType(DataType.PhoneNumber)]
        //[Required(ErrorMessage = "Phone Number Required!")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
        //           ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string Comment { get; set; }
        public int CourseId { get; set; }
        public Nullable<int> EntryTestId { get; set; }
        public Nullable<int> Points { get; set; }
    }
}
