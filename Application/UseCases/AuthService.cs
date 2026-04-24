using MinhaApi.Domain.Interfaces;
using MinhaApi.Domain.Models;

namespace MinhaApi.Application.UseCases;

// O AUTH SERVICE É O "SEGURANÇA DA PORTARIA":
// Ele não gerencia funcionários, ele apenas checa se quem está tentando entrar 
// é quem diz ser e se tem o "convite" (token) correto.
public class AuthService
{
    private readonly IUnitOfWork _uow; // O acesso aos arquivos da empresa (Banco de Dados).
    private readonly ITokenService _tokenService; // O "Carimbador": quem emite a pulseira VIP (Token JWT).

    public AuthService(IUnitOfWork uow, ITokenService tokenService)
    {
        _uow = uow;
        _tokenService = tokenService;
    }

    // AÇÃO: EXECUTAR O LOGIN
    // Entrada: As credenciais (E-mail e Senha).
    // Saída: Uma string (O Token) ou um erro se a casa cair.
    public async Task<string?> Execute(string email, string password)
    {
        // 1. O Segurança olha a lista de convidados:
        // Ele busca todo mundo no repositório para procurar o e-mail informado.
        var employees = await _uow.Repository<Employee>().Get();
        var employee = employees.FirstOrDefault(x => x.email == email);

        // 2. A HORA DA VERDADE (Validação):
        // Se o e-mail não existir (employee == null) OU se a senha digitada 
        // não bater com o "segredo trancado" no banco (BCrypt.Verify)...
        if (employee == null || !BCrypt.Net.BCrypt.Verify(password, employee.password))
        {
            // ...ele barra a entrada na hora! Esse "throw" manda o erro direto 
            // pro seu ThrowController avisar que o acesso foi negado (401).
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        // 3. ENTRADA LIBERADA:
        // Se passou por tudo, o TokenService cria uma "pulseira" (JWT) contendo 
        // os dados do funcionário. Esse código é o que o Front-end vai usar 
        // para provar que está logado nas próximas chamadas.
        return _tokenService.GenerateToken(employee);
    }
}
