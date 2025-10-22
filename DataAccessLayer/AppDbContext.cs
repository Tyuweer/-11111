using System.Data.Entity;
using DomainModels;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=BookDbConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }   

        public DbSet<Book> Books { get; set; }
    }
}