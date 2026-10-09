using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Entities;

namespace MyFirstAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

        {

        }

        public DbSet<User> AcountUsers { get; set; }

        public DbSet<Employee> Employees { get; set; }



    }
}