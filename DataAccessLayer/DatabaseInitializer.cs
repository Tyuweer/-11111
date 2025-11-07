using System.Data.Entity;
using DomainModels;

namespace DataAccessLayer
{
    // проверка есть ли БД, если нет создаем новую
    /// <summary>
    /// Инициализатор базы данных
    /// </summary>
    public class DatabaseInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        /// <summary>
        /// Заполняет базу данных начальными данными
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        protected override void Seed(AppDbContext context)
        {
            // Тестовые данные при создании БД
            context.Books.Add(new Book { Title = "Пример книги", Author = "Пример автора" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}