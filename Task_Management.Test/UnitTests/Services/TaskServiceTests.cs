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
    public class TaskServiceTests
    {
        [Fact]
        public async Task GetAll_ReturnsMappedList()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetAll()).ReturnsAsync(new List<TaskItem> { new TaskItem { Id = 1, Title = "T" } });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<List<TaskDto>>(It.IsAny<List<TaskItem>>())).Returns(new List<TaskDto> { new TaskDto { Id = 1, Title = "T" } });

            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetAll();

            result.Should().HaveCount(1);
            result[0].Title.Should().Be("T");
        }

        [Fact]
        public async Task GetByUser_ReturnsMappedList()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetByUser(2)).ReturnsAsync(new List<TaskItem> { new TaskItem { Id = 2, Title = "U" } });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<List<TaskDto>>(It.IsAny<List<TaskItem>>())).Returns(new List<TaskDto> { new TaskDto { Id = 2, Title = "U" } });

            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetByUser(2);

            result.Should().HaveCount(1);
            result[0].Id.Should().Be(2);
        }

        [Fact]
        public async Task GetById_ReturnsDto_WhenFound()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(3)).ReturnsAsync(new TaskItem { Id = 3, Title = "X" });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<TaskDto>(It.IsAny<TaskItem>())).Returns(new TaskDto { Id = 3, Title = "X" });

            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetById(3);

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenNotFound()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(9)).ReturnsAsync((TaskItem?)null);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.GetById(9);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Post_ReturnsCreated_WhenIdNotZero()
        {
            var task = new TaskItem { Id = 0, Title = "New" };
            var created = new TaskItem { Id = 5, Title = "New" };

            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.Post(task)).ReturnsAsync(created);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.Post(task);

            result.Should().NotBeNull();
            result!.Id.Should().Be(5);
        }

        [Fact]
        public async Task Post_ReturnsNull_WhenCreatedHasZeroId()
        {
            var task = new TaskItem { Id = 0, Title = "New" };
            var created = new TaskItem { Id = 0, Title = "New" };

            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.Post(task)).ReturnsAsync(created);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.Post(task);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateTask_ReturnsDto_WhenUpdated()
        {
            var task = new TaskItem { Id = 7, Title = "Up" };

            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);
            repo.Setup(r => r.Update(task)).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<TaskDto>(task)).Returns(new TaskDto { Id = 7, Title = "Up" });

            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.UpdateTask(task);

            result.Should().NotBeNull();
            result!.Id.Should().Be(7);
        }

        [Fact]
        public async Task UpdateTask_ReturnsNull_WhenNotFoundOrNotUpdated()
        {
            var task = new TaskItem { Id = 8, Title = "Up" };

            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(task.Id)).ReturnsAsync((TaskItem?)null);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.UpdateTask(task);

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteTask_ReturnsTrue_WhenDeleted()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.DeleteTask(3)).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.DeleteTask(3);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteTask_ReturnsFalse_WhenNotDeleted()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.DeleteTask(4)).ReturnsAsync(false);

            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<TaskService>>();

            var sut = new TaskService(repo.Object, mapper.Object, logger.Object);

            var result = await sut.DeleteTask(4);

            result.Should().BeFalse();
        }
    }
}
