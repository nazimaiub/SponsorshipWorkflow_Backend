using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestRepository
    {
        Task<SponsorshipRequest?> GetByIdAsync(Guid id);

        Task<Guid> SaveDraftAsync(SponsorshipRequest entity);

        Task<Guid> SubmitAsync(SponsorshipRequest entity);

        Task<List<SponsorshipRequest>> GetAllAsync();
    }
}
