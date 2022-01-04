using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        public DrugRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
