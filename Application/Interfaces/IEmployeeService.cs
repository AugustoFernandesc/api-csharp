using MinhaApi.Application.ViewModel;

namespace MinhaApi.Service;

//usa I antes do nome da classe pra dizer que e Interface
//Imagine que a Interface é o Menu do Restaurante. O cliente (Controller) escolhe o prato Add,
//mas ele não sabe como o cozinheiro (Service) vai fritar o ovo ou cortar a carne. 
//Ele só sabe que, se pedir Add e entregar um EmployeeViewModel, o trabalho será feito.
public interface IEmployeeService
{
    //Entrada: Um ViewModel (DTO). É o formulário limpo que veio da web.
    //Saída: void (nada). Significa: "Apenas faça o seu trabalho de salvar, não preciso de resposta agora".
    Task Add(EmployeeViewModel employeeViewModel);

    //Saída: Uma lista de objetos Employee.
    //Lógica: O Service vai no Repository, pega o que está no Postgres e entrega para o Controller.
    Task<List<EmployeeDTO>> Get();

    //Entrada: Um int id.
    // Saída: byte[]? (um array de bytes que pode ser nulo).
    Task<byte[]?> GetEmployeePhoto(int id);

    Task Update(int id, EmployeeViewModel dto);

    Task Delete(int id);
}
