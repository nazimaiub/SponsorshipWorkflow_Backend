using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestService
    {
        Task<Guid> SaveDraftAsync(SponsorshipRequestDto dto);

        Task<Guid> SubmitAsync(SponsorshipRequestDto dto);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string email, string role);
        Task<List<RequestWorkflowHistory>> GetHistories();
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);

        Task<Guid> CancelByRequestor(Guid id);

        Task<Guid> ManagerApproveAsync(Guid id, string managerId, string remarks);
        Task<Guid> ManagerRejectAsync(Guid id, string managerId, string remarks);

        Task<Guid> FinanceApproveAsync(Guid id, string financeId, string remarks);
        Task<Guid> FinanceRejectAsync(Guid id, string financeId, string remarks);

    }
}
