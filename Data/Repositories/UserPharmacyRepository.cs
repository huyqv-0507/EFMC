using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class UserPharmacyRepository : BaseRepository<UserPharmacy>, IUserPharmacyRepository
    {
        public UserPharmacyRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
