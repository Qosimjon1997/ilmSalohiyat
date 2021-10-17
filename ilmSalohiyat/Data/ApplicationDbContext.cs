using ilmSalohiyat.Models;
using Microsoft.EntityFrameworkCore;

namespace ilmSalohiyat.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

    }
}
