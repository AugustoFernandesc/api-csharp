using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories;

// IMPLEMENTAÇÃO DO REPOSITÓRIO GENÉRICO:
// Aqui moram as operações básicas de banco reaproveitáveis por qualquer entidade.
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    // O contexto é o "canal aberto" com o banco via EF Core.
    protected readonly ConnectionContext _context;

    public GenericRepository(ConnectionContext context)
    {
        _context = context;
    }

    // Traz todos os registros da tabela referente ao tipo T.
    public async Task<List<T>> Get() => await _context.Set<T>().ToListAsync();

    // Procura pelo id usando o mecanismo do EF Core.
    public async Task<T?> GetById(Guid id) => _context.Set<T>().Find(id);

    // Adiciona a entidade ao contexto, mas ainda não grava no banco sem Commit().
    public async Task Add(T entity) => await _context.Set<T>().AddAsync(entity);

    // Marca a entidade como modificada para o EF persistir depois.
    public void Update(T entity) => _context.Set<T>().Update(entity);

    public async Task Delete(Guid id)
    {
        // Primeiro localiza o registro no banco/contexto.
        var entity = await _context.Set<T>().FindAsync(id);

        // Só remove se realmente encontrou alguma coisa.
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
