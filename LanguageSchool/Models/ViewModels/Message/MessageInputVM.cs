using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.MessageViewModels
{
    public class MessageInputVM
    {
        [Required(ErrorMessage = "Uzupełnij temat wiadomości")]
        [StringLength(50, ErrorMessage = "Długość pola jest ograniczona do 50 znaków")]
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Wprowadź treść wiadomości")]
        [StringLength(4000, ErrorMessage = "Długość pola jest ograniczona do 4000 znaków")]
        public string Contents { get; set; }
        public bool IsSystem { get; set; }

        public SelectList MessageTypes { get; set; }
        public int MessageTypeId { get; set; }
        public SelectList Groups { get; set; }
        public int GroupId { get; set; }
        public SelectList Courses { get; set; }
        public int CourseId { get; set; }
        public SelectList Roles { get; set; }
        public int RoleId { get; set; }
    }
}