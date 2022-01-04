using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
#nullable enable
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
#nullable disable
        #region Relationship
        public ICollection<UserPharmacy> UserPharmacies { get; set; }
        public ICollection<PharmacyIndustry> PharmacyIndustries { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
        public ICollection<Consignment> Consignments { get; set; }
        #endregion
    }
}
