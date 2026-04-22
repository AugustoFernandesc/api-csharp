namespace MinhaApi.Application.UseCases;

using MinhaApi.Domain.Models;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Service;
using MinhaApi.Application.ViewModel;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _uow;
    public EmployeeService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException();
    }
    public async Task Add(EmployeeViewModel dto)
    {

        string? filePath = null;
        if (dto.Photo != null)
        {
            if (!Directory.Exists("Storage"))
            {
                Directory.CreateDirectory("Storage");
            }

            filePath = Path.Combine("Storage", dto.Photo.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                await dto.Photo.CopyToAsync(fileStream);
        }


        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var employee = new Employee(dto.Name, dto.Email, passwordHash, dto.Age, filePath);

        await _uow.Repository<Employee>().Add(employee);

        await _uow.Commit();
    }
    public async Task<byte[]?> GetEmployeePhoto(int id)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);
        if (employee == null || string.IsNullOrEmpty(employee.photo))
            return null;

        return System.IO.File.ReadAllBytes(employee.photo);
    }
    public async Task<List<EmployeeDTO>> Get()
    {
        var employees = await _uow.Repository<Employee>().Get();

        return employees.Select(e => new EmployeeDTO
        {
            Id = e.id,
            Name = e.name,
            Email = e.email,
            Age = e.age
        }).ToList();
    }

    public async Task Update(int id, EmployeeViewModel dto)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);
        if (employee == null)
        {
            throw new Exception("Funcionario nao encontrado");
        }

        string? passwordHash = !string.IsNullOrEmpty(dto.Password)
        ? BCrypt.Net.BCrypt.HashPassword(dto.Password)
        : null;

        employee.UpdateData(dto.Name, dto.Email, dto.Age, passwordHash);

        _uow.Repository<Employee>().Update(employee);
        await _uow.Commit();

    }

    public async Task Delete(int id)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);

        if (employee == null)
        {
            throw new Exception("Funcionario nao encontrado");
        }


        await _uow.Repository<Employee>().Delete(id);
        await _uow.Commit();
    }
}