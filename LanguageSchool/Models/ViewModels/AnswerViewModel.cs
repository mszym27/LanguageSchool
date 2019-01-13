using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;

namespace LanguageSchool.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        public bool IsMarked { get; set; }

        public AnswerViewModel()
        {
        }

        public AnswerViewModel (Answer a)
        {
            Id = a.Id;
            Answer = a.AnswerContent;
            IsCorrect = a.IsCorrect;
        }
    }
}