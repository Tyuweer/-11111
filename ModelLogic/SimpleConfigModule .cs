using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using DataAccessLayer;
using DomainModels;

namespace ModelLogic
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            // "Когда кто-то попросит IRepository<Book>, верни ему экземпляр EntityRepository<Book>"
            // InSingletonScope() означает, что будет создан один экземпляр на всё приложение.
            Bind<IRepository<Book>>().To<EntityRepository<Book>>().InSingletonScope();

            // Bind<IRepository<Book>>().To<DapperRepository<Book>>().InSingletonScope();
        }
    }
}
