using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestRepository
    {
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);

        Task<Guid> SaveDraftAsync(SponsorshipRequest entity);

        Task<Guid> SubmitAsync(SponsorshipRequest entity);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail);
    }
}
;