using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Industry
    {
        public int IndustryId { get; set; }
        public string Name { get; set; }

        #region Relationship
        public ICollection<PharmacyIndustry> PharmacyIndustries { get; set; }
        public ICollection<DrugIndustry> DrugIndustries { get; set; }
        #endregion
    }
}
