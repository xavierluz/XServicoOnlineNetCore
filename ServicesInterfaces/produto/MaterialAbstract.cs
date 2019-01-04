using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces.produto
{
    public abstract class MaterialAbstract
    {
        protected IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        protected DbConnection dbConnection = null;

        protected MaterialAbstract(IsolationLevel isolationLevel)
        {
            this.isolationLevel = isolationLevel;
        }
        #region "Atributos publicos"
        public int totalRegistrosRetorno { get; protected set; }
        public int registroIndex { get; protected set; }
        public int totalRegistroPorPagina { get; protected set; }
        public string filtro { get; protected set; }
        public IMaterial material { get; protected set; }
        #endregion

        #region "Métodos abstratos"
        public abstract Task<IMaterial> Incluir(IMaterial  material);
        public abstract Task<IMaterial> Atualizar(IMaterial material);
        public abstract Task<IMaterial> ConsultarPorId(int materialId);
        public abstract Task<IList<IMaterial>> GetMateriais();
        public abstract Task<Int32> GetQuantidade();
        public abstract Task<IMaterialStatus> GetMaterialStatus();
        public abstract Task<IList<IMaterial>> GetMateriaisParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina);
        #endregion
    }
}
