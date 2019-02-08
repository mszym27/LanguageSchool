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
        public int Id { get; set; }
        public string CreationDate { get; set; }
        [Required(ErrorMessage = "Proszę podać imię")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname { get; set; }
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
        public Course Course { get; set; }
        public Nullable<int> EntryTestId { get; set; }
        public Nullable<int> Points { get; set; }

        public ContactRequestViewModel()
        {
            PreferredHoursFrom = 8;
            PreferredHoursTo = 20;
        }

        public ContactRequestViewModel(ContactRequest contactRequest)
        {
            Id = contactRequest.Id;
            Fullname = contactRequest.Name + " " + contactRequest.Surname;
            CreationDate = contactRequest.CreationDate.ToString("yyyy-MM-dd");
            PhoneNumber = contactRequest.PhoneNumber;
            EmailAdress = contactRequest.EmailAdress;
            Comment = contactRequest.Comment;
            PreferredHoursFrom = contactRequest.PreferredHoursFrom;
            PreferredHoursTo = contactRequest.PreferredHoursTo;
            Course = contactRequest.Course;
        }
    }
}
