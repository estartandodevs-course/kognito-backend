using Kognito.Tarefas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kognito.Tarefas.Infra.Mappings;

public class NotaMapping : IEntityTypeConfiguration<Nota>
{
    public void Configure(EntityTypeBuilder<Nota> builder)
    {
        builder.ToTable("Notas");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.TituloTarefa)
            .IsRequired();

        builder.Property(c => c.ValorNota)
            .IsRequired();

        builder.Property(c => c.AlunoId)
            .IsRequired();

        builder.Property(c => c.TurmaId)
            .IsRequired();

        builder.Property(c => c.EntregaId)
            .IsRequired();

        builder.Property(c => c.AtribuidoEm)
            .IsRequired();
    }
}