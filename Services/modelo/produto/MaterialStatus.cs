using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.modelo.produto
{
    internal class MaterialStatus : IMaterialStatus
    {
        private MaterialStatus() { }
        internal static MaterialStatus GetInstance()
        {
            return new MaterialStatus();
        }
        public int QuantidadeDeCategoria { get ; set ; }
        public int QuantidadeDeClassificacao { get ; set ; }
        public int QuantidadeDeMaterial { get ; set ; }
    }
}
