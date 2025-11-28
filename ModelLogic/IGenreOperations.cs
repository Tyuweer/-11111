using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLogic
{
    public interface IGenreOperations : IBookLogic
    {
        /// <summary>
        /// Находит все книги в жанре фэнтези
        /// </summary>
        /// <returns>Список книг в жанре фэнтези</returns>
        List<Book> FindFantasyBooks();

        List<Book> FindRaitingBooks(int raiting);
    }

}
