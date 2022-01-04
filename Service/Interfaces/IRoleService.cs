using System;
using System.Collections.Generic;
using System.Linq;
using EFMC.Data.Entities;
using EFMC.Service.Common.Results;
using EFMC.Service.Models;

namespace EFMC.Service.Interfaces
{
    public interface IRoleService
    {
        public Result<Role> AddRole(RoleCreation role);
        public Result<List<RoleInfo>> GetRoles();
    }
}
