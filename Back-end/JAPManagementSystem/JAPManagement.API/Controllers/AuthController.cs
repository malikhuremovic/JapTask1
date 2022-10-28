﻿using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagement.API.Controllers
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
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto user)
        {
            var response = await _authService.Login(user);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add/admin")]
        public async Task<ActionResult<GetUserDto>> CreateAdmin(AddAdminDto admin)
        {
            var response = await _authService.RegisterAdminUser(admin);
            return Ok(response);
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("get/user")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserByToken()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            var response = await _authService.GetUserByToken(tokenValue);
            return Ok(response);
        }
    }
}
