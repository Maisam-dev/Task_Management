using FluentAssertions;
using Moq;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories.Interfaces;
using Task_Management_Api.Services;
using Xunit;

namespace Task_Management.Test.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUserByEmail_ReturnsUser_WhenFound()
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(r => r.GetUserByEmail("a@x.com")).ReturnsAsync(new User { Id = 1, Email = "a@x.com" });

            var sut = new UserService(repo.Object);

            var result = await sut.GetUserByEmail("a@x.com");

            result.Should().NotBeNull();
            result!.Email.Should().Be("a@x.com");
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsNull_WhenNotFound()
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(r => r.GetUserByEmail("x@x.com")).ReturnsAsync((User?)null);

            var sut = new UserService(repo.Object);

            var result = await sut.GetUserByEmail("x@x.com");

            result.Should().BeNull();
        }
    }
}
