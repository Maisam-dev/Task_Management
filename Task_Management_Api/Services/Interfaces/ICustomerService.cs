using Task_Management_Api.DTOs;
using Task_Management_Api.Models;

namespace Task_Management_Api.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<int> DeleteAsync(int id);
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetbyId(int id);
        Task<CustomerDto?> PostAsync(Customer customer);
        Task<CustomerDto?> Put(Customer customer);
    }
}