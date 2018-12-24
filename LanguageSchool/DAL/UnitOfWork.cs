using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

using LanguageSchool.Models;

namespace LanguageSchool.DAL
{
    public class UnitOfWork : IDisposable
    {
        private LanguageSchoolEntities entities = new LanguageSchoolEntities();

        private Repository<Course> courseRepository;
        private Repository<ContactRequest> contactRequestRepository;
        private Repository<User> userRepository;
        private Repository<Message> messageRepository;
        private Repository<UserMessage> userMessageRepository;
        private Repository<MessageType> messageTypeRepository;

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

        public void Save()
        {
            entities.SaveChanges();
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