using System.Data.Entity;
using DomainModels;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных Entity Framework
    /// </summary>
    // DbContext - базовый класс из Entity Framework
    // DbContext это центральный класс в Entity Framework
    // который представляет собой "сеанс с базой данных" и объединяет шаблоны "единица работы" и "репозиторий"
    // Он используется для запросов, отслеживания изменений и сохранения данных в базе,
    // Также управляет соединением с базой и следит за сущностями, которые загружаются или изменяются
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует контекст с настройкой инициализатора БД
        /// </summary>
        //Вызываем конструктор базового класса DbContext
        //Передаем строку подключения к БД
        //DatabaseConfig.ConnectionString - статическое свойство с настройками подключения
        public AppDbContext() : base(DatabaseConfig.ConnectionString)
        {
            // Устанавливаем стратегию инициализации БД
            //Если БД не существует → создает новую +запускает Seed()
            //Если БД уже существует → ничего не делает
            //Database.SetInitializer(new DatabaseInitializer());
            Database.SetInitializer<AppDbContext>(null);
        }

        /// <summary>
        /// Настраивает модель данных при создании
        /// </summary>
        /// <param name="modelBuilder">Построитель модели данных</param>
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
        /// <summary>
        /// Набор данных для книг
        /// </summary>
        // представляет таблицу Books в БД
        public DbSet<Book> Books { get; set; }
    }
}