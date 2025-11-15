using AutoCarERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoCarERP.Infra.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Codigo);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(c => c.Telefone)
            .HasMaxLength(15);

        builder.Property(c => c.CpfCnpj)
            .HasMaxLength(15);

        builder.Property(c => c.Endereco)
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .HasMaxLength(50);
    }
}
