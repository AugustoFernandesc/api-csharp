using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Domain.Exceptions;

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
        var exception = exceptionHandlerFeature.Error;

        // SE O ERRO FOR DO TIPO 'EntityNotFoundException', RETORNA 404
        if (exception is EntityNotFoundException)
        {
            return Problem(
                detail: exception.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso não encontrado");
        }

        // Se for qualquer outro erro, mantém o padrão
        return Problem(
        detail: exception.StackTrace,
        title: exception.Message);
    }
}
