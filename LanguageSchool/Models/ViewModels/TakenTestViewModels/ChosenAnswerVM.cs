using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.TakenTestViewModels
{
    public class ChosenAnswerVM
    {
        public string Contents { get; }
        public bool IsCorrect { get; }

        public ChosenAnswerVM(UserClosedAnswer userAnswer)
        {
            var answer = userAnswer.Answer;

            Contents = answer.AnswerContent;
            IsCorrect = answer.IsCorrect;
        }
    }
}