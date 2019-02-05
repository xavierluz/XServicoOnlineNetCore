using Services.modelo.cadastro;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public DateTime? DataEstornoDoMovimento { get; set; }
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
        #region "Métodos publicos"
        public static Movimento GetMovimento(IMovimento movimento)
        {
            return new Movimento
            {
                Id = movimento.Id,
                Almoxarifado = Almoxarifado.GetAlmoxarifado(movimento.IAlmoxarifado),
                AlmoxarifadoId = movimento.AlmoxarifadoId,
                Ativo = movimento.Ativo,
                DataDocumento = movimento.DataDocumento,
                DataEstornoDoMovimento = movimento.DataEstornoDoMovimento,
                DataMovimento = movimento.DataMovimento,
                MovimentoItens =(movimento.IMovimentoItens !=null ? movimento.IMovimentoItens.ToList().ConvertAll(new Converter<IMovimentoItem, MovimentoItem>(MovimentoItem.GetMovimentoItem)): new List<MovimentoItem>()),
                NumeroDocumento = movimento.NumeroDocumento,
                Observacao = movimento.Observacao,
                TipoMovimento = TipoMovimento.GetTipoMovimento(movimento.ITipoMovimento),
                TipoMovimentoId = movimento.TipoMovimentoId,
                Usuario = Usuario.GetUsuario(movimento.IUsuario),
                UsuarioId = movimento.UsuarioId
            };
        }
        public IMovimento GetMovimento()
        {
            IMovimento movimento = new Movimento
            {
                Id = this.Id,
                IAlmoxarifado = this.Almoxarifado,
                AlmoxarifadoId = this.AlmoxarifadoId,
                Ativo = this.Ativo,
                DataDocumento = this.DataDocumento,
                DataEstornoDoMovimento = this.DataEstornoDoMovimento,
                DataMovimento = this.DataMovimento,
                IMovimentoItens = (ICollection<IMovimentoItem>)this.MovimentoItens,
                NumeroDocumento = this.NumeroDocumento,
                Observacao = this.Observacao,
                TipoMovimento = this.TipoMovimento,
                TipoMovimentoId = this.TipoMovimentoId,
                Usuario = this.Usuario,
                UsuarioId = this.UsuarioId
            };

            return movimento;
        }
        #endregion
    }
}
