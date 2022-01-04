using System;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private EfmcDbContext _dbContext;
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public EfmcDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
