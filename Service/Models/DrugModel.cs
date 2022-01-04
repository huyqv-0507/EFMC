using System;
using Data.Entities;
using Mapster;

namespace EFMC.Service.Models
{
    public class DrugModel
    {
        public DrugModel()
        {
        }
    }

    public class IndustryModel
    {
        public int IndustryId { get; set; }
        public string Name { get; set; }

        public static Industry ToIndustry(string industryName)
        {
            return new Industry() { Name = industryName };
        }
        public static IndustryModel ToIndustryModel(Industry industry)
        {
            return industry.Adapt<IndustryModel>();
        }
    }

    public class IndustryCreation
    {
        public string Name { get; set; }
    }
}
