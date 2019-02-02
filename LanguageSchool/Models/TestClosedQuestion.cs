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
    
    public partial class TestClosedQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestClosedQuestion()
        {
            this.TestAnswers = new HashSet<TestAnswer>();
            this.UserClosedAnswers = new HashSet<UserClosedAnswer>();
        }
    
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual ClosedQuestion ClosedQuestion { get; set; }
        public virtual Test Test { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserClosedAnswer> UserClosedAnswers { get; set; }
    }
}
