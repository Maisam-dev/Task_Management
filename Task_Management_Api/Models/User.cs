using System.ComponentModel.DataAnnotations;

namespace Task_Management_Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = default!;

        public List<Customer>? customers { get; set; }
    }
}