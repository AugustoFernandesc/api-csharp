using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.Models;
using MinhaApi.Infrastructure.Mappings;

namespace MinhaApi.Infrastructure.Data;

public class ConnectionContext : DbContext
{
    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
    }
}
