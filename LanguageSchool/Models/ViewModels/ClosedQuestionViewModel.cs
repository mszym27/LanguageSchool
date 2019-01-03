using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;

namespace LanguageSchool.Models.ViewModels
{
    public class ClosedQuestionViewModel
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int NumberOfPossibleAnswers { get; set; }
        public bool IsMultichoice { get; set; }
        public int Points { get; set; }

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