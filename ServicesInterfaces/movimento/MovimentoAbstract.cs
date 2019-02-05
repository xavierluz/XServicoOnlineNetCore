using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces.movimento
{
    public abstract class MovimentoAbstract
    {
        protected IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        public MovimentoAbstract(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
        }
        #region "Atributos publicos"
        public int totalRegistrosRetorno { get; protected set; }
        public int registroIndex { get; protected set; }
        public int totalRegistroPorPagina { get; protected set; }
        public string filtro { get; protected set; }
        public IMovimento IMovimento { get; protected set; }
        #endregion
        #region "Métodos abstratos"
        public abstract Task<IMovimento> Incluir(IMovimento movimento);
        public abstract Task<IMovimento> Atualizar(IMovimento movimento);
        public abstract Task<IMovimento> ConsultarPorId(Guid movimentoId);
        public abstract Task<IList<IMovimento>> GetMovimentos();
        public abstract Task<IList<IMovimento>> getMovimentosParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina);
        public abstract Task<Int32> GetQuantidade();
        public abstract Task<IMovimento> GetMovimento(string userName);
        public abstract Task Commit();
        public abstract Task Rollback();
        #endregion
    }
}
