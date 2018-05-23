using System.Collections.Generic;

namespace GOOS_Sample.Repoitories
{
    public interface IRepository<T, TKey>
    {
        void Save(T entity);
        T Get(TKey Id);
        List<T> GetList(TKey start, TKey end);
    }
}