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
        public string Contents { get; set; }
        public int NumberOfPossibleAnswers { get; set; }
        public bool IsMultichoice { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów")]
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int Points { get; set; }
        public int CurrentAnswer { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

        public ClosedQuestionViewModel()
        {
            Answers = new List<AnswerViewModel>();
        }

        public ClosedQuestionViewModel(LessonSubject lessonSubject)
        {
            LessonSubjectId = lessonSubject.Id;
            Points = 1;
            Answers = new List<AnswerViewModel>();
        }

        public ClosedQuestionViewModel(ClosedQuestion cq)
        {
            Id = cq.Id;
            Contents = cq.Contents;
            NumberOfPossibleAnswers = cq.NumberOfPossibleAnswers;
            IsMultichoice = cq.IsMultichoice;
            Points = cq.Points;

            Answers = new List<AnswerViewModel>();
        }
    }
}