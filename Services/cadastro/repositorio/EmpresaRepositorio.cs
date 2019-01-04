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
    internal class EmpresaRepositorio
    {
        private IQueryable<Empresa> query;
        private CadastroContexto cadastroContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        internal EmpresaRepositorio(CadastroContexto cadastroContexto, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.cadastroContexto = cadastroContexto;

        }


        internal  async Task AdicionarAsync(Empresa entidade)
        {
            await this.cadastroContexto.Set<Empresa>().AddAsync(entidade);
        }

        internal  async Task AdicionarAsync(IList<Empresa> entidades)
        {
            await this.cadastroContexto.Set<Empresa>().AddRangeAsync(entidades);
        }

        internal  async Task AtualizarAsync(Empresa entidade)
        {
            await Task.Run(() => this.cadastroContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal  async Task ExcluirAsync(Func<Empresa, bool> predicate)
        {
            var Entidades = await this.cadastroContexto.Set<Empresa>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.cadastroContexto.Set<Empresa>().Remove(d));
        }

        internal  async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.cadastroContexto.Database.SetCommandTimeout(0);
            this.cadastroContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.cadastroContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal  async Task<Empresa> FindAsync(params object[] key)
        {
            return await this.cadastroContexto.Set<Empresa>().FindAsync(key);
        }
        internal  List<Expression<Func<Empresa, object>>> Includes { get; } = new List<Expression<Func<Empresa, object>>>();

        internal  List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Empresa, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal  async Task SetQueryAsync(IQueryable<Empresa> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal  async Task<Empresa> GetAsync(IQueryable<Empresa> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal  async Task<List<Empresa>> GetsAsync(IQueryable<Empresa> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal  async Task<int> GetCountAsync(IQueryable<Empresa> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal  async Task<Empresa> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
