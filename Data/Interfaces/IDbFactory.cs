using System;
using EFMC.Data.Common;

namespace EFMC.Data.Interfaces
{
    public interface IDbFactory
    {
        EfmcDbContext Init();
    }
}
