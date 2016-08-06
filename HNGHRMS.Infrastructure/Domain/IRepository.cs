using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNGHRMS.Infrastructure.Domain
{
    public interface IRepository<T, TId> : IReadOnlyRepository<T, TId> where T : IAggregateRoot
    {
        void Save(T entity);
        void Add(T entity);
        void Remove(T entity);
    }
}
