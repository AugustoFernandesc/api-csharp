using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.ViewModel;
using MinhaApi.Service;

namespace MinhaApi.Controllers;

// O CONTROLLER É A "PORTA DE ENTRADA HTTP":
// Ele recebe requisições, chama o service certo e devolve resposta HTTP.
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException();
    }

    // POST /api/employee
    // Recebe os dados do formulário, valida e manda cadastrar.
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] EmployeeViewModel employeeView, [FromServices] IValidator<EmployeeViewModel> validator)
    {
        // Roda o FluentValidation manualmente para devolver erro amigável.
        var validationResult = await validator.ValidateAsync(employeeView);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _employeeService.Add(employeeView);
        return Ok();
    }

    // [Authorize]
    // GET /api/employee/{id}/download
    // Faz o download da foto do funcionário.
    [HttpGet]
    [Route("{id}/download")]
    public async Task<IActionResult> DownLoadPhoto(Guid id)
    {
        var dataBytes = await _employeeService.GetEmployeePhoto(id);

        if (dataBytes == null)
        {
            return NotFound("Funcionário ou foto não encontrados");
        }

        return File(dataBytes, "image/jpeg");
    }

    // [Authorize]
    // GET /api/employee
    // Lista todos os funcionários.
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employess = await _employeeService.Get();
        return Ok(employess);
    }

    // [Authorize]
    // PUT /api/employee/{id}
    // Atualiza os dados de um funcionário específico.
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] EmployeeViewModel employeeView)
    {
        await _employeeService.Update(id, employeeView);
        return Ok("Atualizado com sucesso");
    }

    // [Authorize]
    // DELETE /api/employee/{id}
    // Remove o funcionário informado.
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _employeeService.Delete(id);
        return NoContent();
    }
}
