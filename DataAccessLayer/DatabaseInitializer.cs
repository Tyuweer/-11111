using System.Data.Entity;
using DomainModels;

namespace DataAccessLayer
{
    // проверка есть ли БД, если нет создаем новую
    public class DatabaseInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Тестовые данные при создании БД
            context.Books.Add(new Book { Title = "Пример книги", Author = "Пример автора" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}