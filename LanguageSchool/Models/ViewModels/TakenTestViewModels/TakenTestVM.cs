using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels.TakenTestViewModels
{
    public class TakenTestVM
    {
        public string Name { get; }
        public string Comment { get; }
        public string Mark { get; }
        public bool IsMarked { get; }
        public int PointsAwarded { get; }
        public int Points { get; }

        public List<TakenClosedQuestionVM> ClosedQuestions { get; }
        public List<TakenOpenQuestionVM> OpenQuestions { get; }

        public TakenTestVM(UserTest userTest)
        {
            Mark = userTest.Mark.PLName;
            IsMarked = userTest.IsMarked;
            PointsAwarded = userTest.Points;

            var test = userTest.Test;

            Name = test.Name;
            Comment = test.Comment == null? "-": test.Comment;
            Points = test.Points;

            var student = userTest.User;

            ClosedQuestions = new List<TakenClosedQuestionVM>();

            foreach (
                var question in test.UserClosedAnswers
                    .Where(a => a.UserId == student.Id)
                    .Select(a => a.TestClosedQuestion)
            )
            {
                ClosedQuestions.Add(new TakenClosedQuestionVM(question, student));
            }

            OpenQuestions = new List<TakenOpenQuestionVM>();

            foreach (var question in userTest.UserOpenAnswers.Where(a => a.IsMarked))
            {
                OpenQuestions.Add(new TakenOpenQuestionVM(question));
            }
        }
    }
}