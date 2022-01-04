using System;
namespace Data.Entities
{
    public class DrugIndustry
    {
        public int DrugIndustryId { get; set; }
        public int IndustryId { get; set; }
        public int DrugId { get; set; }

        #region Relationship
        public Industry Industry { get; set; }
        public Drug Drug { get; set; }
        #endregion
    }
}
