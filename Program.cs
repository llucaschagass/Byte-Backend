using Byte_Backend.Dados;
using Byte_Backend.Interfaces;
using Byte_Backend.Repositories;
using Byte_Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ByteDbContext>(options =>
    options.UseNpgsql(connectionString));

// Injeção de Dependência - Repositories
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

// Injeção de Dependência - Services
builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<FuncionarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Byte API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();