using System.Linq;
using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels.CourseViewModels
{
    public class CourseDetailsVM
    {
        public int CourseId { get; }
        public string StartDate { get; }
        public string EndDate { get; }
        public string Name { get; }
        public string Description { get; }
        public string NumberOfHours { get; }
        public string LanguageProficency { get; }
        public bool IsActive { get; }

        public List<GroupShortDetailsVM> Groups { get; }
        
        public CourseDetailsVM(Course course)
        {
            CourseId = course.Id;
            StartDate = course.StartDate.ToString("yyyy-MM-dd");
            EndDate = course.EndDate.ToString("yyyy-MM-dd");
            Name = course.Name;
            Description = course.Description;
            NumberOfHours = course.NumberOfHours;
            LanguageProficency = course.LanguageProficency.PLName;
            IsActive = course.IsActive;

            Groups = new List<GroupShortDetailsVM>();

            Groups = course.Groups
                .Where(g => !g.IsDeleted)
                .Select(g => new GroupShortDetailsVM(g))
                .ToList();
        }
    }
}