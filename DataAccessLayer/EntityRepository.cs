using System.Collections.Generic;
using System.Linq;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Entity Framework
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        // гарантирует, что контекст не будет переопределен
        private readonly AppDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория
        /// </summary>
        public EntityRepository()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Добавляет сущность через Entity Framework
        /// </summary>
        /// <param name="item">Добавляемая сущность</param> 
        public void Add(T item)
        {
            // Когда вы вызывается logic.Add("Название", "Автор")
            // Внутри создается Book и вызывается _repository.Add(book);
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность по идентификатору через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
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
        
        /// <summary>
        /// Получает все сущности через Entity Framework
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        public IEnumerable<T> ReadAll()
        {
            // Преобразует IEnumerable<Book> в List<Book>
            return _context.Set<T>().ToList();
        }


        /// <summary>
        /// Получает сущность по идентификатору через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        public T ReadById(int id)
        {
            //Используется в:
            //Delete() - для проверки существования
            //Update() - для получения объекта для изменения
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Обновляет сущность через Entity Framework
        /// </summary>
        /// <param name="item">Сущность с обновленными данными</param>
        public void Update(T item)
        {
            var existing = _context.Set<T>().Find(item.Id);
            if (existing != null)
            {
                //Entry возвращает объект EntityEntry для работы с сущностями
                // EntityEntry предоставляет доступ к информации об отслеживании изменений для сущности и операциям, которые можно с ней выполнить
                _context.Entry(existing).CurrentValues.SetValues(item);
                //Для сохранения всех отслеживаемых изменений (создание, обновление, удаление) в базе данных
                _context.SaveChanges();
            }
        }
    }
}