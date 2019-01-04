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
            modelBuilder.Entity<Material>(ma =>
            {
                ma.ToTable("Material", "dbo");
                ma.HasKey(x => x.Id);
                ma.Property(x => x.Id).UseNpgsqlSerialColumn<int>();
                ma.Property(x => x.Nome).IsRequired().HasMaxLength(50);
                ma.Property(x => x.Descricao).HasMaxLength(500);
                ma.Property(x => x.Ativo).IsRequired();
                ma.HasIndex(x => x.Nome).HasName("INDX_MATERIAL_NOME");
            });
        }

        internal static MaterialMap Create(ModelBuilder modelBuilder)
        {
            return new MaterialMap(modelBuilder);
        }
    }
}
