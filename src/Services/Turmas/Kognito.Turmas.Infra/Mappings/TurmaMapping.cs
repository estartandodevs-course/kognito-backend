using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.Infra.Data.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            ConfigurarPropriedadesBasicas(builder);
            ConfigurarProfessor(builder);
            ConfigurarEnturmamentos(builder);
            ConfigurarLinksDeAcesso(builder);
            ConfigurarConteudos(builder);

            builder.ToTable("Turmas");
        }

        private void ConfigurarPropriedadesBasicas(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired(false);

            builder.Property(t => t.Materia)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Cor)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.Icones)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.DataDeCadastro)
                .IsRequired();

            builder.Property(t => t.DataDeAlteracao)
                .IsRequired(false);
        }

        private void ConfigurarProfessor(EntityTypeBuilder<Turma> builder)
        {
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
        }

        private void ConfigurarEnturmamentos(EntityTypeBuilder<Turma> builder)
        {
            builder.OwnsMany(t => t.Enturmamentos, enturmamento =>
            {
                enturmamento.WithOwner();
                enturmamento.HasKey(e => e.Id);

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

                enturmamento.Property(e => e.DataDeCadastro)
                    .IsRequired();

                enturmamento.Property(e => e.DataDeAlteracao)
                    .IsRequired(false);

                enturmamento.ToTable("Enturmamentos");
            });
        }

        private void ConfigurarLinksDeAcesso(EntityTypeBuilder<Turma> builder)
        {
            builder.OwnsMany(t => t.LinksDeAcesso, link =>
            {
                link.ToTable("LinksDeAcesso");
                link.WithOwner().HasForeignKey("TurmaId");
                link.HasKey(l => l.Codigo);

                link.Property(l => l.Codigo)
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                link.Property(l => l.DataCriacao)
                    .IsRequired();

                link.Property(l => l.DataExpiracao)
                    .IsRequired();

                link.Property(l => l.Ativo)
                    .IsRequired();

                link.Property(l => l.LimiteUsos)
                    .IsRequired();

                link.Property(l => l.QuantidadeUsos)
                    .IsRequired()
                    .HasDefaultValue(0);
            });
        }

        private void ConfigurarConteudos(EntityTypeBuilder<Turma> builder)
        {
            builder.HasMany<Conteudo>()
                .WithOne(c => c.Turma)
                .HasForeignKey(c => c.TurmaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}