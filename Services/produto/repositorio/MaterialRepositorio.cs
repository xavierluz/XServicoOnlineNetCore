using Microsoft.EntityFrameworkCore;
using Services.modelo.produto;
using Services.produto.contexto;
using ServicesInterfaces.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.repositorio
{
    internal class MaterialRepositorio : BaseProdutoRepositorio<Material>
    {
        private IQueryable<Material> query;
        internal MaterialRepositorio(ProdutoContexto produtoContexto, IsolationLevel isolationLevel) : base(produtoContexto, isolationLevel)
        {

        }

        internal override async Task AdicionarAsync(Material entidade)
        {
            await this.produtoContexto.Set<Material>().AddAsync(entidade);
        }

        internal override async Task AdicionarAsync(IList<Material> entidades)
        {
            await this.produtoContexto.Set<Material>().AddRangeAsync(entidades);
        }

        internal override async Task AtualizarAsync(Material entidade)
        {
            await Task.Run(() => this.produtoContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal override async Task ExcluirAsync(Func<Material, bool> predicate)
        {
            var Entidades = await this.produtoContexto.Set<Material>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.produtoContexto.Set<Material>().Remove(d));
        }

        internal override async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.produtoContexto.Database.SetCommandTimeout(0);
            this.produtoContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.produtoContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal override async Task<Material> FindAsync(params object[] key)
        {
            return await this.produtoContexto.Set<Material>().FindAsync(key);
        }


        internal override List<Expression<Func<Material, object>>> Includes { get; } = new List<Expression<Func<Material, object>>>();

        internal override List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Material, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal override async Task SetQueryAsync(IQueryable<Material> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal override async Task<Material> GetAsync(IQueryable<Material> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal override async Task<List<Material>> GetsAsync(IQueryable<Material> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal override async Task<int> GetCountAsync(IQueryable<Material> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal override async Task<Material> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
