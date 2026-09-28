using Task_Management_Api.Models;
using Task_Management_Api.Repositories.Interfaces;
using Task_Management_Api.Services.Interfaces;

namespace Task_Management_Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _repository.GetUserByEmail(email);
        }
    }
}