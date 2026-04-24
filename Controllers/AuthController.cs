using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.UseCases;

namespace MinhaApi.Controllers;

// ESSE CONTROLLER CUIDA DA ENTRADA NO SISTEMA:
// Ele recebe login e devolve um token JWT quando as credenciais estão corretas.
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    // POST /api/v1/auth
    // Recebe email e senha e tenta autenticar o usuário.
    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] LoginRequest login, [FromServices] AuthService authService)
    {
        var token = await authService.Execute(login.email, login.password);

        // Esse trecho é uma proteção extra caso o service devolva null.
        if (token == null)
        {
            return Unauthorized(new { message = "Email ou senha inválidos" });
        }

        return Ok(new { token = token });
    }
}

// Esse objeto representa o corpo JSON esperado no login.
public class LoginRequest
{
    public required string email { get; set; }
    public required string password { get; set; }
}
