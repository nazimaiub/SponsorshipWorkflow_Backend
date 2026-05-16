using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
