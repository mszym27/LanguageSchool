using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;

namespace LanguageSchool.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; }
        public string Answer { get; }
        public bool IsCorrect { get; }

        public List<Answer> Answers { get; set; }

        public bool IsMarked { get; set; }

        public AnswerViewModel (Answer a)
        {
            Id = a.Id;
            Answer = a.AnswerContent;
            IsCorrect = a.IsCorrect;
        }
    }
}