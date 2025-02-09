using Microsoft.EntityFrameworkCore;
using VueBlog.API.DbContext;
using VueBlog.API.Models;
using VueBlog.API.Utility;

namespace VueBlog.API.Services.User
{
    public class UserService : IUserService
    {
        AppDbContext db;
        public UserService(AppDbContext db) 
        {
            this.db = db;
        }

        public async Task<VueBlog.API.Models.User> CreateUserAsync(Models.User user,string Role="User")
        {
            var randomSalt = PasswordHasher.SaltGenerator();
            var newUser = new Models.User()
            {
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                Role = Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Salt = randomSalt,
                PasswordHash = PasswordHasher.HashPassword(user.PasswordHash, randomSalt)
            };

            var res = await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<Models.User> SoftDeleteAsync(Guid Id)
        {
            var deletedUser = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (deletedUser != null)
            {
                deletedUser.IsDeleted = true;
                await db.SaveChangesAsync();
                return deletedUser;
            }
            return null;
        }

        public async Task<Models.User> SearchByIdAsync(Guid Id)
        {
            var resp = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(resp != null)
            {
                return resp;
            }
            return null;
        }

        public async Task<Models.User> UpdateUserAsync(Guid Id, Models.User user)
        {
            var UpdateUser = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (UpdateUser != null)
            {
                UpdateUser.Username = user.Username;
                UpdateUser.AvatarUrl = user.AvatarUrl;
                UpdateUser.Email = user.Email;
                await db.SaveChangesAsync();
                return UpdateUser;
            }
            return null;
        }

        public async Task<Models.User> GetUserByNameAsync(string Name)
        {
            var res = await db.Users.FirstOrDefaultAsync(x=>x.Username == Name);
            if(res != null)
            {
                return res;
            }
            return null;
        }

        public async Task<List<Models.User>> GetAllUsersAsync()
        {
            var res = await db.Users.ToListAsync();
            if(res == null)
            {
                return null;
            }
            return res;
        }

        public bool IsNameConflict(string name)
        {
            int res = db.Users.Where(x=>x.Username.Equals(name)).Count();
            if (res <= 1)
            {
                return false;
            }
            return true;
        }
    }
}
