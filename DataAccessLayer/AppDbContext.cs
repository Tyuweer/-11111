using System.Data.Entity;
using DomainModels;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(DatabaseConfig.ConnectionString)
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        // Явно указываем провайдер для EF
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Указываем использовать SQL Server вместо Compact
            var providerServices = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
    }
}