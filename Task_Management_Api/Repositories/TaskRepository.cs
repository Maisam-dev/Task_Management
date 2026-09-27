using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;

namespace Task_Management_Api.Repositories
{
    public class TaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<List<Models.TaskItem>> GetAll()
        {
            return await _dbContext.taskItems.Include(t => t.Customer).ToListAsync();
        }

        public async Task<List<Models.TaskItem>> GetByUser(int userId)
        {
            return
                await _dbContext.taskItems
                .Include(t => t.Customer)
                .Where(t => t.Customer!.UserId == userId)
                .ToListAsync();
        }

        public async Task<Models.TaskItem?> GetById(int id)
        {
            return await _dbContext.taskItems.FindAsync(id);
        }

        public async Task<Models.TaskItem> Post(Models.TaskItem task)
        {
            _dbContext.taskItems.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Update(Models.TaskItem task)
        {
            _dbContext.taskItems.Update(task);
           var affectedRows =  await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
            
        }

        public async Task<bool> DeleteTask(int id)
        {
            var affectedRows = await _dbContext.taskItems
                .Where(t => t.Id == id).
                ExecuteDeleteAsync();

            return affectedRows > 0;
        }
    }
}