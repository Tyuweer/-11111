using System.Collections.Generic;
using System.Linq;
using DomainModels;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        // гарантирует, что контекст не будет переопределен
        private readonly AppDbContext _context;

        // Создается новый экземпляр контекста при создании репозитория
        public EntityRepository()
        {
            _context = new AppDbContext();
        }

        public void Add(T item)
        {
            // Когда вы вызывается logic.Add("Название", "Автор")
            // Внутри создается Book и вызывается _repository.Add(book);
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Find(id) - ищет запись в БД по первичному ключу
            var item = _context.Set<T>().Find(id);
            // Если найдено -удаляет и сохраняет изменения
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                _context.SaveChanges();
            }
        }
        // Преобразует IEnumerable<Book> в List<Book>
        public IEnumerable<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }
        //Используется в:
        //Delete() - для проверки существования
        //Update() - для получения объекта для изменения
        public T ReadById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            var existing = _context.Set<T>().Find(item.Id);
            if (existing != null)
            {
                //Entry возвращает объект EntityEntry для работы с сущностями
                _context.Entry(existing).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
        }
    }
}