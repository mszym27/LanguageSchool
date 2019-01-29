using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić treść pytania")]
        [StringLength(250, ErrorMessage = "Maksymalna długość odpowiedzi to 250 znaków")]
        public string Answer { get; set; }

        public bool IsCorrect { get; set; }
        public bool IsMarked { get; set; }

        public AnswerViewModel()
        {
        }

        public AnswerViewModel (Answer a)
        {
            AnswerId = a.Id;
            Answer = a.AnswerContent;
        }
    }
}