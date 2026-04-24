using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.Models;
using MinhaApi.Infrastructure.Mappings;

namespace MinhaApi.Infrastructure.Data;

// O DBCONTEXT É A "PONTE" ENTRE O C# E O BANCO:
// O EF Core usa essa classe para saber quais tabelas existem
// e como transformar objetos em registros no PostgreSQL.
public class ConnectionContext : DbContext
{
    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
    {
    }

    // Cada DbSet normalmente representa uma tabela que o EF vai mapear.
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aqui o EF procura automaticamente todas as classes de configuração
        // (como EmployeeConfiguration) e aplica as regras de mapeamento.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
    }
}
