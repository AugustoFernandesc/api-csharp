namespace MinhaApi.Application.ViewModel;

// O DTO É A "VERSÃO SEGURA DE SAÍDA":
// Ele define o que a API entrega para fora sem expor dados sensíveis, como senha.
public class EmployeeDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }
}
