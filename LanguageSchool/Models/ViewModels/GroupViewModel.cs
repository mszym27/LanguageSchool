using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class GroupViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SelectList Users { get; set; }
        public int UserId { get; set; }

        // Create
        public GroupViewModel(int CourseId)
        {
            this.CourseId = CourseId;

            Users = PopulateList.GetAllTeachers();
        }
    }
}