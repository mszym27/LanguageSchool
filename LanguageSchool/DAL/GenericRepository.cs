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
    public class Repository<TEntity> where TEntity : class
    {
        internal LanguageSchoolEntities entities;
        internal DbSet<TEntity> dbSet;

        public Repository(LanguageSchoolEntities entities)
        {
            this.entities = entities;
            this.dbSet = entities.Set<TEntity>();
        }

        //public virtual IEnumerable<TEntity> GetAllActive(
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        //)
        //{
        //    return this.Get(filter => (((TEntity)filter).IsActive == 1 ), orderBy, null);
        //}

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (entities.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            entities.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}