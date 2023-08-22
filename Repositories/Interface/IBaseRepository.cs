namespace qwerty_chat_api.Repositories.Interface
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(string id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T obj);
        Task UpdateAsync(string id, T obj);
        Task RemoveAsync(string id);
    }
}
