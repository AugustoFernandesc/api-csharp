using MinhaApi.Domain.Models;

namespace MinhaApi.Domain.Interfaces;

// O TOKEN SERVICE É O CONTRATO DO "EMISSOR DE CRACHÁ":
// Quem implementar essa interface deve saber transformar um funcionário em JWT.
public interface ITokenService
{
    string GenerateToken(Employee employee);
}
