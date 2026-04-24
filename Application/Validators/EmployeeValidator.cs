using FluentValidation;
using MinhaApi.Application.ViewModel;

// O VALIDATOR É O "PORTEIRO DOS DADOS":
// Antes da regra de negócio rodar, ele verifica se o que chegou faz sentido.
public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
{
    public EmployeeValidator()
    {
        // Nome não pode vir vazio.
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Nome e obrigatorio");

        // E-mail precisa existir e seguir formato válido.
        RuleFor(x => x.Email)
        .NotEmpty()
        .WithMessage("Email e obrigatorio")
        .EmailAddress()
        .WithMessage("Email invalido! Cade o @ e o dominio?");

        // Senha precisa ter pelo menos 8 caracteres.
        RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .WithMessage("Senha deve ter no minimo 8 caracteres");

        // Exemplo de regra de negócio simples para idade aceitável.
        RuleFor(x => x.Age)
        .InclusiveBetween(18, 100)
        .WithMessage("Idade deve ser entre 18 e 100");
    }
}
