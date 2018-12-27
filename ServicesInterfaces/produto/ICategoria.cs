using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.produto
{
    public interface ICategoria
    {
        int Id { get; set; }
        String Nome { get; set; }
        String Descricao { get; set; }
        bool Ativo { get; set; }
        ICollection<IMaterial> IMateriais { get; set; }
    }
}
