using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.IO;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.DAL
{
    public class UnitOfWork : IDisposable
    {
        private string EncryptionKey = "0ram@1234xxxxxxxxxxtttttuuuuuiiiiio";  //we can change the code converstion key as per our requirement

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
                showUserData,
                showContactRequests,
                sortColumn,
                sortDirection,
                pageIndex,
                pageSize
            ).ToList();
        }

        public string Encrypt(string password)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }

        public string Decrypt(string password)
        {
            byte[] cipherBytes = Convert.FromBase64String(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                }
        );
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    password = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return password;
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