namespace GOOS_Sample.Repoitories
{
    public interface IRepository<T, TKey>
    {
        void Save(T entity);
        T Get(TKey Id);
    }
}