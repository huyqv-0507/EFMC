using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
