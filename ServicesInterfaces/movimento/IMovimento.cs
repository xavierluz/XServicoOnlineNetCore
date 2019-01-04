using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.movimento
{
    public interface IMovimento
    {
        Guid Id { get; set; }
        Int32 AlmoxarifadoId { get; set; }
        Int16 TipoMovimentoId { get; set; }
        String UsuarioId { get; set; }
        Int64 NumeroDocumento { get; set; }
        DateTime DataDocumento { get; set; }
        DateTime DataMovimento { get; set; }
        DateTime DataEstornoDoMovimento { get; set; }
        String Observacao { get; set; }
        bool Ativo { get; set; }
        IAlmoxarifado IAlmoxarifado { get; set; }
        ITipoMovimento ITipoMovimento { get; set; }
        IUsuario IUsuario { get; set; }
        ICollection<IMovimentoItem> IMovimentoItens { get; set; }
    }
}
