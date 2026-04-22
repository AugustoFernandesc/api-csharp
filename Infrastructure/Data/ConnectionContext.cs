using Microsoft.EntityFrameworkCore;
using MinhaApi.Domain.Models;

namespace MinhaApi.Infrastructure.Data;

public class ConnectionContext : DbContext
{
    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}
