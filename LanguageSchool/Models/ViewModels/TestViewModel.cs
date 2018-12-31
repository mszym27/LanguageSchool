using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CreationDate { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Points { get; set; }

        public List<ClosedQuestionViewModel> Questions { get; set; }

        public TestViewModel() { }

        public TestViewModel(Test t)
        {
            Id = t.Id;
            CourseId = t.CourseId;
            CreationDate = t.CreationDate.ToString("yyyy-MM-dd");
            Name = t.Name;
            Comment = t.Comment == null? "-": t.Comment;
            NumberOfQuestions = t.NumberOfQuestions;
            Points = t.Points;
        }
    }
}