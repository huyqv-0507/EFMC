using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class DebtTransactionRepository : BaseRepository<DebtTransaction>, IDebtTransactionRepository
    {
        public DebtTransactionRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
