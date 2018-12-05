using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;

namespace LanguageSchool.Models.ViewModels
{
    public class ClosedQuestionViewModel
    {
        public int Id { get; }
        public string Contents { get; }
        public int NumberOfPossibleAnswers { get; }
        public bool IsMultichoice { get; }
        public int Points { get; }

        public List<AnswerViewModel> Answers { get; set; }

        public ClosedQuestionViewModel (ClosedQuestion cq)
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