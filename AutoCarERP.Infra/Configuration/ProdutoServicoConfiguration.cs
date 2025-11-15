using AutoCarERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoCarERP.Infra.Configuration;

public class ProdutoServicoConfiguration : IEntityTypeConfiguration<ProdutoServico>
{
    public void Configure(EntityTypeBuilder<ProdutoServico> builder)
    {
        builder.ToTable("ProdutoServico");

        builder.HasKey(ps => ps.Codigo);

        builder.Property(ps => ps.Nome)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(ps => ps.Descricao)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ps => ps.Fornecedor)
            .HasMaxLength(30);

        builder.Property(ps => ps.Custo)
            .HasColumnType("decimal(7,2)");

        builder.Property(ps => ps.Valor)
            .IsRequired()
            .HasColumnType("decimal(7,2)");
    }
}
