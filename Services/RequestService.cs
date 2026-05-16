using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;

namespace SponsorshipWorkflow.Api.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repo;

        public RequestService(IRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task SaveDraftAsync(SponsorshipRequestDto dto)
        {
            var entity = Map(dto, "Draft");

            await _repo.SaveDraftAsync(entity);
        }

        public async Task SubmitAsync(SponsorshipRequestDto dto)
        {
            var entity = Map(dto, "Pending Manager Approval");

            await _repo.SubmitAsync(entity);
        }

        public async Task<List<SponsorshipRequest>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<SponsorshipRequest?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }
        private SponsorshipRequest Map(SponsorshipRequestDto dto, string status)
        {
            return new SponsorshipRequest
            {
                Id = Guid.NewGuid(),
                RequestorId = dto.RequestorId,
                RequestTitle = dto.RequestTitle,
                Department = dto.Department,
                SponsorshipType = dto.SponsorshipType,
                EventName = dto.EventName,
                EventDate = dto.EventDate,
                RequestedAmount = dto.RequestedAmount,
                Purpose = dto.Purpose,
                RequestorRemarks = dto.Remarks ?? string.Empty,
                Status = status,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
