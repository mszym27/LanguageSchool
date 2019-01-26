using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class ExistingLessonSubjectsVM
    {
        public int TeacherGroupId { get; set; }
        public List<Group> CourseGroups { get; set; }

        public ExistingLessonSubjectsVM(Course course, int groupId)
        {
            TeacherGroupId = groupId;

            CourseGroups = course.Groups.Where(g => !g.IsDeleted).ToList();
        }
    }
}