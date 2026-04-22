using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;

namespace MinhaApi.Infrastructure.Services;

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

        var secretKey = _configuration["JwtSettings:SecretKey"];
        var key = Encoding.ASCII.GetBytes(secretKey);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, employee.name),
                new Claim("employeeId", employee.id.ToString()),
                new Claim(ClaimTypes.Role, "Employee")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
