using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using LanguageSchool.DAL;

namespace LanguageSchool.Models.ViewModels.ContactRequestViewModels
{
    public class ContactRequestInputVM
    {
        public int ContactRequestId { get; set; }
        [Required(ErrorMessage = "Proszę podać imię")]
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Numer telefonu wymagany")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Adres ma nieprawidłowy format")]
        public string EmailAdress { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [RangeAttribute(8, 20, ErrorMessage = "Wartość musi się mieścić w godzinach pracy sekretariatu (8 - 20)")]
        [IntGreaterThan("PreferredHoursTo", ErrorMessage = "Wartość nie może być większa od godziny końcowej")]
        public int PreferredHoursFrom { get; set; }
        [RangeAttribute(8, 20, ErrorMessage = "Wartość musi się mieścić w godzinach pracy sekretariatu (8 - 20)")]
        public int PreferredHoursTo { get; set; }
        public bool IsAwaiting { get; set; }
        public int CourseId { get; set; }

        // Create
        public ContactRequestInputVM()
        {
            PreferredHoursFrom = 8;
            PreferredHoursTo = 20;
        }

        // Edit
        public ContactRequestInputVM(ContactRequest contactRequest)
        {
            ContactRequestId = contactRequest.Id;
            Name = contactRequest.Name;
            Surname = contactRequest.Surname;
            PhoneNumber = contactRequest.PhoneNumber;
            EmailAdress = contactRequest.EmailAdress;
            Comment = contactRequest.Comment;
            PreferredHoursFrom = contactRequest.PreferredHoursFrom;
            PreferredHoursTo = contactRequest.PreferredHoursTo;
            IsAwaiting = contactRequest.IsAwaiting;
        }
    }
}