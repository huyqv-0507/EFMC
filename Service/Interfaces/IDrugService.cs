using System;
using System.Collections.Generic;
using EFMC.Service.Common.Results;
using EFMC.Service.Models;

namespace EFMC.Service.Interfaces
{
    public interface IDrugService
    {
        public Result<List<IndustryCreation>> CreateIndustry(IndustryCreation industryCreation);
    }
}
