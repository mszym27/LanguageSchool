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

        public Repository<Course> CourseRepository
        {
            get
            {
                return this.courseRepository ?? new Repository<Course>(entities);
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