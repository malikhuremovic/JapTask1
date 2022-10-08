using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<String>>> Login(UserLoginDto user)
        {
            var response = await _authService.Login(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
