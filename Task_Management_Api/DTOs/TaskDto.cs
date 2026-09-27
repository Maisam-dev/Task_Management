//using Task_Management_Api.Models;
namespace Task_Management_Api.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
    }
}