using Microsoft.EntityFrameworkCore;
// using Byte_Backend.Entidades; // Comentado porque ainda não existe

namespace Byte_Backend.Dados;

public class ByteDbContext : DbContext
{
    public ByteDbContext(DbContextOptions<ByteDbContext> options) : base(options) { }
    
    // public DbSet<Cargo> Cargos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}