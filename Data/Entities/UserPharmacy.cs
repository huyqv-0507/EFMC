using System;

namespace Data.Entities
{
    public class UserPharmacy
    {
        public int UserPharmacyId { get; set; }
        public int OwnerId { get; set; }
        public int PharmacistId { get; set; }
        public int PharmacyId { get; set; }
        public string Status { get; set; }

        #region Relationship
        public User Owners { get; set; }
        public User Pharmacists { get; set; }
        public Pharmacy Pharmacies { get; set; }
        #endregion
    }
}
