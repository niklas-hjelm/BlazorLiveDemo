namespace BlazorLiveDemo.Server.DataAccess;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(object id);
    Task<T> GetAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
}