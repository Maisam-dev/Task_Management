using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Data;
using Task_Management_Api.Models;

namespace Task_Management_Api.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}