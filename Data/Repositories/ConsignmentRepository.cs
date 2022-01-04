using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class ConsignmentRepository : BaseRepository<Consignment>, IConsignmentRepository
    {
        public ConsignmentRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
