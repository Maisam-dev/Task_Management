using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Task_Management.Test.UnitTests.Repositories
{
    public class TaskRepositoryTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAll_ReturnsTasks()
        {
            using var ctx = CreateContext(nameof(GetAll_ReturnsTasks));
            var customer = new Customer { Name = "Cust", Email = "c@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            ctx.taskItems.Add(new TaskItem { Title = "T1", CustomerId = customer.Id });
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            var res = await repo.GetAll();

            res.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetByUser_ReturnsOwnedTasks()
        {
            using var ctx = CreateContext(nameof(GetByUser_ReturnsOwnedTasks));
            var customer = new Customer { Name = "C", Email = "c@x", UserId = 42 };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            ctx.taskItems.Add(new TaskItem { Title = "T2", CustomerId = customer.Id });
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            var res = await repo.GetByUser(42);

            res.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetById_ReturnsTask_WhenExists()
        {
            using var ctx = CreateContext(nameof(GetById_ReturnsTask_WhenExists));
            var customer = new Customer { Name = "C2", Email = "c2@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var task = new TaskItem { Title = "FindMe", CustomerId = customer.Id };
            ctx.taskItems.Add(task);
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            var res = await repo.GetById(task.Id);

            res.Should().NotBeNull();
            res!.Title.Should().Be("FindMe");
        }

        [Fact]
        public async Task Post_AddsTask()
        {
            using var ctx = CreateContext(nameof(Post_AddsTask));
            var customer = new Customer { Name = "C3", Email = "c3@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            var newTask = new TaskItem { Title = "NewTask", CustomerId = customer.Id };
            var created = await repo.Post(newTask);

            created.Should().NotBeNull();
            created.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task Update_ReturnsTrue_WhenUpdated()
        {
            using var ctx = CreateContext(nameof(Update_ReturnsTrue_WhenUpdated));
            var customer = new Customer { Name = "C4", Email = "c4@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var task = new TaskItem { Title = "Old", CustomerId = customer.Id };
            ctx.taskItems.Add(task);
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            task.Title = "Updated";
            var updated = await repo.Update(task);

            updated.Should().BeTrue();
            ctx.taskItems.First(t => t.Id == task.Id).Title.Should().Be("Updated");
        }

        [Fact]
        public async Task DeleteTask_RemovesTask()
        {
            using var ctx = CreateContext(nameof(DeleteTask_RemovesTask));
            var customer = new Customer { Name = "C5", Email = "c5@x" };
            ctx.customers.Add(customer);
            ctx.SaveChanges();

            var task = new TaskItem { Title = "ToDelete", CustomerId = customer.Id };
            ctx.taskItems.Add(task);
            ctx.SaveChanges();

            var repo = new TaskRepository(ctx);

            var deleted = await repo.DeleteTask(task.Id);

            deleted.Should().BeTrue();
        }
    }
}
