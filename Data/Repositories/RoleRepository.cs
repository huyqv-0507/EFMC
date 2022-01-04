using System;
using EFMC.Data.Common;
using EFMC.Data.Entities;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
