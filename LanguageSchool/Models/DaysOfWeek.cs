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
    
    public partial class DaysOfWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaysOfWeek()
        {
            this.GroupTimes = new HashSet<GroupTime>();
        }
    
        public int Id { get; set; }
        public string PLName { get; set; }
        public string ENName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupTime> GroupTimes { get; set; }
    }
}
