using System;
namespace Data.Entities
{
    public class PrescriptionDetail
    {
        public int PrescriptionDetailId { get; set; }
        public int DrugId { get; set; }
        public int PrescriptionId { get; set; }
        public string DrugName { get; set; }
        public decimal UnitPrice { get; set; }
        public string UseTime { get; set; }
        public int Morning { get; set; }
        public int Noon { get; set; }
        public int Afternoon { get; set; }
        public int Night { get; set; }
        public string Note { get; set; }

        #region Relationship
        public Prescription Prescription { get; set; }
        public Drug Drug { get; set; }
        #endregion
    }
}
