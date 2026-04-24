using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;

namespace MinhaApi.Infrastructure.Services;

// O TOKEN SERVICE É A "MÁQUINA DE GERAR CRACHÁ":
// Ele pega os dados do funcionário e cria um JWT assinado.
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Employee employee)
    {
        var handler = new JwtSecurityTokenHandler();

        // Busca a chave secreta do appsettings para assinar o token.
        var secretKey = _configuration["JwtSettings:SecretKey"]
            ?? throw new InvalidOperationException("JwtSettings:SecretKey não foi configurado.");
        var key = Encoding.ASCII.GetBytes(secretKey);

        // O descriptor define o conteúdo do token:
        // quem é o usuário, até quando vale e com qual chave será assinado.
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                // Claims são as "informações carregadas" dentro do token.
                new Claim(ClaimTypes.Name, employee.name),
                new Claim("employeeId", employee.id.ToString()),
                new Claim(ClaimTypes.Role, "Employee")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        // Cria o token em memória e depois converte para string.
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
