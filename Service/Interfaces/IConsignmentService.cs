using System;
using System.Collections.Generic;
using EFMC.Service.Common.Results;
using EFMC.Service.Models;

namespace EFMC.Service.Interfaces
{
    public interface IConsignmentService
    {
        public Result<ConsignmentImported> ImportConsignment(int pharmacyId, ConsignmentImported consignmentImported);
        public Result<List<ConsignmentModel>> GetConsignments(
            int pharmacyId,
            DateTime? specificDate,
            int? year,
            int? month,
            int? day);
    }
}
