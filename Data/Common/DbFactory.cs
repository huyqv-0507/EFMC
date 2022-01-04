using System;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Common
{
    public class DbFactory : Disposable, IDbFactory
    {
        EfmcDbContext dbContext;
        public EfmcDbContext Init()
        {
            return dbContext ?? (dbContext = new EfmcDbContext());
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
