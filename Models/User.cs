namespace Task_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
