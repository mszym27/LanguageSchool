﻿using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels
{
    public class SubjectsQuestionsViewModel
    {
        public List<ClosedQuestion> ClosedQuestions { get; set; }
        public List<OpenQuestion> OpenQuestions { get; set; }

        public SubjectsQuestionsViewModel(LessonSubject lessonSubject)
        {
            ClosedQuestions = lessonSubject.ClosedQuestions.Where(cq => !cq.IsDeleted).ToList();
            OpenQuestions = lessonSubject.OpenQuestions.Where(oq => !oq.IsDeleted).ToList();
        }
    }
}
