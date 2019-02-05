using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.movimento.contexto;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Services.movimento.repositorio
{
    internal class MovimentoUnitOfWork
    {
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        internal MovimentoContexto movimentoContexto = null;
        private MovimentoRepositorio movimentoRepositorio = null;

        private MovimentoUnitOfWork(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.movimentoContexto = new MovimentoContextoFactory().CreateDbContext(null);
        }

        internal static MovimentoUnitOfWork GetInstance(IsolationLevel isolationLevel)
        {
            return new MovimentoUnitOfWork(isolationLevel);
        }

        internal MovimentoRepositorio GetMovimentoRepositorio()
        {
            if (this.movimentoRepositorio == null)
                this.movimentoRepositorio = MovimentoRepositorio.GetInstance(this.movimentoContexto, this.isolationLevel);

            return this.movimentoRepositorio;
        }
        
        internal async Task<int> SalvarAsync()
        {
            return await this.movimentoContexto.SaveChangesAsync();
        }

        internal async Task CreateTransacao()
        {
            await this.movimentoContexto.Database.BeginTransactionAsync(this.isolationLevel);
        }

        internal void Commit()
        {
            if (this.movimentoContexto.Database.CurrentTransaction != null)
                this.movimentoContexto.Database.CommitTransaction();
        }
        internal void Rollback()
        {
            if (this.movimentoContexto.Database != null && this.movimentoContexto.Database.CurrentTransaction != null)
                this.movimentoContexto.Database.RollbackTransaction();
        }

        internal IDbContextTransaction GetTransacao()
        {
            return this.movimentoContexto.Database.CurrentTransaction;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    movimentoContexto.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
