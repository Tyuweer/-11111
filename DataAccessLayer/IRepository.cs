using System.Collections.Generic;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый интерфейс репозитория для работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности, должен реализовывать IDomainObject</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность в хранилище
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        void Add(T item);

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        void Delete(int id);

        /// <summary>
        /// Получает все сущности из хранилища
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Получает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null если не найдена</returns>
        T ReadById(int id);

        /// <summary>
        /// Обновляет данные сущности в хранилище
        /// </summary>
        /// <param name="item">Сущность с обновленными данными</param>
        void Update(T item);
    }
}   