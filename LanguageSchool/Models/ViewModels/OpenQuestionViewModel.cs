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
        public int LessonSubjectId { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Proszę wprowadzić treść pytania")]
        public string Contents { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów")]
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int Points { get; set; }

        public OpenQuestionViewModel() { }

        public OpenQuestionViewModel(LessonSubject lessonSubject)
        {
            LessonSubjectId = lessonSubject.Id;
            Points = 1;
        }
    }
}
