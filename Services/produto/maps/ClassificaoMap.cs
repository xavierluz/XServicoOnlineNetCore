using Microsoft.EntityFrameworkCore;
using Services.modelo.produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.maps
{
    internal class ClassificaoMap
    {
        private ClassificaoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classificacao>().ToTable("Classificacao", "dbo");
            modelBuilder.Entity<Classificacao>().HasKey(x => x.Id);
            modelBuilder.Entity<Classificacao>().Property(x => x.Id).UseNpgsqlSerialColumn<int>();
            modelBuilder.Entity<Classificacao>().Property(x => x.Nome).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Classificacao>().Property(x => x.Descricao).HasMaxLength(500);
            modelBuilder.Entity<Classificacao>().Property(x => x.Ativo).IsRequired();
            modelBuilder.Entity<Classificacao>().HasIndex(x => x.Nome).HasName("INDX_CLASSIFICAO_NOME");
        }

        internal static ClassificaoMap Create(ModelBuilder modelBuilder)
        {
            return new ClassificaoMap(modelBuilder);
        }
    }
}
