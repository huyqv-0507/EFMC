using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class MedicalRecordDiseaseRepository : BaseRepository<MedicalRecordDisease>, IMedicalRecordDiseaseRepository
    {
        public MedicalRecordDiseaseRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
