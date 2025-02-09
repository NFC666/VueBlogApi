using Microsoft.EntityFrameworkCore;
using VueBlog.API.Models;
namespace VueBlog.API.DbContext
{
    public class AppDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions ops):base(ops)
        {

        }

        public DbSet<Essay> Essays { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
