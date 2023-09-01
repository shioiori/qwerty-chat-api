namespace qwerty_chat_api.Domain.Services.Interface
{
    public interface IBaseService<T>
    {
        Task<T> GetAsync(string id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T obj);
        Task UpdateAsync(string id, T obj);
        Task RemoveAsync(string id);
    }
}
