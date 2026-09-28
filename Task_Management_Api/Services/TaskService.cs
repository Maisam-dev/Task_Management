using AutoMapper;
using Task_Management_Api.DTOs;
using Task_Management_Api.Models;
using Task_Management_Api.Repositories.Interfaces;
using Task_Management_Api.Services.Interfaces;

namespace Task_Management_Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskrepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskrepository = taskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TaskDto>> GetAll()
        {
            var tasks = await _taskrepository.GetAll();

            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<List<TaskDto>> GetByUser(int userId)
        {
            var tasks = await _taskrepository.GetByUser(userId);
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto?> GetById(int id)
        {
            var task = await _taskrepository.GetById(id);
            if (task == null)
            {
                return null;
            }
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskItem?> Post(TaskItem task)
        {
            var createdTask = await _taskrepository.Post(task);
            if (createdTask.Id == 0)
            {
                _logger.LogWarning("create failed with ID {id}", createdTask.Id);
                return null;
            }
            _logger.LogInformation("create successfull with ID {id}", createdTask.Id);
            return createdTask;
        }

        public async Task<TaskDto?> UpdateTask(TaskItem task)

        {
            var existingTask = await _taskrepository.GetById(task.Id);
            if (existingTask == null)
            {
                return null;
            }

            var isUpdated = await _taskrepository.Update(task);
            if (!isUpdated)
            {
                _logger.LogWarning("update failed with ID {Id}", task.Id);
                return null;
            }
            _logger.LogInformation("update Successfull with ID {Id}", task.Id);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var isUpdated = await _taskrepository.DeleteTask(id);
            if (!isUpdated)
            {
                _logger.LogWarning("delete failed {id}", id);
                return false;
            }
            _logger.LogInformation("delete successfull with id {id} ", id);
            return true;
        }
    }
}