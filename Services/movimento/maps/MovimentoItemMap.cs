using Microsoft.EntityFrameworkCore;
using Services.modelo.movimento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.movimento.maps
{
    internal class MovimentoItemMap
    {
        private MovimentoItemMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovimentoItem>(b =>
                {
                    b.ToTable("MovimentoItem");
                // Primary key
                b.HasKey(u => u.Id);
                    b.Property(u => u.Id).UseNpgsqlSerialColumn<long>();
                    b.Property(u => u.MovimentoId).IsRequired();
                    b.HasIndex(u => u.MovimentoId).HasName("IDX_MOVIMENTOITEM_MOVIMENTO");
                    b.Property(u => u.MaterialId).IsRequired();
                    b.HasIndex(u => u.MaterialId).HasName("IDX_MOVIMENTOITEM_MATERIALITEM");
                    b.Property(u => u.Lote).HasMaxLength(50);
                    b.Property(u => u.DataFabricacaoDoLote).IsRequired();
                    b.Property(u => u.DataVencimentoDoLote).IsRequired();
                    b.Property(u => u.QuantidadeMovimentadaDoMaterial).IsRequired();
                    b.Property(u => u.PrecoUnitarioDoMaterial).IsRequired();
                    b.Property(u => u.ValorMovimentadoDoMaterial).IsRequired();
                    b.Property(u => u.QuantidadeSaldoDoMaterial).IsRequired();
                    b.Property(u => u.ValorSaldoDoMaterial).IsRequired();
                    b.Property(u => u.ValorDesdobroDoMaterial).IsRequired();
                    b.HasIndex(u => new { u.Movimento, u.MaterialId, u.QuantidadeMovimentadaDoMaterial }).HasName("IDX_MOVIMENTOITEM_CHAVE1").IsUnique();
                    b.HasIndex(u => new
                    {
                        u.QuantidadeMovimentadaDoMaterial,
                        u.PrecoUnitarioDoMaterial,
                        u.ValorMovimentadoDoMaterial,
                        u.QuantidadeSaldoDoMaterial,
                        u.ValorSaldoDoMaterial,
                        u.ValorDesdobroDoMaterial
                    }).HasName("IDX_MOVIMENTOITEM_VALORES1");

                //FK - Material( 1 para 1)
                b.HasOne(e => e.Material)
                        .WithMany(e => e.MovimentoItens)
                        .HasForeignKey(f => f.MaterialId);
                });
        }
        internal static MovimentoItemMap Create(ModelBuilder modelBuilder)
        {
            return new MovimentoItemMap(modelBuilder);
        }
    }
}
