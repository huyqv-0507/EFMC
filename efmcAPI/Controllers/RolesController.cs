using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFMC.Service.Common;
using EFMC.Service.Common.Constants;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFMC.API.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        public IActionResult AddRole(RoleCreation roleCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(roleCreation);

            var result = roleService.AddRole(roleCreation);

            if (result.Success == ResultConstant.SUCCESS)
                return StatusCode(201, result);
            return StatusCode(500, result);
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var result = roleService.GetRoles();
            if (result.Success == ResultConstant.SUCCESS)
                return Ok(result);
            return StatusCode(500, result);
        }
    }
}
