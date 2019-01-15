using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces.cadastro
{
    public abstract class EmpresaAbstract
    {
        protected IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        public EmpresaAbstract(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
        }
        #region "Atributos publicos"
        public int totalRegistrosRetorno { get; protected set; }
        public int registroIndex { get; protected set; }
        public int totalRegistroPorPagina { get; protected set; }
        public string filtro { get; protected set; }
        public IEmpresa IEmpresa { get; protected set; }
        #endregion

        #region "Métodos abstratos"
        public abstract Task<IEmpresa> Incluir(IEmpresa empresa);
        public abstract Task<IEmpresa> Atualizar(IEmpresa empresa);
        public abstract Task<IEmpresa> ConsultarPorId(Guid empresaId);
        public abstract Task<IList<IEmpresa>> GetEmpresas();
        public abstract Task<IList<IEmpresa>> getEmpresasParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina);
        public abstract Task<Int32> GetQuantidade();
        public abstract Task<string> GetSenhaPadraoDoUsuarioEmpresa();
        public abstract Task Commit();
        public abstract Task Rollback();
        #endregion
    }
}
