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
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
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