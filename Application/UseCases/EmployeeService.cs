namespace MinhaApi.Application.UseCases;

using MinhaApi.Domain.Models;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Service;
using MinhaApi.Application.ViewModel;
using MinhaApi.Domain.Exceptions;

// O EMPLOYEE SERVICE É O "GERENTE DE FUNCIONÁRIOS":
// O Controller entrega a solicitação, e esse service decide como executar a regra de negócio.
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _uow;

    public EmployeeService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException();
    }

    // CADASTRO COMPLETO:
    // Aqui juntamos upload da foto, hash da senha, criação da entidade e gravação no banco.
    public async Task Add(EmployeeViewModel dto)
    {
        // Começa sem foto. Se vier arquivo, esse caminho será preenchido.
        string? filePath = null;

        if (dto.Photo != null)
        {
            // Garante que a pasta exista antes de tentar salvar o arquivo.
            if (!Directory.Exists("Storage"))
            {
                Directory.CreateDirectory("Storage");
            }

            // Monta o caminho físico do arquivo dentro da pasta Storage.
            filePath = Path.Combine("Storage", dto.Photo.FileName);

            // Copia os bytes enviados pelo formulário para um arquivo no disco.
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Photo.CopyToAsync(fileStream);
            }
        }

        // Nunca salvamos senha pura. Antes, ela vira hash com BCrypt.
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Agora montamos a entidade de domínio com os dados já tratados.
        var employee = new Employee(dto.Name, dto.Email, passwordHash, dto.Age, filePath);

        // Envia para o repositório e confirma a transação.
        await _uow.Repository<Employee>().Add(employee);
        await _uow.Commit();
    }

    // BUSCA DA FOTO:
    // Retorna os bytes da imagem para o controller devolver como arquivo.
    public async Task<byte[]?> GetEmployeePhoto(Guid id)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);

        // Se não existe funcionário ou não existe caminho da foto, devolve null.
        if (employee == null || string.IsNullOrEmpty(employee.photo))
        {
            throw new EntityNotFoundException("Funcionario ou foto nao encontrado");
        }

        // Lê o arquivo salvo em disco e entrega seu conteúdo em bytes.
        return System.IO.File.ReadAllBytes(employee.photo);
    }

    // LISTAGEM SEGURA:
    // Busca entidades completas, mas converte para DTO para não expor senha.
    public async Task<List<EmployeeDTO>> Get()
    {
        var employees = await _uow.Repository<Employee>().Get();

        // O Select transforma cada entidade em um objeto seguro para a resposta da API.
        return employees.Select(e => new EmployeeDTO
        {
            Id = e.id,
            Name = e.name,
            Email = e.email,
            Age = e.age
        }).ToList();
    }

    // ATUALIZAÇÃO:
    // Primeiro procura o funcionário, depois aplica as mudanças e salva.
    public async Task Update(Guid id, EmployeeViewModel dto)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);
        if (employee == null)
        {
            throw new EntityNotFoundException("Funcionario nao encontrado para atualizacaoo.");
        }

        // Se vier nova senha, ela é reprocessada em hash.
        // Se não vier, mantemos a senha antiga.
        string? passwordHash = !string.IsNullOrEmpty(dto.Password)
        ? BCrypt.Net.BCrypt.HashPassword(dto.Password)
        : null;

        // A própria entidade atualiza seus dados de forma controlada.
        employee.UpdateData(dto.Name, dto.Email, dto.Age, passwordHash);

        _uow.Repository<Employee>().Update(employee);
        await _uow.Commit();
    }

    // REMOÇÃO:
    // Confere se o funcionário existe antes de mandar apagar.
    public async Task Delete(Guid id)
    {
        var employee = await _uow.Repository<Employee>().GetById(id);

        if (employee == null)
        {
            throw new EntityNotFoundException("Funcionario nao encontrado");
        }

        await _uow.Repository<Employee>().Delete(id);
        await _uow.Commit();
    }
}
