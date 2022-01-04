using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class MedicalInstructionImage
    {
        public int MedicalInstructionImageId { get; set; }
        public int MedicalInstructionTypeId { get; set; }
        public int MedicalRecordImageId { get; set; }

        #region Relationship
        public MedicalInstructionType MedicalInstructionType { get; set; }
        public ICollection<MedicalRecordImage> MedicalRecordImages { get; set; }
        #endregion
    }
}
