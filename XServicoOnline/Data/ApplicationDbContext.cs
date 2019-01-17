using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XServicoOnline.Models;

namespace XServicoOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Funcao, string,UsuarioReivindicacao,UsuarioFuncao,UsuarioLogin,FuncaoReivindicacao,UsuarioToken>                                                
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            //sequence
            
            builder.HasSequence("SEQ_GENERATED_USUARIO_REIVINDICAO_ID", schema: "dbo")
                                    .StartsAt(100)
                                    .IncrementsBy(1);

            builder.HasSequence("SEQ_GENERATED_FUNCAO_REIVINDICAO_ID", schema: "dbo")
                                  .StartsAt(100)
                                  .IncrementsBy(1);

            builder.Entity<Usuario>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);
                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("IDX_USUARIO_NOME_NORMALIZADO").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("IDX_USUARIO_EMAIL");
                b.HasIndex(u => u.Nome).HasName("IDX_USUARIO_NOME");
                b.HasIndex(u => u.EmpresaId).HasName("IDX_USUARIO_EMPRESA");
                // Maps to the AspNetUsers table
                b.ToTable("Usuario");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken().HasColumnName("CodigoConcorrencia");

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Id).HasMaxLength(100).ValueGeneratedOnAdd();
                b.Property(u => u.EmpresaId).HasMaxLength(100);
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
                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany(e => e.UsuarioReivindicacao)
                        .WithOne(e => e.Usuario)
                        .HasForeignKey(uc => uc.UserId)
                        .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UsuarioLogin)
                        .WithOne(e => e.Usuario)
                        .HasForeignKey(ul => ul.UserId)
                        .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UsuarioToken)
                        .WithOne(e => e.Usuario)
                        .HasForeignKey(ut => ut.UserId)
                        .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UsuarioFuncao)
                        .WithOne(e => e.Usuario)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<UsuarioReivindicacao>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);
                b.Property(uc => uc.Id).UseNpgsqlSerialColumn<int>();
                b.Property(uc => uc.ClaimType).HasColumnName("TipoReivindicacao").HasMaxLength(100);
                b.Property(uc => uc.ClaimValue).HasColumnName("ValorReivindicacao").HasMaxLength(200);
                b.Property(uc => uc.UserId).HasColumnName("UsuarioId");
                // Maps to the AspNetUserClaims table
                b.ToTable("UsuarioReivindicacao");
            });

            builder.Entity<UsuarioLogin>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasColumnName("ProvedorLogin").HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasColumnName("ChaveProvedor").HasMaxLength(128);
                b.Property(l => l.ProviderDisplayName).HasColumnName("NomeProvedor").HasMaxLength(100);
                b.Property(l => l.UserId).HasColumnName("UsuarioId").HasMaxLength(100);
                // Maps to the AspNetUserLogins table
                b.ToTable("UsuarioLogin");
            });

            builder.Entity<UsuarioToken>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider).HasColumnName("ProvedorLogin").HasMaxLength(200);
                b.Property(t => t.Name).HasColumnName("Nome").HasMaxLength(100);
                b.Property(t => t.UserId).HasColumnName("UsuarioId").HasMaxLength(100);
                b.Property(t => t.Value).HasColumnName("TokenValor").HasMaxLength(200);
                // Maps to the AspNetUserTokens table
                b.ToTable("UsuarioToken");
            });

            builder.Entity<Funcao>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("Funcao");
                b.Property(r => r.Id).HasMaxLength(100).ValueGeneratedOnAdd();
                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).HasColumnName("CodigoConcorrencia").IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasColumnName("Nome").HasMaxLength(300);
                b.Property(u => u.NormalizedName).HasColumnName("NomeNormalizado").HasMaxLength(300);
                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UsuarioFuncao)
                        .WithOne(e => e.Funcao)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.FuncaoReivindicacao)
                        .WithOne(e => e.Funcao)
                        .HasForeignKey(rc => rc.RoleId)
                        .IsRequired();
            });

            builder.Entity<FuncaoReivindicacao>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("FuncaoReivindicacao");
                b.Property(rc => rc.Id).UseNpgsqlSerialColumn<int>();
                b.Property(rc => rc.ClaimType).HasColumnName("TipoReivindicacao").HasMaxLength(100);
                b.Property(rc => rc.ClaimValue).HasColumnName("ValorReivindicacao").HasMaxLength(200);
                b.Property(rc => rc.RoleId).HasColumnName("FuncaoId").HasMaxLength(100);
                
            });

            builder.Entity<UsuarioFuncao>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("UsuarioFuncao");
                b.Property(r=> r.RoleId).HasColumnName("FuncaoId").HasMaxLength(100);
                b.Property(r => r.UserId).HasColumnName("UsuarioId").HasMaxLength(100);
            });
        }
    }
}

