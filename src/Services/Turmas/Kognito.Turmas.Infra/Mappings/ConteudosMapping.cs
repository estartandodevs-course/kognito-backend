using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.Infra.Data.Mappings
{
    public class ConteudoMapping : IEntityTypeConfiguration<Conteudo>
    {
        public void Configure(EntityTypeBuilder<Conteudo> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.ConteudoDidatico)
                .IsRequired()  
                .HasColumnType("text");
            
            builder.Property(c => c.TurmaId)  
                .IsRequired();

            builder.HasOne(c => c.Turma)
                .WithMany()
                .HasForeignKey(c => c.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Conteudos");
        }
    }
}