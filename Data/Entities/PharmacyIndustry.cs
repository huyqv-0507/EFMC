using System;
namespace Data.Entities
{
    public class PharmacyIndustry
    {
        public int PharmacyIndustryId { get; set; }
        public int PharmacyId { get; set; }
        public int IndustryId { get; set; }

        #region Relationship
        public Pharmacy Pharmacy { get; set; }
        public Industry Industry { get; set; }
        #endregion
    }
}
