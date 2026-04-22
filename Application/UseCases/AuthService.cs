using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;

namespace MinhaApi.Application.UseCases;

public class AuthService
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork uow, ITokenService tokenService)
    {
        _uow = uow;
        _tokenService = tokenService;
    }

    public async Task<string?> Execute(string email, string password)
    {
        var employees = await _uow.Repository<Employee>().Get();
        var employee = employees.FirstOrDefault(x => x.email == email);

        if (employee == null || !BCrypt.Net.BCrypt.Verify(password, employee.password))
            return null;

        return _tokenService.GenerateToken(employee);
    }
}
