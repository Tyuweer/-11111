using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Базовый интерфейс для всех доменных сущностей
    /// </summary>
    public interface IDomainObject
    {
        // гарантирует, что у всех сущностей есть Id при реализации
        int Id { get; set; }
    }
}
