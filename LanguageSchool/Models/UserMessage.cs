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
    
    public partial class UserMessage
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public bool HasBeenReceived { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
    
        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
    }
}
