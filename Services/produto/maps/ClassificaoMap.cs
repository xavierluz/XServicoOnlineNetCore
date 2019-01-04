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
            modelBuilder.Entity<Classificacao>(cl =>
            {
                cl.ToTable("Classificacao", "dbo");
                cl.HasKey(x => x.Id);
                cl.Property(x => x.Id).UseNpgsqlSerialColumn<int>();
                cl.Property(x => x.Nome).IsRequired().HasMaxLength(50);
                cl.Property(x => x.Descricao).HasMaxLength(500);
                cl.Property(x => x.Ativo).IsRequired();
                cl.HasIndex(x => x.Nome).HasName("INDX_CLASSIFICAO_NOME");
                //FK - Materiais
                cl.HasMany(e => e.Materiais)
                    .WithOne(e => e.Classificacao)
                    .HasForeignKey(uc => uc.classificacaoId)
                    .IsRequired();
        });
        }

        internal static ClassificaoMap Create(ModelBuilder modelBuilder)
        {
            return new ClassificaoMap(modelBuilder);
        }
    }
}
