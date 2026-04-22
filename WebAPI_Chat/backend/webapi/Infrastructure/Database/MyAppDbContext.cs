using Microsoft.EntityFrameworkCore;
using webapi.Infrastructure.Database.Models;

namespace webapi.Infrastructure.Database
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UserDBE> Users { get; set; }
        public DbSet<MessageDBE> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAppDbContext).Assembly);
        }
    }
}
