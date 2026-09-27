using Task_Management_Api.DTOs;

namespace Task_Management_Api.Services
{
    public class AccountService
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly ILogger<AccountService> _logger;

        public AccountService(UserService userServis, TokenService tokenService, ILogger<AccountService> logger)
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