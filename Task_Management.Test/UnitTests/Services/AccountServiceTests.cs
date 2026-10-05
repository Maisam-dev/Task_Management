using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Task_Management_Api.DTOs;
using Task_Management_Api.Models;
using Task_Management_Api.Services;
using Task_Management_Api.Services.Interfaces;
using Xunit;

namespace Task_Management.Test.UnitTests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task Login_ReturnsResponse_WhenCredentialsMatch()
        {
            var user = new User { Id = 1, Email = "a@mail.com", Password = "pwd", Name = "U" };

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserByEmail(user.Email)).ReturnsAsync(user);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(t => t.CreateToken(user)).Returns("token123");

            var logger = new Mock<ILogger<AccountService>>();

            var sut = new AccountService(userService.Object, tokenService.Object, logger.Object);

            var result = await sut.Login(new LoginRequestDto { Email = user.Email, Password = user.Password });

            result.Should().NotBeNull();
            result!.Token.Should().Be("token123");
            result.UserName.Should().Be(user.Name);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenUserNotFoundOrWrongPassword()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserByEmail("x@mail.com")).ReturnsAsync((User?)null);

            var tokenService = new Mock<ITokenService>();
            var logger = new Mock<ILogger<AccountService>>();

            var sut = new AccountService(userService.Object, tokenService.Object, logger.Object);

            var result = await sut.Login(new LoginRequestDto { Email = "x@mail.com", Password = "no" });

            result.Should().BeNull();
        }
    }
}
