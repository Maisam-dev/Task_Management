using Task_Management_Api.Models;

namespace Task_Management_Api.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}