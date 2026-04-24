namespace MinhaApi.Application.ViewModel;

// O VIEWMODEL É O "FORMULÁRIO DE ENTRADA" DA API:
// Ele define exatamente o que o mundo externo (front, Swagger, Postman)
// pode mandar para dentro da aplicação.
public class EmployeeViewModel
{
    // required ajuda a exigir que o campo exista na entrada.
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    // O .NET já tenta converter o que vier do formulário para inteiro.
    public int Age { get; set; }

    // IFormFile representa um arquivo enviado em multipart/form-data.
    // É isso que permite upload de imagem pelo Swagger ou front.
    public IFormFile? Photo { get; set; }
}
