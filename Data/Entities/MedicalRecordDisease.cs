using System;
namespace Data.Entities
{
    public class MedicalRecordDisease
    {
        public int MedicalRecordDiseaseId { get; set; }
        public int DiseaseId { get; set; }
        public int MedicalRecordId { get; set; }
        public string DiseaseDescription { get; set; }

        #region Relationship
        public MedicalRecord MedicalRecord { get; set; }
        public Disease Disease { get; set; }
        #endregion
    }
}
