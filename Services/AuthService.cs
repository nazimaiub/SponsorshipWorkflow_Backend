using SponsorshipWorkflow.Api.Models;
using SponsorshipWorkflow.Api.Repositories;

namespace SponsorshipWorkflow.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!validPassword)
                return null;

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Role = user.Role,
                Name = user.Name
            };
        }
    }
}