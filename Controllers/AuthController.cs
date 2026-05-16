using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SponsorshipWorkflow.Api.Data;
using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Services;
using System;

namespace SponsorshipWorkflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
        
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return null;
        }

    }

}