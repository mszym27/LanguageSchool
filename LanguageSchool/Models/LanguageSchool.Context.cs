﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LanguageSchoolEntities : DbContext
    {
        public LanguageSchoolEntities()
            : base("name=LanguageSchoolEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }
        public virtual DbSet<UserData> UserData { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<EntryTest> EntryTests { get; set; }
        public virtual DbSet<LessonSubject> LessonSubjects { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ClosedQuestion> ClosedQuestions { get; set; }
        public virtual DbSet<OpenQuestion> OpenQuestions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestsLessonSubject> TestsLessonSubjects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserOpenAnswer> UserOpenAnswers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersTests> UsersTests1 { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<LanguageProficency> LanguageProficencies { get; set; }
        public virtual DbSet<Classrom> Classroms { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupTime> GroupTimes { get; set; }
        public virtual DbSet<UsersGroup> UsersGroups { get; set; }
        public virtual DbSet<TestClosedQuestion> TestClosedQuestions { get; set; }
        public virtual DbSet<TestOpenQuestion> TestOpenQuestions { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
    
        public virtual ObjectResult<GetContactInfoListItem> GetContactInfoList(Nullable<System.DateTime> creationDateFrom, Nullable<System.DateTime> creationDateTo, Nullable<int> preferredHoursFrom, Nullable<int> preferredHoursTo, string fullName, string city, string street, string phoneNumber, string emailAdress, Nullable<int> courseId, Nullable<int> roleId, Nullable<bool> showUserData, Nullable<bool> showContactRequests, string sortColumn, string sortDirection, Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var creationDateFromParameter = creationDateFrom.HasValue ?
                new ObjectParameter("CreationDateFrom", creationDateFrom) :
                new ObjectParameter("CreationDateFrom", typeof(System.DateTime));
    
            var creationDateToParameter = creationDateTo.HasValue ?
                new ObjectParameter("CreationDateTo", creationDateTo) :
                new ObjectParameter("CreationDateTo", typeof(System.DateTime));
    
            var preferredHoursFromParameter = preferredHoursFrom.HasValue ?
                new ObjectParameter("PreferredHoursFrom", preferredHoursFrom) :
                new ObjectParameter("PreferredHoursFrom", typeof(int));
    
            var preferredHoursToParameter = preferredHoursTo.HasValue ?
                new ObjectParameter("PreferredHoursTo", preferredHoursTo) :
                new ObjectParameter("PreferredHoursTo", typeof(int));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var streetParameter = street != null ?
                new ObjectParameter("Street", street) :
                new ObjectParameter("Street", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var emailAdressParameter = emailAdress != null ?
                new ObjectParameter("EmailAdress", emailAdress) :
                new ObjectParameter("EmailAdress", typeof(string));
    
            var courseIdParameter = courseId.HasValue ?
                new ObjectParameter("CourseId", courseId) :
                new ObjectParameter("CourseId", typeof(int));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            var showUserDataParameter = showUserData.HasValue ?
                new ObjectParameter("ShowUserData", showUserData) :
                new ObjectParameter("ShowUserData", typeof(bool));
    
            var showContactRequestsParameter = showContactRequests.HasValue ?
                new ObjectParameter("ShowContactRequests", showContactRequests) :
                new ObjectParameter("ShowContactRequests", typeof(bool));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortDirectionParameter = sortDirection != null ?
                new ObjectParameter("SortDirection", sortDirection) :
                new ObjectParameter("SortDirection", typeof(string));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetContactInfoListItem>("GetContactInfoList", creationDateFromParameter, creationDateToParameter, preferredHoursFromParameter, preferredHoursToParameter, fullNameParameter, cityParameter, streetParameter, phoneNumberParameter, emailAdressParameter, courseIdParameter, roleIdParameter, showUserDataParameter, showContactRequestsParameter, sortColumnParameter, sortDirectionParameter, pageIndexParameter, pageSizeParameter);
        }
    }
}
