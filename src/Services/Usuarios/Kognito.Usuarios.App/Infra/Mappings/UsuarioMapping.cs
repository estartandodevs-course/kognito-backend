using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.Infra.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Nome)
            .HasColumnName("Nome")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.DataDeCadastro)
            .HasColumnName("DataDeCadastro")
            .IsRequired();

        builder.Property(c => c.DataDeAlteracao)
            .HasColumnName("DataDeAlteracao")
            .IsRequired();

        builder.Property(c => c.Neurodivergencia)
            .HasColumnName("Neurodivergencia")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.OwnsOne(c => c.Cpf, cpf =>
        {
            cpf.Property(c => c.Numero)
                .HasMaxLength(11)
                .HasColumnName("Cpf")
                .IsRequired();
        });

        builder.OwnsOne(c => c.Login, login =>
        {
            login.Property(c => c.Hash)
                .HasColumnName("LoginHash");

            login.OwnsOne(c => c.Email, email =>
            {
                email.Property(x => x.Endereco)
                    .HasMaxLength(256)
                    .HasColumnName("Email");
            });
        });
    }
}