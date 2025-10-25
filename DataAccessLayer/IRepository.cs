using System.Collections.Generic;
using DomainModels;

namespace DataAccessLayer
{
    // реализация IDomainObject
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T item);
        void Delete(int id);
        IEnumerable<T> ReadAll();
        T ReadById(int id);
        void Update(T item);
    }
}   