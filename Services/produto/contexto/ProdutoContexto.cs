using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.modelo.produto;
using Services.produto.maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.contexto
{
    internal class ProdutoContexto : DbContext
    {
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Classificacao> Classificacaos { get; set; }
        public virtual DbSet<Material> Materials { get; set; }

        private ProdutoContexto(DbContextOptions options) : base(options)
        {
            this.DesabiitarLazyLoading();
            this.DesabilitarAutoDetectChangesEnabled();
            
        }

        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        internal static ProdutoContexto GetInstance(DbContextOptions options)
        {
            return new ProdutoContexto(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CategoriaMap.Create(modelBuilder);
            ClassificaoMap.Create(modelBuilder);

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
