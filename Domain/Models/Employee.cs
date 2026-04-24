using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaApi.Domain.Models;

// A ENTIDADE É O "FUNCIONÁRIO REAL" DO SISTEMA:
// Ela representa o dado importante do negócio do jeito que a aplicação enxerga,
// não do jeito que o front envia nem do jeito que o banco necessariamente salva.
[Table("employee")]
public class Employee
{
    // O Guid é o "RG único" do funcionário.
    public Guid id { get; private set; }

    // private set = ninguém de fora sai alterando tudo livremente.
    // A ideia é forçar mudanças por métodos controlados da própria entidade.
    public string name { get; private set; }

    public string email { get; private set; }

    public string password { get; private set; }

    public int age { get; private set; }

    public string? photo { get; private set; }

    // O construtor é a "porta de entrada oficial" para criar um funcionário novo.
    // Aqui a entidade já nasce com os dados mínimos necessários.
    public Employee(string name, string email, string password, int age, string? photo)
    {
        // Se o nome vier nulo, a entidade nem deixa nascer.
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.email = email;
        this.password = password;
        this.age = age;
        this.photo = photo;
    }

    // Esse método centraliza a atualização dos dados do funcionário.
    // Em vez de liberar set público, a entidade decide como pode ser alterada.
    public void UpdateData(string name, string email, int age, string? password = null)
    {
        this.name = name;
        this.email = email;
        this.age = age;

        // Só troca a senha se uma nova senha realmente vier preenchida.
        if (!string.IsNullOrEmpty(password))
        {
            this.password = password;
        }
    }
}
