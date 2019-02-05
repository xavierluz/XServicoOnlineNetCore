using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.movimento
{
    public class TipoMovimento : ITipoMovimento
    {
        protected TipoMovimento() {
            this.IMovimentos = new List<IMovimento>();
            this.Movimentos = new List<Movimento>();
        }
        internal static TipoMovimento GetInstance()
        {
            return new TipoMovimento();
        }
        public short Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
        #region "Não mapeadas"
        [NotMapped]
        public ICollection<IMovimento> IMovimentos { get; set; }
        #endregion

        #region "Métodos publicos"
        public static TipoMovimento GetTipoMovimento(ITipoMovimento tipoMovimento)
        {
            return new TipoMovimento
            {
                Ativo = tipoMovimento.Ativo,
                Descricao = tipoMovimento.Descricao,
                Id = tipoMovimento.Id,
                Tipo = tipoMovimento.Tipo 
            };
        }
        #endregion
    }
}
