using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class MedicalInstructionImageRepository : BaseRepository<MedicalInstructionImage>, IMedicalInstructionImageRepository
    {
        public MedicalInstructionImageRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
