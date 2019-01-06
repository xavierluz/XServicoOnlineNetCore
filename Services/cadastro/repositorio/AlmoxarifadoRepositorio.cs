using Microsoft.EntityFrameworkCore;
using Services.cadastro.contexto;
using Services.modelo.cadastro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.cadastro.repositorio
{
    internal class AlmoxarifadoRepositorio
    {
        private IQueryable<Almoxarifado> query;
        private CadastroContexto cadastroContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private AlmoxarifadoRepositorio(CadastroContexto cadastroContexto, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.cadastroContexto = cadastroContexto;

        }
        internal static AlmoxarifadoRepositorio GetInstance (CadastroContexto cadastroContexto, IsolationLevel isolationLevel)
        {
            return new AlmoxarifadoRepositorio(cadastroContexto, isolationLevel);
        }
        internal async Task AdicionarAsync(Almoxarifado entidade)
        {
            await this.cadastroContexto.Set<Almoxarifado>().AddAsync(entidade);
        }

        internal async Task AdicionarAsync(IList<Almoxarifado> entidades)
        {
            await this.cadastroContexto.Set<Almoxarifado>().AddRangeAsync(entidades);
        }

        internal async Task AtualizarAsync(Almoxarifado entidade)
        {
            await Task.Run(() => this.cadastroContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal async Task ExcluirAsync(Func<Almoxarifado, bool> predicate)
        {
            var Entidades = await this.cadastroContexto.Set<Almoxarifado>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.cadastroContexto.Set<Almoxarifado>().Remove(d));
        }

        internal async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.cadastroContexto.Database.SetCommandTimeout(0);
            this.cadastroContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.cadastroContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal async Task<Almoxarifado> FindAsync(params object[] key)
        {
            return await this.cadastroContexto.Set<Almoxarifado>().FindAsync(key);
        }
        internal List<Expression<Func<Almoxarifado, object>>> Includes { get; } = new List<Expression<Func<Almoxarifado, object>>>();

        internal List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Almoxarifado, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal async Task SetQueryAsync(IQueryable<Almoxarifado> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal async Task<Almoxarifado> GetAsync(IQueryable<Almoxarifado> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal async Task<List<Almoxarifado>> GetsAsync(IQueryable<Almoxarifado> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal async Task<int> GetCountAsync(IQueryable<Almoxarifado> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal async Task<Almoxarifado> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
