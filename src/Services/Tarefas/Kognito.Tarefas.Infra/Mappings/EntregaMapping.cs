using Kognito.Tarefas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kognito.Tarefas.Infra.Mappings;

public class EntregaMapping : IEntityTypeConfiguration<Entrega>
{
    public void Configure(EntityTypeBuilder<Entrega> builder)
    {
        builder.ToTable("Entregas");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Conteudo)
            .IsRequired();

        builder.Property(c => c.EntregueEm)
            .IsRequired();

        builder.Property(c => c.AlunoId)
            .IsRequired();

        builder.Property(c => c.TarefaId)
            .IsRequired();

        builder.Property(c => c.Atrasada)
            .IsRequired();

        builder.HasMany(c => c.Notas)
            .WithOne(n => n.Entrega)
            .HasForeignKey(n => n.EntregaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}