using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Disease
    {
        public int DiseaseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        #region Relationship
        public ICollection<MedicalRecordDisease> MedicalRecordDiseases { get; set; }
        #endregion
    }
}
