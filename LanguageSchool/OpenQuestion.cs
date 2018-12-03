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
    
    public partial class OpenQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpenQuestion()
        {
            this.UserOpenAnswers = new HashSet<UserOpenAnswer>();
        }
    
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public int LessonSubjectId { get; set; }
        public string Contents { get; set; }
        public int Points { get; set; }
    
        public virtual LessonSubject LessonSubject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserOpenAnswer> UserOpenAnswers { get; set; }
    }
}
