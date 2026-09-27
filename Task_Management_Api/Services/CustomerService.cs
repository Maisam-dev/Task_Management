using AutoMapper;
using Task_Management_Api.DTOs;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories;

namespace Task_Management_Api.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            CustomerRepository customerRepository,
            IMapper mapper,
            ILogger<CustomerService> logger
            )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetbyId(int id)
        {
            var customer = await _customerRepository.GetbyId(id);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto?> PostAsync(Customer customer)
        {
            var createdCustomer = await _customerRepository.PostAsync(customer);

            if (createdCustomer.Id == 0)
            {
                return null;
            }
            _logger.LogInformation("create successful with ID {id}", createdCustomer.Id);
            return _mapper.Map<CustomerDto>(createdCustomer);
        }

        public async Task<CustomerDto?> Put(Customer customer)

        {
            var oldCustomer = await _customerRepository.GetbyId(customer.Id);
            if (oldCustomer == null)
            {
                return null;
            }

            var isChenged = await _customerRepository.Put(customer);
            if (!isChenged)
            {
                return null;
            }
            _logger.LogInformation("update Successfull with ID {Id}", customer.Id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var deletedCount = await _customerRepository.Delete(id);
            if (deletedCount == 0)
            {
                _logger.LogWarning("delete failed {id}", id);
                return deletedCount;
            }
            _logger.LogInformation("delete Successfull with ID {}", id);
            return deletedCount;
        }
    }
}