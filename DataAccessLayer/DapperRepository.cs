using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Dapper
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    // Реализуем generic интерфейс, Ограничение: T должен быть классом (не структурой), Ограничение: T должен реализовывать интерфейс IDomainObject
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Dapper
        /// </summary>
        public DapperRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        /// <summary>
        /// Добавляет сущность через Dapper
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        public void Add(T item)
        {
            // Создается объект подключения к БД
            using (var connection = new SqlConnection(_connectionString))
            {
                // Установка физического соединения с SQL Server
                connection.Open();
                //INSERT INTO Books - вставить в таблицу Books

                //(Title, Author) - перечисляем столбцы для заполнения

                //VALUES(@Title, @Author) - значения для вставки
                string sql = "INSERT INTO Books (Title, Author) VALUES (@Title, @Author)";
                // Для использования Dapper, Dapper смотрит на все свойства объекта, автоматически создает параметры
                // sql Server выполняет запрос и атоматически генерирует ID
                // sql — это строка с SQL-командой, а item — это набор данных, который подставляется в запрос вместо заполнителей
                connection.Execute(sql, item);
            }
            //using нужен для гарантирует, что подключение к БД будет закрыто в любом случае -
            // даже если произойдет ошибка во время выполнения запроса.
            // после закрытия блока using вызывается Process Dispose(): Он закрывает подключение к БД
        }

        /// <summary>
        /// Удаляет сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Books WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }
        /// <summary>
        /// Получает все сущности через Dapper
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Возвращает результат в виде таблицы с помощью Sql запроса
                // Dapper автоматически сопоставляет столбцы с свойствами класса
                // Типизация через Generic <T>, Dapper создает объекты типа Book
                // Возвращается List<Book> - конкретная коллекция
                return connection.Query<T>("SELECT * FROM Books").ToList();
            }
        }

        /// <summary>
        /// Получает сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Books WHERE Id = @Id";
                // Dapper автоматически преобразует в @Id = id
                // Query<T> возвращает IEnumerable<T> 
                // FirstOrDefault() берет первый элемент или null
                return connection.Query<T>(sql, new { Id = id }).FirstOrDefault();
                // если не найдется книга по id ничего не произойдет (return null)
            }
        }

        /// <summary>
        /// Обновляет сущность через Dapper
        /// </summary>
        /// <param name="item">Сущность с обновленными данными</param>
        public void Update(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id";
                connection.Execute(sql, item);
            }
        }
    }
}