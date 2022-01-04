using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class DiseaseRepository : BaseRepository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
