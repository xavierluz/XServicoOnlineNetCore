using Microsoft.EntityFrameworkCore;
using Services.modelo.produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.maps
{
    internal class CategoriaMap
    {
        private CategoriaMap(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Categoria>(ca =>
            {
                ca.ToTable("Categoria", "dbo");
                ca.HasKey(x => x.Id);
                ca.Property(x => x.Id).UseNpgsqlSerialColumn<int>();
                ca.Property(x => x.Nome).IsRequired().HasMaxLength(50);
                ca.Property(x => x.Descricao).HasMaxLength(500);
                ca.Property(x => x.Ativo).IsRequired();
                ca.HasIndex(x => x.Nome).HasName("INDX_CATEGORIA_NOME");
                //FK - Materiais
                ca.HasMany(e => e.Materiais)
                    .WithOne(e => e.Categoria)
                    .HasForeignKey(uc => uc.categoriaId)
                    .IsRequired();
            });
        }

        internal static CategoriaMap Create(ModelBuilder modelBuilder)
        {
            return new CategoriaMap(modelBuilder);
        }
    }
}
