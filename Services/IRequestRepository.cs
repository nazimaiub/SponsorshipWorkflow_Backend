using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestRepository
    {
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);

        Task<Guid> SaveDraftAsync(SponsorshipRequest entity);

        Task<Guid> SubmitAsync(SponsorshipRequest entity);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail, string role);


        Task<Guid> ManagerApproveAsync(Guid id, string managerId);
        Task<Guid> ManagerRejectAsync(Guid id, string managerId);

        Task<Guid> FinanceApproveAsync(Guid id, string financeId);
        Task<Guid> FinanceRejectAsync(Guid id, string financeId);
    }
}
;