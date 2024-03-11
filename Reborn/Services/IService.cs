namespace Reborn.Services
{
    public interface IService<T>
    {
        public Task<T> Create(T model);
        public Task<List<T>?> FindAll();
        public Task<T?> FindOneById(int id);
        public Task<T?> Update(int id, T model);
        public Task<T?> Remove(int id);
    }
}
