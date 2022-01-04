using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class MedicalRecordImageRepository : BaseRepository<MedicalRecordImage>, IMedicalRecordImageRepository
    {
        public MedicalRecordImageRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
