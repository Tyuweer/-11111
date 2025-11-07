using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLogic
{
    /// <summary>
    /// Полный интерфейс бизнес-логики для работы с книгами
    /// </summary>
    public interface IBookLogic : IBookCommandLogic, IBookQueryLogic
    {
    }
}
