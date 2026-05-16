using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SponsorshipWorkflow.Api.Data;
using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;
using SponsorshipWorkflow.Api.Services;
using System.Security.Claims;

namespace SponsorshipWorkflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;

        public RequestsController(IRequestService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("draft")]
        public async Task<IActionResult> SaveDraft([FromBody] SponsorshipRequestDto dto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            dto.RequestorId = userEmail;
            
            await _service.SaveDraftAsync(dto);

            return Ok(new { message = "Draft saved successfully" });
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SponsorshipRequestDto dto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            dto.RequestorId = userEmail;
            await _service.SubmitAsync(dto);

            return Ok(new { message = "Request submitted successfully" });
        }

        // GET ALL
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            return Ok(data);
        }
    }
}
