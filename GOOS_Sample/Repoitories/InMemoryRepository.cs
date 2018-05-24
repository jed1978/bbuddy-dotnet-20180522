using System;
using System.Collections.Generic;
using GOOS_Sample.Entities;

namespace GOOS_Sample.Repoitories
{
    public class InMemoryRepository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private static readonly Dictionary<TKey, T> Repository = new Dictionary<TKey, T>();

        public void Save(T entity)
        {
            Repository.Add(entity.Id, entity);
        }

        public T Get(TKey Id)
        {
            T entity;
            Repository.TryGetValue(Id, out entity);
            return entity;
        }

        public List<T> GetList(TKey start, TKey end)
        {
            throw new NotImplementedException();
        }
    }
}