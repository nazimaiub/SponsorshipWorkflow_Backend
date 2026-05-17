using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestRepository
    {
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);

        Task<Guid> SaveDraftAsync(SponsorshipRequest entity);

        Task<Guid> SubmitAsync(SponsorshipRequest entity);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail, string role);

        Task<Guid> CancelByRequestor(Guid id);
        Task<Guid> ManagerApproveAsync(Guid id, string managerId, string remarks);
        Task<Guid> ManagerRejectAsync(Guid id, string managerId, string remarks);

        Task<Guid> FinanceApproveAsync(Guid id, string financeId, string remarks);
        Task<Guid> FinanceRejectAsync(Guid id, string financeId, string remarks);
    }
}
;