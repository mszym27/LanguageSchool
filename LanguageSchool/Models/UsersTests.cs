//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LanguageSchool.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersTests
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public Nullable<int> Points { get; set; }
        public bool IsMarked { get; set; }
        public int MarkId { get; set; }
    
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public virtual Mark Mark { get; set; }
    }
}
