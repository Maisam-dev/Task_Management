using Task_Management_Api.DTOs;
using Task_Management_Api.Models;

namespace Task_Management_Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<bool> DeleteTask(int id);
        Task<List<TaskDto>> GetAll();
        Task<TaskDto?> GetById(int id);
        Task<List<TaskDto>> GetByUser(int userId);
        Task<TaskItem?> Post(TaskItem task);
        Task<TaskDto?> UpdateTask(TaskItem task);
    }
}