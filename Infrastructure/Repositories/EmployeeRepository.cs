using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;
using MinhaApi.Infrastructure.Data;

namespace MinhaApi.Infrastructure.Repositories;

// ESSE REPOSITÓRIO É O "SETOR DE FUNCIONÁRIOS" DO BANCO:
// Ele herda as operações básicas do GenericRepository e pode ter filtros próprios.
public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ConnectionContext context) : base(context)
    {
    }

    public List<Employee> GetEmployeesPhoto()
    {
        // Aqui fica uma consulta especializada da entidade Employee.
        // Hoje ela busca funcionários sem foto cadastrada.
        return _context.Employees
            .Where(e => string.IsNullOrEmpty(e.photo))
            .ToList();
    }
}
