using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.cadastro
{
    public interface IAlmoxarifado
    {
        Int32 Id { get; set; }
        Guid EmpresaId { get; set; }
        IEmpresa IEmpresa { get; set; }
        String Descricao { get; set; }
        String Indentificacao { get; set; }
        ICollection<IMovimento> IMovimentos { get; set; }
    }
}
