using System;
using System.Collections.Generic;
using EFMC.Service.Common.Results;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;

namespace EFMC.Service.Services
{
    public class DrugService : IDrugService
    {
        public DrugService()
        {
        }

        public Result<List<IndustryCreation>> CreateIndustry(IndustryCreation industryCreation)
        {
            throw new NotImplementedException();
        }
    }
}
