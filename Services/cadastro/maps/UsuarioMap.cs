using Microsoft.EntityFrameworkCore;
using Services.modelo.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.cadastro.maps
{
    internal class UsuarioMap
    {
        private UsuarioMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.NormalizedUserName).HasName("IDX_USUARIO_NOME_NORMALIZADO").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("IDX_USUARIO_EMAIL");
                b.HasIndex(u => u.Nome).HasName("IDX_USUARIO_NOME");
                b.ToTable("Usuario");
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken().HasColumnName("CodigoConcorrencia");
                b.Property(u => u.EmpresaId);
                b.Property(u => u.Id).HasMaxLength(100).ValueGeneratedOnAdd();
                b.Property(u => u.Nome).HasMaxLength(50).HasColumnName("Nome").IsRequired();
                b.Property(u => u.UserName).HasMaxLength(50).HasColumnName("Usuario").IsRequired();
                b.Property(u => u.NormalizedUserName).HasMaxLength(256).HasColumnName("NomeNormalizado"); ;
                b.Property(u => u.Email).HasMaxLength(100).HasColumnName("Email").IsRequired();
                b.Property(u => u.NormalizedEmail).HasMaxLength(256).HasColumnName("EmailNormalizado"); ;
                b.Property(u => u.AccessFailedCount).HasColumnName("QuantidadeAcessoFalho");
                b.Property(u => u.LockoutEnabled).HasColumnName("Bloqueado");
                b.Property(u => u.LockoutEnd).HasColumnName("DataDesbloqueio");
                b.Property(u => u.PasswordHash).HasColumnName("HashSenha").IsRequired();
                b.Property(u => u.PhoneNumber).HasColumnName("Telefone");
                b.Property(u => u.PhoneNumberConfirmed).HasColumnName("TelefoneConfirmado");
                b.Property(u => u.SecurityStamp).HasColumnName("CodigoSeguranca").IsRequired();
                b.Property(u => u.TwoFactorEnabled).HasColumnName("DoisTipoAcesso");
                b.Property(u => u.RegisterDate).HasColumnName("DataCadastro").IsRequired();

            });
        }

        internal static UsuarioMap Create(ModelBuilder modelBuilder)
        {
            return new UsuarioMap(modelBuilder);
        }
    }
}
