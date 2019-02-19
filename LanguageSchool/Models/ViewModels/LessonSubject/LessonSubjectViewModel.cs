using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class LessonSubjectViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę tematu")]
        [StringLength(150, ErrorMessage = "Długość pola jest ograniczona do 150 znaków")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "Długość pola jest ograniczona do 4000 znaków")]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int MaxNumberOfClosedQuestions { get; set; }
        public int MaxNumberOfOpenQuestions { get; set; }
        [IntGreaterThan("MaxNumberOfOpenQuestions", ErrorMessage = "Większa od maksymalnej")]
        public int NumberOfOpenQuestions { get; set; }
        [IntGreaterThan("MaxNumberOfClosedQuestions", ErrorMessage = "Większa od maksymalnej")]
        public int NumberOfClosedQuestions { get; set; }

        public bool IsMarked { get; set; }

        public LessonSubjectViewModel() { }

        public LessonSubjectViewModel(LessonSubject lessonSubject)
        {
            Id = lessonSubject.Id;
            Name = lessonSubject.Name;
            Description = lessonSubject.Description;
            IsActive = lessonSubject.IsActive;

            MaxNumberOfClosedQuestions = lessonSubject.ClosedQuestions
                .Where(q => !q.IsDeleted)
                .ToList()
                .Count;

            MaxNumberOfOpenQuestions = lessonSubject.OpenQuestions
                .Where(q => !q.IsDeleted)
                .ToList()
                .Count;
        }
    }
}
