using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class ConsignmentDrugRepository : BaseRepository<ConsignmentDrug>, IConsignmentDrugRepository
    {
        public ConsignmentDrugRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
