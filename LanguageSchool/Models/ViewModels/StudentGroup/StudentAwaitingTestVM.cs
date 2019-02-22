using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.StudentGroupViewModels
{
    public class StudentAwaitingTestVM
    {
        public string TestName { get; }
        public List<UserOpenAnswer> AnswersAwaitingMark { get; }

        public StudentAwaitingTestVM(UserTest awaiting)
        {
            var test = awaiting.Test;

            TestName = test.Name;

            AnswersAwaitingMark = awaiting.UserOpenAnswers.Where(a => !a.IsDeleted && !a.IsMarked).ToList();
        }
    }
}