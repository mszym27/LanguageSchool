using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class UserOpenAnswerViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserTestId { get; set; }
        public int QuestionId { get; set; }
        public int Points { get; set; }
        [IntGreaterThan("Points", ErrorMessage = "Liczba większa od maksymalnej")]
        [RangeAttribute(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera")]
        public int PointsAwarded { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public bool IsMarked { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Maksymalna długość komentarza to 1000 znaków")]
        public string Comment { get; set; }

        public UserOpenAnswerViewModel()
        {
        }

        public UserOpenAnswerViewModel(UserOpenAnswer answer)
        {
            Id = answer.Id;
            UserId = answer.UserId;
            UserTestId = answer.UserTestId;
            QuestionId = answer.OpenQuestionId;
            PointsAwarded = answer.Points;
            Points = answer.OpenQuestion.Points;
            Content = answer.Content;
        }
    }
}