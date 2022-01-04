using System;
namespace Data.Entities
{
    public class ConsignmentDrug
    {
        public int ConsignmentDrugId { get; set; }
        public int DrugId { get; set; }
        public int ConsignmentId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }

        #region Relationship
        public Consignment Consignment { get; set; }
        public Drug Drug { get; set; }
        #endregion
    }
}
