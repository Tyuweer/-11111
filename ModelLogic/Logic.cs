
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelLogic
{
    public class Logic
    {
        private List<Book> books = new List<Book>();
        private readonly string dataFilePath = "C:\\Users\\aleks\\Desktop\\FileSystemWatcher.txt";
        
        public Logic()
        {
            LoadBooks();
        }

        public List<Book> GetAll() => books;

        public bool Add(string title, string author)
        {
            if (title != "" & author != "")
            {
                books.Add(new Book { Id = books.Count + 1, Title = title, Author = author });
                SaveBooks();
                Console.WriteLine("Книга успешно добавлена");
                Console.WriteLine($"Id: {books.Count} | Название: {title} | Автор: {author}");
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                string title = book.Title;
                string author = book.Author;
                books.Remove(book);
                SaveBooks();
                Console.WriteLine("Книга успешно удалена!");
                Console.WriteLine($"Id: {id} | Название: {title} | Автор: {author}");
                return true;
            }
            Console.WriteLine("Книга, не удалена, возможно ошибка в ID");
            return false;
        }

        public bool Update(int id, string newTitle, string newAuthor)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                SaveBooks();
                Console.WriteLine("Книга успешно обновлена!");
                Console.WriteLine($"Новое название: {newTitle} | Новый автор: {newAuthor}");
                return true;
            }
            Console.WriteLine("Не удалось обновить книгу, возможно ошибка в ID");
            return false;
        }

        public List<Book> FindByAuthor(string author)
        {
            var foundBooks = books.Where(b => b.Author == author).ToList();
            if (!foundBooks.Any())
            {
                Console.WriteLine("У этого автора нет книг(");
            }
            return foundBooks;
        }

        public List<string> GroupByAuthor()
        {
            return books.GroupBy(b => b.Author).Select(g => $"{g.Key}: {g.Count()} книг").ToList();
        }

        private void SaveBooks()
        {
            using (var writer = new StreamWriter(dataFilePath))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Id}|{book.Title}|{book.Author}");
                }
            }
        }

        private void LoadBooks()
        {
            if (File.Exists(dataFilePath))
            {
                books.Clear();
                var lines = File.ReadAllLines(dataFilePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split('|');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id))
                    {
                        books.Add(new Book
                        {
                            Id = id,
                            Title = parts[1],
                            Author = parts[2]
                        });
                    }
                }
            }
        }
        public string GetDataFilePath()
        {
            return dataFilePath;
        }
    }
}