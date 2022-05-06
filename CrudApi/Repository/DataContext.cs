using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Repository
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated(); // убираем, если используем миграции
        }
    }
}