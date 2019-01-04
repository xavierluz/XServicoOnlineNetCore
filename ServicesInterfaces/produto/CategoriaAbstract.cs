using ServicesInterfaces.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces.produto
{
    public abstract class CategoriaAbstract
    {
        protected IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        protected DbConnection dbConnection = null;

        protected CategoriaAbstract(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
        }

        #region "Atributos publicos"
        public int totalRegistrosRetorno { get; protected set; }
        public int registroIndex { get; protected set; }
        public int totalRegistroPorPagina { get; protected set; }
        public string filtro { get; protected set; }
        public ICategoria categoria { get; protected set; }
        #endregion

        #region "Métodos abstratos"
        public abstract Task<ICategoria> Incluir(ICategoria categoria);
        public abstract Task<ICategoria> Atualizar(ICategoria categoria);
        public abstract Task<ICategoria> ConsultarPorId(int categoriaId);
        public abstract Task<IList<ICategoria>> GetCategorias();
        public abstract Task<IList<ICategoria>> getCategoriasParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina);
        public abstract Task<Int32> GetQuantidade();
        #endregion
    }
}
