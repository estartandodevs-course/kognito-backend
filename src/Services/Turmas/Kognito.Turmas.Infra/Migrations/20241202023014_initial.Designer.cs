﻿// <auto-generated />
using System;
using Kognito.Turmas.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kognito.Turmas.Infra.Migrations
{
    [DbContext(typeof(TurmaContext))]
    [Migration("20241202023014_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kognito.Turmas.Domain.Conteudo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConteudoDidatico")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataDeAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("TurmaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Conteudos", (string)null);
                });

            modelBuilder.Entity("Turma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cor")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDeAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("HashAcesso")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<int>("Icones")
                        .HasColumnType("int");

                    b.Property<string>("LinkAcesso")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Materia")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Turmas", (string)null);
                });

            modelBuilder.Entity("Kognito.Turmas.Domain.Conteudo", b =>
                {
                    b.HasOne("Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Turma", b =>
                {
                    b.OwnsMany("Enturmamento", "Enturmamentos", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("DataDeAlteracao")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("DataDeCadastro")
                                .HasColumnType("datetime2");

                            b1.Property<int>("Status")
                                .HasColumnType("int");

                            b1.Property<Guid>("TurmaId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("TurmaId");

                            b1.ToTable("Enturmamentos", (string)null);

                            b1.WithOwner("Turma")
                                .HasForeignKey("TurmaId");

                            b1.OwnsOne("Kognito.Turmas.Domain.Usuario", "Aluno", b2 =>
                                {
                                    b2.Property<Guid>("EnturmamentoId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("AlunoId");

                                    b2.Property<string>("Nome")
                                        .IsRequired()
                                        .HasColumnType("varchar(100)")
                                        .HasColumnName("AlunoNome");

                                    b2.HasKey("EnturmamentoId");

                                    b2.ToTable("Enturmamentos");

                                    b2.WithOwner()
                                        .HasForeignKey("EnturmamentoId");
                                });

                            b1.Navigation("Aluno")
                                .IsRequired();

                            b1.Navigation("Turma");
                        });

                    b.OwnsOne("Kognito.Turmas.Domain.Usuario", "Professor", b1 =>
                        {
                            b1.Property<Guid>("TurmaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ProfessorId");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("ProfessorNome");

                            b1.HasKey("TurmaId");

                            b1.ToTable("Turmas");

                            b1.WithOwner()
                                .HasForeignKey("TurmaId");
                        });

                    b.Navigation("Enturmamentos");

                    b.Navigation("Professor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}