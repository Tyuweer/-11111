using DataAccessLayer;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelLogic
{
    public class Logic
    {
        private readonly IRepository<Book> _repository;

        // Конструктор с возможностью выбора реализации репозитория
        public Logic(IRepository<Book> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Для обратной совместимости - используем EF по умолчанию

        public List<Book> GetAll() => _repository.ReadAll().ToList();

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

        public List<Book> FindByAuthor(string author)
        {
            return _repository.ReadAll()
                .Where(b => b.Author == author)
                .ToList();
        }

        public List<string> GroupByAuthor()
        {
            return _repository.ReadAll()
                .GroupBy(b => b.Author)
                .Select(g => $"{g.Key}: {g.Count()} книг")
                .ToList();
        }
    }
}