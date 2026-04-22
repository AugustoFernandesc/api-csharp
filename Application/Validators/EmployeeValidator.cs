using FluentValidation;
using MinhaApi.Application.ViewModel;

public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Nome e obrigatorio");

        RuleFor(x => x.Email)
        .NotEmpty()
        .WithMessage("Email e obrigatorio")
        .EmailAddress()
        .WithMessage("Email invalido! Cade o @ e o dominio?");

        RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .WithMessage("Senha deve ter no minimo 8 caracteres");

        RuleFor(x => x.Age)
        .InclusiveBetween(18, 100)
        .WithMessage("Idade deve ser entre 18 e 100");

    }
}