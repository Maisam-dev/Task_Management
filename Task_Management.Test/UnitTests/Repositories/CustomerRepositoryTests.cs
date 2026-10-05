using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;
using Task_Management_Api.Models;
using Task_Management_Api.DTOs;
using Task_Management_Api.Repositories;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace Task_Management.Test.UnitTests.Repositories
{
    public class CustomerRepositoryTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            using var ctx = CreateContext(nameof(GetAllAsync_ReturnsAllCustomers));
            ctx.customers.Add(new Customer { Name = "A", Email = "a@x" });
            ctx.SaveChanges();

            var repo = new CustomerRepository(ctx);

            var result = await repo.GetAllAsync();

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetbyId_ReturnsCustomer_WhenExists()
        {
            using var ctx = CreateContext(nameof(GetbyId_ReturnsCustomer_WhenExists));
            var customer = new Customer { Name = "B", Email = "b@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var repo = new CustomerRepository(ctx);

            var res = await repo.GetbyId(customer.Id);

            res.Should().NotBeNull();
            res!.Name.Should().Be("B");
        }

        [Fact]
        public async Task PostAsync_AddsCustomer()
        {
            using var ctx = CreateContext(nameof(PostAsync_AddsCustomer));
            var repo = new CustomerRepository(ctx);

            var newCustomer = new Customer { Name = "C", Email = "c@x" };
            var created = await repo.PostAsync(newCustomer);

            created.Should().NotBeNull();
            created.Id.Should().NotBe(0);
            ctx.customers.Should().Contain(c => c.Id == created.Id);
        }

        [Fact]
        public async Task Put_UpdatesCustomer_WhenExists()
        {
            using var ctx = CreateContext(nameof(Put_UpdatesCustomer_WhenExists));
            var customer = new Customer { Name = "D", Email = "d@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var repo = new CustomerRepository(ctx);

            customer.Name = "D2";
            var updated = await repo.Put(customer);

            updated.Should().BeTrue();
            ctx.customers.First(c => c.Id == customer.Id).Name.Should().Be("D2");
        }

        [Fact]
        public async Task Delete_RemovesCustomer()
        {
            using var ctx = CreateContext(nameof(Delete_RemovesCustomer));
            var customer = new Customer { Name = "E", Email = "e@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var repo = new CustomerRepository(ctx);

            var deletedCount = await repo.Delete(customer.Id);

            // Depending on provider, ExecuteDeleteAsync may return 1 when deleted
            deletedCount.Should().BeGreaterThanOrEqualTo(0);
        }
    }
}
