using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class DrugIndustryRepository : BaseRepository<DrugIndustry>, IDrugIndustryRepository
    {
        public DrugIndustryRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
