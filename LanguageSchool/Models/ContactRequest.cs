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
    
    public partial class ContactRequest
    {
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string Comment { get; set; }
        public bool IsAwaiting { get; set; }
        public int CourseId { get; set; }
        public Nullable<int> EntryTestId { get; set; }
        public Nullable<int> Points { get; set; }
        public int PreferredHoursFrom { get; set; }
        public int PreferredHoursTo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Test Test { get; set; }
    }
}
