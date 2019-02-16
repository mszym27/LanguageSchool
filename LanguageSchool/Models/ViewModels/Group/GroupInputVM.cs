using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using LanguageSchool.DAL;

namespace LanguageSchool.Models.ViewModels.GroupViewModels
{
    public class GroupInputVM
    {
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwę grupy")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę podać datę rozpoczęcia zajęć")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Proszę podać datę zakończenia zajęć")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DateLessThan("StartDate", ErrorMessage = "Data nie może być wcześniejsza od daty rozpoczęcia")]
        public DateTime EndDate { get; set; }

        public SelectList Teachers
        {
            get { return PopulateList.AllUsersInRole((int)Consts.Roles.Teacher); }
        }

        [Required(ErrorMessage = "Proszę wybrać prowadzącego")]
        public int TeacherId { get; set; }

        // Create
        public GroupInputVM() { }

        //// Create - based on course
        //public GroupInputVM(Course course) { }

        // Edit
        public GroupInputVM(Group group)
        {
            GroupId = group.Id;
            Name = group.Name;
            StartDate = group.StartDate;
            EndDate = group.EndDate;
        }
    }
}