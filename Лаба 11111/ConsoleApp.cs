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
    class ConsoleApp
    {
        static void Main()
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            
            var logic = ninjectKernel.Get<Logic>();

            while (true)
            {
                Console.WriteLine("\n1. Добавить книгу\n2. Удалить книгу\n3. Обновить книгу\n4. Показать все\n5. Найти по автору\n6. Группировать по каждому автору\n0. Выход");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Название: ");
                        var title = Console.ReadLine();
                        Console.Write("Автор: ");
                        var author = Console.ReadLine();
                        if (title != "" && author != "")
                        {
                            logic.Add(title, author);
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
                        logic.Update(idUpd, newTitle, newAuthor);
                        break;
                    case "4":
                        foreach (var b in logic.GetAll())
                            Console.WriteLine($"ID: {b.Id} | Название: {b.Title} | Автор: {b.Author}");
                        break;
                    case "5":
                        Console.Write("Автор: ");
                        var findAuthor = Console.ReadLine();
                        Console.WriteLine($"Книги автора: {findAuthor}");
                        foreach (var b in logic.FindByAuthor(findAuthor))
                         
                            Console.WriteLine($"ID: {b.Id} | Название: {b.Title}");
                        break;
                    case "6":
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
