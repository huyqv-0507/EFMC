using System;
using System.Collections.Generic;
using EFMC.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
        public bool IsLogin { get; set; }

        #region Relationship
        public Role Role { get; set; }
        public ICollection<UserPharmacy> OwnerPharmacies { get; set; }
        public ICollection<UserPharmacy> PharmacistPharmacies { get; set; }
        #endregion

#nullable enable
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Address { get; set; }
        public double? LoginFailedCount { get; set; }
    }
}
