using System;
using System.Collections.Generic;

namespace GOOS_Sample.Repoitories
{
    public class Repository<T, TKey> : IRepository<T, TKey>
    {
        public virtual void Save(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual T Get(TKey Id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList(TKey start, TKey end)
        {
            throw new NotImplementedException();
        }
    }
}