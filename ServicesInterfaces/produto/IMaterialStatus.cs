using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.produto
{
    public interface IMaterialStatus
    {
        int QuantidadeDeCategoria { get; set; }
        int QuantidadeDeClassificacao { get; set; }
        int QuantidadeDeMaterial { get; set; }
    }
}
