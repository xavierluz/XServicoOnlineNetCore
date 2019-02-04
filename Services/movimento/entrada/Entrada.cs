using ServicesInterfaces.cadastro;
using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.movimento.entrada
{
    internal class Entrada : IMovimento
    {
        protected Entrada()
        {
            this.IMovimentoItens = new List<IMovimentoItem>();
        }
        public Guid Id { get; set; }
        public int AlmoxarifadoId { get; set; }
        public short TipoMovimentoId { get; set; }
        public string UsuarioId { get; set; }
        public long NumeroDocumento { get; set; }
        public DateTime DataDocumento { get; set; }
        public DateTime DataMovimento { get; set; }
        public DateTime DataEstornoDoMovimento { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public IAlmoxarifado IAlmoxarifado { get; set; }
        public ITipoMovimento ITipoMovimento { get; set; }
        public IUsuario IUsuario { get; set; }
        public ICollection<IMovimentoItem> IMovimentoItens { get; set; }
    }
}
