using Task_Management_Api.Models;

namespace Task_Management_Api.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<bool> DeleteTask(int id);
        Task<List<TaskItem>> GetAll();
        Task<TaskItem?> GetById(int id);
        Task<List<TaskItem>> GetByUser(int userId);
        Task<TaskItem> Post(TaskItem task);
        Task<bool> Update(TaskItem task);
    }
}