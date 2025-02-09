using VueBlog.API.Models;

namespace VueBlog.API.Services.User
{
    public interface IUserService
    {
        public Task<VueBlog.API.Models.User> CreateUserAsync(Models.User user,string role = "User");
        public Task<VueBlog.API.Models.User> SoftDeleteAsync(Guid Id);
        public Task<VueBlog.API.Models.User> UpdateUserAsync(Guid Id, Models.User user);
        public Task<VueBlog.API.Models.User> SearchByIdAsync(Guid Id);
        public Task<Models.User> GetUserByNameAsync(string Name);
        public Task<List<Models.User>> GetAllUsersAsync();
        public bool IsNameConflict(string name);
    }
}
