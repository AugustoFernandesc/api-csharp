using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.UseCases;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] LoginRequest login, [FromServices] AuthService authService)
    {
        var token = await authService.Execute(login.email, login.password);

        if (token == null)
        {
            return Unauthorized(new { message = "Email ou senha inválidos" });
        }

        return Ok(new { token = token });
    }
}

public class LoginRequest
{
    public required string email { get; set; }
    public required string password { get; set; }
}