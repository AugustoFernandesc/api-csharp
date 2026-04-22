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
        var filePath = Path.Combine("Storage", dto.Photo.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Photo.CopyToAsync(fileStream);
        }


        var employee = new Employee(dto.Name, dto.Age, filePath);

        await _uow.Repository<Employee>().Add(employee);

        await _uow.Commit();
    }
    public async Task<byte[]?> GetEmployeePhoto(int id)
    {
        var employee = await _uow.Repository<Employee>().Get(id);
        if (employee == null || string.IsNullOrEmpty(employee.photo))
            return null;

        return System.IO.File.ReadAllBytes(employee.photo);
    }
    public async Task<List<Employee>> Get()
    {
        return await _uow.Repository<Employee>().Get();
    }
}