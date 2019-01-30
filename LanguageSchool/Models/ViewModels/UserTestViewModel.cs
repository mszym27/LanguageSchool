using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class UserTestViewModel
    {
        public int UserTestId { get; set; }
        public string CreationDate { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int PointsAwarded { get; set; }
        public int Points { get; set; }
        public string Mark { get; set; }
        public bool IsMarked { get; set; }

        public UserTestViewModel(UserTest userTest)
        {
            CreationDate = userTest.CreationDate.ToString("yyyy/MM/dd");
            UserTestId = userTest.Id;
            PointsAwarded = userTest.Points;
            Points = userTest.Test.Points;
            Name = userTest.Test.Name;
            Comment = userTest.Test.Comment;
            Mark = userTest.Mark.PLName;
            IsMarked = userTest.IsMarked;
        }
    }
}