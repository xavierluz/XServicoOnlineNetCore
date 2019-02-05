using Microsoft.EntityFrameworkCore;
using Services.modelo.movimento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.movimento.maps
{
    internal class MovimentoMap
    {
        private MovimentoMap(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Movimento>(b =>
            {
                b.ToTable("Movimento");
                // Primary key
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).ValueGeneratedOnAdd();
                b.Property(u => u.AlmoxarifadoId).IsRequired();
                b.HasIndex(u => u.AlmoxarifadoId).HasName("IDX_MOVIMENTO_ALMOXARIFADO");
                b.Property(u => u.TipoMovimentoId).IsRequired();
                b.HasIndex(u => u.TipoMovimentoId).HasName("IDX_MOVIMENTO_TIPOMOVIMENTO");
                b.Property(u => u.UsuarioId).HasMaxLength(100).IsRequired();
                b.HasIndex(u => u.UsuarioId).HasName("IDX_MOVIMENTO_USUARIO");
                b.Property(u => u.NumeroDocumento).IsRequired();
                b.Property(u => u.DataMovimento).IsRequired().ValueGeneratedOnAdd();
                b.Property(u => u.DataDocumento).IsRequired();
                b.Property(u => u.DataEstornoDoMovimento);
                b.Property(u => u.Observacao).HasMaxLength(500);
                b.Property(u => u.Ativo).IsRequired();
                b.HasIndex(u => new { u.AlmoxarifadoId, u.TipoMovimentoId, u.UsuarioId, u.DataMovimento }).HasName("IDX_MOVIMENTO_PRINCIPAL");

                //FK - MovimentoItem( 1 para N)
                b.HasMany(e => e.MovimentoItens)
                    .WithOne(e => e.Movimento)
                    .HasForeignKey(uc => uc.MovimentoId)
                    .IsRequired();
                //FK - TipoMovimento( 1 para 1)
                b.HasOne(e => e.TipoMovimento)
                    .WithMany(e => e.Movimentos)
                    .HasForeignKey(f => f.TipoMovimentoId);
                //FK - Almoxarifado( 1 para 1)
                b.HasOne(e => e.Almoxarifado)
                    .WithMany(e => e.Movimentos)
                    .HasForeignKey(f => f.AlmoxarifadoId);
                //FK - Usuario( 1 para 1)
                b.HasOne(e => e.Usuario)
                    .WithMany(e => e.Movimentos)
                    .HasForeignKey(f => f.UsuarioId);

            });
        }

        internal static MovimentoMap Create(ModelBuilder modelBuilder)
        {
            return new MovimentoMap(modelBuilder);
        }
    }
}
