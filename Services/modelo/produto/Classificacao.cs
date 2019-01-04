using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.produto
{
    public class Classificacao:IClassificacao
    {
        protected Classificacao() {
            this.IMateriais = new List<IMaterial>();
            this.Materiais = new List<Material>();
        }

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
        internal Classificacao GetClassificacao(IClassificacao classificacao)
        {
            Classificacao _classificacao = new Classificacao();
            _classificacao.Ativo = classificacao.Ativo;
            _classificacao.Descricao = classificacao.Descricao;
            _classificacao.Id = classificacao.Id;
            _classificacao.Nome = classificacao.Nome;
            return _classificacao;
        }
    }
}
