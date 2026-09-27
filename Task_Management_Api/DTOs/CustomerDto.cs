namespace Task_Management_Api.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TaskDto>? Tasks { get; set; }
    }
}