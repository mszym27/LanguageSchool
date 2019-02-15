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
        [Required(ErrorMessage = "Proszę podać datę rozpoczęcia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Proszę podać datę zakończenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public SelectList Teachers { get; }
        public int TeacherId { get; set; }

        // Create
        public GroupInputVM() { }
        
        // Edit
        public GroupInputVM(Group group)
        {
            GroupId = group.Id;
            Name = group.Name;
            StartDate = group.StartDate;
            EndDate = group.EndDate;

            Teachers = PopulateList.AllUsersInRole((int)Consts.Roles.Teacher);
        }
    }
}