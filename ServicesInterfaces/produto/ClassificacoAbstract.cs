using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces.produto
{
    public abstract class ClassificacoAbstract
    {
        protected IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        protected DbConnection dbConnection = null;

        protected ClassificacoAbstract(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
        }

        #region "Atributos publicos"
        public int totalRegistrosRetorno { get; protected set; }
        public int registroIndex { get; protected set; }
        public int totalRegistroPorPagina { get; protected set; }
        public string filtro { get; protected set; }
        public IClassificacao classificacao { get; protected set; }
        #endregion

        #region "Métodos abstratos"
        public abstract Task<IClassificacao> Incluir(IClassificacao classificacao);
        public abstract Task<IClassificacao> Atualizar(IClassificacao classificacao);
        public abstract Task<IClassificacao> ConsultarPorId(int classificacaoId);
        public abstract Task<IList<IClassificacao>> GetClassificacoes();
        public abstract Task<IList<IClassificacao>> GetClassificacaoesParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina);
        public abstract Task<Int32> GetQuantidade();
        #endregion
    }
}
