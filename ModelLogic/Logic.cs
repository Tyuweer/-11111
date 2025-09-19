using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLogic
{
    public class Logic
    {
        private List<Book> books = new List<Book>();
        private int nextId = 1;

        public List<Book> GetAll() => books;

        public void Add(string title, string author)
        {
            books.Add(new Book { Id = nextId++, Title = title, Author = author });
        }

        public bool Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public bool Update(int id, string newTitle, string newAuthor)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                return true;
            }
            return false;
        }

        public List<Book> FindByAuthor(string author) =>
            books.Where(b => b.Author == author).ToList();

        public List<string> GroupByAuthor() =>
            books.GroupBy(b => b.Author).Select(g => $"{g.Key}: {g.Count()} книг").ToList();
    }
}
