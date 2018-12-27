﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Services.produto.contexto;

namespace Services.Migrations
{
    [DbContext(typeof(ProdutoContexto))]
    [Migration("20181221174006_InitialService")]
    partial class InitialService
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Services.modelo.produto.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .HasName("INDX_CATEGORIA_NOME");

                    b.ToTable("Categoria","dbo");
                });

            modelBuilder.Entity("Services.modelo.produto.Classificacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .HasName("INDX_CLASSIFICAO_NOME");

                    b.ToTable("Classificacao","dbo");
                });

            modelBuilder.Entity("Services.modelo.produto.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.Property<int>("categoriaId");

                    b.Property<int>("classificacaoId");

                    b.HasKey("Id");

                    b.HasIndex("categoriaId");

                    b.HasIndex("classificacaoId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Services.modelo.produto.Material", b =>
                {
                    b.HasOne("Services.modelo.produto.Categoria", "Categoria")
                        .WithMany("Materiais")
                        .HasForeignKey("categoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Services.modelo.produto.Classificacao", "Classificacao")
                        .WithMany("Materiais")
                        .HasForeignKey("classificacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
