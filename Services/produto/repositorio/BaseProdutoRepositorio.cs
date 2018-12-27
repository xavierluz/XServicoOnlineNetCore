using Microsoft.EntityFrameworkCore;
using Services.produto.contexto;
using ServicesInterfaces.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.repositorio
{
    internal abstract class BaseProdutoRepositorio<T> where T : class
    {
        internal ProdutoContexto produtoContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        protected BaseProdutoRepositorio(ProdutoContexto produtoContexto, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.produtoContexto = produtoContexto;
        }

        internal abstract Task AdicionarAsync(T entidade);
        internal abstract Task AdicionarAsync(IList<T> entidades);
        internal abstract Task AtualizarAsync(T entidade);
        internal abstract Task ExcluirAsync(Func<T, bool> predicate);
        internal abstract Task<T> FindAsync(params object[] key);
        internal abstract Task<int> ExecuteNoQueryAsync(string query);
        internal abstract List<Expression<Func<T, object>>> Includes { get; }
        internal abstract Task SetQueryAsync(IQueryable<T> query);
        internal abstract Task<T> GetAsync(IQueryable<T> query);
        internal abstract Task<T> GetAsync();
        internal abstract Task<List<T>> GetsAsync(IQueryable<T> query);
        internal abstract Task<Int32> GetCountAsync(IQueryable<T> query);
        internal abstract List<string> IncludeStrings { get; }

    }
}
