namespace VueBlog.API.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
