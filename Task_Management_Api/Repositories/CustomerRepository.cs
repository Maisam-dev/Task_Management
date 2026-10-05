using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories.Interfaces;

namespace Task_Management_Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbContext.customers.Include(c => c.Tasks).ToListAsync();
        }

        public async Task<Customer?> GetbyId(int id)
        {
            return await _dbContext.customers.FindAsync(id);
        }

        public async Task<Customer> PostAsync(Customer customer)
        {
            _dbContext.customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<int> Delete(int id)
        {
            var customer = await _dbContext.customers.FindAsync(id);
            if (customer == null)
                return 0;

            _dbContext.customers.Remove(customer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Put(Customer customer)
        {
            _dbContext.customers.Update(customer);
            var affecteRows = await _dbContext.SaveChangesAsync();
            return affecteRows > 0;

        }
    }
}