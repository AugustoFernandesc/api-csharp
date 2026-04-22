using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.ViewModel;
using MinhaApi.Service;


namespace MinhaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException();
    }

    [HttpPost]
    public IActionResult Add([FromForm] EmployeeViewModel employeeView)
    {
        _employeeService.Add(employeeView);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("{id}/download")]

    public async Task<IActionResult> DownLoadPhoto(int id)
    {
        var dataBytes = await _employeeService.GetEmployeePhoto(id);

        if (dataBytes == null)
        {
            return NotFound("Funcionário ou foto não encontrados");
        }

        return File(dataBytes, "image/jpeg");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employess = await _employeeService.Get();
        return Ok(employess);
    }


}
