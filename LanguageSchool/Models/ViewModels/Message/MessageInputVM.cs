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

        public SelectList MessageTypes { get; }
        public int MessageTypeId { get; set; }
        public SelectList Groups { get; }
        public int GroupId { get; set; }
        public SelectList Courses { get; }
        public int CourseId { get; set; }
        public SelectList Roles { get; }
        public int RoleId { get; set; }

        public MessageInputVM()
        {
            Groups = PopulateList.AllGroups();
            Courses = PopulateList.AllCourses();

            Roles = new SelectList(Consts.RoleList,
                "Key",
                "Value");
            MessageTypes = new SelectList(Consts.MessageTypeList,
                "Key",
                "Value");
        }
    }
}