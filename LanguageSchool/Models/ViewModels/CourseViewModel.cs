using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; }
        public DateTime CreationDate { get; }
        public string Name { get; }
        public string ShortDescription { get; }

        public CourseViewModel (Course c)
        {
            Id = c.Id;
            CreationDate = c.CreationDate;
            Name = c.Name;

            int index = c.Description.IndexOf('.') + 1;

            ShortDescription = c.Description.Substring(0, index);
        }
    }
}