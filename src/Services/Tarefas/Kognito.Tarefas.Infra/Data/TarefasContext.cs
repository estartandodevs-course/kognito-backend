using EstartandoDevsCore.Data;
using EstartandoDevsCore.Ultilities;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.Mediator;
using EstartandoDevsCore.Messages;
using Kognito.Tarefas.Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Kognito.Tarefas.Infra.Data;

public class TarefasContext : DbContext, IUnitOfWorks
{
    private readonly IMediatorHandler _mediatorHandler;

    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Entrega> Entregas { get; set; }
    public DbSet<Nota> Notas { get; set; }

    public TarefasContext(DbContextOptions<TarefasContext> options, IMediatorHandler mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
            .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TarefasContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        var cetZone = ZonaDeTempo.ObterZonaDeTempo();

        foreach (var entry in ChangeTracker.Entries()
            .Where(entry => entry.Entity.GetType().GetProperty("CriadoEm") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CriadoEm").CurrentValue = 
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
            }
        }

        var sucesso = await SaveChangesAsync() > 0;

        if (sucesso) await PublicarEventos();

        return sucesso;
    }

    private async Task PublicarEventos()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => await _mediatorHandler.PublicarEvento(domainEvent));

        await Task.WhenAll(tasks);
    }
}