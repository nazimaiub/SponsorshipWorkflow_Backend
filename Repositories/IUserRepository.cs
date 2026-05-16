using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}