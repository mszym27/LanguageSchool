using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using LanguageSchool.DAL;

namespace LanguageSchool.Models.ViewModels.ContactRequestViewModels
{
    public class ContactRequestsDetailsVM
    {
        public int ContactRequestId { get; }
        public string CreationDate { get; }
        public string ModificationDate { get; }
        public string Name { get; }
        public string Surname { get; }
        public string PhoneNumber { get; }
        public string PhoneNumberContact { get; }
        public string EmailAdress { get; }
        public int PreferredHoursFrom { get; }
        public int PreferredHoursTo { get; }
        public string Comment { get; }
        public string CourseName { get; }

        public ContactRequestsDetailsVM() { }

        public ContactRequestsDetailsVM(ContactRequest contactRequest)
        {
            ContactRequestId = contactRequest.Id;
            CreationDate = contactRequest.CreationDate.ToString("yyyy-MM-dd");
            ModificationDate = contactRequest.ModificationDate != null ? contactRequest.ModificationDate.Value.ToString("yyyy-MM-dd") : "-";
            Name = contactRequest.Name;
            Surname = contactRequest.Surname ?? "-";
            PhoneNumber = contactRequest.PhoneNumber;
            PhoneNumberContact = contactRequest.PhoneNumber.Replace("-", "");
            PreferredHoursFrom = contactRequest.PreferredHoursFrom;
            PreferredHoursTo = contactRequest.PreferredHoursTo;
            EmailAdress = contactRequest.EmailAdress ?? "-";
            Comment = contactRequest.Comment ?? "-";
            CourseName = contactRequest.Course.Name;
        }
    }
}