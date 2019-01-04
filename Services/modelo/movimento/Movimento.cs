using Services.modelo.cadastro;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.movimento
{
    public class Movimento : IMovimento
    {
        protected Movimento() {
            this.IMovimentoItens = new List<IMovimentoItem>();
            this.MovimentoItens = new List<MovimentoItem>();
        }
        internal static Movimento GetInstance()
        {
            return new Movimento();
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
        #region "Navegação(Relacionamento)"
        public virtual Almoxarifado Almoxarifado { get; set; }
        public virtual TipoMovimento TipoMovimento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<MovimentoItem> MovimentoItens { get; set; }
        #endregion
        #region "Não mapeadas"
        [NotMapped]
        public IAlmoxarifado IAlmoxarifado { get; set; }
        [NotMapped]
        public ITipoMovimento ITipoMovimento { get; set; }
        [NotMapped]
        public IUsuario IUsuario { get; set; }
        [NotMapped]
        public ICollection<IMovimentoItem> IMovimentoItens { get; set ; }
        #endregion
    }
}
