using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using System.Data.Entity.Validation;

namespace LanguageSchool.DAL
{
    public class UnitOfWork : IDisposable
    {
        private LanguageSchoolEntities entities = new LanguageSchoolEntities();

        private Repository<LessonSubject> lessonSubjectRepository;
        private Repository<Material> materialRepository;
        private Repository<Group> groupRepository;
        private Repository<Course> courseRepository;
        private Repository<ContactRequest> contactRequestRepository;
        private Repository<Role> roleRepository;
        private Repository<User> userRepository;
        private Repository<UserData> userDataRepository;
        private Repository<Message> messageRepository;
        private Repository<UserMessage> userMessageRepository;
        private Repository<MessageType> messageTypeRepository;
        private Repository<LanguageProficency> languageProficencyRepository;
        private Repository<ClosedQuestion> closedQuestionRepository;
        private Repository<OpenQuestion> openQuestionRepository;

        public Repository<OpenQuestion> OpenQuestionRepository
        {
            get
            {
                return this.openQuestionRepository ?? new Repository<OpenQuestion>(entities);
            }
        }

        public Repository<ClosedQuestion> ClosedQuestionRepository
        {
            get
            {
                return this.closedQuestionRepository ?? new Repository<ClosedQuestion>(entities);
            }
        }

        public Repository<LanguageProficency> LanguageProficencyRepository
        {
            get
            {
                return this.languageProficencyRepository ?? new Repository<LanguageProficency>(entities);
            }
        }

        public Repository<LessonSubject> LessonSubjectRepository
        {
            get
            {
                return this.lessonSubjectRepository ?? new Repository<LessonSubject>(entities);
            }
        }

        public Repository<Material> MaterialRepository
        {
            get
            {
                return this.materialRepository ?? new Repository<Material>(entities);
            }
        }

        public Repository<Group> GroupRepository
        {
            get
            {
                return this.groupRepository ?? new Repository<Group>(entities);
            }
        }

        public Repository<Course> CourseRepository
        {
            get
            {
                return this.courseRepository ?? new Repository<Course>(entities);
            }
        }

        public Repository<ContactRequest> ContactRequestRepository
        {
            get
            {
                return this.contactRequestRepository ?? new Repository<ContactRequest>(entities);
            }
        }

        public Repository<Role> RoleRepository
        {
            get
            {
                return this.roleRepository ?? new Repository<Role>(entities);
            }
        }

        public Repository<User> UserRepository
        {
            get
            {
                return this.userRepository ?? new Repository<User>(entities);
            }
        }

        public Repository<UserData> UserDataRepository
        {
            get
            {
                return this.userDataRepository ?? new Repository<UserData>(entities);
            }
        }

        public Repository<MessageType> MessageTypeRepository
        {
            get
            {
                return this.messageTypeRepository ?? new Repository<MessageType>(entities);
            }
        }

        public Repository<Message> MessageRepository
        {
            get
            {
                return this.messageRepository ?? new Repository<Message>(entities);
            }
        }

        public Repository<UserMessage> UserMessageRepository
        {
            get
            {
                return this.userMessageRepository ?? new Repository<UserMessage>(entities);
            }
        }

        public List<GetContactInfoListItem> GetContactInfoList(
                Nullable<System.DateTime> creationDateFrom,
                Nullable<System.DateTime> creationDateTo,
                int PreferredHoursFrom,
                int PreferredHoursTo,
                string fullName,
                string city,
                string street,
                string phoneNumber,
                string emailAdress,
                int? courseId,
                int? roleId,
                bool showUserData,
                bool showContactRequests,
                string sortColumn,
                string sortDirection,
                int pageIndex,
                int pageSize)
        {
            return entities.GetContactInfoList(
                creationDateFrom,
                creationDateTo,
                PreferredHoursFrom,
                PreferredHoursTo,
                fullName,
                city,
                street,
                phoneNumber,
                emailAdress,
                courseId,
                roleId,
                showUserData,
                showContactRequests,
                sortColumn,
                sortDirection,
                pageIndex,
                pageSize
            ).ToList();
        }

        public void Save()
        {
            try {
                entities.SaveChanges();
            }
            catch (DbEntityValidationException ee)
            {
                foreach (var error in ee.EntityValidationErrors)
                {
                    foreach (var thisError in error.ValidationErrors)
                    {
                        var errorMessage = thisError.ErrorMessage;
                    }
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}