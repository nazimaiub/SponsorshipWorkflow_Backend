using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestService
    {
        Task<Guid> SaveDraftAsync(SponsorshipRequestDto dto);

        Task<Guid> SubmitAsync(SponsorshipRequestDto dto);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string email, string role);
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);
    }
}
