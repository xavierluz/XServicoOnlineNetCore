using Microsoft.EntityFrameworkCore;
using Services.modelo.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.cadastro.maps
{
    internal class AlmoxarifadoMap
    {
        private AlmoxarifadoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Almoxarifado>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Indentificacao).HasName("IDX_ALMOXARIFADO_INDENTIFICACAO").IsUnique();
                b.ToTable("Almoxarifado");
                b.Property(u => u.Id).UseNpgsqlSerialColumn<int>();
                b.Property(u => u.Indentificacao).HasMaxLength(200).IsRequired();
                b.Property(u => u.Descricao).HasMaxLength(500).IsRequired();
                b.Property(u => u.EmpresaId).HasMaxLength(100).IsRequired();
            });
        }

        internal static AlmoxarifadoMap Create(ModelBuilder modelBuilder)
        {
            return new AlmoxarifadoMap(modelBuilder);
        }
    }
}
