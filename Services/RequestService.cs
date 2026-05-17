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

        public async Task<Guid> SaveDraftAsync(SponsorshipRequestDto dto)
        {
            var entity = Map(dto, "Draft");

           return await _repo.SaveDraftAsync(entity);
        }

        public async Task<Guid> SubmitAsync(SponsorshipRequestDto dto)
        {
            var entity = Map(dto, "Pending Manager Approval");

            return await _repo.SubmitAsync(entity);
        }

        public async Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail)
        {
            return await _repo.GetAllMyRequests(userEmail);
        }

        public async Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id)
        {
            return await _repo.GetSponsorshipRequestsByIdAsync(id);
        }
        private SponsorshipRequest Map(SponsorshipRequestDto dto, string status)
        {
            return new SponsorshipRequest
            {
                Id = dto.Id ?? Guid.NewGuid(),
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
