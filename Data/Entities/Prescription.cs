using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateFinished { get; set; }
        public string BuyerName { get; set; }
        public decimal TotalPrice { get; set; }
        public int MedicalRecordId { get; set; }
        public string Status { get; set; }
        public string ReasonCanceled { get; set; }

        #region Relationship
        public MedicalRecord MedicalRecord { get; set; }
        public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
        public ICollection<DebtTransaction> DebtTransactions { get; set; }
        #endregion
#nullable enable
        public string? Description { get; set; }
    }
}
