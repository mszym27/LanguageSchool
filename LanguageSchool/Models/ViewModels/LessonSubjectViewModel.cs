﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class LessonSubjectViewModel
    {
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Proszę podać temat")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
