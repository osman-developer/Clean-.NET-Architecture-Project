using ClearnArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace CleanArch.Infrastructure
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        /// Overriding OnModelCreating to let ef know about our entities configs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
