﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int? CourseId { get; set; }
        public int GroupId { get; set; }
        public string CreationDate { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić nazwę testu")]
        [StringLength(250, ErrorMessage = "Maksymalna długość nazwy to 50 znaków")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań")]
        public int NumberOfQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań zamkniętych")]
        public int NumberOfOpenQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić liczbę pytań otwartych")]
        public int NumberOfClosedQuestions { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić ilość punktów do uzyskania")]
        public int Points { get; set; }

        public List<ClosedQuestionViewModel> ClosedQuestions { get; set; }
        public List<OpenQuestionViewModel> OpenQuestions { get; set; }

        public List<LessonSubjectViewModel> LessonSubjects { get; set; }

        public TestViewModel() { }

        public TestViewModel(Test test)
        {
            Id = test.Id;
            GroupId = test.GroupId;
            CourseId = test.CourseId;
            CreationDate = test.CreationDate.ToString("yyyy-MM-dd");
            Name = test.Name;
            Comment = test.Comment == null? "-": test.Comment;
            NumberOfQuestions = test.NumberOfQuestions;
            Points = test.Points;

            ClosedQuestions = new List<ClosedQuestionViewModel>();
            OpenQuestions = new List<OpenQuestionViewModel>();

            foreach (TestsLessonSubject testLessonSubject in test.TestsLessonSubjects)
            {
                foreach(ClosedQuestion closedQuestion in testLessonSubject.LessonSubject.ClosedQuestions)
                {
                    ClosedQuestions.Add(new ClosedQuestionViewModel(closedQuestion));
                }
                foreach (OpenQuestion openQuestion in testLessonSubject.LessonSubject.OpenQuestions)
                {
                    OpenQuestions.Add(new OpenQuestionViewModel(openQuestion));
                }
            }
        }

        public TestViewModel(Group group)
        {
            GroupId = group.Id;

            NumberOfOpenQuestions = 1;
            NumberOfClosedQuestions = 1;

            var lessonSubjects = group.LessonSubjects.Where(ls => !ls.IsDeleted && ls.IsActive).ToList();

            LessonSubjects = new List<LessonSubjectViewModel>();

            foreach (var lessonSubject in lessonSubjects)
            {
                LessonSubjects.Add(new LessonSubjectViewModel(lessonSubject));
            }
        }
    }
}