using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Entities;
using EFMC.Service.Common.Enums;
using Mapster;

namespace EFMC.Service.Models
{
    public class PharmacyModel
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public List<UserPharmacyLocal> UserPharmacies { get; set; }
        public class UserPharmacyLocal
        {
            public int UserPharmacyId { get; set; }
            public int OwnerId { get; set; }
            public string OwnerName { get; set; }
            public int PharmacistId { get; set; }
            public string PharmacistName { get; set; }
            public int PharmacyId { get; set; }
            public string Status { get; set; }
        }
        // Convert UserPharmacy to UserPharmacyLocal
        public static UserPharmacyLocal ToUserPharmacyLocal(UserPharmacy userPharmacy)
        {
            return userPharmacy.Adapt<UserPharmacyLocal>();
        }
    }

    public class PharmacyCreationModel
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
#nullable enable
        public Pharmacist? PharmacistGeneration { get; set; }
        public class Pharmacist
        {
#nullable disable
            public int Count { get; set; }
            public string UserNameFormat { get; set; }
        }
    }

    public class PharmacyUpdateModel
    {
#nullable enable
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public List<UserPharmacyLocal>? UserPharmacies { get; set; }
        public class UserPharmacyLocal
        {
            public int UserPharmacyId { get; set; }
            public int? OwnerId { get; set; }
            public int? PharmacistId { get; set; }
            public string? Status { get; set; }
        }
    }
}
