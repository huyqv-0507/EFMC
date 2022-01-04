using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFMC.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFMC.Data.Common
{
    public abstract class BaseRepository<T> where T : class
    {
        #region Properties
        private EfmcDbContext dbContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected EfmcDbContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }
        #endregion
        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }
        #region Implementation
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
        public T Get(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).FirstOrDefault<T>();
        }
        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        public virtual IEnumerable<T> GetPaged(int page, int pageSize)
        {
            return dbSet.Skip(page * pageSize).Take(pageSize).ToList();
        }
        #endregion
    }
}
