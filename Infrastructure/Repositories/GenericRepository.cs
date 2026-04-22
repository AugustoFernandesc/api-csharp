using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ConnectionContext _context;

    public GenericRepository(ConnectionContext context)
    {
        _context = context;
    }


    public async Task<List<T>> Get() => await _context.Set<T>().ToListAsync();

    public async Task<T?> Get(int id) => _context.Set<T>().Find(id);

    public async Task Add(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public async Task Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }

}
