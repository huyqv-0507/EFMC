using System;
namespace Data.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }

        #region relationship
        public MedicalRecordImage MedicalRecordImage { get; set; }
        #endregion
    }
}
