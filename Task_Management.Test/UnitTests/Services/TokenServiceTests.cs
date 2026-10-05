using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Task_Management_Api.Models;
using Task_Management_Api.Services;
using Xunit;

namespace Task_Management.Test.UnitTests.Services
{
    public class TokenServiceTests
    {
        [Fact]
        public void CreateToken_ReturnsTokenString()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                { "JWT:Key", "super_secret_key_1234567890_For_JWT" }
            };

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

            var sut = new TokenService(configuration);

            var token = sut.CreateToken(new User { Id = 1, Name = "A", Email = "a@x.com", Role = "User" });

            token.Should().NotBeNullOrWhiteSpace();
            token.Should().Contain(".");
        }
    }
}
