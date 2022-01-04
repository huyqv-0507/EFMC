using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class MedicalInstructionType
    {
        public int MedicalInstructionTypeId { get; set; }
        public string Name { get; set; }

        #region Relationship
        public ICollection<MedicalInstructionImage> MedicalInstructionImages { get; set; }
        #endregion
    }
}
