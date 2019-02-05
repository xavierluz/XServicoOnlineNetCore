using Microsoft.EntityFrameworkCore;
using Services.modelo.movimento;
using Services.movimento.contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.movimento.repositorio
{
    internal class MovimentoRepositorio
    {
        private IQueryable<Movimento> query;
        internal MovimentoContexto movimentoContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private MovimentoRepositorio(MovimentoContexto materialContexto, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.movimentoContexto = materialContexto;

        }
        internal static MovimentoRepositorio GetInstance(MovimentoContexto materialContexto, IsolationLevel isolationLevel)
        {
            return new MovimentoRepositorio(materialContexto, isolationLevel);
        }
        internal async Task AdicionarAsync(Movimento entidade)
        {
            await this.movimentoContexto.Set<Movimento>().AddAsync(entidade);
        }

        internal async Task AdicionarAsync(IList<Movimento> entidades)
        {
            await this.movimentoContexto.Set<Movimento>().AddRangeAsync(entidades);
        }

        internal async Task AtualizarAsync(Movimento entidade)
        {
            await Task.Run(() => this.movimentoContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal async Task ExcluirAsync(Func<Movimento, bool> predicate)
        {
            var Entidades = await this.movimentoContexto.Set<Movimento>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.movimentoContexto.Set<Movimento>().Remove(d));
        }

        internal async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.movimentoContexto.Database.SetCommandTimeout(0);
            this.movimentoContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.movimentoContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal async Task<Movimento> FindAsync(params object[] key)
        {
            return await this.movimentoContexto.Set<Movimento>().FindAsync(key);
        }
        internal List<Expression<Func<Movimento, object>>> Includes { get; } = new List<Expression<Func<Movimento, object>>>();

        internal List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Movimento, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal async Task SetQueryAsync(IQueryable<Movimento> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal async Task<Movimento> GetAsync(IQueryable<Movimento> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal async Task<List<Movimento>> GetsAsync(IQueryable<Movimento> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal async Task<int> GetCountAsync(IQueryable<Movimento> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal async Task<Movimento> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
