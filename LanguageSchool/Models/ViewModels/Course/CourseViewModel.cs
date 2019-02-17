using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set ; }
        [Required(ErrorMessage = "Proszę podać datę rozpoczęcia kursu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Proszę podać datę zakończenia kursu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        public string StartDateShown { get; set ; }
        [Required(ErrorMessage = "Proszę podać nazwę kursu")]
        [StringLength(150, ErrorMessage = "Długość pola jest ograniczona do 150 znaków")]
        public string Name { get; set ; }
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "Długość pola jest ograniczona do 4000 znaków")]
        public string Description { get; set; }
        public string ShortDescription { get; set ; }
        [Required(ErrorMessage = "Proszę wprowadzić orientacyjną liczbę godzin dla kursu")]
        [StringLength(200, ErrorMessage = "Długość pola jest ograniczona do 200 znaków")]
        public string NumberOfHours { get; set ; }
        public string LanguageProficency { get; set ; }

        public SelectList LanguageProficenciens { get; set ; }
        [Required(ErrorMessage = "Proszę wybrać poziom kursu")]
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
        public CourseViewModel (Course course)
        {
            Id = course.Id;
            StartDateShown = course.StartDate.ToString("yyyy-MM-dd");
            Name = course.Name;
            Description = course.Description;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            NumberOfHours = course.NumberOfHours;
            LanguageProficencyId = course.LanguageProficencyId;
            LanguageProficency = course.LanguageProficency.PLName;
            IsActive = course.IsActive;

            if(course.Description != null)
            {
                int index = course.Description.IndexOf('.') + 1;

                ShortDescription = course.Description.Substring(0, index);
            }
            else
            {
                ShortDescription = "-";
            }
        }
    }
}