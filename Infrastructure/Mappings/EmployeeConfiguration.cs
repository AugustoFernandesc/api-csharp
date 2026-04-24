using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaApi.Domain.Models;

namespace MinhaApi.Infrastructure.Mappings;

// ESSA CLASSE ENSINA AO EF COMO A ENTIDADE EMPLOYEE VIRA TABELA:
// Em vez de poluir a entidade com muitas regras de banco, o mapeamento fica separado aqui.
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Define o nome real da tabela no banco.
        builder.ToTable("employees");

        // Define a chave primária.
        builder.HasKey(x => x.id);

        // Configura coluna obrigatória e tamanho máximo.
        builder.Property(x => x.name)
        .IsRequired()
        .HasMaxLength(255);

        builder.Property(x => x.email)
        .IsRequired()
        .HasMaxLength(255);

        // Impede e-mails duplicados no banco.
        builder.HasIndex(x => x.email)
        .IsUnique()
        .HasDatabaseName("UQ_Employee_Email");

        builder.Property(x => x.password)
        .IsRequired()
        .HasMaxLength(255);

        builder.Property(x => x.age)
        .IsRequired();

        // A foto é opcional, por isso não exigimos IsRequired().
        builder.Property(x => x.photo);
    }
}
