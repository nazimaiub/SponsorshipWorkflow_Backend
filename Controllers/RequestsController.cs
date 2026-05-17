using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SponsorshipWorkflow.Api.Models.Dto;
using SponsorshipWorkflow.Api.Services;
using System.Security.Claims;

namespace SponsorshipWorkflow.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;

        public RequestsController(IRequestService service)
        {
            _service = service;
        }

        
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
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var data = await _service.GetAllMyRequests(userEmail,userRole);

            return Ok(data);
        }

        [HttpGet("request-by-id/{id}")]
        public async Task<IActionResult> GetSponsorshipRequestsByIdAsync(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var data = await _service.GetSponsorshipRequestsByIdAsync(id);

            return Ok(data);
        }

        [HttpGet("approveByManager/{id}")]
        public async Task<IActionResult> ApproveByManager(Guid id)
        {
            var managerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(managerId))
                return Unauthorized();

            await _service.ManagerApproveAsync(id, managerId);

            return Ok(new
            {
                message = "Request approved by manager"
            });
        }

        [HttpGet("rejectedByManager/{id}")]
        public async Task<IActionResult> RejectedByManager(Guid id)
        {
            var financeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(financeId))
                return Unauthorized();

            await _service.ManagerRejectAsync(id, financeId);

            return Ok(new
            {
                message = "Request rejected by finance"
            });
        }

        [HttpGet("approveByFinance/{id}")]
        public async Task<IActionResult> ApproveByFinance(Guid id)
        {
            var financeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(financeId))
                return Unauthorized();

            await _service.FinanceApproveAsync(id, financeId);

            return Ok(new
            {
                message = "Request approved by manager"
            });
        }


        [HttpGet("rejectedByFinance/{id}")]
        public async Task<IActionResult> RejectedByFinance(Guid id)
        {
            var financeId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(financeId))
                return Unauthorized();

            await _service.FinanceRejectAsync(id, financeId);

            return Ok(new
            {
                message = "Request rejected by finance"
            });
        }
    }
}
