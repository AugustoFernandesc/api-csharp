using MinhaApi.Application.ViewModel;

namespace MinhaApi.Service;

// A INTERFACE É O CONTRATO: 
// Ela dita as regras de O QUE deve ser feito, mas não se mete em COMO será feito. 
// Se o Service assinar esse contrato, ele é obrigado a entregar todos esses métodos.
public interface IEmployeeService
{
    // AÇÃO: CADASTRAR (Create)
    // Entrada: O "Formulário de Inscrição" (ViewModel) preenchido pelo usuário.
    // Saída: Task (Uma promessa de entrega). O Controller entrega os dados e o 
    // Service promete que vai processar tudo (salvar foto, fazer hash da senha e guardar no banco).
    Task Add(EmployeeViewModel employeeViewModel);

    // AÇÃO: LISTAR TUDO (Read)
    // Saída: Uma "Vitrine Segura" (Lista de DTOs).
    // Lógica: O Service busca os dados brutos no banco, mas só entrega o que o cliente 
    // pode ver (Id, Nome, etc.), escondendo segredos como a senha.
    Task<List<EmployeeDTO>> Get();

    // AÇÃO: BUSCAR FOTO
    // Entrada: O "Número do Crachá" (id).
    // Saída: A foto em "Código Binário" (byte[]). Se o cara não tiver foto, 
    // o "?" diz que o Service pode devolver "nada" (null) sem quebrar o sistema.
    Task<byte[]?> GetEmployeePhoto(Guid id);

    // AÇÃO: ATUALIZAR (Update)
    // Entrada: O "ID do Alvo" e os "Novos Dados".
    // Lógica: O Service localiza o funcionário antigo e usa o método UpdateData 
    // (aquele com private set que a gente criou) para aplicar as mudanças com segurança.
    Task Update(Guid id, EmployeeViewModel dto);

    // AÇÃO: DEMITIR/REMOVER (Delete)
    // Entrada: O "ID do Alvo".
    // Lógica: É o "caminhão de lixo". O Service verifica se o cara existe no banco, 
    // se existir, ele dá a ordem pro repositório remover e pro Unit of Work confirmar a saída.
    Task Delete(Guid id);
}
