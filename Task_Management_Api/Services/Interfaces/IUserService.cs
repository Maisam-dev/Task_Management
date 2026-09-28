using Task_Management_Api.Models;

namespace Task_Management_Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
    }
}