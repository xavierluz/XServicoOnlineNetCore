using Microsoft.EntityFrameworkCore;
using Services.modelo.produto;
using Services.produto.contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.repositorio
{
    internal class CategoriaRepositorio : BaseProdutoRepositorio<Categoria>
    {
        private IQueryable<Categoria> query;
        internal CategoriaRepositorio(ProdutoContexto produtoContexto, IsolationLevel isolationLevel) : base(produtoContexto, isolationLevel)
        {

        }


        internal override async Task AdicionarAsync(Categoria entidade)
        {
            await this.produtoContexto.Set<Categoria>().AddAsync(entidade);
        }

        internal override async Task AdicionarAsync(IList<Categoria> entidades)
        {
            await this.produtoContexto.Set<Categoria>().AddRangeAsync(entidades);
        }

        internal override async Task AtualizarAsync(Categoria entidade)
        {
          await Task.Run(() => this.produtoContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal override async Task ExcluirAsync(Func<Categoria, bool> predicate)
        {
            var Entidades = await this.produtoContexto.Set<Categoria>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.produtoContexto.Set<Categoria>().Remove(d));
        }

        internal override async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.produtoContexto.Database.SetCommandTimeout(0);
            this.produtoContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.produtoContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal override async Task<Categoria> FindAsync(params object[] key)
        {
            return await this.produtoContexto.Set<Categoria>().FindAsync(key);
        }


        internal override List<Expression<Func<Categoria, object>>> Includes { get; } = new List<Expression<Func<Categoria, object>>>();

        internal override List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Categoria, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal override async Task SetQueryAsync(IQueryable<Categoria> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal override async Task<Categoria> GetAsync(IQueryable<Categoria> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal override async Task<List<Categoria>> GetsAsync(IQueryable<Categoria> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal override async Task<int> GetCountAsync(IQueryable<Categoria> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal override async Task<Categoria> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
