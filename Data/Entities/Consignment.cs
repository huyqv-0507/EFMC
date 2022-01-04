using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Consignment
    {
        public int ConsignmentId { get; set; }
        public DateTime DateImported { get; set; }
        public string From { get; set; }
        public int PharmacyId { get; set; }
        public decimal TotalCost { get; set; }

        #region Relationship
        public Pharmacy Pharmacy { get; set; }
        public ICollection<ConsignmentDrug> ConsignmentDrugs { get; set; }
        #endregion
    }
}
