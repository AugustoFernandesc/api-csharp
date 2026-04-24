
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers;

// ESSE CONTROLLER É O "TRADUTOR DE ERROS":
// Quando algo estoura na aplicação, ele transforma a exceção em resposta HTTP.
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThrowController : ControllerBase
{
    // Ambiente de produção: devolve um Problem genérico sem vazar detalhes.
    [Route("/error")]
    public IActionResult HandleError() =>
    Problem();

    // Ambiente de desenvolvimento: mostra detalhes da exceção para facilitar debug.
    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
        // Segurança extra: esse endpoint detalhado só deve existir em desenvolvimento.
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        if (exceptionHandlerFeature == null) return Problem();

        // Devolve mensagem e stack trace para facilitar descobrir o problema.
        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }
}
