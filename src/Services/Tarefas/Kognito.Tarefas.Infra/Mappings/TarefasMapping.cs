using Kognito.Tarefas.Domain;
using Kognito.Usuarios.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kognito.Tarefas.Infra.Mappings;

public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("Tarefas");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(c => c.Conteudo)
            .IsRequired();

        builder.Property(c => c.DataFinalEntrega)
            .IsRequired();

        builder.Property(c => c.CriadoEm)
            .IsRequired();

        builder.Property(c => c.TurmaId)
            .IsRequired();
        
        builder.Property<List<Neurodivergencia>>("NeurodivergenciasAlvo")
            .HasConversion(
                v => string.Join(',', v.Select(e => (int)e)),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => (Neurodivergencia)int.Parse(e))
                    .ToList()
            )
            .HasColumnType("varchar(200)");
        
        builder.HasMany(c => c.Entregas)
            .WithOne(e => e.Tarefa)
            .HasForeignKey(e => e.TarefaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}