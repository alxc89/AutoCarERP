using AutoCarERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoCarERP.Infra.Configuration;

public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("Veiculo");

        builder.HasKey(v => v.Codigo);

        builder.Property(v => v.Placa)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(v => v.Placa)
            .IsUnique();

        builder.Property(v => v.Marca)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(v => v.Modelo)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(v => v.Cor)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(v => v.Ano)
            .IsRequired();
    }
}
