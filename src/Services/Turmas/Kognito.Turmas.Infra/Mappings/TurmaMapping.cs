using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.Infra.Data.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired(true);

            builder.Property(t => t.Materia)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Cor)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.Icones)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.HashAcesso)
            .IsRequired()
            .HasColumnType("varchar(8)");
        

            builder.Property(t => t.LinkAcesso)
                .IsRequired()
                .HasColumnType("varchar(200)");
             

            builder.OwnsOne(t => t.Professor, professor =>
            {
                professor.Property(p => p.Id)
                    .HasColumnName("ProfessorId")
                    .IsRequired();
                    
                professor.Property(p => p.Nome)
                    .HasColumnName("ProfessorNome")
                    .HasColumnType("varchar(100)")
                    .IsRequired();
                    
            });

            
            builder.OwnsMany(t => t.Enturmamentos, enturmamento =>
            {
                enturmamento.WithOwner(e => e.Turma);
                
                enturmamento.HasKey(e => e.Id);
                
                // Configuração do Aluno
                enturmamento.OwnsOne(e => e.Aluno, aluno =>
                {
                    aluno.Property(a => a.Id)
                        .HasColumnName("AlunoId")
                        .IsRequired();
                        
                    aluno.Property(a => a.Nome)
                        .HasColumnName("AlunoNome")
                        .HasColumnType("varchar(100)")
                        .IsRequired();
                });

                enturmamento.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("int"); 

                enturmamento.ToTable("Enturmamentos");
            });

            
            builder.HasMany<Conteudo>()
                .WithOne(c => c.Turma)
                .HasForeignKey(c => c.TurmaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Turmas");
        }
    }
}