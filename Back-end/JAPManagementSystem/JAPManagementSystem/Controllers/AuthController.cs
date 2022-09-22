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
        public async Task<ActionResult<ServiceResponse<String>>> Login(AddUserDto user)
        {
            var response = await _authService.Login(user.UserName, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
