using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCloudStorageServer.Dto;
using SimpleCloudStorageServer.Service;

namespace SimpleCloudStorageServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserForRegisterDto registerDto)
        {
            return Ok(await _userService.CreateUser(registerDto));
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _userService.GetUser(userId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _userService.ApplyDeleteActionToUser(userId));
        }
    }
}
