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

        public async Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail,string role)
        {
            return await _repo.GetAllMyRequests(userEmail,role);
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

        public async Task<Guid> CancelByRequestor(Guid id)
        {
            return await _repo.CancelByRequestor(id);

        }
        public async Task<Guid> ManagerApproveAsync(Guid id, string managerId, string remarks)
        {
             return await _repo.ManagerApproveAsync(id,managerId,remarks);
        }

        public async Task<Guid> ManagerRejectAsync(Guid id, string managerId, string remarks)
        {
            return await _repo.ManagerRejectAsync(id,managerId,remarks);
        }

        public async Task<Guid> FinanceApproveAsync(Guid id, string financeId, string remarks)
        {
            return await _repo.FinanceApproveAsync(id, financeId,remarks);
        }

        public async Task<Guid> FinanceRejectAsync(Guid id, string financeId, string remarks)
        {
            return await _repo.FinanceRejectAsync(id, financeId,remarks);
        }

        public async Task<List<RequestWorkflowHistory>> GetHistories()
        {
            return await _repo.GetHistories();
        }
    }
}
