using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_Api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public List<TaskItem>? Tasks { get; set; }
    }
}