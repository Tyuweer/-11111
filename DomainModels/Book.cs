using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Book : IDomainObject
    {
        // Уникальный идентификатор книги
        public int Id { get; set; }
        // Название книг
        public string Title { get; set; }
        // Автор книги
        public string Author { get; set; }
    }
}
