using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFMC.Service.Common.Constants;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFMC.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        // Sign up for new user
        [HttpPost("register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
                return BadRequest(userRegistration);
            var result = userService.Register(userRegistration);
            if (result.Success == ResultConstant.SUCCESS)
                return StatusCode(201, result);
            return StatusCode(500, result);
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userLogin);

            }
            var result = userService.Login(userLogin);
            if (result.Success == ResultConstant.SUCCESS)
                return Ok(result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }

        // Get user's information by Id
        [HttpGet("{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            var result = userService.GetUserInfo(userId);
            if (result.Success == ResultConstant.SUCCESS)
                return Ok(result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }
    }
}
