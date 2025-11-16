using AutoCarERP.Application.Services.Cliente;
using AutoCarERP.Application.Services.Veiculo;
using AutoCarERP.Application.Services.ProdutoServico;
using AutoCarERP.Application.Services.OrdemDeServico;
using AutoCarERP.Core.Repositories;
using AutoCarERP.Infra.EF;
using AutoCarERP.Infra.EF.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IProdutoServicoService, ProdutoServicoService>();
builder.Services.AddScoped<IOrdemDeServicoService, OrdemDeServicoService>();
builder.Services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
