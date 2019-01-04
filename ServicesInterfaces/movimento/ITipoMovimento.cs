using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.movimento
{
    public interface ITipoMovimento
    {
        Int16 Id { get; set; }
        String Tipo { get; set; }
        String Descricao { get; set; }
        bool Ativo { get; set; }
        ICollection<IMovimento> IMovimentos { get; set; }  
    }
}
