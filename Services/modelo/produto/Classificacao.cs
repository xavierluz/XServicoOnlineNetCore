using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.produto
{
    public class Classificacao:IClassificacao
    {
        protected Classificacao() { }

        public int Id { get ; set ; }
        public string Nome { get ; set ; }
        public string Descricao { get ; set ; }
        public bool Ativo { get ; set ; }
        public virtual ICollection<Material> Materiais { get; set; }

        [NotMapped]
        public ICollection<IMaterial> IMateriais { get; set; }
        internal static Classificacao GetInstance()
        {
            return new Classificacao();
        } 
        internal IClassificacao GetClassificacao()
        {
            IClassificacao classificacao = new Classificacao();
            classificacao.Ativo = this.Ativo;
            classificacao.Descricao = this.Descricao;
            classificacao.Id = this.Id;
            classificacao.Nome = this.Nome;
            return classificacao;
        } 
    }
}
