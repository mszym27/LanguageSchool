using System.Collections.Generic;

namespace LanguageSchool.Models.ViewModels.GroupViewModels
{
    public class UsersGroupInputVM
    {
        public int GroupId { get; }

        public List<UserVM> UsersAvaible { get; set; }
        public List<UserVM> UsersNonavaible { get; set; }

        public UsersGroupInputVM() { }

        //// Create - based on course
        //public GroupInputVM() { }

        // Edit
        public UsersGroupInputVM(Group group)
        {
            GroupId = group.Id;


        }
    }
}