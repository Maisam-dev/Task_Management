using Task_Management_Api.DTOs;
using Task_Management_Api.Services.Interfaces;

namespace Task_Management_Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUserService userServis, ITokenService tokenService, ILogger<AccountService> logger)
        {
            _userService = userServis;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ResponseAuthDto?> Login(LoginRequestDto request)
        {
            var user = await _userService.GetUserByEmail(request.Email);

            if (user != null && user.Password == request.Password)
            {
                var token = _tokenService.CreateToken(user);
                _logger.LogInformation("User logged in successfully {Email}", user.Email);
                return new ResponseAuthDto { Token = token, UserName = user.Name };
            }
            else
                _logger.LogWarning("Login atttempt failed for user {Email}", request.Email);
            return null;
        }
    }
}