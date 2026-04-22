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
        var token = await authService.Execute(login.username, login.age);

        if (token == null)
        {
            return Unauthorized(new { message = "Usuário ou idade inválidos" });
        }

        return Ok(new { token = token });
    }
}

public class LoginRequest
{
    public string username { get; set; }
    public int age { get; set; }
}