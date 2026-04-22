using MinhaApi.Domain.Models;

namespace MinhaApi.Domain.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{

    List<Employee> GetEmployeesPhoto();

}
