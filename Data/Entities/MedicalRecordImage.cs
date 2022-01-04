using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class MedicalRecordImage
    {
        public int MedicalRecordImageId { get; set; }
        public int MedicalRecordId { get; set; }
        public int DiseaseId { get; set; }

        #region Relationship
        public MedicalInstructionImage MedicalInstructionImage { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public ICollection<Image> Images { get; set; }
        #endregion
    }
}
