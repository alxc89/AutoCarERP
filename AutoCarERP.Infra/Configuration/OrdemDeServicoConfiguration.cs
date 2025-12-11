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

        builder.Property(os => os.VeiculoId)
            .IsRequired();

        builder.Property(os => os.ClienteId)
            .IsRequired();

        builder.Property(os => os.ProdutoServicoId)
            .IsRequired();

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

        builder.HasOne(os => os.Veiculo)
            .WithMany()
            .HasForeignKey(os => os.VeiculoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(os => os.Cliente)
            .WithMany()
            .HasForeignKey(os => os.ClienteId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(os => os.ProdutoServico)
            .WithMany()
            .HasForeignKey(os => os.ProdutoServicoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Auto-include navigation properties
        builder.Navigation(os => os.Cliente).AutoInclude();
        builder.Navigation(os => os.Veiculo).AutoInclude();
        builder.Navigation(os => os.ProdutoServico).AutoInclude();
    }
}
