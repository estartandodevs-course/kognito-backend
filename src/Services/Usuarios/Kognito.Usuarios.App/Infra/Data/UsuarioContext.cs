using Microsoft.EntityFrameworkCore;
using Kognito.Usuarios.App.Domain;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Infra.Data;

public class UsuarioContext : DbContext, IUnitOfWorks
{
    public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Metas> Metas { get; set; }
    public DbSet<Emblemas> Emblemas { get; set; }
    public DbSet<Entregas> Entregas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(p => p.Id);
            e.HasMany(u => u.Emblemas)
                .WithOne()
                .HasForeignKey(e => e.UsuarioId);
            e.HasMany(u => u.Metas)
                .WithOne()
                .HasForeignKey(m => m.UsuarioId);
        });

        modelBuilder.Entity<Emblemas>(e =>
        {
            e.ToTable("Emblemas");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");
            e.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");
            e.Property(p => p.OrdemDesbloqueio)
                .IsRequired();
            e.Property(p => p.Desbloqueado)
                .IsRequired()
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<Metas>(e =>
        {
            e.ToTable("Metas");
            e.HasKey(p => p.Id);
        });

        modelBuilder.Entity<Entregas>(e =>
        {
            e.ToTable("Entregas");
            e.HasKey(p => p.Id);
            e.Property(p => p.UsuarioId)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=KognitoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}

public class Entregas
{
    public object? Id { get; set; }
    public object UsuarioId { get; }
}