using Task_Management_Api.Models;

namespace Task_Management_Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
    }
}