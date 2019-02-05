using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.modelo.movimento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.movimento.contexto
{
    public class MovimentoContexto: DbContext
    {
        public DbSet<Movimento> Movimentos { get; set; }
        private MovimentoContexto(DbContextOptions options) : base(options)
        {
            this.DesabiitarLazyLoading();
            this.DesabilitarAutoDetectChangesEnabled();
        }
        internal static MovimentoContexto GetInstance(DbContextOptions options)
        {
            return new MovimentoContexto(options);
        }
        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
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
