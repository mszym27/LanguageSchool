using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; }
        public string StartDate { get; }
        public string Name { get; }
        public string ShortDescription { get; }
        public string NumberOfHours { get; }

        public CourseViewModel (Course c)
        {
            Id = c.Id;
            StartDate = c.StartDate.ToString("yyyy-MM-dd");
            Name = c.Name;
            NumberOfHours = c.NumberOfHours;

            int index = c.Description.IndexOf('.') + 1;

            ShortDescription = c.Description.Substring(0, index);
        }
    }
}