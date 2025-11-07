using DataAccessLayer;
using DomainModels;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ninject это IoC - контейнер(Inversion of Control container) для платформы .NET, который автоматизирует процесс внедрения зависимостей (Dependency Injection).
// Он управляет созданием объектов и их зависимостями, упрощая код и делая его более гибким, слабее связанным. 
// Классы становятся независимыми друг от друга, что упрощает тестирование и поддержку
namespace ModelLogic
{
    /// <summary>
    /// Модуль конфигурации Ninject для зависимостей
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Загружает привязки зависимостей
        /// </summary>
        public override void Load()
        {
            // "Когда кто-то попросит IRepository<Book>, верни ему экземпляр EntityRepository<Book>"
            // InSingletonScope() означает, что будет создан один экземпляр на всё приложение.
            Bind<IRepository<Book>>().To<EntityRepository<Book>>().InSingletonScope();
            Bind<IBookLogic>().To<BookLogic>().InSingletonScope();

            // Раскоментировать строку чтобы выбрать реализацию через Даппер
            // Bind<IRepository<Book>>().To<DapperRepository<Book>>().InSingletonScope();
        }
    }
}
