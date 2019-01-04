using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.cadastro.maps;
using Services.modelo.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.cadastro.contexto
{
    public class CadastroContexto : DbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Almoxarifado> Almoxarifados { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        private CadastroContexto(DbContextOptions options) : base(options)
        {
            this.DesabiitarLazyLoading();
            this.DesabilitarAutoDetectChangesEnabled();
        }
        internal static CadastroContexto GetInstance(DbContextOptions options)
        {
            return new CadastroContexto(options);
        }
        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EmpresaMap.Create(modelBuilder);
            UsuarioMap.Create(modelBuilder);
            AlmoxarifadoMap.Create(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
        }

        internal void DesabiitarLazyLoading()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        internal void HabilitarazyLoading()
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }
        internal void HabilitarAutoDetectChangesEnabled()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        internal void DesabilitarAutoDetectChangesEnabled()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}
