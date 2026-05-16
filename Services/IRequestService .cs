using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Models.Dto;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IRequestService
    {
        Task SaveDraftAsync(SponsorshipRequestDto dto);

        Task SubmitAsync(SponsorshipRequestDto dto);

        Task<List<SponsorshipRequest>> GetAllAsync();
        Task<SponsorshipRequest?> GetByIdAsync(Guid id);
    }
}
