using System;
using System.Collections.Generic;
using Data.Entities;

namespace EFMC.Data.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        #region Relationship
        public ICollection<User> Users { get; set; }
        #endregion
    }
}
