using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories.Interfaces;
using Task_Management_Api.Services;
using Task_Management_Api.Services.Interfaces;
using Task_Management_Api.DTOs;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task_Management.Test.UnitTests.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsMappedList()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Customer> { new Customer { Id = 1, Name = "C" } });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<List<CustomerDto>>(It.IsAny<List<Customer>>())).Returns(new List<CustomerDto> { new CustomerDto { Id = 1, Name = "C" } });

            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("C");
        }

        [Fact]
        public async Task GetbyId_ReturnsDto_WhenFound()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.GetbyId(1)).ReturnsAsync(new Customer { Id = 1, Name = "C" });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<CustomerDto>(It.IsAny<Customer>())).Returns(new CustomerDto { Id = 1, Name = "C" });

            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetbyId(1);

            result.Should().NotBeNull();
            result!.Name.Should().Be("C");
        }

        [Fact]
        public async Task GetbyId_ReturnsNull_WhenNotFound()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.GetbyId(5)).ReturnsAsync((Customer?)null);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetbyId(5);

            result.Should().BeNull();
        }

        [Fact]
        public async Task PostAsync_ReturnsDto_WhenCreated()
        {
            var input = new Customer { Name = "New" };
            var created = new Customer { Id = 10, Name = "New" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.PostAsync(input)).ReturnsAsync(created);

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<CustomerDto>(created)).Returns(new CustomerDto { Id = 10, Name = "New" });

            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.PostAsync(input);

            result.Should().NotBeNull();
            result!.Id.Should().Be(10);
        }

        [Fact]
        public async Task PostAsync_ReturnsNull_WhenCreatedHasNoId()
        {
            var input = new Customer { Name = "New" };
            var created = new Customer { Id = 0, Name = "New" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.PostAsync(input)).ReturnsAsync(created);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.PostAsync(input);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Put_ReturnsDto_WhenUpdated()
        {
            var customer = new Customer { Id = 1, Name = "X" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.GetbyId(customer.Id)).ReturnsAsync(customer);
            repo.Setup(r => r.Put(customer)).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<CustomerDto>(customer)).Returns(new CustomerDto { Id = 1, Name = "X" });

            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.Put(customer);

            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
        }

        [Fact]
        public async Task Put_ReturnsNull_WhenOldCustomerMissing()
        {
            var customer = new Customer { Id = 5, Name = "X" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.GetbyId(customer.Id)).ReturnsAsync((Customer?)null);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.Put(customer);

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ReturnsCount()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.Delete(2)).ReturnsAsync(1);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<CustomerService>>();

            var sut = new CustomerService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.DeleteAsync(2);

            result.Should().Be(1);
        }
    }
}
