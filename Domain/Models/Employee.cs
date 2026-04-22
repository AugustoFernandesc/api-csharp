using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaApi.Domain.Models;

[Table("employee")]
public class Employee
{
    [Key]

    public int id { get; private set; }

    public string name { get; private set; }

    public string email { get; private set; }

    public string password { get; private set; }

    public int age { get; private set; }

    public string? photo { get; private set; }

    public Employee(string name, string email, string password, int age, string? photo)
    {

        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.email = email;
        this.password = password;
        this.age = age;
        this.photo = photo;

    }

    public void UpdateData(string name, string email, int age, string? password = null)
    {
        this.name = name;
        this.email = email;
        this.age = age;

        if (!string.IsNullOrEmpty(password))
        {
            this.password = password;
        }
    }
}
