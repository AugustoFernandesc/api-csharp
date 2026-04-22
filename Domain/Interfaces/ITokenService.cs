using MinhaApi.Domain.Models;

namespace MinhaApi.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(Employee employee);
}
