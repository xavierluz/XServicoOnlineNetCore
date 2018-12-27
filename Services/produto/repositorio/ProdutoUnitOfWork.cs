using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.modelo.produto;
using Services.produto.contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.repositorio
{
    internal class ProdutoUnitOfWork : IDisposable
    {
        private ProdutoContexto produtoContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private BaseProdutoRepositorio<Categoria> categoriaRepositorio = null;
        private BaseProdutoRepositorio<Classificacao> classificaoRepositorio = null;
        private BaseProdutoRepositorio<Material> materialRepositorio = null;

        private ProdutoUnitOfWork(DbContextOptions<ProdutoContexto> options, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.produtoContexto = ProdutoContexto.GetInstance(options);
        }

        internal static ProdutoUnitOfWork GetInstance(DbContextOptions<ProdutoContexto> options, IsolationLevel isolationLevel)
        {
            return new ProdutoUnitOfWork(options, isolationLevel);
        }

        internal BaseProdutoRepositorio<Categoria> CategoriaRepositorio {
            get
            {
                if(this.categoriaRepositorio == null)
                {
                    this.categoriaRepositorio = new CategoriaRepositorio(this.produtoContexto, this.isolationLevel);
                }
                return this.categoriaRepositorio;
            }
        }
        internal BaseProdutoRepositorio<Classificacao> ClassificaoRepositorio
        {
            get
            {
                if (this.classificaoRepositorio == null)
                {
                    this.classificaoRepositorio = new ClassificacaoRepositorio(this.produtoContexto, this.isolationLevel);
                }
                return this.classificaoRepositorio;
            }
        }
        internal BaseProdutoRepositorio<Material> MaterialRepositorio
        {
            get
            {
                if (this.materialRepositorio == null)
                {
                    this.materialRepositorio = new MaterialRepositorio(this.produtoContexto, this.isolationLevel);
                }
                return this.materialRepositorio;
            }
        }
        internal async Task<int> SalvarAsync()
        {
            return await this.produtoContexto.SaveChangesAsync();
        }

        internal async Task CreateTransacao()
        {
            await this.produtoContexto.Database.BeginTransactionAsync(this.isolationLevel);
        }

        internal void Commit()
        {
            if (this.produtoContexto.Database.CurrentTransaction != null)
                this.produtoContexto.Database.CommitTransaction();
        }
        internal void Rollback()
        {
            if (this.produtoContexto.Database.CurrentTransaction != null)
                this.produtoContexto.Database.RollbackTransaction();
        }

        internal IDbContextTransaction GetTransacao()
        {
            return this.produtoContexto.Database.CurrentTransaction;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    produtoContexto.Dispose();
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
