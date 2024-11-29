using Kognito.Tarefas.Domain;
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

        builder.HasMany(c => c.Entregas)
            .WithOne(e => e.Tarefa)
            .HasForeignKey(e => e.TarefaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}