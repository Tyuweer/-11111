using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLogic
{
    /// <summary>
    /// Интерфейс для операций чтения с книгами
    /// </summary>
    public interface IBookQueryLogic
    {
        /// <summary>
        /// Получает все книги из хранилища
        /// </summary>
        /// <returns>Список всех книг</returns>
        List<Book> GetAll();

        /// <summary>
        /// Находит книги по автору
        /// </summary>
        /// <param name="author">Автор для поиска</param>
        /// <returns>Список книг указанного автора</returns>
        List<Book> FindByAuthor(string author);

        /// <summary>
        /// Группирует книги по авторам с подсчетом количества
        /// </summary>
        /// <returns>Список строк в формате "Автор: X книг"</returns>
        List<string> GroupByAuthor();
    }
}
