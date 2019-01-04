using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class MaterialStatusViewModel: IMaterialStatus
    {
        public Int32 QuantidadeDeCategoria { get; set; }
        public Int32 QuantidadeDeClassificacao { get; set; }
        public Int32 QuantidadeDeMaterial { get; set; }

        internal MaterialStatusViewModel GetMaterialStatus(IMaterialStatus materialStatus)
        {
            MaterialStatusViewModel _materialStatus = new MaterialStatusViewModel()
            {
                QuantidadeDeCategoria = materialStatus.QuantidadeDeCategoria,
                QuantidadeDeClassificacao = materialStatus.QuantidadeDeClassificacao,
                QuantidadeDeMaterial = materialStatus.QuantidadeDeMaterial
            };
            return _materialStatus;
        }
    }
}
