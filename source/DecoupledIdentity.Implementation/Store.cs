using DecoupledIdentity.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecoupledIdentity.InMemory
{
    public abstract class Store<T>
        where T : CoreEntity
    {
        private readonly IList<T> _entities = new List<T>();

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public T Get(string id)
            => _entities.FirstOrDefault(e => e.Id == id);

        public IEnumerable<T> Find(Func<T, bool> pred)
            => _entities.Where(pred);
    }
}
