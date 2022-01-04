using System;
using EFMC.Data.Entities;
using Mapster;

namespace EFMC.Service.Models
{

    public class RoleCreation
    {
        public string RoleName { get; set; }
    }

    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public static RoleInfo ToRoleInfo(Role role)
        {
            return role.Adapt<RoleInfo>();
        }
    }
}
