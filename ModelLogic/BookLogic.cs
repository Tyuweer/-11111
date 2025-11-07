using DataAccessLayer;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelLogic
{
    /// <summary>
    /// Реализация бизнес-логики для работы с книгами
    /// </summary>
    public class BookLogic : IBookLogic
    {
        /// <summary>
        /// Инициализирует бизнес-логику с указанным репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными</param>
        /// <exception cref="ArgumentNullException">Выбрасывается если repository равен null</exception>
        private readonly IRepository<Book> _repository;

        // Конструктор с возможностью выбора реализации репозитория
        public BookLogic(IRepository<Book> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Получает все книги из хранилища
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAll() => _repository.ReadAll().ToList();

        /// <summary>
        /// Добавляет новую книгу после проверки валидности данных
        /// </summary>
        /// <param name="title">Название книги</param>
        /// <param name="author">Автор книги</param>
        /// <returns>True если книга успешно добавлена, False если название или автор пустые</returns>
        public bool Add(string title, string author)
        {
            //String.IsNullOrWhiteSpace() — это метод в C#,
            //который проверяет, является ли строка null, пустой ("") или содержит только пробельные символы 
            //Если одно из этих условий верно, метод возвращает true, в противном случае — false
            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(author))
            {
                _repository.Add(new Book { Title = title, Author = author });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>True если книга найдена и удалена, False если книга не найдена</returns>
        public bool Delete(int id)
        {
            var book = _repository.ReadById(id);
            if (book != null)
            {
                _repository.Delete(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Обновляет данные книги после проверки валидности
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления</param>
        /// <param name="newTitle">Новое название книги</param>
        /// <param name="newAuthor">Новый автор книги</param>
        /// <returns>True если книга найдена и данные обновлены, False если книга не найдена или данные невалидны</returns>
        public bool Update(int id, string newTitle, string newAuthor)
        {
            var book = _repository.ReadById(id);
            if (book != null && !string.IsNullOrWhiteSpace(newTitle) && !string.IsNullOrWhiteSpace(newAuthor))
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                _repository.Update(book);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Находит книги по автору
        /// </summary>
        /// <param name="author">Автор для поиска</param>
        /// <returns>Список книг указанного автора</returns>
        public List<Book> FindByAuthor(string author)
        {
            return _repository.ReadAll()
                .Where(b => b.Author == author)
                .ToList();
        }

        /// <summary>
        /// Группирует книги по авторам с подсчетом количества книг каждого автора
        /// </summary>
        /// <returns>Список строк в формате "Автор: X книг"</returns>
        public List<string> GroupByAuthor()
        {
            return _repository.ReadAll()
                .GroupBy(b => b.Author)
                .Select(g => $"{g.Key}: {g.Count()} книг")
                .ToList();
        }
    }
}