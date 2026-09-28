using Task_Management_Api.Models;

namespace Task_Management_Api.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> Delete(int id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetbyId(int id);
        Task<Customer> PostAsync(Customer customer);
        Task<bool> Put(Customer customer);
    }
}