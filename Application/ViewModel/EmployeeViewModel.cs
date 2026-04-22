namespace MinhaApi.Application.ViewModel;

//viewModel e o DTO
// ela e uma classe que define exatamente o que a API espera receber do mundo externo(swagger, front)
//em comparacao com o nest, usaria o class validator(@IsNoEmpity), no c# e required
public class EmployeeViewModel
{
    //Garante que o .NET barra a requisição se o nome vier vazio.
    public required string Name { get; set; }

    //O .NET já tenta converter o que vier do formulário para um número inteiro.
    public int Age { get; set; }

    //Esta é a chave! Ela diz ao .NET: "Prepare-se para receber um fluxo de bytes (o arquivo) e não apenas um texto". É isso que faz o Swagger mostrar aquele botão de "Fazer upload de arquivo".
    public required IFormFile Photo { get; set; }
}
