using System.Data.Entity;
using DomainModels;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer
{
    // DbContext - базовый класс из Entity Framework
    public class AppDbContext : DbContext
    {
        //Вызываем конструктор базового класса DbContext
        //Передаем строку подключения к БД
        //DatabaseConfig.ConnectionString - статическое свойство с настройками подключения
        public AppDbContext() : base(DatabaseConfig.ConnectionString)
        {
            // Устанавливаем стратегию инициализации БД
            //Если БД не существует → создает новую +запускает Seed()
            //Если БД уже существует → ничего не делает
            Database.SetInitializer(new DatabaseInitializer());
        }

        // Явно указываем провайдер для EF
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Принудительно загружаем провайдер для обычного SQL Server
            // EF понимает, что нужно использовать полноценный SQL Server, а не Compact
            var providerServices = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            // OnModelCreating: Это переопределяемый метод в классе DbContext, который вызывается при создании модели данных.
            // Он используется для определения схемы базы данных.
            base.OnModelCreating(modelBuilder);
        }
        // представляет таблицу Books в БД
        public DbSet<Book> Books { get; set; }
    }
}