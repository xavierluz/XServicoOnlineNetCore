using Microsoft.EntityFrameworkCore;
using Services.modelo.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.cadastro.maps
{
    public class EmpresaMap 
    {

        private EmpresaMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(b => {
                b.ToTable("Empresa", "dbo");
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.RazaoSocial).IsRequired().HasMaxLength(200);
                b.Property(x => x.NomeFantasia).HasMaxLength(200);
                b.Property(x => x.Email).HasMaxLength(100).IsRequired();
                b.Property(x => x.Chave).IsRequired().HasMaxLength(1000);
                b.Property(x => x.VetorInicializacao).IsRequired().HasMaxLength(1000);
                b.Property(x => x.CnpjCpf).IsRequired().HasMaxLength(15);
                b.Property(x => x.Cep).IsRequired().HasMaxLength(10);
                b.Property(x => x.Logradouro).IsRequired().HasMaxLength(100);
                b.Property(x => x.Bairro).IsRequired().HasMaxLength(50);
                b.Property(x => x.Cidade).IsRequired().HasMaxLength(50);
                b.Property(x => x.Site).HasMaxLength(100);
                b.Property(x => x.Telefone).HasMaxLength(12);
                b.Property(x => x.WhatsApp).HasMaxLength(12);
                b.Property(x => x.Ativo).IsRequired().HasColumnType("boolean");

                b.HasIndex(x => x.CnpjCpf).HasName("INDX_EMPRESA_CNPJ").IsUnique();
                b.HasIndex(x => x.Email).HasName("INDX_EMPRESA_EMAIL").IsUnique();
                b.HasIndex(x => x.RazaoSocial).HasName("INDX_EMPRESA_RAZAOSOCIAL");
                b.HasIndex(x => x.Logradouro).HasName("INDX_EMPRESA_LOGRADOURO");

                //FK - Almoxarifado
                b.HasMany(e => e.Almoxarifados)
                    .WithOne(e => e.Empresa)
                    .HasForeignKey(uc => uc.EmpresaId)
                    .IsRequired();
            });
        }

        internal static EmpresaMap Create(ModelBuilder modelBuilder)
        {
            return new EmpresaMap(modelBuilder);
        }
    }
}
