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
    internal class ClassificacaoRepositorio : BaseProdutoRepositorio<Classificacao>
    {
        internal ClassificacaoRepositorio(ProdutoContexto produtoContexto, IsolationLevel isolationLevel) : base(produtoContexto, isolationLevel)
        {
        }

        internal override async Task AdicionarAsync(Classificacao entidade)
        {
            await this.produtoContexto.Set<Classificacao>().AddAsync(entidade);
        }

        internal override async Task AdicionarAsync(IList<Classificacao> entidades)
        {
            await this.produtoContexto.Set<Classificacao>().AddRangeAsync(entidades);
        }

        internal override async Task AtualizarAsync(Classificacao entidade)
        {
            await Task.Run(() => this.produtoContexto.Entry(entidade).State = EntityState.Modified);
        }

        internal override async Task ExcluirAsync(Func<Classificacao, bool> predicate)
        {
            var Entidades = await this.produtoContexto.Set<Classificacao>().Where(predicate).AsQueryable().ToListAsync();
            Entidades.ForEach(d => this.produtoContexto.Set<Classificacao>().Remove(d));
        }

        internal override async Task<int> ExecuteNoQueryAsync(string query)
        {
            this.produtoContexto.Database.SetCommandTimeout(0);
            this.produtoContexto.ChangeTracker.AutoDetectChangesEnabled = false;
            return await this.produtoContexto.Database.ExecuteSqlCommandAsync(query);
        }

        internal override async Task<Classificacao> FindAsync(params object[] key)
        {
            return await this.produtoContexto.Set<Classificacao>().FindAsync(key);
        }

        internal override List<Expression<Func<Classificacao, object>>> Includes { get; } = new List<Expression<Func<Classificacao, object>>>();
        internal override List<string> IncludeStrings { get; } = new List<string>();
        protected virtual async Task AddInclude(Expression<Func<Classificacao, object>> includeExpression)
        {
            await Task.Run(() => Includes.Add(includeExpression));
        }
        protected virtual async Task AddInclude(string includeString)
        {
            await Task.Run(() => IncludeStrings.Add(includeString));
        }

        internal override Task SetQueryAsync(IQueryable<Classificacao> query)
        {
            throw new NotImplementedException();
        }

        internal override Task<Classificacao> GetAsync(IQueryable<Classificacao> query)
        {
            throw new NotImplementedException();
        }

        internal override Task<List<Classificacao>> GetsAsync(IQueryable<Classificacao> query)
        {
            throw new NotImplementedException();
        }

        internal override Task<int> GetCountAsync(IQueryable<Classificacao> query)
        {
            throw new NotImplementedException();
        }

        internal override Task<Classificacao> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
