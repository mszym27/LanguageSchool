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
using System.Data.Entity.Infrastructure;

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
        private Repository<User> userRepository;
        private Repository<UserData> userDataRepository;
        private Repository<Message> messageRepository;
        private Repository<UserMessage> userMessageRepository;
        private Repository<ClosedQuestion> closedQuestionRepository;
        private Repository<OpenQuestion> openQuestionRepository;
        private Repository<Test> testRepository;
        private Repository<Answer> answerRepository;
        private Repository<UserOpenAnswer> userOpenAnswerRepository;
        private Repository<UserTest> userTestRepository;
        private Repository<UserGroup> userGroupRepository;
        private Repository<DictionaryItem> dictionaryItemRepository;

        public Repository<DictionaryItem> DictionaryItemRepository
        {
            get
            {
                return this.dictionaryItemRepository ?? new Repository<DictionaryItem>(entities);
            }
        }

        public Repository<UserGroup> UserGroupRepository
        {
            get
            {
                return this.userGroupRepository ?? new Repository<UserGroup>(entities);
            }
        }

        public Repository<UserTest> UserTestRepository
        {
            get
            {
                return this.userTestRepository ?? new Repository<UserTest>(entities);
            }
        }

        public Repository<UserOpenAnswer> UserOpenAnswerRepository
        {
            get
            {
                return this.userOpenAnswerRepository ?? new Repository<UserOpenAnswer>(entities);
            }
        }

        public Repository<Answer> AnswerRepository
        {
            get
            {
                return this.answerRepository ?? new Repository<Answer>(entities);
            }
        }

        public Repository<Test> TestRepository
        {
            get
            {
                return this.testRepository ?? new Repository<Test>(entities);
            }
        }

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
                int pageIndex)
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
                pageIndex
            ).ToList();
        }

        public void Save()
        {
            try {
                entities.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception ex = dbEx;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);

                        ex = new InvalidOperationException(message, ex);
                    }
                }

                throw ex;
            }
            catch (DbUpdateException dbEx)
            {
                Exception ex = dbEx;

                var message = "";

                foreach (var result in dbEx.Entries)
                {
                    message += string.Format("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }

                ex = new InvalidOperationException(message, ex);

                throw ex;
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