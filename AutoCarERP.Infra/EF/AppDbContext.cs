using AutoCarERP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoCarERP.Infra.EF;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<ProdutoServico> ProdutosServicos => Set<ProdutoServico>();
    public DbSet<OrdemDeServico> OrdensDeServico => Set<OrdemDeServico>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=AutoCarERP;Username=postgres;Password=postgres"
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
