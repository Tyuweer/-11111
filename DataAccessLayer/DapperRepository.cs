using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DomainModels;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;

        public DapperRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        // Все остальные методы остаются БЕЗ ИЗМЕНЕНИЙ
        public void Add(T item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Books (Title, Author) VALUES (@Title, @Author)";
                connection.Execute(sql, item);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Books WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<T>("SELECT * FROM Books").ToList();
            }
        }

        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Books WHERE Id = @Id";
                return connection.Query<T>(sql, new { Id = id }).FirstOrDefault();
            }
        }

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