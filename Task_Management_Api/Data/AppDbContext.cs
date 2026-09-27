using Microsoft.EntityFrameworkCore;
using Task_Management_Api.Models;

namespace Task_Management_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Models.TaskItem> taskItems { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // users----------------------------
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@mail.com",
                    Password = "admin",
                    Role = "Admin"
                },

                new User
                {
                    Id = 2,
                    Name = "Alex",
                    Email = "alex@mail.com",
                    Password = "alex123",
                    Role = "User"
                },
                
                new User
                {
                    Id = 3,
                    Name = "Ana",
                    Email = "Ana@mail.com",
                    Password = "ana123",
                    Role = "User"
                }
                        );
            // Customer Items-------------------------------------
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "TechCorp Solutions GmbH",
                    Email = "contact@techcorp.de",
                    UserId = 3
                },
                    new Customer
                    {
                        Id = 2,
                        Name = "Logistics Express AG",
                        Email = "support@logisticsexpress.com",
                        UserId = 2
                    },
                    new Customer
                    {
                        Id = 3,
                        Name = "Apex Financial Group",
                        Email = "info@apexfinancial.com",
                        UserId = 2
                    }
                );

            // 3.  (TaskItems) -----------------------------------
            modelBuilder.Entity<TaskItem>().HasData(
               
                new TaskItem
                {
                    Id = 1,
                    Title = "System Infrastructure Audit",
                    Description = "Perform a security and performance check on cloud servers.",
                    Status = 2, // In Progress
                    IsCompleted = false,
                    CustomerId = 1
                },

            
                new TaskItem
                {
                    Id = 2,
                    Title = "API Integration Setup",
                    Description = "Integrate shipment tracking API with the main dashboard.",
                    Status = 3, // Completed
                    IsCompleted = true,
                    CustomerId = 2
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Database Migration",
                    Description = "Migrate legacy SQL database to PostgreSQL cluster.",
                    Status = 1, // Pending
                    IsCompleted = false,
                    CustomerId = 2
                },
                new TaskItem
                {
                    Id = 4,
                    Title = "Quarterly Maintenance",
                    Description = "Update dependencies and apply server patches.",
                    Status = 1, // Pending
                    IsCompleted = false,
                    CustomerId = 2
                },

              
                new TaskItem
                {
                    Id = 5,
                    Title = "Financial Report Module",
                    Description = "Implement export to PDF/Excel for monthly transactions.",
                    Status = 3, // Completed
                    IsCompleted = true,
                    CustomerId = 3
                },
                new TaskItem
                {
                    Id = 6,
                    Title = "User Role Authorization",
                    Description = "Restrict access to sensitive financial records based on claims.",
                    Status = 2, // In Progress
                    IsCompleted = false,
                    CustomerId = 3
                },
                new TaskItem
                {
                    Id = 7,
                    Title = "Payment Gateway Testing",
                    Description = "Test Stripe integration in sandbox environment.",
                    Status = 1, // Pending
                    IsCompleted = false,
                    CustomerId = 3
                },
                new TaskItem
                {
                    Id = 8,
                    Title = "Client Onboarding Review",
                    Description = "Conduct initial technical meeting for new portal usage.",
                    Status = 3, // Completed
                    IsCompleted = true,
                    CustomerId = 3
                }
            );
        }
    }
}