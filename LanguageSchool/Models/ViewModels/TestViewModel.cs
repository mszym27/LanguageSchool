using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; }
        public string CreationDate { get; }
        public string Name { get; }
        public string Comment { get; }
        public int NumberOfQuestions { get; }
        public int Points { get; }

        public TestViewModel(Test t)
        {
            Id = t.Id;
            CreationDate = t.CreationDate.ToString("yyyy-MM-dd");
            Name = t.Name;
            Comment = t.Comment == null? "-": t.Comment;
            NumberOfQuestions = t.NumberOfQuestions;
            Points = t.Points;
        }
    }
}