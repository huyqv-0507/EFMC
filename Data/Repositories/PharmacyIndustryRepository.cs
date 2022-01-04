using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class PharmacyIndustryRepository : BaseRepository<PharmacyIndustry>, IPharmacyIndustryRepository
    {
        public PharmacyIndustryRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
