using System.Linq;

namespace LanguageSchool.Models.ViewModels.CourseViewModels
{
    public class GroupShortDetailsVM
    {
        public int GroupId { get; }
        public string CreationDate { get; }
        public string Name { get; }
        public UserDataVM Teacher { get; }

        public GroupShortDetailsVM(Group group)
        {
            GroupId = group.Id;
            CreationDate = group.CreationDate.ToString("yyyy-MM-dd");
            Name = group.Name;

            Teacher = group.UsersGroups
                .Where(ug => !ug.IsDeleted)
                .Where(ug => ug.User.RoleId == (int)Consts.Roles.Teacher)
                .Select(ug => new UserDataVM(ug.User))
                .First();
        }
    }
}