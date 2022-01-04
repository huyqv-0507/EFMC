using System;
using Data.Entities;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;

namespace EFMC.Data.Repositories
{
    public class MedicalInstructionTypeRepository : BaseRepository<MedicalInstructionType>, IMedicalInstructionTypeRepository
    {
        public MedicalInstructionTypeRepository(IDbFactory db)
            : base(db)
        {
        }
    }
}
