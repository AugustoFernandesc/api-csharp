namespace MinhaApi.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> Get();
    Task<T?> GetById(int id);
    Task Add(T entity);
    void Update(T entity);
    Task Delete(int id);
}
