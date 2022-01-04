using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Drug
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Package { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; } // Sell
        public int Quantity { get; set; }

        #region Relationship
        public ICollection<DrugIndustry> DrugIndustries { get; set; }
        public ICollection<ConsignmentDrug> ConsignmentDrugs { get; set; }
        public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
        #endregion
#nullable enable
        public string? BrandName { get; set; }
        public string? MainIngredient { get; set; }
        public string? Ingredient { get; set; }
        public string? Description { get; set; }
    }
}
