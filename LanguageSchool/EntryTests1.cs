//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LanguageSchool
{
    using System;
    using System.Collections.Generic;
    
    public partial class EntryTests1
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Cours Cours { get; set; }
    }
}
