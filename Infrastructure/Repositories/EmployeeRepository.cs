using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{

    public EmployeeRepository(ConnectionContext context) : base(context)
    {
    }

    public List<Employee> GetEmployeesPhoto()
    {
        return _context.Employees
            .Where(e => string.IsNullOrEmpty(e.photo))
            .ToList();
    }


}
