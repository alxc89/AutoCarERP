using AutoCarERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoCarERP.Infra.Configuration;

public class OrdemDeServicoConfiguration : IEntityTypeConfiguration<OrdemDeServico>
{
    public void Configure(EntityTypeBuilder<OrdemDeServico> builder)
    {
        builder.ToTable("OrdemDeServico");

        builder.HasKey(os => os.Codigo);

        builder.Property(os => os.HoraAbertura)
            .IsRequired();

        builder.Property(os => os.Veiculo)
            .HasMaxLength(15);

        builder.Property(os => os.Cliente)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(os => os.ProdutoServico)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(os => os.Quantidade)
            .IsRequired();

        builder.Property(os => os.ValorUnitario)
            .IsRequired()
            .HasColumnType("decimal(7,2)");

        builder.Property(os => os.ValorTotal)
            .IsRequired()
            .HasColumnType("decimal(7,2)");

        builder.Property(os => os.Observacao)
            .HasMaxLength(50);

        builder.Property(os => os.Status)
            .IsRequired()
            .HasMaxLength(15);
    }
}
