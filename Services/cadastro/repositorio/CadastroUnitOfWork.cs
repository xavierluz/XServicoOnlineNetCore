﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.cadastro.contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Services.cadastro.repositorio
{
    internal class CadastroUnitOfWork : IDisposable
    {
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        internal CadastroContexto cadastroContexto = null;

        private CadastroUnitOfWork(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.cadastroContexto = new CadastroContextoFactory().CreateDbContext(null);
        }

        internal static CadastroUnitOfWork GetInstance(IsolationLevel isolationLevel)
        {
            return new CadastroUnitOfWork(isolationLevel);
        }
        internal async Task<int> SalvarAsync()
        {
            return await this.cadastroContexto.SaveChangesAsync();
        }

        internal async Task CreateTransacao()
        {
            await this.cadastroContexto.Database.BeginTransactionAsync(this.isolationLevel);
        }

        internal void Commit()
        {
            if (this.cadastroContexto.Database.CurrentTransaction != null)
                this.cadastroContexto.Database.CommitTransaction();
        }
        internal void Rollback()
        {
            if (this.cadastroContexto.Database.CurrentTransaction != null)
                this.cadastroContexto.Database.RollbackTransaction();
        }

        internal IDbContextTransaction GetTransacao()
        {
            return this.cadastroContexto.Database.CurrentTransaction;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    cadastroContexto.Dispose();
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