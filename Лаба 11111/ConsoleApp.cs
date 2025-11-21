using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLogic;
using DomainModels;
using DataAccessLayer;
using Ninject;

namespace Лаба_11111
{

    /// <summary>
    /// Консольное приложение для управления библиотекой книг
    /// </summary>
    class ConsoleApp
    {
        static void Main()
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            
            var logic = ninjectKernel.Get<IGenreOperations>();

            while (true)
            {
                Console.WriteLine("\n1. Добавить книгу\n2. Удалить книгу\n3. Обновить книгу\n4. Показать все\n5. Найти по автору\n6. Лучшие книги\n7. Поиск по рейтингу\n8. Группировать по каждому автору\n0. Выход");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Название: ");
                        var title = Console.ReadLine();
                        Console.Write("Автор: ");
                        var author = Console.ReadLine();
                        Console.Write("Жанр: ");
                        var genre = Console.ReadLine();
                        Console.Write("Рейтинг: ");
                        var raiting = Convert.ToInt32(Console.ReadLine());
                        if (title != "" && author != "" && genre != "")
                        {
                            logic.Add(title, author, genre, raiting);
                        }
                        else
                        {
                            Console.WriteLine("Название и автор должны быть указаны!");
                        }
                            
                        break;
                    case "2":
                        Console.Write("ID: ");
                        int.TryParse(Console.ReadLine(), out int idDel);
                        logic.Delete(idDel);
                        break;
                    case "3":
                        Console.Write("ID: ");
                        int.TryParse(Console.ReadLine(), out int idUpd);
                        Console.Write("Новое название: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("Новый автор: ");
                        var newAuthor = Console.ReadLine();
                        Console.Write("Новый жанр: ");
                        var newGenre = Console.ReadLine();
                        Console.Write("Новый рейтинг: ");
                        var newRaiting = Convert.ToInt32(Console.ReadLine());
                        logic.Update(idUpd, newTitle, newAuthor,newGenre, newRaiting);
                        break;
                    case "4":
                        foreach (var b in logic.GetAll())
                            Console.WriteLine($"ID: {b.Id} | Название: {b.Title} | Автор: {b.Author} | Жанр: {b.Genre} | Рейтинг: {b.Raiting}");
                        break;
                    case "5":
                        Console.Write("Автор: ");
                        var findAuthor = Console.ReadLine();
                        Console.WriteLine($"Книги автора: {findAuthor}");
                        foreach (var b in logic.FindByAuthor(findAuthor))
                         
                            Console.WriteLine($"ID: {b.Id} | Название: {b.Title}");
                        break;
                    case "6":
                        Console.WriteLine("\nЛучшие книги");
                        foreach (var book in logic.FindFantasyBooks())
                            Console.WriteLine($"Название: {book.Title} | Автор: {book.Author}");
                        break;
                    case "7":
                        Console.WriteLine("Рейтинг: ");
                        if (int.TryParse(Console.ReadLine(), out int Raiting))
                        {
                            var findRaitingBooks = logic.FindRaitingBooks(Raiting);
                            if (findRaitingBooks.Any())
                            {
                                Console.WriteLine($"Книги с рейтингом {Raiting}:");
                                foreach (var book in findRaitingBooks)
                                {
                                    Console.WriteLine($"- {book.Title} (Автор: {book.Author}, Жанр: {book.Genre}, Рейтинг: {book.Raiting})");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Книги с таким рейтингом не найдены.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод рейтинга.");
                        }
                        break;
                    case "8":
                        foreach (var g in logic.GroupByAuthor())
                            Console.WriteLine(g);
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}
