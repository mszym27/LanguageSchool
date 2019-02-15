﻿using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels
{
    public class UserDataViewModel
    {
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string HomeNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PublicPhoneNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Nieprawidłowy format numeru telefonu")]
        public string PrivatePhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        // na potrzeby tworzenia

        public int? OriginContactRequestId { get; set; }
        public Course Course { get; set; }

        public SelectList Roles { get; set; }
        public int RoleId { get; set; }

        public SelectList Groups { get; set; }
        public int GroupId { get; set; }

        public UserDataViewModel() { }

        // createFrom
        public UserDataViewModel(ContactRequest contactRequest)
        {
            OriginContactRequestId = contactRequest.Id;

            Name = contactRequest.Name;
            Surname = contactRequest.Surname;
            PrivatePhoneNumber = contactRequest.PhoneNumber;
            EmailAdress = contactRequest.EmailAdress;
            Course = contactRequest.Course;
        }

        // edit
        public UserDataViewModel(UserData userData)
        {
            UserId = userData.UserId;
            Name = userData.Name;
            Surname = userData.Surname;
            City = userData.City;
            Street = userData.Street;
            HouseNumber = userData.HouseNumber;
            HomeNumber = userData.HomeNumber;
            PublicPhoneNumber = userData.PublicPhoneNumber;
            PrivatePhoneNumber = userData.PrivatePhoneNumber;
            EmailAdress = userData.EmailAdress;
            Comment = userData.Comment;
        }
    }
}