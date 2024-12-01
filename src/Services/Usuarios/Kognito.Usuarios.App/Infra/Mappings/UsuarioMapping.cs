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
            .HasColumnType("nvarchar(200)")
            .HasMaxLength(200)
            .HasColumnName("Nome")
            .IsRequired();
            
        builder.Property(u => u.Ofensiva)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(c => c.Neurodivergencia)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .HasColumnName("Neurodivergencia")
            .IsRequired(false);

        builder.Property(c => c.DataDeCadastro)
            .HasColumnType("datetime2")
            .HasColumnName("DataDeCadastro");

        builder.Property(c => c.DataDeAlteracao)
            .HasColumnType("datetime2");

        builder.HasMany(c => c.Emblemas)
            .WithOne()
            .HasForeignKey("UsuarioId");

        builder.HasMany(c => c.Metas)
            .WithOne()
            .HasForeignKey("UsuarioId");

        builder.OwnsOne(c => c.Cpf, cpf =>
        {
            cpf.Property(c => c.Numero)
                .HasMaxLength(11)
                .HasColumnName("Cpf")
                .IsRequired();

            cpf.HasIndex(c => c.Numero)
                .IsUnique();
        });

        builder.OwnsOne(c => c.Login, login =>
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
    }
}