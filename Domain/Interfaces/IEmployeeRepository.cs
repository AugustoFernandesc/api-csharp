using MinhaApi.Domain.Models;

namespace MinhaApi.Domain.Interfaces;

// ESSE É O REPOSITÓRIO ESPECIALIZADO DE FUNCIONÁRIOS:
// Ele herda o kit padrão do repositório genérico e pode ganhar consultas próprias.
public interface IEmployeeRepository : IGenericRepository<Employee>
{
    // Exemplo de consulta específica da entidade Employee.
    // Quando uma regra faz sentido só para funcionário, ela mora aqui.
    List<Employee> GetEmployeesPhoto();
}
