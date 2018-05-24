using System.Collections.Generic;
using GOOS_Sample.Entities;

namespace GOOS_Sample.Repoitories
{
    public interface IRepository<T, TKey> where T: BaseEntity<TKey>
    {
        void Save(T entity);
        T Get(TKey Id);
        List<T> GetList(TKey start, TKey end);
    }
}