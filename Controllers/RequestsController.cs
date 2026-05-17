using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                dto.RequestorId = userEmail;
                var result= await _service.SaveDraftAsync(dto);
                return Ok(new
                {
                    message = "Draft saved successfully",
                    id = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SponsorshipRequestDto dto)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                dto.RequestorId = userEmail;
                var result = await _service.SubmitAsync(dto);
                return Ok(new
                {
                    message = "Request submitted successfully",
                    id = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET ALL
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetAllMyRequests()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var data = await _service.GetAllMyRequests(userEmail);

            return Ok(data);
        }

        [HttpGet("request-by-id")]
        public async Task<IActionResult> GetSponsorshipRequestsByIdAsync(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var data = await _service.GetSponsorshipRequestsByIdAsync(id);

            return Ok(data);
        }
    }
}
