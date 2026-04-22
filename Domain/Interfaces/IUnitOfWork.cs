namespace MinhaApi.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{

    IEmployeeRepository EmployeeRepository { get; }
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> Commit();
}
