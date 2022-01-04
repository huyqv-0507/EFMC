using System;
using System.Collections.Generic;
using EFMC.Service.Common.Results;
using EFMC.Service.Models;

namespace EFMC.Service.Interfaces
{
    public interface IPharmacyService
    {
        public Result<PharmacyModel> CreatePharmacy(PharmacyCreationModel pharmacy);
        public Result<List<PharmacyModel>> GetPharmacies(int ownerId, string? status);
        public Result<PharmacyModel> UpdatePharmacy(int pharmacyId, PharmacyUpdateModel pharmacyUpdate);
        public Result<List<IndustryModel>> CreateIndustry(int pharmacyId, List<string> industries);
        public Result<List<IndustryModel>> GetIndustries(int pharmacyId);
    }
}
