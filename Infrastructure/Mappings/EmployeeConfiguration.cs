using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaApi.Domain.Models;

namespace MinhaApi.Infrastructure.Mappings;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(x => x.id);
        builder.Property(x => x.name)
        .IsRequired()
        .HasMaxLength(255);
        builder.Property(x => x.email)
        .IsRequired()
        .HasMaxLength(255);
        builder.HasIndex(x => x.email)
        .IsUnique()
        .HasDatabaseName("UQ_Employee_Email");
        builder.Property(x => x.password)
        .IsRequired()
        .HasMaxLength(255);
        builder.Property(x => x.age)
        .IsRequired();
        builder.Property(x => x.photo);
    }
}