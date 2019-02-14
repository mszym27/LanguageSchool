using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.GroupViewModels
{
    public class TeacherGroupVM
    {
        public int GroupId { get; }

        public List<StudentVM> Students { get; }
        public List<LessonSubject> LessonSubjects { get; }

        public List<Test> Tests { get; }

        public TeacherGroupVM(Group group)
        {
            Students = group.UsersGroups
                .Where(ug => !ug.IsDeleted)
                .Select(ug => new StudentVM(ug))
                .ToList();

            if (group.LessonSubjects != null)
                LessonSubjects = group.LessonSubjects
                    .Where(ls => !ls.IsDeleted)
                    .OrderByDescending(ls => ls.CreationDate)
                    .ToList();

            Tests = group.Tests
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.CreationDate)
                .ToList();
        }
    }
}