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
    public DbSet<CartaoComanda> CartoesComanda { get; set; }
    public DbSet<Comanda> Comandas { get; set; }
    public DbSet<ItemComanda> ItensComanda { get; set; }
    public DbSet<FilaCozinha> FilaCozinha { get; set; }

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

        // Configurações de CartaoComanda
        modelBuilder.Entity<CartaoComanda>().HasIndex(cc => cc.NumeroCartao).IsUnique();
        modelBuilder.Entity<CartaoComanda>().HasIndex(cc => cc.CodigoRfid).IsUnique();
        modelBuilder.Entity<CartaoComanda>().Property(cc => cc.CodigoRfid).IsRequired();

        // Configurações de Comanda
        modelBuilder.Entity<Comanda>().Property(c => c.Status).IsRequired();
        modelBuilder.Entity<Comanda>()
            .HasOne(c => c.Cartao)
            .WithMany(cc => cc.Comandas)
            .HasForeignKey(c => c.CartaoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Comanda>()
            .HasOne(c => c.Cliente)
            .WithMany()
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurações de ItemComanda
        modelBuilder.Entity<ItemComanda>().Property(ic => ic.PrecoUnitario).HasPrecision(18, 2);
        modelBuilder.Entity<ItemComanda>()
            .HasOne(ic => ic.Comanda)
            .WithMany(c => c.Itens)
            .HasForeignKey(ic => ic.ComandaId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ItemComanda>()
            .HasOne(ic => ic.Produto)
            .WithMany()
            .HasForeignKey(ic => ic.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ItemComanda>()
            .HasOne(ic => ic.OpcaoProduto)
            .WithMany()
            .HasForeignKey(ic => ic.OpcaoProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurações de FilaCozinha
        modelBuilder.Entity<FilaCozinha>().Property(fk => fk.StatusPreparo).IsRequired();
        modelBuilder.Entity<FilaCozinha>()
            .HasOne(fk => fk.ItemComanda)
            .WithOne(ic => ic.FilaCozinha)
            .HasForeignKey<FilaCozinha>(fk => fk.ItemComandaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}