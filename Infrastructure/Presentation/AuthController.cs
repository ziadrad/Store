using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class AuthController(IServicesManager servicesManager):ControllerBase
    {
        [HttpPost(template: "login")] // POST: /api/auth/login
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var result = await servicesManager.authService.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost(template: "register")] // POST: /api/auth/register
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await servicesManager.authService.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
