using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class ClosedQuestionViewModel
    {
        public int Id { get; set; }
        public int LessonSubjectId { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić treść pytania")]
        [StringLength(250, ErrorMessage = "Maksymalna długość pytania to 250 znaków")]
        public string Contents { get; set; }
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int NumberOfPossibleAnswers { get; set; }
        public int? ChosenAnswerId { get; set; }
        public bool IsMultichoice { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów")]
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int Points { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

        public ClosedQuestionViewModel()
        {
        }

        public ClosedQuestionViewModel(LessonSubject lessonSubject)
        {
            LessonSubjectId = lessonSubject.Id;
            Points = 1;
            NumberOfPossibleAnswers = 3;
            Answers = new List<AnswerViewModel>();
        }

        public ClosedQuestionViewModel(ClosedQuestion closedQuestion)
        {
            Id = closedQuestion.Id;
            Contents = closedQuestion.Contents;
            NumberOfPossibleAnswers = closedQuestion.NumberOfPossibleAnswers;
            IsMultichoice = closedQuestion.IsMultichoice;
            Points = closedQuestion.Points;

            Answers = new List<AnswerViewModel>();
        }
    }
}