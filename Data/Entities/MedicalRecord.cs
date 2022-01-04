using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public DateTime DateTreatment { get; set; }
        public DateTime DateFinished { get; set; }
        public int PharmacyId { get; set; }
        public string PatientName { get; set; }
        public decimal TotalDebt { get; set; }

        #region Relationship
        public Pharmacy Pharmacy { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<MedicalRecordDisease> MedicalRecordDiseases { get; set; }
        public ICollection<MedicalInstructionImage> MedicalInstructionImages { get; set; }
        #endregion

    }
}
