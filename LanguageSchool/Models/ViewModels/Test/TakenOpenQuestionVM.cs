using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels.TakenTestViewModels
{
    public class TakenOpenQuestionVM
    {
        public string Question { get; }
        public string Answer { get; }
        public string Comment { get; }
        public int PointsAwarded { get; }
        public int Points { get; }

        public TakenOpenQuestionVM(UserOpenAnswer userAnswer)
        {
            Answer = userAnswer.Content;
            Comment = userAnswer.Comment.Replace("\r\n", "<br>");
            PointsAwarded = userAnswer.Points;

            var question = userAnswer.OpenQuestion;

            Question = question.Contents;
            Points = question.Points;
        }
        
    }
}