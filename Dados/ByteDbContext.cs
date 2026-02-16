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
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<OpcaoProduto> OpcoesProdutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Pessoa>().HasIndex(p => p.CPF).IsUnique();
        modelBuilder.Entity<Usuario>().HasIndex(u => u.Login).IsUnique();
        
        // Configurações de Categoria
        modelBuilder.Entity<Categoria>().Property(c => c.Nome).IsRequired();

        // Configurações de Produto
        modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(18, 2); // Define precisão para decimal

        // Configurações de OpcaoProduto
        modelBuilder.Entity<OpcaoProduto>().Property(op => op.Nome).IsRequired();
        modelBuilder.Entity<OpcaoProduto>().Property(op => op.PrecoAdicional).HasPrecision(18, 2); // Define precisão para decimal
    }
}