using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLogic
{
    /// <summary>
    /// Интерфейс для операций записи с книгами
    /// </summary>
    public interface IBookCommandLogic
    {
        /// <summary>
        /// Добавляет новую книгу
        /// </summary>
        /// <param name="title">Название книги</param>
        /// <param name="author">Автор книги</param>
        /// <param name="genre">Автор книги</param>
        /// <param name="raiting">Рейтинг книги</param>
        /// <returns>True если книга успешно добавлена, False если данные невалидны</returns>
        bool Add(string title, string author, string genre, int raiting);

        /// <summary>
        /// Удаляет книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>True если книга найдена и удалена, False если книга не найдена</returns>
        bool Delete(int id);

        /// <summary>
        /// Обновляет данные книги
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления</param>
        /// <param name="newTitle">Новое название книги</param>
        /// <param name="newAuthor">Новый автор книги</param>
        /// <param name="newGenre">Новый автор книги</param>
        /// <param name="newRaiting">Новый рейтинг книги</param>
        /// <returns>True если книга найдена и данные обновлены, False если книга не найдена или данные невалидны</returns>
        bool Update(int id, string newTitle, string newAuthor, string newGenre, int newRaiting);
    }
}
