using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IModel
    {
        List<Book> GetAll();
        List<Book> GetAllSorted(int sortType);
        bool Add(string title, string author, string genre, int raiting);
        bool Update(int id, string newTitle, string newAuthor, string newGenre, int newRaiting);
        bool Delete(int id);
        List<Book> FindByAuthor(string author);
        List<Book> FindFantasyBooks();
        List<Book> FindRaitingBooks(int raiting);
        List<string> GroupByAuthor();
    }
}
