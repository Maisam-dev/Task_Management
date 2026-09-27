using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_Api.DTOs;
using Task_Management_Api.Models;
using Task_Management_Api.Services;

namespace Task_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        [HttpGet("ById")]
        public async Task<ActionResult> GetById(int id)
        {
            var customer = await _customerService.GetbyId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Customer customer)
        {
            var createdCustomer = await _customerService.PostAsync(customer);
            if (createdCustomer == null)
            {
                return StatusCode(500, "An internal server error occurred. Please try again later");
            }
            return Ok(createdCustomer);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Customer customer)
        {
            var updatedCustomer = await _customerService.Put(customer);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var deletedCount = await _customerService.DeleteAsync(id);
            if (deletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}