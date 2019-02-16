using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class LessonSubjectViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Podaj temat")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int MaxNumberOfClosedQuestions { get; set; }
        public int MaxNumberOfOpenQuestions { get; set; }
        [IntGreaterThan("MaxNumberOfClosedQuestions", ErrorMessage = "Ilość pytań nie może być większa od maksymalnej")]
        public int NumberOfOpenQuestions { get; set; }
        [IntGreaterThan("MaxNumberOfOpenQuestions", ErrorMessage = "Ilość pytań nie może być większa od maksymalnej")]
        public int NumberOfClosedQuestions { get; set; }

        public bool IsMarked { get; set; }

        public LessonSubjectViewModel() { }

        public LessonSubjectViewModel(LessonSubject lessonSubject)
        {
            Id = lessonSubject.Id;
            Name = lessonSubject.Name;
            Description = lessonSubject.Description;

            MaxNumberOfClosedQuestions = lessonSubject.ClosedQuestions.Count;
            MaxNumberOfOpenQuestions = lessonSubject.OpenQuestions.Count;

            NumberOfClosedQuestions = MaxNumberOfClosedQuestions;
            NumberOfOpenQuestions = MaxNumberOfOpenQuestions;
        }
    }
}
