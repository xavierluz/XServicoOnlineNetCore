using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.produto
{
    public interface IMaterial
    {
        int Id { get; set; }
        String Nome { get; set; }
        String Descricao { get; set; }
        ICategoria ICategoria { get; set; }
        IClassificacao IClassificacao { get; set; }

    }
}
