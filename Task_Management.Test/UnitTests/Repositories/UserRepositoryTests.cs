using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories;
using Xunit;

namespace Task_Management.Test.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsUser_WhenExists()
        {
            using var ctx = CreateContext(nameof(GetUserByEmail_ReturnsUser_WhenExists));
            var user = new User { Name = "U1", Email = "u1@x", Password = "p", Role = "User" };
            ctx.Users.Add(user);
            ctx.SaveChanges();

            var repo = new UserRepository(ctx);

            var res = await repo.GetUserByEmail("u1@x");

            res.Should().NotBeNull();
            res!.Email.Should().Be("u1@x");
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsNull_WhenNotFound()
        {
            using var ctx = CreateContext(nameof(GetUserByEmail_ReturnsNull_WhenNotFound));
            var repo = new UserRepository(ctx);

            var res = await repo.GetUserByEmail("missing@x");

            res.Should().BeNull();
        }
    }
}
