using Task_Management_Api.Models;
using Task_Management_Api.Repositories;

namespace Task_Management_Api.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _repository.GetUserByEmail(email);
        }
    }
}