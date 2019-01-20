using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class OpenQuestionViewModel
    {
        public int Id { get; set; }
        public int LessonSubjectId { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Proszę wprowadzić treść pytania")]
        [StringLength(1000, ErrorMessage = "Maksymalna długość pytania to 1000 znaków")]
        public string Contents { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów")]
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int Points { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "Maksymalna długość odpowiedzi to 4000 znaków")]
        public string Answer { get; set; }

        public OpenQuestionViewModel() { }

        public OpenQuestionViewModel(OpenQuestion question)
        {
            Id = question.Id;
            Contents = question.Contents;
            Points = question.Points;
        }

        public OpenQuestionViewModel(LessonSubject lessonSubject)
        {
            LessonSubjectId = lessonSubject.Id;
            Points = 1;
        }
    }
}
