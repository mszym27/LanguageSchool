using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels.CourseViewModels
{
    public class CourseInputVM
    {
        public int CourseId { get; set ; }
        [Required(ErrorMessage = "Proszę podać nazwę kursu")]
        [StringLength(150, ErrorMessage = "Długość pola jest ograniczona do 150 znaków")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę podać datę rozpoczęcia kursu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Proszę podać datę zakończenia kursu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DateLessThan("StartDate", ErrorMessage = "Data nie może być wcześniejsza od daty rozpoczęcia")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(4000,
                ErrorMessage = "Długość pola ograniczona do 4000 znaków")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić orientacyjną liczbę godzin dla kursu")]
        [StringLength(200, ErrorMessage = "Długość pola jest ograniczona do 200 znaków")]
        public string NumberOfHours { get; set ; }
        public bool IsActive { get; set; }

        public SelectList LanguageProficenciens { get; set ; }
        [Required(ErrorMessage = "Proszę wybrać poziom kursu")]
        public int LanguageProficencyId { get; set; }

        // Create
        public CourseInputVM()
        {
            StartDate = DateTime.Now;
            EndDate = StartDate.AddMonths(1);
            IsActive = true;
        }

        // Edit
        public CourseInputVM(Course course)
        {
            CourseId = course.Id;
            StartDate = course.StartDate;
            Name = course.Name;
            Description = course.Description;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            NumberOfHours = course.NumberOfHours;
            IsActive = course.IsActive;
            LanguageProficencyId = course.LanguageProficencyId;
        }
    }
}