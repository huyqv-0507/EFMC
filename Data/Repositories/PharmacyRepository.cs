using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class PharmacyRepository : BaseRepository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
