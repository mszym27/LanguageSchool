using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels.TakenTestViewModels
{
    public class TakenClosedQuestionVM
    {
        public string Contents { get; }
        public int Points { get; }
        public List<ChosenAnswerVM> ChosenAnswers { get; }

        public TakenClosedQuestionVM(TestClosedQuestion testQuestion, User student)
        {
            var question = testQuestion.ClosedQuestion;

            Contents = question.Contents;
            Points = question.Points;

            ChosenAnswers = new List<ChosenAnswerVM>();

            foreach (var answer in student.UserClosedAnswers.Where(a => a.TestClosedQuestionId == testQuestion.Id).OrderBy(a => a.Answer.IsCorrect))
            {
                ChosenAnswers.Add(new ChosenAnswerVM(answer));
            }
        }
        
    }
}