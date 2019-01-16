using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class LessonSubjectViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Proszę podać temat")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public bool IsMarked { get; set; }

        public LessonSubjectViewModel() { }

        public LessonSubjectViewModel(LessonSubject lessonSubject)
        {
            Id = lessonSubject.Id;
            Name = lessonSubject.Name;
            Description = lessonSubject.Description;
        }
    }
}
