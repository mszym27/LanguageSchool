using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set ; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDateShown { get; set ;}
        public string Name { get; set ; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ShortDescription { get; set ;}
        public string NumberOfHours { get; set ;}
        public string LanguageProficency { get; set ; }

        public SelectList LanguageProficenciens { get; set ; }
        public int LanguageProficencyId { get; set; }
        public bool IsActive { get; set; }

        // Create
        public CourseViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = StartDate.AddMonths(1);
            IsActive = true;
        }

        // Details
        public CourseViewModel (Course c)
        {
            Id = c.Id;
            StartDateShown = c.StartDate.ToString("yyyy-MM-dd");
            Name = c.Name;
            NumberOfHours = c.NumberOfHours;
            LanguageProficency = c.LanguageProficency.Name;

            int index = c.Description.IndexOf('.') + 1;

            ShortDescription = c.Description.Substring(0, index);
        }
    }
}