using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestService
    {
        Task SaveDraftAsync(SponsorshipRequestDto dto);

        Task SubmitAsync(SponsorshipRequestDto dto);

        Task<List<SponsorshipRequest>> GetAllMyRequests(string email);
        Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id);
    }
}
