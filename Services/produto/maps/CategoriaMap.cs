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
            modelBuilder.Entity<Categoria>().ToTable("Categoria", "dbo");
            modelBuilder.Entity<Categoria>().HasKey(x => x.Id);
            modelBuilder.Entity<Categoria>().Property(x => x.Id).UseNpgsqlSerialColumn<int>();
            modelBuilder.Entity<Categoria>().Property(x => x.Nome).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Categoria>().Property(x => x.Descricao).HasMaxLength(500);
            modelBuilder.Entity<Categoria>().Property(x => x.Ativo).IsRequired();
            modelBuilder.Entity<Categoria>().HasIndex(x => x.Nome).HasName("INDX_CATEGORIA_NOME");
        }

        internal static CategoriaMap Create(ModelBuilder modelBuilder)
        {
            return new CategoriaMap(modelBuilder);
        }
    }
}
