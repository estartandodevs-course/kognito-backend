using System;
using EstartandoDevsCore.Data;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.Messages;
using EstartandoDevsCore.Ultilities;
using EstartandoDevsCore.Mediator;
using Kognito.Usuarios.App.Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Kognito.Usuarios.App.Infra.Data;

public class UsuarioContext : DbContext, IUnitOfWorks
{
    private readonly IMediatorHandler _mediatorHandler;

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Emblemas> Emblemas { get; set; }
    public DbSet<Metas> Metas { get; set; }

    public UsuarioContext(DbContextOptions<UsuarioContext> options, IMediatorHandler mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            
            e.HasKey(x => x.Id);

            e.Property(c => c.Nome)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200)
                .HasColumnName("Nome")
                .IsRequired();
            
            e.Property(u => u.Ofensiva)
                .IsRequired()
                .HasDefaultValue(0);

            e.Property(c => c.Neurodivergencia)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasColumnName("Neurodivergencia")
                .IsRequired(false);

            e.Property(c => c.DataDeCadastro)
                .HasColumnType("datetime2")
                .HasColumnName("DataDeCadastro");

            e.Property(c => c.DataDeAlteracao)
                .HasColumnType("datetime2");

            e.HasMany(c => c.Emblemas)
                .WithOne()
                .HasForeignKey("UsuarioId");

            e.HasMany(c => c.Metas)
                .WithOne()
                .HasForeignKey("UsuarioId");

            e.OwnsOne(c => c.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                    .HasColumnType("nvarchar(11)")
                    .HasMaxLength(11)
                    .HasColumnName("Cpf")
                    .IsRequired(false);
            });

            e.OwnsOne(c => c.Login, login =>
            {
                login.Property(c => c.Hash)
                    .HasColumnType("uniqueidentifier")
                    .HasColumnName("LoginHash");

                login.OwnsOne(c => c.Email, email =>
                {
                    email.Property(x => x.Endereco)
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256)
                        .HasColumnName("Email");
                });

                login.OwnsOne(c => c.Senha, senha =>
                {
                    senha.Property(x => x.Valor)
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000)
                        .HasColumnName("Senha");
                });
            });
        });
    }

    public async Task<bool> Commit()
    {
        var cetZone = ZonaDeTempo.ObterZonaDeTempo();

        foreach (var entry in ChangeTracker.Entries()
            .Where(entry => entry.Entity.GetType().GetProperty("DataDeCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataDeCadastro").CurrentValue = 
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataDeCadastro").IsModified = false;
                entry.Property("DataDeAlteracao").CurrentValue = 
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
            }
        }

        var sucesso = await SaveChangesAsync() > 0;

        if (sucesso) await _mediatorHandler.PublicarEventos(this);

        return sucesso;
    }
}

public static class MediatorExtension
{
    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => { await mediator.PublicarEvento(domainEvent); });

        await Task.WhenAll(tasks);
    }
}