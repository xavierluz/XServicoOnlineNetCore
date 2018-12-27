using Microsoft.EntityFrameworkCore;
using Services.modelo.produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.maps
{
    internal class MaterialMap
    {
        private MaterialMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().ToTable("Material", "dbo");
            modelBuilder.Entity<Material>().HasKey(x => x.Id);
            modelBuilder.Entity<Material>().Property(x => x.Id).UseNpgsqlSerialColumn<int>();
            modelBuilder.Entity<Material>().Property(x => x.Nome).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Material>().Property(x => x.Descricao).HasMaxLength(500);
            modelBuilder.Entity<Material>().Property(x => x.Ativo).IsRequired();
            modelBuilder.Entity<Material>().HasIndex(x => x.Nome).HasName("INDX_MATERIAL_NOME");
        }

        internal static MaterialMap Create(ModelBuilder modelBuilder)
        {
            return new MaterialMap(modelBuilder);
        }
    }
}
