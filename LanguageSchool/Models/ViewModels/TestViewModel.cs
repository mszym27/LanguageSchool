using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int? CourseId { get; set; }
        public int GroupId { get; set; }
        public string CreationDate { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić nazwę testu")]
        [StringLength(250, ErrorMessage = "Maksymalna długość nazwy to 50 znaków")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań")]
        public int NumberOfQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań zamkniętych")]
        public int NumberOfOpenQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań otwartych")]
        public int NumberOfClosedQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów do uzyskania")]
        public int Points { get; set; }

        public List<ClosedQuestionViewModel> Questions { get; set; }

        public List<LessonSubjectViewModel> LessonSubjects { get; set; }

        public TestViewModel() { }

        public TestViewModel(Test t)
        {
            Id = t.Id;
            CourseId = t.CourseId;
            CreationDate = t.CreationDate.ToString("yyyy-MM-dd");
            Name = t.Name;
            Comment = t.Comment == null? "-": t.Comment;
            NumberOfQuestions = t.NumberOfQuestions;
            Points = t.Points;
        }

        public TestViewModel(Group group)
        {
            GroupId = group.Id;

            NumberOfOpenQuestions = 1;
            NumberOfClosedQuestions = 1;

            var lessonSubjects = group.LessonSubjects.Where(ls => !ls.IsDeleted && ls.IsActive).ToList();

            LessonSubjects = new List<LessonSubjectViewModel>();

            foreach (var lessonSubject in lessonSubjects)
            {
                LessonSubjects.Add(new LessonSubjectViewModel(lessonSubject));
            }
        }
    }
}