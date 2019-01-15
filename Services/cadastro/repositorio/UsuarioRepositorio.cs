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

    internal class UsuarioRepositorio
    {
        private IQueryable<Usuario> query;
        private CadastroContexto cadastroContexto = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private UsuarioRepositorio(CadastroContexto cadastroContexto, IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
            this.cadastroContexto = cadastroContexto;

        }
        internal static UsuarioRepositorio GetInstance(CadastroContexto cadastroContexto, IsolationLevel isolationLevel)
        {
            return new UsuarioRepositorio(cadastroContexto, isolationLevel);
        }
        internal async Task AdicionarAsync(Usuario entidade)
        {
            await this.cadastroContexto.Set<Usuario>().AddAsync(entidade);
        }

        internal async Task AdicionarAsync(IList<Usuario> entidades)
        {
            await this.cadastroContexto.Set<Usuario>().AddRangeAsync(entidades);
        }

        internal async Task AtualizarAsync(Usuario entidade)
        {
            await Task.Run(() => this.cadastroContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal async Task ExcluirAsync(Func<Usuario, bool> predicate)
        {
            var Entidades = await this.cadastroContexto.Set<Usuario>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.cadastroContexto.Set<Usuario>().Remove(d));
        }

        internal async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.cadastroContexto.Database.SetCommandTimeout(0);
            this.cadastroContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.cadastroContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal async Task<Usuario> FindAsync(params object[] key)
        {
            return await this.cadastroContexto.Set<Usuario>().FindAsync(key);
        }
        internal List<Expression<Func<Usuario, object>>> Includes { get; } = new List<Expression<Func<Usuario, object>>>();

        internal List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Usuario, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal async Task SetQueryAsync(IQueryable<Usuario> query)
        {
            await Task.Run(() => this.query = query);
        }

        internal async Task<Usuario> GetAsync(IQueryable<Usuario> query)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        internal async Task<List<Usuario>> GetsAsync(IQueryable<Usuario> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }

        internal async Task<int> GetCountAsync(IQueryable<Usuario> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        internal async Task<Usuario> GetAsync()
        {
            return await this.query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
