using Byte_Backend.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Dados;

public class ByteDbContext : DbContext
{
    public ByteDbContext(DbContextOptions<ByteDbContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Pessoa>().HasIndex(p => p.CPF).IsUnique();
        modelBuilder.Entity<Usuario>().HasIndex(u => u.Login).IsUnique();
    }
}