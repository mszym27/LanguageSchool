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
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Numer telefonu wymagany")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string EmailAdress { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [RangeAttribute(8, 20, ErrorMessage = "Wartość musi się mieścić w godzinach pracy sekretariatu (8 - 20)")]
        public int PreferredHoursFrom { get; set; }
        [RangeAttribute(8, 20, ErrorMessage = "Wartość musi się mieścić w godzinach pracy sekretariatu (8 - 20)")]
        public int PreferredHoursTo { get; set; }
        public int CourseId { get; set; }
        public Nullable<int> EntryTestId { get; set; }
        public Nullable<int> Points { get; set; }
    }
}
