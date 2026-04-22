using MinhaApi.Domain.Interfaces;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ConnectionContext _context;
    private IEmployeeRepository? _employeeRepository;

    public UnitOfWork(ConnectionContext context)
    {
        _context = context;
    }

    public IEmployeeRepository EmployeeRepository =>
        _employeeRepository ??= new EmployeeRepository(_context);

    public IGenericRepository<T> Repository<T>() where T : class
    {
        return new GenericRepository<T>(_context);
    }

    public async Task<int> Commit() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
