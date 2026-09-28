using Task_Management_Api.DTOs;

namespace Task_Management_Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseAuthDto?> Login(LoginRequestDto request);
    }
}