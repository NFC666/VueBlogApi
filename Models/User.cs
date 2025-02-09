using System.Text.Json.Serialization;

namespace VueBlog.API.Models
{
    public class User
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public string Role { get; set; } = "user"; // 角色：user/admin
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false; // 软删除标记
        [JsonIgnore]
        public string Salt { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
