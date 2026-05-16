using Microsoft.EntityFrameworkCore;
using SponsorshipWorkflow.Api.Data;
using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
